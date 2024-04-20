using GameFramework.Fsm;
using UnityEngine;
using UnityGameFramework.Runtime;

/// <summary>
/// FootAtk 状态
/// </summary>
public class PlayerFootAtkTwoState : PlayerBaseState
{

    //一定时间内无其他操作退出当前状态
    private float exitTime;

    //进入有限状态机时调用
    protected override void OnEnter(IFsm<PlayerRoleFsm> fsm)
    {
        base.OnEnter(fsm);
        animator.SetInteger("atkCount", atkCount);
        exitTime = 0.3f;
    }

    //有限状态机的固定轮询调用逻辑
    protected override void OnUpdate(IFsm<PlayerRoleFsm> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
        exitTime -= elapseSeconds;
        if (exitTime <= 0)
            ChangeState<PlayerIdleState>(fsm);
    }

    //离开有限状态机时调用
    protected override void OnLeave(IFsm<PlayerRoleFsm> fsm, bool isShutdown)
    {
        atkCount = 0;
        animator.SetInteger("atkCount", atkCount);
        fsm.RemoveData("atkCount");
        base.OnLeave(fsm, isShutdown);
    }
}