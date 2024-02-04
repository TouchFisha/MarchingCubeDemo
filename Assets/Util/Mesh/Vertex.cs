using System;
using Unity.Mathematics;

namespace NUtil.NMesh
{
    [Serializable]
    public struct Vertex
    {
        public float3 position;
        public float3 normal;
        public float3 uvw0;
        public float ambo;
    }

}
