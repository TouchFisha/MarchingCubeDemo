using System;
using Unity.Collections;
using Unity.Mathematics;
using static Unity.Mathematics.math;

namespace NUtil.NMesh
{
    /// <summary>
    /// 区块位置设置
    /// </summary>
    [Serializable]
    public struct LocateConfig
    {
        [ReadOnly]
        public readonly int3 wsChunkIndex;
        [ReadOnly]
        public readonly int3 gsChunkIndex;
        [ReadOnly]
        public readonly int3 wsLLCubeIndex;
        [ReadOnly]
        public readonly int3 gsLLCubeIndex;
        [ReadOnly]
        public readonly float3 wsLLCubePosition;
        [ReadOnly]
        public readonly float3 gsLLCubePosition;

        public LocateConfig(DimensionConfig config, int3 wsChunkIndex, int3 gsChunkIndex)
        {
            this.wsChunkIndex = wsChunkIndex;
            this.gsChunkIndex = gsChunkIndex;

            wsLLCubeIndex = wsChunkIndex * config.subSize;
            gsLLCubeIndex = gsChunkIndex * config.subSize;

            wsLLCubePosition = float3(wsLLCubeIndex) * config.miniSpace;
            gsLLCubePosition = float3(gsLLCubeIndex) * config.miniSpace;
        }
    }

}
