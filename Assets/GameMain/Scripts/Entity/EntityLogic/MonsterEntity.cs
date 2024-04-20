using GameFramework.Event;

/// <summary>
/// 敌人逻辑实体
/// </summary>
public class MonsterEntity : RoleEntity
{
    protected override void OnShow(object userData)
    {
        base.OnShow(userData);
        roleFsm.m_Fsm.SetData<VarEntityData>("monsterData", GetData<MonsterData>());
        //状态机初始状态为 IdleState
        roleFsm.m_Fsm.Start<MonsterIdleState>();
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