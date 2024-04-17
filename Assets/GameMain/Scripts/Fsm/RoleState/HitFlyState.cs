using GameFramework.Fsm;
using UnityEngine;
using UnityGameFramework.Runtime;

/// <summary>
/// HitFly 状态
/// </summary>
public class HitFlyState : RoleBaseState
{

    private float xSpeed;
    private float ySpeed;

    //进入有限状态机时调用
    protected override void OnEnter(IFsm<RoleFsm> fsm)
    {
        base.OnEnter(fsm);base.OnEnter(fsm);
        xSpeed = fsm.GetData<VarSingle>("xSpeed");
        ySpeed = fsm.GetData<VarSingle>("ySpeed");
        fsm.RemoveData("xSpeed");
        fsm.RemoveData("ySpeed");
        animator.SetBool("isHitFly", true);
        animator.SetBool("isGround", false);
    }

    //有限状态机的固定轮询调用逻辑
    protected override void OnUpdate(IFsm<RoleFsm> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
    }

    //离开有限状态机时调用
    protected override void OnLeave(IFsm<RoleFsm> fsm, bool isShutdown)
    {
        base.OnLeave(fsm, isShutdown);
        animator.SetBool("isHitFly", false);
        animator.SetBool("isGround", true);
    }
}