using NUtil.NMesh;
using NUtil.NThreadNative;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace NUtil
{
    public class MarchingCubeDemo : MonoBehaviour
    {
        public MarchingCubeInspector inspector;
        public int3 buildSize = new int3(5, 3, 5);
        private void Start()
        {
            MarchingCube marchingCube = inspector.Init();
            for (int x = 0; x < buildSize.x; x++)
            {
                for (int y = 0; y < buildSize.y; y++)
                {
                    for (int z = 0; z < buildSize.z; z++)
                    {
                        int3 xyz = new int3(x, y, z);
                        MeshDataBuffer data = new MeshDataBuffer(Allocator.Persistent);
                        new Job(() => {
                            data = marchingCube.Build(xyz, data);
                        }, () => {
                            GameObject go = new GameObject(xyz.ToString());
                            go.transform.position = new float3(xyz) * inspector.ChunkSize;
                            Mesh mesh = new Mesh();
                            data.ApplyMesh(mesh);
                            go.AddComponent<MeshFilter>().sharedMesh = mesh;
                            go.AddComponent<MeshRenderer>().sharedMaterials = inspector.Materials;
                        });
                    }
                }
            }
        }
        private void OnApplicationQuit()
        {
            MarchingCubeConstantBuffer.Dispose();
        }
    }
}