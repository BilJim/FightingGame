using GameFramework.Fsm;
using UnityEngine;
using UnityGameFramework.Runtime;

/// <summary>
/// 人物基础状态
/// </summary>
public abstract class RoleBaseState : FsmState<RoleFsm>
{
    //玩家人物
    protected Transform player;
    protected Animator animator;
    //用于调整人物方向
    private SpriteRenderer roleSprite;
    protected float moveSpeed;
    protected Vector2 moveDir;
    protected IFsm<RoleFsm> roleFsm;
    
    //创建有限状态机时调用
    protected override void OnInit(IFsm<RoleFsm> fsm)
    {
        base.OnInit(fsm);
        roleFsm = fsm;
    }

    //进入有限状态机时调用
    protected override void OnEnter(IFsm<RoleFsm> fsm)
    {
        base.OnEnter(fsm);
        player = (Transform)fsm.GetData<VarUnityObject>("player").Value;
        animator = (Animator)fsm.GetData<VarUnityObject>("animator").Value;
        roleSprite = (SpriteRenderer)fsm.GetData<VarUnityObject>("roleSprite").Value;
        moveSpeed = fsm.GetData<VarSingle>("moveSpeed").Value;
    }

    //有限状态机的固定轮询调用逻辑
    protected override void OnUpdate(IFsm<RoleFsm> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
        moveDir = fsm.GetData<VarVector2>("moveDir").Value;
    }

    //离开有限状态机时调用
    protected override void OnLeave(IFsm<RoleFsm> fsm, bool isShutdown)
    {
        base.OnLeave(fsm, isShutdown);
    }

    //销毁有限状态机时调用
    protected override void OnDestroy(IFsm<RoleFsm> fsm)
    {
        base.OnDestroy(fsm);
    }

    protected void Move(float elapseSeconds)
    {
        //移动
        player.Translate(moveDir.normalized * moveSpeed * elapseSeconds);
        //转向
        if (moveDir.x > 0)
            roleSprite.flipX = false;
        else if (moveDir.x < 0)
            roleSprite.flipX = true;
    }
    
    /// <summary>
    /// 跳跃
    /// </summary>
    public void Jump(IFsm<RoleFsm> fsm)
    {
        //切换动作
        if (animator.GetBool("isGround"))
        {
            ChangeState<JumpState>(fsm);
        }
    }
}