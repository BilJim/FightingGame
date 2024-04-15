using GameFramework.Event;
using GameFramework.Fsm;
using UnityEngine;
using UnityGameFramework.Runtime;

/// <summary>
/// Jump 状态
/// </summary>
public class JumpState : RoleBaseState
{

    //跳跃速度
    private float jumpSpeed = 10;
    protected float nowJumpSpeed;
    //重力加速度
    private float gSpeed = 50f;
    //身体对象，用于 y轴 模拟跳跃
    private Transform bodyTransform;

    //进入有限状态机时调用
    protected override void OnEnter(IFsm<RoleFsm> fsm)
    {
        base.OnEnter(fsm);
        bodyTransform = (Transform)fsm.GetData<VarUnityObject>("body");
        if (fsm.CurrentState.GetType() == typeof(JumpAtkState))
            return;
        animator.SetBool("isGround", false);
        animator.SetTrigger("jumpTrigger");
        nowJumpSpeed = jumpSpeed;
        //订阅监听事件
        GameEntry.Event.Subscribe(InputControlEventArgs.EventId, OnNotice);
    }

    //有限状态机的固定轮询调用逻辑
    protected override void OnUpdate(IFsm<RoleFsm> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
        //重力影响 v = v - gt;
        if (bodyTransform.localPosition.y != 0)
            nowJumpSpeed -= gSpeed * elapseSeconds;
        if (nowJumpSpeed <= 0 && bodyTransform.localPosition.y <= 0)
        {
            animator.SetBool("isGround", true);
            bodyTransform.localPosition = Vector2.zero;
            ChangeState<IdleState>(fsm);
            return;
        }
        //跳跃逻辑
        bodyTransform.Translate(Vector2.up * nowJumpSpeed * elapseSeconds);
        Move(elapseSeconds);
    }

    protected override void OnLeave(IFsm<RoleFsm> fsm, bool isShutdown)
    {
        base.OnLeave(fsm, isShutdown);
        GameEntry.Event.Unsubscribe(InputControlEventArgs.EventId, OnNotice);
    }

    /// <summary>
    /// 订阅事件后要触发的通知
    /// </summary>
    /// <param name="sender">事件源</param>
    /// <param name="args">事件参数</param>
    public void OnNotice(object sender, GameEventArgs args)
    {
        InputControlEventArgs eventArgs = args as InputControlEventArgs;
        switch (eventArgs.inputType)
        {
            case InputControlType.Atk1:
            case InputControlType.Atk2:
                roleFsm.SetData<VarSingle>("nowJumpSpeed", nowJumpSpeed);
                ChangeState<JumpAtkState>(roleFsm);
                break;
        }
    }
}