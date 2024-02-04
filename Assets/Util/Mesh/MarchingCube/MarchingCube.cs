using System;
using Unity.Collections;
using Unity.Mathematics;
using static Unity.Mathematics.math;
using static NUtil.NMesh.MarchingCubeConstantBuffer;

namespace NUtil.NMesh
{
    [Serializable]
    public struct MarchingCube
    {
        /// <summary>
        /// 计算区块边缘额外密度值范围设置为2，，用于计算 edge + 1 个顶点 以及顶点处的法向量
        /// </summary>
        public const int Margin = 2;

        public int densityEdgeSize;
        public int densityEdgeSizeSQ;
        public int densityLength;

        public int vertexEdgeSize;
        public int vertexEdgeSizeSQ;
        public int vertexLength;

        public int cubeEdgeSize;
        public int cubeEdgeSizeSQ;
        public int cubeLength;

        [ReadOnly]
        public DimensionConfig config;
        [ReadOnly]
        public FloorConfig floorConfig;
        [ReadOnly]
        public readonly NativeArray<int3> edge_start;
        [ReadOnly]
        public readonly NativeArray<int3> edge_dir;
        [ReadOnly]
        public readonly NativeArray<int3> edge_end;
        [ReadOnly]
        public readonly NativeArray<int> case_to_numpolys;
        [ReadOnly]
        public readonly NativeArray<int> edge_axis;
        [ReadOnly]
        public readonly NativeArray<int4> g_triTable;

        public MarchingCube(DimensionConfig config, FloorConfig floorConfig)
        {
            this.config = config;
            this.floorConfig = floorConfig;

            densityEdgeSize = config.subSize + Margin * 2;
            densityEdgeSizeSQ = densityEdgeSize * densityEdgeSize;
            densityLength = densityEdgeSize * densityEdgeSize * densityEdgeSize;

            vertexEdgeSize = config.subSize + 1;
            vertexEdgeSizeSQ = vertexEdgeSize * vertexEdgeSize;
            vertexLength = vertexEdgeSize * vertexEdgeSize * vertexEdgeSize;

            cubeEdgeSize = config.subSize;
            cubeEdgeSizeSQ = config.subSize * config.subSize;
            cubeLength = config.subSize * config.subSize * config.subSize;

            edge_start = MarchingCubeConstantBuffer.edge_start;
            edge_dir = MarchingCubeConstantBuffer.edge_dir;
            edge_end = MarchingCubeConstantBuffer.edge_end;
            case_to_numpolys = MarchingCubeConstantBuffer.case_to_numpolys;
            edge_axis = MarchingCubeConstantBuffer.edge_axis;
            g_triTable = MarchingCubeConstantBuffer.g_triTable;
        }

