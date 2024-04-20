using GameFramework.Fsm;
using UnityGameFramework.Runtime;

/// <summary>
/// AI基础状态
/// </summary>
public class AIBaseState : FsmState<AIFsm>
{
    protected IFsm<AIFsm> aiFsm;
    protected MonsterEntity monster; 
    
    //创建有限状态机时调用
    protected override void OnInit(IFsm<AIFsm> fsm)
    {
        base.OnInit(fsm);
        aiFsm = fsm;
    }

    protected override void OnEnter(IFsm<AIFsm> fsm)
    {
        base.OnEnter(fsm);
        monster = (MonsterEntity)fsm.GetData<VarUnityObject>("monster");
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
    }
}