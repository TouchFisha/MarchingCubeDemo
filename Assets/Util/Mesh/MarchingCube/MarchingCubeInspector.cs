using System;
using Unity.Mathematics;
using UnityEngine;

namespace NUtil.NMesh
{
    [Serializable]
    public class MarchingCubeInspector
    {
        public DimensionConfig config;
        public FloorConfig floorConfig;

        public Material[] rawMaterials;
        public Material[] Materials => MaterialUtil.Clone(rawMaterials);
        public float ChunkSize => config.subSize * config.miniSpace;

        [NonSerialized]
        public MarchingCube marchingCube;
        [NonSerialized]
        public float floorHeightSQ;

        public MarchingCube Init()
        {
            floorHeightSQ = floorConfig.floorHeight * floorConfig.floorHeight;
            return marchingCube = new MarchingCube(config, floorConfig);
        }
    }
}