        public int GetDensity3DIndex(int3 index)
        {
            return index.x * densityEdgeSizeSQ + index.y * densityEdgeSize + index.z;
        }
        public int GetVertex3DIndex(int3 index)
        {
            return index.x * vertexEdgeSizeSQ + index.y * vertexEdgeSize + index.z;
        }
        public MeshDataBuffer Build(float3 worldPosition)
        {
            return Build(worldPosition, new MeshDataBuffer(Allocator.Temp));
        }
        public MeshDataBuffer Build(float3 worldPosition, MeshDataBuffer buffer)
        {
            /*空间被分为多个小正方块，最小块被称为体素，体素有四个顶点
            包含着体素的稍大块叫区块，每个区块包含等量的体素
            每个体素的XYZ空间长度都是dim.miniSpace，【取块左下角做为中心坐标】
            左下角的含义是，第一个体素左下空间坐标为(0,0,0)，并向各轴正方向延伸
            这样就可以根据任意世界坐标用floor函数获取所停留的体素的左下角坐标 */
            int3 wsChunkIndex = WorldToChunkIndex(worldPosition);
            return Build(wsChunkIndex, buffer);
        }
        public MeshDataBuffer Build(int3 wsChunkIndex)
        {
            return Build(wsChunkIndex, new MeshDataBuffer(Allocator.Temp));
        }
        public MeshDataBuffer Build(int3 wsChunkIndex, MeshDataBuffer buffer)
        {
            /*计算全部体素的左下角三条边的值，【其他边都是别的相邻体素的左下三边之一】
            左下三边的值从每个体素八个顶点的深度值计算得来
            而我们只需要每个体素计算一个深度值，【其他顶点都是其他相邻体素的深度值】
            深度值在[-1,1]范围内波动，当边上的两顶点中一个>0另一个<0时
            这条边存在于虚实交界处，其间会生成面
            而两顶点都>0或<0时则表示边存在于纯虚或实体内部中，不会生成面
            PS：深度值 == 密度值 （俩说法一意思）*/
            LocateConfig locate = new LocateConfig(config, wsChunkIndex, wsChunkIndex);
            // # Step 1: Generate Density Value
            // # Time: 0.16
            // # 计算所需的全部密度值
            // 为构建Mesh需处理 edge + 1 个顶点，需要计算 edge + 2 (margin) 个顶点的密度值
            NativeArray<float> densities = new NativeArray<float>(densityLength, Allocator.Temp);

            for (int xIndex = 0; xIndex < densityEdgeSize; xIndex++)
            {
                for (int yIndex = 0; yIndex < densityEdgeSize; yIndex++)
                {
                    for (int zIndex = 0; zIndex < densityEdgeSize; zIndex++)
                    {
                        // 边缘加了margin的2倍，为了在正负方向都扩张margin个点，所以回归正常世界坐标就是索引减去margin
                        float3 localCubeIndex = new float3(xIndex, yIndex, zIndex) - Margin;
                        float3 wsCubePosition = (localCubeIndex + locate.wsLLCubeIndex) * config.miniSpace;
                        densities[GetDensity3DIndex(int3(xIndex, yIndex, zIndex))] = BuildDensity(wsCubePosition);
                    }
                }
            }

            // # Step 2: Process Vertex Data
            // # Time: 0.03
            // # 处理 edge + 1 个顶点信息
            NativeList<float3> vertexPositionBuffer = buffer.position;
            NativeList<float3> vertexNormalBuffer = buffer.normal;
            NativeList<float3> uvw0Buffer = buffer.uvw0;
            NativeList<float> amboBuffer = buffer.ambo;
            NativeList<int> triangleBuffer = buffer.triangle;

            NativeArray<int4> e3_e0_e8_case_buffer = new NativeArray<int4>(vertexLength, Allocator.Temp);
            int vertexCount = 0;
            for (int i = 0; i < vertexLength; i++)
            {
                int3 xyz = int3(i / vertexEdgeSizeSQ, i % vertexEdgeSizeSQ / vertexEdgeSize, i % vertexEdgeSize);
                int xIndex = xyz.x;
                int yIndex = xyz.y;
                int zIndex = xyz.z;
                float3 localCubeIndex = new float3(xyz);
                float3 localCubePos = localCubeIndex * config.miniSpace;
                float3 localCubeUVW = float3(config.subSize) / localCubeIndex;

                float3 wsCubePos = locate.wsLLCubePosition + localCubePos;
                float3 gsCubePos = localCubePos;

                // 各顶点深度值
                float4 field0123;
                float4 field4567;

                field0123.x = densities[GetDensity3DIndex(int3(xIndex, yIndex, zIndex) + Margin)];
                field0123.y = densities[GetDensity3DIndex(int3(xIndex, yIndex + 1, zIndex) + Margin)];
                field0123.z = densities[GetDensity3DIndex(int3(xIndex + 1, yIndex + 1, zIndex) + Margin)];
                field0123.w = densities[GetDensity3DIndex(int3(xIndex + 1, yIndex, zIndex) + Margin)];
                field4567.x = densities[GetDensity3DIndex(int3(xIndex, yIndex, zIndex + 1) + Margin)];
                field4567.y = densities[GetDensity3DIndex(int3(xIndex, yIndex + 1, zIndex + 1) + Margin)];
                field4567.z = densities[GetDensity3DIndex(int3(xIndex + 1, yIndex + 1, zIndex + 1) + Margin)];
                field4567.w = densities[GetDensity3DIndex(int3(xIndex + 1, yIndex, zIndex + 1) + Margin)];

                uint4 i0123 = (uint4)saturate(field0123 * 99999);
                uint4 i4567 = (uint4)saturate(field4567 * 99999);

                // 按位或取得这个体素的案例值
                int cube_case = (int)(i0123.x | i0123.y << 1 | i0123.z << 2 | i0123.w << 3 |
                                i4567.x << 4 | i4567.y << 5 | i4567.z << 6 | i4567.w << 7);

                int bit0 = cube_case & 1;
                int bit3 = cube_case >> 3 & 1;
                int bit1 = cube_case >> 1 & 1;
                int bit4 = cube_case >> 4 & 1;

                // 是否在体素左下角三条边上 创建顶点
                int3 build_vert_on_edge = abs(int3(bit3, bit1, bit4) - int3(bit0));
                int4 vertexIndexAndCase = -1;

                Vertex output;
                if (build_vert_on_edge.x != 0)
                {
                    const int edgeNum = 3;
                    int vertexIndex = vertexCount++;
                    vertexIndexAndCase.x = vertexIndex;
                    output = BuildVertex(int3(localCubeIndex), wsCubePos, gsCubePos, localCubeUVW, edgeNum, ref densities);
                    vertexPositionBuffer.Add(output.position);
                    vertexNormalBuffer.Add(output.normal);
                    uvw0Buffer.Add(output.uvw0);
                    amboBuffer.Add(output.ambo);
                }

                if (build_vert_on_edge.y != 0)
                {
                    const int edgeNum = 0;
                    vertexIndexAndCase.y = vertexCount++;
                    output = BuildVertex(int3(localCubeIndex), wsCubePos, gsCubePos, localCubeUVW, edgeNum, ref densities);
                    vertexPositionBuffer.Add(output.position);
                    vertexNormalBuffer.Add(output.normal);
                    uvw0Buffer.Add(output.uvw0);
                    amboBuffer.Add(output.ambo);
                }

                if (build_vert_on_edge.z != 0)
                {
                    const int edgeNum = 8;
                    vertexIndexAndCase.z = vertexCount++;
                    output = BuildVertex(int3(localCubeIndex), wsCubePos, gsCubePos, localCubeUVW, edgeNum, ref densities);
                    vertexPositionBuffer.Add(output.position);
                    vertexNormalBuffer.Add(output.normal);
                    uvw0Buffer.Add(output.uvw0);
                    amboBuffer.Add(output.ambo);
                }

                vertexIndexAndCase.w = cube_case;

                e3_e0_e8_case_buffer[i] = vertexIndexAndCase;
            }

            // # Step 3: Process Triangles
            // # Time: 0.0003
            // # 构建三角面
            for (int i = 0; i < cubeLength; i++)
            {
                int3 xyz = int3(i / cubeEdgeSizeSQ, i % cubeEdgeSizeSQ / cubeEdgeSize, i % cubeEdgeSize);

                // TODO OK: 根据体素案例值生成三角并关联索引
                int cube_case = e3_e0_e8_case_buffer[GetVertex3DIndex(xyz)].w;
                int num_polys = case_to_numpolys[cube_case];
                if (max(max(xyz.x, xyz.y), xyz.z) >= config.subSize)
                    num_polys = 0;

                for (int n = 0; n < num_polys; n++)
                {
                    // range: 0-11
                    int3 edgeNums_for_triangle = g_triTable[cube_case * 5 + n].xyz;

                    // now sample the 3D VertexIDVol texture to get the vertex IDs
                    // for those vertices!

                    int3 xyz_edge;
                    int3 VertexID = -1;
                    // 值范围 0 1 2 对应 3 0 8 哪条边上的顶点
                    int index_of_e3_e0_e8;

                    xyz_edge = xyz + edge_start[edgeNums_for_triangle.x].xyz;
                    index_of_e3_e0_e8 = edge_axis[edgeNums_for_triangle.x];
                    VertexID.x = e3_e0_e8_case_buffer[GetVertex3DIndex(xyz_edge)][index_of_e3_e0_e8];

                    xyz_edge = xyz + edge_start[edgeNums_for_triangle.y].xyz;
                    index_of_e3_e0_e8 = edge_axis[edgeNums_for_triangle.y];
                    VertexID.y = e3_e0_e8_case_buffer[GetVertex3DIndex(xyz_edge)][index_of_e3_e0_e8];

                    xyz_edge = xyz + edge_start[edgeNums_for_triangle.z].xyz;
                    index_of_e3_e0_e8 = edge_axis[edgeNums_for_triangle.z];
                    VertexID.z = e3_e0_e8_case_buffer[GetVertex3DIndex(xyz_edge)][index_of_e3_e0_e8];

                    triangleBuffer.Add(VertexID.x);
                    triangleBuffer.Add(VertexID.y);
                    triangleBuffer.Add(VertexID.z);
                }
            }

            return buffer;
        }

