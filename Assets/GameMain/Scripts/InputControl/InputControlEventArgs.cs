using GameFramework;
using GameFramework.Event;

/// <summary>
/// InputControl 事件参数
/// </summary>
public class InputControlEventArgs : GameEventArgs
{
    //事件编号
    public static readonly int EventId = typeof(InputControlEventArgs).GetHashCode();

    public override int Id => EventId;

    //事件类型
    public InputControlType inputType;

    //按键值
    public float keyValue;

    //注意，Clear方法 不是取消事件监听相关
    //而是引用池相关
    public override void Clear()
    {
        keyValue = 0;
    }

    /// <summary>
    /// 创建事件参数
    /// </summary>
    public static InputControlEventArgs TriggerEvent(InputControlType inputType)
    {
        //通过引用池来创建或获取对象
        InputControlEventArgs args = ReferencePool.Acquire<InputControlEventArgs>();
        args.inputType = inputType;
        return args;
    }

    /// <summary>
    /// 创建事件参数
    /// </summary>
    public static InputControlEventArgs TriggerEvent(InputControlType inputType, float keyValue)
    {
        //通过引用池来创建或获取对象
        InputControlEventArgs args = ReferencePool.Acquire<InputControlEventArgs>();
        args.inputType = inputType;
        args.keyValue = keyValue;
        return args;
    }
}