using FightingGame;
using GameFramework.Fsm;
using UnityEngine;
using UnityGameFramework.Runtime;

/// <summary>
/// AI基础状态
/// </summary>
public class AIBaseState : FsmState<AIFsm>
{
    protected IFsm<AIFsm> aiFsm;
    protected MonsterEntity monster;
    protected DATAMonster monsterData;
    protected PlayerEntity player;
    
    //动画状态机
    protected Animator animator;
    //用于调整人物方向
    protected SpriteRenderer roleSprite;
    
    //创建有限状态机时调用
    protected override void OnInit(IFsm<AIFsm> fsm)
    {
        base.OnInit(fsm);
        aiFsm = fsm;
        player = (PlayerEntity)GameEntry.Entity.GetEntity(1000).Logic;
    }

    protected override void OnEnter(IFsm<AIFsm> fsm)
    {
        base.OnEnter(fsm);
        monster = (MonsterEntity)fsm.GetData<VarUnityObject>("monster");
        monsterData = fsm.GetData<VarDataRowBase>("monsterData").Value as DATAMonster;
        animator = (Animator)fsm.GetData<VarUnityObject>("animator").Value;
        roleSprite = (SpriteRenderer)fsm.GetData<VarUnityObject>("roleSprite").Value;
    }

    //有限状态机的固定轮询调用逻辑
    protected override void OnUpdate(IFsm<AIFsm> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
    }

    //离开有限状态机时调用
    protected override void OnLeave(IFsm<AIFsm> fsm, bool isShutdown)
    {
        base.OnLeave(fsm, isShutdown);
    }

    //销毁有限状态机时调用
    protected override void OnDestroy(IFsm<AIFsm> fsm)
    {
        base.OnDestroy(fsm);
        player = null;
    }

    protected void ChangeAIState<TState>() where TState : FsmState<AIFsm>
    {
        ChangeState<TState>(aiFsm);
    }
}