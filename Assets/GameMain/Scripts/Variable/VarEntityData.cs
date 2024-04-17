using GameFramework;

/// <summary>
/// EntityData 变量类。
/// </summary>
public sealed class VarEntityData : Variable<EntityData>
{
    /// <summary>
    /// 初始化 System.String 变量类的新实例。
    /// </summary>
    public VarEntityData()
    {
    }

    /// <summary>
    /// 隐式转换。
    /// </summary>
    /// <param name="value">值。</param>
    public static implicit operator VarEntityData(EntityData value)
    {
        VarEntityData varValue = ReferencePool.Acquire<VarEntityData>();
        varValue.Value = value;
        return varValue;
    }

    /// <summary>
    /// 隐式转换。
    /// </summary>
    /// <param name="value">值。</param>
    public static implicit operator EntityData(VarEntityData value)
    {
        return value.Value;
    }
}
