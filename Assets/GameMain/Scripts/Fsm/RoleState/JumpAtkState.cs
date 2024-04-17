using GameFramework.Fsm;
using UnityEngine;
using UnityGameFramework.Runtime;

/// <summary>
/// JumpAtk 状态
/// </summary>
public class JumpAtkState : JumpState
{

    //进入有限状态机时调用
    protected override void OnEnter(IFsm<RoleFsm> fsm)
    {
        base.OnEnter(fsm);
        nowJumpSpeed = fsm.GetData<VarSingle>("nowJumpSpeed").Value;
        fsm.RemoveData("nowJumpSpeed");
        //攻击动画
        animator.SetTrigger("atkTrigger");
    }

    //有限状态机的固定轮询调用逻辑
    protected override void OnUpdate(IFsm<RoleFsm> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
    }

    //离开有限状态机时调用
    protected override void OnLeave(IFsm<RoleFsm> fsm, bool isShutdown)
    {
        // base.OnLeave(fsm, isShutdown);
    }
}