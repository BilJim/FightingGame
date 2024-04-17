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
    protected IFsm<RoleFsm> roleFsm;
    //攻击次数
    protected int atkCount;
    protected PlayerData playerData;
    
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
        playerData = fsm.GetData<VarEntityData>("playerData").Value as PlayerData;
        if (fsm.GetData<VarInt32>("atkCount") == null)
            atkCount = 0;
        else
            atkCount = fsm.GetData<VarInt32>("atkCount").Value;
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
                Jump();
                break;
            case InputControlType.Atk1:
                Attack();
                break;
            case InputControlType.Atk2:
                FootAttack();
                break;
            case InputControlType.Defend:
                Defend();
                break;
            case InputControlType.UnDefend:
                Defend(false);
                break;
        }
    }

    protected void Move(float elapseSeconds)
    {
        //移动
        player.Translate(playerData.moveDir.normalized * playerData.moveSpeed * elapseSeconds);
        //转向
        if (playerData.moveDir.x > 0)
            roleSprite.flipX = false;
        else if (playerData.moveDir.x < 0)
            roleSprite.flipX = true;
    }
    
    /// <summary>
    /// 跳跃
    /// </summary>
    public void Jump()
    {
        //切换动作
        if (animator.GetBool("isGround"))
            ChangeState<JumpState>(roleFsm);
    }

    /// <summary>
    /// 手部攻击
    /// </summary>
    public void Attack()
    {
        //切换动作
        if (!animator.GetBool("isGround"))
            return;
        FsmState<RoleFsm> currentState = roleFsm.CurrentState;
        atkCount ++;
        if (currentState.GetType() == typeof(AtkOneState))
        {
            roleFsm.SetData<VarInt32>("atkCount", atkCount);
            ChangeState<AtkTwoState>(roleFsm);
        }
        else if (currentState.GetType() == typeof(AtkTwoState))
        {
            roleFsm.SetData<VarInt32>("atkCount", atkCount);
            ChangeState<AtkThreeState>(roleFsm);
        }
        else
        {
            roleFsm.SetData<VarInt32>("atkCount", atkCount);
            ChangeState<AtkOneState>(roleFsm);
        }
    }
    
    /// <summary>
    /// 脚部攻击
    /// </summary>
    public void FootAttack()
    {
        //切换动作
        if (!animator.GetBool("isGround"))
            return;
        FsmState<RoleFsm> currentState = roleFsm.CurrentState;
        atkCount ++;
        if (currentState.GetType() == typeof(FootAtkOneState))
        {
            roleFsm.SetData<VarInt32>("atkCount", atkCount);
            ChangeState<FootAtkTwoState>(roleFsm);
        }
        else
        {
            roleFsm.SetData<VarInt32>("atkCount", atkCount);
            ChangeState<FootAtkOneState>(roleFsm);
        }
    }

    /// <summary>
    /// 格挡
    /// </summary>
    public void Defend(bool isDefend = true)
    {
        //切换动作
        if (!animator.GetBool("isGround"))
            return;
        if (isDefend)
            // ChangeState<DefendState>(fsm);
            HitFly(10, 20);
        else
            ChangeState<IdleState>(roleFsm);
    }

    /// <summary>
    /// 击飞
    /// </summary>
    /// <param name="xSpeed">水平速度</param>
    /// <param name="ySpeed">垂直速度</param>
    public void HitFly(float xSpeed, float ySpeed)
    {
        roleFsm.SetData<VarSingle>("xSpeed", xSpeed);
        roleFsm.SetData<VarSingle>("ySpeed", ySpeed);
        ChangeState<HitFlyState>(roleFsm);
    }
}