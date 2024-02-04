using UnityEngine;

namespace NUtil
{
    public class MaterialUtil
    {
        public static Material[] Clone(params Material[] materials)
        {
            Material[] res = new Material[materials.Length];
            for (int i = 0; i < materials.Length; i++)
            {
                Material mat = materials[i];
                res[i] = new Material(mat);
            }
            return res;
        }

        public static void Set(Material material, string name, object value)
        {
            if (value is float f)
            {
                material.SetFloat(name, f);
            }
            else if (value is Vector3 v3)
            {
                material.SetVector(name, v3);
            }
            else if (value is Vector4 v4)
            {
                material.SetVector(name, v4);
            }
            else if (value is int i)
            {
                material.SetInt(name, i);
            }
            else if (value is Color c)
            {
                material.SetColor(name, c);
            }
            else if (value is Texture || value is Texture2D || value is Texture3D)
            {
                material.SetTexture(name, (Texture)value);
            }
            else if (value is Matrix4x4 matrix)
            {
                material.SetMatrix(name, matrix);
            }
            else if (value is ComputeBuffer buffer)
            {
                material.SetBuffer(name, buffer);
            }
            else if (value is bool key)
            {
                if (key)
                {
                    material.EnableKeyword(name);
                }
                else
                {
                    material.DisableKeyword(name);
                }
            }
        }

        public static void Set(Material material, int id, object value)
        {
            if (value is float f)
            {
                material.SetFloat(id, f);
            }
            else if (value is Vector3 v3)
            {
                material.SetVector(id, v3);
            }
            else if (value is Vector4 v4)
            {
                material.SetVector(id, v4);
            }
            else if (value is int i)
            {
                material.SetInt(id, i);
            }
            else if (value is Color c)
            {
                material.SetColor(id, c);
            }
            else if (value is Texture || value is Texture2D || value is Texture3D)
            {
                material.SetTexture(id, (Texture)value);
            }
            else if (value is Matrix4x4 matrix)
            {
                material.SetMatrix(id, matrix);
            }
            else if (value is ComputeBuffer buffer)
            {
                material.SetBuffer(id, buffer);
            }
        }
    }
}