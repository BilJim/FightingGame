using GameFramework.Fsm;
using UnityEngine;

/// <summary>
/// Idle 状态
/// </summary>
public class MonsterIdleState : MonsterBaseState
{
    //有限状态机的固定轮询调用逻辑
    protected override void OnUpdate(IFsm<MonsterRoleFsm> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
    }

    protected override void OnEnter(IFsm<MonsterRoleFsm> fsm)
    {
        base.OnEnter(fsm);
        
    }
}