using GameFramework.Fsm;
using UnityGameFramework.Runtime;

/// <summary>
/// AI状态机
/// </summary>
public class AIFsm
{
    public IFsm<AIFsm> m_Fsm = null;
    
    
    //AI状态机列表
    public FsmState<AIFsm>[] roleStates =
    {
        new AIPatrolState(), new AIMoveState(),
        new AIAttackState(), new AIBackState()
    };

    public AIFsm()
    {
        FsmComponent fsmComponent = GameEntry.Fsm;
        m_Fsm = fsmComponent.CreateFsm(this, roleStates);
    }

    public void DestroyFsm()
    {
        FsmComponent fsmComponent = GameEntry.Fsm;
        fsmComponent.DestroyFsm(m_Fsm);
    }
}