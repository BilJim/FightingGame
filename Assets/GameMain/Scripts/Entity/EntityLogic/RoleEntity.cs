using UnityEngine;
using UnityGameFramework.Runtime;

/// <summary>
/// 角色对象基类
/// </summary>
public abstract class RoleEntity : TargetableObject
{
    protected RoleFsm roleFsm;

    //移动方向
    protected Vector2 moveDir = Vector2.zero;

    //移动速度
    protected float moveSpeed = 3;
    protected SpriteRenderer roleSprite;

    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        roleSprite = transform.Find("Role").GetComponentInChildren<SpriteRenderer>();
        Animator = transform.Find("Role").GetComponentInChildren<Animator>();
    }


    protected override void OnShow(object userData)
    {
        base.OnShow(userData);
        //创建角色状态机
        roleFsm = new RoleFsm();
        roleFsm.m_Fsm.SetData<VarUnityObject>("roleSprite", roleSprite);
        roleFsm.m_Fsm.SetData<VarUnityObject>("player", transform);
        roleFsm.m_Fsm.SetData<VarUnityObject>("animator", Animator);
        roleFsm.m_Fsm.SetData<VarSingle>("moveSpeed", moveSpeed);
        roleFsm.m_Fsm.SetData<VarVector2>("moveDir", moveDir);
        //状态机初始状态为 IdleState
        roleFsm.m_Fsm.Start<IdleState>();
    }

    protected override void OnHide(bool isShutdown, object userData)
    {
        base.OnHide(isShutdown, userData);
        roleFsm.DestroyFsm();
    }

    protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(elapseSeconds, realElapseSeconds);
        roleFsm.m_Fsm.SetData<VarVector2>("moveDir", moveDir);
    }
}