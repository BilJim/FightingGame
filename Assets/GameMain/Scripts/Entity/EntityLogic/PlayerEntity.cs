using System;
using GameFramework.Event;
using UnityGameFramework.Runtime;

/// <summary>
/// 玩家逻辑实体
/// </summary>
public class PlayerEntity : TargetableObject
{
    protected override void OnShow(object userData)
    {
        base.OnShow(userData);
        //订阅监听事件
        GameEntry.Event.Subscribe(InputControlEventArgs.EventId, OnNotice);
    }

    protected override void OnDead(Entity attacker)
    {
        base.OnDead(attacker);
        GameEntry.Event.Unsubscribe(InputControlEventArgs.EventId, OnNotice);
    }

    /// <summary>
    /// 订阅事件后要触发的通知
    /// </summary>
    /// <param name="sender">事件源</param>
    /// <param name="args">事件参数</param>
    public void OnNotice(object sender, GameEventArgs args)
    {
        InputControlEventArgs eventArgs = args as InputControlEventArgs;
        Log.Debug($"current InputControlType: {eventArgs.inputType}");
        switch (eventArgs.inputType)
        {
            case InputControlType.Horizontal:
                break;
            case InputControlType.Vertical:
                break;
            case InputControlType.Atk1:
                break;
            case InputControlType.Atk2:
                break;
            case InputControlType.Defend:
                break;
            case InputControlType.Jump:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}