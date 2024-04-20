using GameFramework.Fsm;
using UnityEngine;

/// <summary>
/// Idle 状态
/// </summary>
public class PlayerIdleState : PlayerBaseState
{
    //进入有限状态机时调用
    protected override void OnEnter(IFsm<PlayerRoleFsm> fsm)
    {
        base.OnEnter(fsm);
        //订阅监听事件
        GameEntry.Event.Subscribe(InputControlEventArgs.EventId, OnNotice);
    }

    //有限状态机的固定轮询调用逻辑
    protected override void OnUpdate(IFsm<PlayerRoleFsm> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
        if (playerData.moveDir != Vector2.zero)
            ChangeState<PlayerMoveState>(fsm);
    }

    protected override void OnLeave(IFsm<PlayerRoleFsm> fsm, bool isShutdown)
    {
        base.OnLeave(fsm, isShutdown);
        GameEntry.Event.Unsubscribe(InputControlEventArgs.EventId, OnNotice);
    }
}