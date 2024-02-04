using System;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

namespace NUtil.NMesh
{
    [Serializable]
    public struct MeshDataBuffer : IDisposable
    {
        public NativeList<float3> position;
        public NativeList<float3> normal;
        public NativeList<float3> uvw0;
        public NativeList<float2> climatic;
        public NativeList<float> ambo;
        public NativeList<int> triangle;

        public MeshDataBuffer(Allocator allocator)
        {
            this.position = new NativeList<float3>(allocator);
            this.normal = new NativeList<float3>(allocator);
            this.uvw0 = new NativeList<float3>(allocator);
            this.climatic = new NativeList<float2>(allocator);
            this.ambo = new NativeList<float>(allocator);
            this.triangle = new NativeList<int>(allocator);
        }

        public void ApplyMesh(Mesh mesh)
        {
            if (position.Length > 2 && triangle.Length > 2)
            {
                mesh.Clear();
                mesh.SetVertices(position.AsArray(), 0, position.Length, MeshUpdateFlags.DontRecalculateBounds | MeshUpdateFlags.DontValidateIndices);
                mesh.SetNormals(normal.AsArray(), 0, normal.Length, MeshUpdateFlags.DontRecalculateBounds | MeshUpdateFlags.DontValidateIndices);
                mesh.SetUVs(0, uvw0.AsArray(), 0, uvw0.Length, MeshUpdateFlags.DontRecalculateBounds | MeshUpdateFlags.DontValidateIndices);
                mesh.SetUVs(1, climatic.AsArray(), 0, climatic.Length, MeshUpdateFlags.DontRecalculateBounds | MeshUpdateFlags.DontValidateIndices);
                mesh.SetTriangles(triangle.AsArray().ToArray(), 0, false);
                mesh.RecalculateBounds();
            }
            Dispose();
        }
        public void Dispose()
        {
            if (position.IsCreated)
            {
                position.Dispose();
            }
            if (normal.IsCreated)
            {
                normal.Dispose();
            }
            if (uvw0.IsCreated)
            {
                uvw0.Dispose();
            }
            if (ambo.IsCreated)
            {
                ambo.Dispose();
            }
            if (triangle.IsCreated)
            {
                triangle.Dispose();
            }
        }
    }

}
