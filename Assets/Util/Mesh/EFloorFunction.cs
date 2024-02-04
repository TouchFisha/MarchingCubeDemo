namespace NUtil.NMesh
{
    /// <summary>
    /// 地面计算方式设置
    /// </summary>
    public enum EFloorFunction
    {
        /// <summary>
        /// 垂直Y轴生成
        /// </summary>
        AxisY,
        /// <summary>
        /// 根据球心生成
        /// </summary>
        Center,
        /// <summary>
        /// 根据空间范围内唯一随机点生成
        /// </summary>
        DensityCenter
    }

}
