using GameFramework.Event;
using GameFramework.Fsm;
using UnityEngine;
using UnityGameFramework.Runtime;

/// <summary>
/// 人物基础状态
/// </summary>
public abstract class PlayerBaseState : RoleBaseState
{
    //玩家人物
    protected Transform player;
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
        playerData = fsm.GetData<VarEntityData>("playerData").Value as PlayerData;
        if (fsm.GetData<VarInt32>("atkCount") == null)
            atkCount = 0;
        else
            atkCount = fsm.GetData<VarInt32>("atkCount").Value;
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
            case InputControlType.PickUp:
                ChangeState<PlayerPickUpState>(roleFsm);
                break;
            case InputControlType.Throw:
                ChangeState<PlayerThrowState>(roleFsm);
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
    public override void Jump()
    {
        //切换动作
        if (animator.GetBool("isGround"))
            ChangeState<PlayerJumpState>(roleFsm);
    }

    /// <summary>
    /// 手部攻击
    /// </summary>
    public override void Attack()
    {
        //切换动作
        if (!animator.GetBool("isGround"))
            return;
        FsmState<RoleFsm> currentState = roleFsm.CurrentState;
        atkCount ++;
        if (currentState.GetType() == typeof(PlayerAtkOneState))
        {
            roleFsm.SetData<VarInt32>("atkCount", atkCount);
            ChangeState<PlayerAtkTwoState>(roleFsm);
        }
        else if (currentState.GetType() == typeof(PlayerAtkTwoState))
        {
            roleFsm.SetData<VarInt32>("atkCount", atkCount);
            ChangeState<PlayerAtkThreeState>(roleFsm);
        }
        else
        {
            roleFsm.SetData<VarInt32>("atkCount", atkCount);
            ChangeState<PlayerAtkOneState>(roleFsm);
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
        if (currentState.GetType() == typeof(PlayerFootAtkOneState))
        {
            roleFsm.SetData<VarInt32>("atkCount", atkCount);
            ChangeState<PlayerFootAtkTwoState>(roleFsm);
        }
        else
        {
            roleFsm.SetData<VarInt32>("atkCount", atkCount);
            ChangeState<PlayerFootAtkOneState>(roleFsm);
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
            ChangeState<PlayerDefendState>(roleFsm);
        else
            ChangeState<PlayerIdleState>(roleFsm);
    }

    public override void Hit()
    {
        ChangeState<PlayerHitState>(roleFsm);
    }

    /// <summary>
    /// 击飞
    /// </summary>
    /// <param name="xSpeed">水平速度</param>
    /// <param name="ySpeed">垂直速度</param>
    public override void HitFly(float xSpeed, float ySpeed)
    {
        roleFsm.SetData<VarSingle>("xSpeed", xSpeed);
        roleFsm.SetData<VarSingle>("ySpeed", ySpeed);
        ChangeState<PlayerHitFlyState>(roleFsm);
    }
}