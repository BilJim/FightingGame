using GameFramework.Fsm;
using UnityEngine;

/// <summary>
/// Move 状态
/// </summary>
public class MonsterMoveState : MonsterBaseState
{

    //进入有限状态机时调用
    protected override void OnEnter(IFsm<MonsterRoleFsm> fsm)
    {
        base.OnEnter(fsm);
        animator.SetBool("isMoving", true);
    }

    //有限状态机的固定轮询调用逻辑
    protected override void OnUpdate(IFsm<MonsterRoleFsm> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
        if (monsterData.movePos == Vector2.zero)
        {
            ChangeState<MonsterIdleState>(fsm);
            return;
        }
        //移动
        monster.Translate(monsterData.movePos.normalized * monsterData.moveSpeed * elapseSeconds);
        //转向
        if (monsterData.movePos.x > 0)
            roleSprite.flipX = false;
        else if (monsterData.movePos.x < 0)
            roleSprite.flipX = true;
    }

    protected override void OnLeave(IFsm<MonsterRoleFsm> fsm, bool isShutdown)
    {
        base.OnLeave(fsm, isShutdown);
        animator.SetBool("isMoving", false);
    }
}