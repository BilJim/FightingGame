using UnityEngine;
using UnityGameFramework.Runtime;

/// <summary>
/// 输入控制组件
/// </summary>
public class InputControlComponent : GameFrameworkComponent
{
    private void Update()
    {
        //方向键
        GameEntry.Event.FireNow(this, InputControlEventArgs.TriggerEvent(InputControlType.Horizontal, Input.GetAxisRaw("Horizontal")));
        GameEntry.Event.FireNow(this, InputControlEventArgs.TriggerEvent(InputControlType.Vertical, Input.GetAxisRaw("Vertical")));

        //攻击1
        if (Input.GetKeyDown(KeyCode.J))
            GameEntry.Event.FireNow(this, InputControlEventArgs.TriggerEvent(InputControlType.Atk1));
        //攻击2
        if (Input.GetKeyDown(KeyCode.K))
            GameEntry.Event.FireNow(this, InputControlEventArgs.TriggerEvent(InputControlType.Atk2));
        //格挡
        if (Input.GetKeyDown(KeyCode.L))
            GameEntry.Event.FireNow(this, InputControlEventArgs.TriggerEvent(InputControlType.Defend));
        if (Input.GetKeyUp(KeyCode.L))
            GameEntry.Event.FireNow(this, InputControlEventArgs.TriggerEvent(InputControlType.UnDefend));
        //跳跃
        if (Input.GetKeyDown(KeyCode.Space))
            GameEntry.Event.FireNow(this, InputControlEventArgs.TriggerEvent(InputControlType.Jump));
    }
}