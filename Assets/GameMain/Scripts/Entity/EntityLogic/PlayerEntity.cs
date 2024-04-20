using GameFramework.Event;
using UnityGameFramework.Runtime;

/// <summary>
/// 玩家逻辑实体
/// </summary>
public class PlayerEntity : RoleEntity
{
    protected PlayerRoleFsm roleFsm;

    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        roleFsm = new PlayerRoleFsm();
        roleFsm.m_Fsm.SetData<VarUnityObject>("player", transform);
        roleFsm.m_Fsm.SetData<VarEntityData>("playerData", userData as PlayerData);
        roleFsm.m_Fsm.SetData<VarUnityObject>("roleSprite", roleSprite);
        roleFsm.m_Fsm.SetData<VarUnityObject>("animator", Animator);
        //状态机初始状态为 IdleState
        roleFsm.m_Fsm.Start<PlayerIdleState>();
    }

    protected override void OnShow(object userData)
    {
        base.OnShow(userData);
        //订阅监听事件
        GameEntry.Event.Subscribe(InputControlEventArgs.EventId, OnNotice);
    }

    protected override void OnDead(Entity attacker)
    {
        base.OnDead(attacker);
        GameEntry.Event.Unsubscribe(InputControlEventArgs.EventId, OnNotice);
        roleFsm.DestroyFsm();
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
            case InputControlType.Horizontal:
                GetData<PlayerData>().moveDir.x = eventArgs.keyValue;
                break;
            case InputControlType.Vertical:
                GetData<PlayerData>().moveDir.y = eventArgs.keyValue;
                break;
        }
    }
}