        private float BuildDensity(float3 ws)
        {
            float height_floor_sub = floorConfig.GetFloorHeight(ws);
            if (floorConfig.floorType == EFloorFunction.DensityCenter)
            {
                ws %= 1f / floorConfig.floorCenterScale;
            }
            ws *= 0.08f;

            float uulf_scale = 0.014f * saturate(0.55f + 0.45f * abs(noise.snoise(ws * 0.1f + 111f)));
            float3 uulf_shape = float3(noise.snoise(ws * uulf_scale + 211.233f), noise.snoise(ws * uulf_scale - 361.841f), noise.snoise(ws * uulf_scale));

            float2 n = noise.cellular(ws + uulf_shape * 20f);
            float2 am_noise = saturate(n * -3f + 2.45f);
            float am_density = -am_noise.x - am_noise.y + 6;
            float2 sm_noise = saturate(n - 0.6f);
            float sm_density = csum(sm_noise);

            float shape_mix_scale = 5f;
            float shape_mix_speed = 0.0666f;

            float height_lerp = saturate((noise.snoise(ws * shape_mix_speed - 172.9137f) + 0.5f) * shape_mix_scale);
            float density = lerp(sm_density * 3f - 7.3f, am_density, height_lerp);
            //float density = am_density;

            density -= height_floor_sub;

            return max(-1f, min(1f, density));
        }
        private Vertex BuildVertex(int3 localCubeIndex, float3 wsCubePos, float3 gsCubePos, float3 localCubeUVW, int edgeNum, ref NativeArray<float> densities)
        {
            Vertex output = new Vertex();

            localCubeIndex += Margin;

            float str0 = densities[GetDensity3DIndex(localCubeIndex + edge_start[edgeNum])];
            float str1 = densities[GetDensity3DIndex(localCubeIndex + edge_end[edgeNum])];
            float t = saturate(str0 / (str0 - str1));  // 'saturate' keeps occasional crazy stray triangle from appearing @ edges

            // reconstruct the interpolated point & place a vertex there.
            // 顶点坐标在体素左下角坐标的偏移（局部坐标）
            float3 rawLocalVertexPosition = edge_start[edgeNum] + float3(t) * edge_dir[edgeNum];  //0..1
            float3 localVertexPosition = rawLocalVertexPosition * config.miniSpace;
            float3 wsVertexPos = wsCubePos + localVertexPosition;
            float3 gsVertexPos = gsCubePos + localVertexPosition;

            // 顶点的 UVW 为体素的UVW+顶点的UVW偏移
            float3 uvw = localCubeUVW + rawLocalVertexPosition;

            // generate ambient occlusion for this vertex
            float ambo = 0;
            // TODO: Ambo ...

            // figure out the normal vector for this vertex
            float3 grad;

            grad.x = densities[GetDensity3DIndex(localCubeIndex + int3(1, 0, 0))]
                     - densities[GetDensity3DIndex(localCubeIndex - int3(1, 0, 0))];

            grad.y = densities[GetDensity3DIndex(localCubeIndex + int3(0, 1, 0))]
                     - densities[GetDensity3DIndex(localCubeIndex - int3(0, 1, 0))];

            grad.z = densities[GetDensity3DIndex(localCubeIndex + int3(0, 0, 1))]
                     - densities[GetDensity3DIndex(localCubeIndex - int3(0, 0, 1))];

            float3 worldNormalMisc = -normalize(grad);

            output.position = gsVertexPos;
            output.normal = worldNormalMisc;
            output.uvw0 = uvw;
            output.ambo = ambo;
            return output;
        }

