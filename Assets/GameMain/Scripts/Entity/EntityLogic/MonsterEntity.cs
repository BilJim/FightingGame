using UnityGameFramework.Runtime;

/// <summary>
/// 敌人逻辑实体
/// </summary>
public class MonsterEntity : RoleEntity
{
    public MonsterRoleFsm roleFsm;
    public AIFsm aiFsm;

    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        //创建角色状态机
        roleFsm = new MonsterRoleFsm();
        roleFsm.m_Fsm.SetData<VarUnityObject>("monster", transform);
        roleFsm.m_Fsm.SetData<VarEntityData>("monsterData", userData as MonsterData);
        roleFsm.m_Fsm.SetData<VarUnityObject>("roleSprite", roleSprite);
        roleFsm.m_Fsm.SetData<VarUnityObject>("animator", Animator);
        //状态机初始状态为 IdleState
        roleFsm.m_Fsm.Start<MonsterIdleState>();
        
        aiFsm = new AIFsm();
        aiFsm.m_Fsm.SetData<VarUnityObject>("monster", this);
        aiFsm.m_Fsm.Start<AIPatrolState>();
    }

    protected override void OnDead(Entity attacker)
    {
        base.OnDead(attacker);
        roleFsm.DestroyFsm();
        aiFsm.DestroyFsm();
    }
}