using GameFramework.Fsm;
using UnityEngine;
using UnityGameFramework.Runtime;

/// <summary>
/// HitFly 状态
/// </summary>
public class HitFlyState : RoleBaseState
{

    private float xSpeed;
    private float ySpeed;
    private float nowXSpeed;
    private float nowYSpeed;
    //身体对象
    private Transform bodyTransform;
    //是否是躺倒状态
    private bool isFloor;
    //躺地事件
    private float floorTime;

    //进入有限状态机时调用
    protected override void OnEnter(IFsm<RoleFsm> fsm)
    {
        base.OnEnter(fsm);
        bodyTransform = player.Find("Role");
        xSpeed = fsm.GetData<VarSingle>("xSpeed");
        ySpeed = fsm.GetData<VarSingle>("ySpeed");
        nowXSpeed = xSpeed;
        nowYSpeed = ySpeed;
        fsm.RemoveData("xSpeed");
        fsm.RemoveData("ySpeed");
        animator.SetBool("isHitFly", true);
        animator.SetBool("isGround", false);
        floorTime = 0.2f;
    }

    //有限状态机的固定轮询调用逻辑
    protected override void OnUpdate(IFsm<RoleFsm> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
        if (isFloor)
        {
            floorTime -= elapseSeconds;
            if (floorTime <= 0)
            {
                ChangeState<IdleState>(fsm);
                return;
            }
        }
        bodyTransform.Translate(Vector2.up * nowYSpeed * elapseSeconds);
        nowYSpeed -= playerData.gSpeed * elapseSeconds;
        if (bodyTransform.localPosition.y <= 0)
        {
            bodyTransform.localPosition = Vector2.zero;
            isFloor = true;
            return;
        }
        player.Translate(Vector2.right * nowXSpeed * elapseSeconds);
    }

    //离开有限状态机时调用
    protected override void OnLeave(IFsm<RoleFsm> fsm, bool isShutdown)
    {
        base.OnLeave(fsm, isShutdown);
        isFloor = false;
        animator.SetBool("isHitFly", false);
        animator.SetBool("isGround", true);
    }
}