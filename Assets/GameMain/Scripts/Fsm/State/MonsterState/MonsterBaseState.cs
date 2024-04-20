using GameFramework.Fsm;
using UnityEngine;
using UnityGameFramework.Runtime;

/// <summary>
/// 人物基础状态
/// </summary>
public abstract class MonsterBaseState : RoleBaseState<MonsterRoleFsm>
{
    //玩家人物
    protected Transform monster;

    //攻击次数
    protected int atkCount;
    protected MonsterData monsterData;

    //创建有限状态机时调用
    protected override void OnInit(IFsm<MonsterRoleFsm> fsm)
    {
        base.OnInit(fsm);
        roleFsm = fsm;
    }

    //进入有限状态机时调用
    protected override void OnEnter(IFsm<MonsterRoleFsm> fsm)
    {
        base.OnEnter(fsm);
        monster = (Transform)fsm.GetData<VarUnityObject>("monster").Value;
        monsterData = fsm.GetData<VarEntityData>("monsterData").Value as MonsterData;
    }

    public void Idle()
    {
        if (roleFsm.CurrentState.GetType() != typeof(MonsterIdleState))
            ChangeState<MonsterIdleState>(roleFsm);
    }

    public void Move(float elapseSeconds, Vector2 movePos)
    {
        if (roleFsm.CurrentState.GetType() != typeof(MonsterMoveState))
            ChangeState<MonsterMoveState>(roleFsm);
        monsterData.movePos = movePos;
    }

    /// <summary>
    /// 跳跃
    /// </summary>
    public override void Jump()
    {
        //切换动作
        // if (animator.GetBool("isGround"))
            // ChangeState<MonsterJumpState>(roleFsm);
    }

    /// <summary>
    /// 手部攻击
    /// </summary>
    public override void Attack()
    {
        //切换动作
        // if (!animator.GetBool("isGround"))
        //     return;
        // roleFsm.SetData<VarInt32>("atkCount", atkCount);
        // ChangeState<PlayerAtkOneState>(roleFsm);
    }

    public override void Hit()
    {
        // ChangeState<PlayerHitState>(roleFsm);
    }

    /// <summary>
    /// 击飞
    /// </summary>
    /// <param name="xSpeed">水平速度</param>
    /// <param name="ySpeed">垂直速度</param>
    public override void HitFly(float xSpeed, float ySpeed)
    {
        // roleFsm.SetData<VarSingle>("xSpeed", xSpeed);
        // roleFsm.SetData<VarSingle>("ySpeed", ySpeed);
        // ChangeState<PlayerHitFlyState>(roleFsm);
    }
}