        /// <summary>
        /// 世界坐标转体素索引
        /// </summary>
        public int3 WorldToCubeIndex(float3 worldPosition)
        {
            return new int3(floor(worldPosition / config.miniSpace));
        }
        /// <summary>
        /// 世界坐标转区块索引
        /// </summary>
        public int3 WorldToChunkIndex(float3 worldPosition)
        {
            return new int3(floor(float3(WorldToCubeIndex(worldPosition)) / config.subSize));
        }
        /// <summary>
        /// 体素索引转世界坐标（正方体中最接近坐标轴负方向的一点）
        /// </summary>
        public float3 CubeIndexToWorld(int3 index)
        {
            return config.miniSpace * new float3(index);
        }
        /// <summary>
        /// 区块索引转世界坐标（正方体中最接近坐标轴负方向的一点）
        /// </summary>
        public float3 ChunkIndexToWorld(int3 index)
        {
            return config.miniSpace * config.subSize * new float3(index);
        }
        /// <summary>
        /// 区块索引转世界坐标（正方体中心点）
        /// </summary>
        public float3 ChunkIndexToWorldCenter(int3 index)
        {
            return ChunkIndexToWorld(index) + config.DeepToWorldSpace(1) / 2f;
        }



        /// <summary>
        /// 重设区块边长
        /// </summary>
        public void SetChunkEdgeSize(int size)
        {
            config.subSize = size;
            densityEdgeSize = config.subSize + Margin * 2;
            vertexEdgeSize = config.subSize + 1;
        }
        /// <summary>
        /// 重设体素边长
        /// </summary>
        public void SetCubeMiniSpace(float miniSpace)
        {
            config.miniSpace = miniSpace;
        }
    }

}
