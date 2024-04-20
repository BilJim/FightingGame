using GameFramework.Fsm;
using UnityEngine;

/// <summary>
/// Move 状态
/// </summary>
public class MonsterMoveState : MonsterBaseState
{

    //进入有限状态机时调用
    protected override void OnEnter(IFsm<RoleFsm> fsm)
    {
        base.OnEnter(fsm);
        animator.SetBool("isMoving", true);
    }

    //有限状态机的固定轮询调用逻辑
    protected override void OnUpdate(IFsm<RoleFsm> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
        if (playerData.moveDir == Vector2.zero)
        {
            ChangeState<MonsterIdleState>(fsm);
            return;
        }
        Move(elapseSeconds, Vector2.zero);
    }

    protected override void OnLeave(IFsm<RoleFsm> fsm, bool isShutdown)
    {
        base.OnLeave(fsm, isShutdown);
        animator.SetBool("isMoving", false);
    }
}