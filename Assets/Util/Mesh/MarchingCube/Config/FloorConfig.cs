using System;
using Unity.Mathematics;
using static Unity.Mathematics.math;

namespace NUtil.NMesh
{
    /// <summary>
    /// 地面高度设置
    /// </summary>
    [Serializable]
    public struct FloorConfig
    {
        /// <summary>
        /// 地面高度计算方式设置
        /// </summary>
        public EFloorFunction floorType;
        /// <summary>
        /// 地面高度计算影响地形的程度
        /// </summary>
        public float floorFactor;
        /// <summary>
        /// 地面高度
        /// </summary>
        public float floorHeight;
        /// <summary>
        /// 当floorType为Center时，使用此坐标作为球心
        /// </summary>
        public float3 floorCenter;
        /// <summary>
        /// 当floorType为DensityCenter时，使用此缩放作为空间唯一点生成依据
        /// </summary>
        public float floorCenterScale;
        /// <summary>
        /// 当floorType为DensityCenter时，使用此作为两个空间唯一点之间相隔的距离
        /// </summary>
        public float floorHeightMargin;

        public FloorConfig(EFloorFunction floorType, float floorFactor, float floorHeight, float3 floorCenter, float floorCenterScale, float floorHeightMargin)
        {
            this.floorType = floorType;
            this.floorFactor = floorFactor;
            this.floorHeight = floorHeight;
            this.floorCenter = floorCenter;
            this.floorCenterScale = floorCenterScale;
            this.floorHeightMargin = floorHeightMargin;
        }
        public float GetFloorHeight(float3 ws)
        {
            float height_floor_ori = 0;
            if (floorType == EFloorFunction.AxisY)
            {
                height_floor_ori = ws.y - floorHeight;
            }
            else if (floorType == EFloorFunction.Center)
            {
                height_floor_ori = length(ws - floorCenter) - floorHeight;
            }
            else if (floorType == EFloorFunction.DensityCenter)
            {
                height_floor_ori = length(ws - DensityCenter(ws)) - floorHeight;
            }
            return height_floor_ori * floorFactor;
        }
        public float3 DensityCenter(float3 ws)
        {
            float3 sf = floor(ws * floorCenterScale);
            float3 Pi = mod289(sf);
            float3 lhs = permute(permute(Pi.x + float3(0f, 1f, 0f)) + Pi.y + float3(0f, 0f, 1f));
            float3 oz = frac(permute(lhs + Pi.z) * 0.28285715f);
            float3 dev = abs(oz);
            float marginScale = 1f / (1f / floorCenterScale - floorHeightMargin * 2);
            return sf / floorCenterScale + dev / marginScale + floorHeightMargin;
        }
        public static float3 permute(float3 x)
        {
            return mod289((34f * x + 1f) * x);
        }
        public static float3 mod289(float3 x)
        {
            return x - floor(x * 0.0034602077f) * 289f;
        }
    }

}
