using FightingGame;
using UnityGameFramework.Runtime;

/// <summary>
/// 敌人逻辑实体
/// </summary>
public class MonsterEntity : RoleEntity
{
    //AI行为状态机
    public AIFsm aiFsm;

    public DATAMonster monsterData;

    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        monsterData = (userData as MonsterData).Data;

        aiFsm = new AIFsm();
        aiFsm.m_Fsm.SetData<VarUnityObject>("monster", this);
        aiFsm.m_Fsm.SetData<VarDataRowBase>("monsterData", monsterData);
        aiFsm.m_Fsm.SetData<VarUnityObject>("roleSprite", roleSprite);
        aiFsm.m_Fsm.SetData<VarUnityObject>("animator", Animator);
        aiFsm.m_Fsm.Start<AIPatrolState>();
    }

    protected override void OnDead(Entity attacker)
    {
        base.OnDead(attacker);
        aiFsm.DestroyFsm();
    }
}