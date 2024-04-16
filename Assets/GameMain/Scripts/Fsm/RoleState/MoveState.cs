using GameFramework.Fsm;
using UnityEngine;

/// <summary>
/// Move 状态
/// </summary>
public class MoveState : RoleBaseState
{

    //进入有限状态机时调用
    protected override void OnEnter(IFsm<RoleFsm> fsm)
    {
        base.OnEnter(fsm);
        animator.SetBool("isMoving", true);
        //订阅监听事件
        GameEntry.Event.Subscribe(InputControlEventArgs.EventId, OnNotice);
    }

    //有限状态机的固定轮询调用逻辑
    protected override void OnUpdate(IFsm<RoleFsm> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
        if (moveDir == Vector2.zero)
        {
            ChangeState<IdleState>(fsm);
            return;
        }
        Move(elapseSeconds);
    }

    protected override void OnLeave(IFsm<RoleFsm> fsm, bool isShutdown)
    {
        base.OnLeave(fsm, isShutdown);
        animator.SetBool("isMoving", false);
        GameEntry.Event.Unsubscribe(InputControlEventArgs.EventId, OnNotice);
    }
}