using GameFramework.Fsm;
using UnityEngine;
using UnityGameFramework.Runtime;

/// <summary>
/// Move 状态
/// </summary>
public class MoveState : RoleBaseState
{
    //用于调整人物方向
    private SpriteRenderer roleSprite;

    //进入有限状态机时调用
    protected override void OnEnter(IFsm<RoleFsm> fsm)
    {
        base.OnEnter(fsm);
        roleSprite = (SpriteRenderer)fsm.GetData<VarUnityObject>("roleSprite").Value;
    }

    //有限状态机的固定轮询调用逻辑
    protected override void OnUpdate(IFsm<RoleFsm> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
        animator.SetBool("isMoving", true);
        if (moveDir == Vector2.zero)
        {
            ChangeState<IdleState>(fsm);
            return;
        }
        //移动
        player.Translate(moveDir.normalized * moveSpeed * elapseSeconds);
        //转向
        if (moveDir.x > 0)
            roleSprite.flipX = false;
        else if (moveDir.x < 0)
            roleSprite.flipX = true;
    }
}