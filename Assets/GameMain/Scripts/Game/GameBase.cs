using GameFramework.Event;
using UnityGameFramework.Runtime;

public abstract class GameBase
{
    //获取当前游戏模式
    public abstract GameMode GameMode { get; }
    public bool GameOver { get; protected set; }
    
    private PlayerEntity player = null;
    
    public virtual void Initialize()
    {
        //注册实体显示相关
        GameEntry.Event.Subscribe(ShowEntitySuccessEventArgs.EventId, OnShowEntitySuccess);
        GameEntry.Event.Subscribe(ShowEntityFailureEventArgs.EventId, OnShowEntityFailure);

        PlayerData playerData = new PlayerData(0, 0, CampType.Player);
        MonsterData monsterData = new MonsterData(0, 1, CampType.Enemy);
        GameEntry.Entity.ShowEntity<PlayerEntity>(1000, AssetUtility.GetEntityAsset("Player"), "Player", Constant.AssetPriority.PlayerAsset, playerData);
        GameEntry.Entity.ShowEntity<MonsterEntity>(1001, AssetUtility.GetEntityAsset("Monster"), "Enemy", Constant.AssetPriority.MonsterAsset, monsterData);
        GameOver = false;
    }

    public virtual void Shutdown()
    {
        GameEntry.Event.Unsubscribe(ShowEntitySuccessEventArgs.EventId, OnShowEntitySuccess);
        GameEntry.Event.Unsubscribe(ShowEntityFailureEventArgs.EventId, OnShowEntityFailure);
    }

    public virtual void Update(float elapseSeconds, float realElapseSeconds)
    {
        if (player != null && player.IsDead)
        {
            GameOver = true;
            return;
        }
    }

    protected virtual void OnShowEntitySuccess(object sender, GameEventArgs e)
    {
        ShowEntitySuccessEventArgs ne = (ShowEntitySuccessEventArgs)e;
        if (ne.EntityLogicType == typeof(PlayerEntity))
        {
            player = (PlayerEntity)ne.Entity.Logic;
        }
    }

    protected virtual void OnShowEntityFailure(object sender, GameEventArgs e)
    {
        ShowEntityFailureEventArgs ne = (ShowEntityFailureEventArgs)e;
        Log.Warning("Show entity failure with error message '{0}'.", ne.ErrorMessage);
    }
}