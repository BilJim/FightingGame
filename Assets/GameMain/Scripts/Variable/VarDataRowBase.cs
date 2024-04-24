using GameFramework;
using UnityGameFramework.Runtime;

/// <summary>
/// DataRowBase 变量类。
/// </summary>
public sealed class VarDataRowBase : Variable<DataRowBase>
{
    /// <summary>
    /// 初始化 变量类的新实例。
    /// </summary>
    public VarDataRowBase()
    {
    }

    /// <summary>
    /// 隐式转换。
    /// </summary>
    /// <param name="value">值。</param>
    public static implicit operator VarDataRowBase(DataRowBase value)
    {
        VarDataRowBase varValue = ReferencePool.Acquire<VarDataRowBase>();
        varValue.Value = value;
        return varValue;
    }

    /// <summary>
    /// 隐式转换。
    /// </summary>
    /// <param name="value">值。</param>
    public static implicit operator DataRowBase(VarDataRowBase value)
    {
        return value.Value;
    }
}
