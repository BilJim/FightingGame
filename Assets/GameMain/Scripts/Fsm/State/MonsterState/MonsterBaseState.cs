using GameFramework.Fsm;
using UnityEngine;
using UnityGameFramework.Runtime;

/// <summary>
/// 人物基础状态
/// </summary>
public abstract class MonsterBaseState : RoleBaseState
{
    //玩家人物
    protected Transform monster;

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
        monster = (Transform)fsm.GetData<VarUnityObject>("monster").Value;
        playerData = fsm.GetData<VarEntityData>("monsterData").Value as PlayerData;
    }

    protected void Move(float elapseSeconds, Vector2 moveDir)
    {
        //移动
        monster.Translate(playerData.moveDir.normalized * playerData.moveSpeed * elapseSeconds);
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
        roleFsm.SetData<VarInt32>("atkCount", atkCount);
        ChangeState<PlayerAtkOneState>(roleFsm);
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