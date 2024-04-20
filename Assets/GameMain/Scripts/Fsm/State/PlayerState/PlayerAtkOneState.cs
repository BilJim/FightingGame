using GameFramework.Fsm;
using UnityEngine;
using UnityGameFramework.Runtime;

/// <summary>
/// AtkOne 状态
/// </summary>
public class PlayerAtkOneState : PlayerBaseState
{
    //一定时间内无其他操作退出当前状态
    private float exitTime;

    //进入有限状态机时调用
    protected override void OnEnter(IFsm<PlayerRoleFsm> fsm)
    {
        base.OnEnter(fsm);
        animator.SetTrigger("atkTrigger");
        atkCount = fsm.GetData<VarInt32>("atkCount");
        animator.SetInteger("atkCount", atkCount);
        exitTime = 0.3f;
        GameEntry.Event.Subscribe(InputControlEventArgs.EventId, OnNotice);
    }

    //有限状态机的固定轮询调用逻辑
    protected override void OnUpdate(IFsm<PlayerRoleFsm> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
        exitTime -= elapseSeconds;
        if (exitTime <= 0)
        {
            atkCount = 0;
            fsm.RemoveData("atkCount");
            ChangeState<PlayerIdleState>(fsm);
        }
    }

    //离开有限状态机时调用
    protected override void OnLeave(IFsm<PlayerRoleFsm> fsm, bool isShutdown)
    {
        base.OnLeave(fsm, isShutdown);
        fsm.SetData<VarInt32>("atkCount", atkCount);
        animator.SetInteger("atkCount", atkCount);
        GameEntry.Event.Unsubscribe(InputControlEventArgs.EventId, OnNotice);
    }
}