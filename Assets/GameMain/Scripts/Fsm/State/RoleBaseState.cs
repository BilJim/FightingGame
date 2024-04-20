using GameFramework.Fsm;
using UnityEngine;
using UnityGameFramework.Runtime;

/// <summary>
/// 人物基础状态
/// </summary>
public abstract class RoleBaseState<T> : FsmState<T> where T : class
{
    protected Animator animator;
    //用于调整人物方向
    protected SpriteRenderer roleSprite;
    protected IFsm<T> roleFsm;
    
    //创建有限状态机时调用
    protected override void OnInit(IFsm<T> fsm)
    {
        base.OnInit(fsm);
    }

    //进入有限状态机时调用
    protected override void OnEnter(IFsm<T> fsm)
    {
        base.OnEnter(fsm);
        animator = (Animator)fsm.GetData<VarUnityObject>("animator").Value;
        roleSprite = (SpriteRenderer)fsm.GetData<VarUnityObject>("roleSprite").Value;
    }

    //有限状态机的固定轮询调用逻辑
    protected override void OnUpdate(IFsm<T> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
    }

    //离开有限状态机时调用
    protected override void OnLeave(IFsm<T> fsm, bool isShutdown)
    {
        base.OnLeave(fsm, isShutdown);
    }

    //销毁有限状态机时调用
    protected override void OnDestroy(IFsm<T> fsm)
    {
        base.OnDestroy(fsm);
    }

    /// <summary>
    /// 跳跃
    /// </summary>
    public abstract void Jump();

    /// <summary>
    /// 手部攻击
    /// </summary>
    public abstract void Attack();

    /// <summary>
    /// 受击
    /// </summary>
    public abstract void Hit();

    /// <summary>
    /// 击飞
    /// </summary>
    /// <param name="xSpeed">水平速度</param>
    /// <param name="ySpeed">垂直速度</param>
    public abstract void HitFly(float xSpeed, float ySpeed);
}