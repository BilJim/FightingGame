using System;
using GameFramework.Event;
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
    //攻击次数
    protected int atkCount;
    
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
        if (fsm.GetData<VarInt32>("atkCount") == null)
            atkCount = 0;
        else
            atkCount = fsm.GetData<VarInt32>("atkCount").Value;
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
    
    /// <summary>
    /// 订阅事件后要触发的通知
    /// </summary>
    /// <param name="sender">事件源</param>
    /// <param name="args">事件参数</param>
    public virtual void OnNotice(object sender, GameEventArgs args)
    {
        InputControlEventArgs eventArgs = args as InputControlEventArgs;
        switch (eventArgs.inputType)
        {
            case InputControlType.Jump:
                Jump(roleFsm);
                break;
            case InputControlType.Atk1:
                Attack(roleFsm);
                break;
            case InputControlType.Atk2:
                FootAttack(roleFsm);
                break;
            case InputControlType.Defend:
                Defend(roleFsm);
                break;
            case InputControlType.UnDefend:
                Defend(roleFsm, false);
                break;
        }
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
            ChangeState<JumpState>(fsm);
    }

    /// <summary>
    /// 手部攻击
    /// </summary>
    /// <param name="fsm"></param>
    public void Attack(IFsm<RoleFsm> fsm)
    {
        //切换动作
        if (!animator.GetBool("isGround"))
            return;
        FsmState<RoleFsm> currentState = fsm.CurrentState;
        atkCount ++;
        if (currentState.GetType() == typeof(AtkOneState))
        {
            fsm.SetData<VarInt32>("atkCount", atkCount);
            ChangeState<AtkTwoState>(fsm);
        }
        else if (currentState.GetType() == typeof(AtkTwoState))
        {
            fsm.SetData<VarInt32>("atkCount", atkCount);
            ChangeState<AtkThreeState>(fsm);
        }
        else
        {
            fsm.SetData<VarInt32>("atkCount", atkCount);
            ChangeState<AtkOneState>(fsm);
        }
    }
    
    /// <summary>
    /// 脚部攻击
    /// </summary>
    /// <param name="fsm"></param>
    public void FootAttack(IFsm<RoleFsm> fsm)
    {
        //切换动作
        if (!animator.GetBool("isGround"))
            return;
        FsmState<RoleFsm> currentState = fsm.CurrentState;
        atkCount ++;
        if (currentState.GetType() == typeof(FootAtkOneState))
        {
            fsm.SetData<VarInt32>("atkCount", atkCount);
            ChangeState<FootAtkTwoState>(fsm);
        }
        else
        {
            fsm.SetData<VarInt32>("atkCount", atkCount);
            ChangeState<FootAtkOneState>(fsm);
        }
    }

    /// <summary>
    /// 格挡
    /// </summary>
    /// <param name="fsm"></param>
    public void Defend(IFsm<RoleFsm> fsm, bool isDefend = true)
    {
        //切换动作
        if (!animator.GetBool("isGround"))
            return;
        if (isDefend)
            ChangeState<DefendState>(fsm);
        else
            ChangeState<IdleState>(fsm);
    }
}