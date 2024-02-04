using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;

namespace NUtil.NMesh
{
    /// <summary>
    /// 维度设置
    /// </summary>
    [Serializable]
    public struct DimensionConfig
    {
        /// <summary>
        /// 每个维度下的子维度数量（XYZ）
        /// </summary>
        public int subSize;
        /// <summary>
        /// 最小维度对应的空间大小（XYZ）
        /// </summary>
        public float miniSpace;

        public DimensionConfig(int subSize, float miniSpace)
        {
            this.subSize = subSize;
            this.miniSpace = miniSpace;
        }
        public float DeepToWorldSpace(int deep)
        {
            return miniSpace * math.pow(subSize, deep);
        }
        public float3 IndexToWorldDimensionLLPosition(int3 index, int deep)
        {
            return new float3(index) * DeepToWorldSpace(deep);
        }
        public float3 IndexToWorldDimensionCenterPosition(int3 index, int deep)
        {
            float ws = DeepToWorldSpace(deep);
            return new float3(index) * ws + ws / 2f;
        }
        public int3 WorldPositionToDimensionIndex(float3 position, int deep)
        {
            return new int3(math.floor(position / DeepToWorldSpace(deep)));
        }
        public int3 WorldIndexToLocalIndex(int3 worldIndex)
        {
            worldIndex = worldIndex % subSize;
            if (worldIndex.x < 0)
            {
                worldIndex.x = subSize + worldIndex.x;
            }
            if (worldIndex.y < 0)
            {
                worldIndex.y = subSize + worldIndex.y;
            }
            if (worldIndex.z < 0)
            {
                worldIndex.z = subSize + worldIndex.z;
            }
            return worldIndex;
        }

        public int DeepToMinLayerSize(int deep)
        {
            int res = 1;
            for (int i = 0; i < deep; i++)
            {
                res *= subSize;
            }
            return res;
        }

        public float TransformSize(int srcDeep, int destDeep)
        {
            return DeepToMinLayerSize(srcDeep) / (float)DeepToMinLayerSize(destDeep);
        }
        public int3 TransformWorldIndex(int3 srcIndex, int srcDeep, int destDeep)
        {
            int dev = math.abs(srcDeep - destDeep);
            if (dev == 0)
            {
                return srcIndex;
            }
            if (srcDeep > destDeep)
            {
                for (int i = 0; i < dev; i++)
                {
                    srcIndex *= subSize;
                }
            }
            if (srcDeep < destDeep)
            {
                for (int i = 0; i < dev; i++)
                {
                    srcIndex = new int3(math.floor(srcIndex / new float3(subSize)));
                }
            }
            return srcIndex;
        }

    }

}
