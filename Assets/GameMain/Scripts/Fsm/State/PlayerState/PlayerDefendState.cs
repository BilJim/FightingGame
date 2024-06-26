using GameFramework.Fsm;
using UnityEngine;
using UnityGameFramework.Runtime;

/// <summary>
/// Defend 状态
/// </summary>
public class PlayerDefendState : PlayerBaseState
{

    //进入有限状态机时调用
    protected override void OnEnter(IFsm<PlayerRoleFsm> fsm)
    {
        base.OnEnter(fsm);
        animator.SetBool("isDefend", true);
        GameEntry.Event.Subscribe(InputControlEventArgs.EventId, OnNotice);
    }

    //有限状态机的固定轮询调用逻辑
    protected override void OnUpdate(IFsm<PlayerRoleFsm> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
    }

    //离开有限状态机时调用
    protected override void OnLeave(IFsm<PlayerRoleFsm> fsm, bool isShutdown)
    {
        base.OnLeave(fsm, isShutdown);
        animator.SetBool("isDefend", false);
        GameEntry.Event.Unsubscribe(InputControlEventArgs.EventId, OnNotice);
    }
}