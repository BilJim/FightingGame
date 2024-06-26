using GameFramework.Event;
using GameFramework.Fsm;
using UnityEngine;
using UnityGameFramework.Runtime;

/// <summary>
/// Jump 状态
/// </summary>
public class PlayerJumpState : PlayerBaseState
{

    protected float nowJumpSpeed;
    //身体对象，用于 y轴 模拟跳跃
    private Transform bodyTransform;

    //进入有限状态机时调用
    protected override void OnEnter(IFsm<PlayerRoleFsm> fsm)
    {
        base.OnEnter(fsm);
        bodyTransform = player.Find("Role");
        if (fsm.CurrentState.GetType() == typeof(PlayerJumpAtkState))
            return;
        animator.SetBool("isGround", false);
        animator.SetTrigger("jumpTrigger");
        nowJumpSpeed = playerData.jumpSpeed;
        //订阅监听事件
        GameEntry.Event.Subscribe(InputControlEventArgs.EventId, OnNotice);
    }

    //有限状态机的固定轮询调用逻辑
    protected override void OnUpdate(IFsm<PlayerRoleFsm> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
        //跳跃逻辑
        bodyTransform.Translate(Vector2.up * nowJumpSpeed * elapseSeconds);
        //重力影响 v = v - gt;
        nowJumpSpeed -= playerData.gSpeed * elapseSeconds;
        if (bodyTransform.localPosition.y <= 0)
        {
            animator.SetBool("isGround", true);
            bodyTransform.localPosition = Vector2.zero;
            ChangeState<PlayerIdleState>(fsm);
            return;
        }
        Move(elapseSeconds);
    }

    protected override void OnLeave(IFsm<PlayerRoleFsm> fsm, bool isShutdown)
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
                ChangeState<PlayerJumpAtkState>(roleFsm);
                break;
        }
    }
}