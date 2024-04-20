using GameFramework.Fsm;
using UnityEngine;
using UnityGameFramework.Runtime;

/// <summary>
/// Hit 状态
/// </summary>
public class PlayerHitState : PlayerBaseState
{

    //一定时间内结束受伤状态
    private float exitTime;
    
    //进入有限状态机时调用
    protected override void OnEnter(IFsm<RoleFsm> fsm)
    {
        base.OnEnter(fsm);
        animator.SetBool("isHit", true);
        if (fsm.GetData<VarSingle>("hitTime") == null)
            exitTime = 0.3f;
        else
        {
            exitTime = fsm.GetData<VarSingle>("hitTime");
            fsm.RemoveData("hitTime");
        }
    }

    //有限状态机的固定轮询调用逻辑
    protected override void OnUpdate(IFsm<RoleFsm> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
        exitTime -= elapseSeconds;
        if (exitTime <= 0)
            ChangeState<PlayerIdleState>(fsm);
    }

    //离开有限状态机时调用
    protected override void OnLeave(IFsm<RoleFsm> fsm, bool isShutdown)
    {
        base.OnLeave(fsm, isShutdown);
        animator.SetBool("isHit", false);
    }
}