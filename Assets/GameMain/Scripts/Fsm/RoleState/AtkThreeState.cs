using GameFramework.Fsm;

/// <summary>
/// 站立状态
/// </summary>
public class AtkThreeState : RoleBaseState
{

    //一定时间内无其他操作退出当前状态
    private float exitTime;

    //进入有限状态机时调用
    protected override void OnEnter(IFsm<RoleFsm> fsm)
    {
        base.OnEnter(fsm);
        animator.SetInteger("atkCount", atkCount);
        exitTime = 0.4f;
    }

    //有限状态机的固定轮询调用逻辑
    protected override void OnUpdate(IFsm<RoleFsm> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
        exitTime -= elapseSeconds;
        if (exitTime <= 0)
            ChangeState<IdleState>(fsm);
    }

    //离开有限状态机时调用
    protected override void OnLeave(IFsm<RoleFsm> fsm, bool isShutdown)
    {
        base.OnLeave(fsm, isShutdown);
        atkCount = 0;
        animator.SetInteger("atkCount", atkCount);
        fsm.RemoveData("atkCount");
    }
}