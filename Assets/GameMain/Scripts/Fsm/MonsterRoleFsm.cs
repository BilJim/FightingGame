using GameFramework.Fsm;
using UnityGameFramework.Runtime;

/// <summary>
/// monster 状态机
/// </summary>
public class MonsterRoleFsm
{

    public IFsm<MonsterRoleFsm> m_Fsm = null;

    //角色状态机列表
    public FsmState<MonsterRoleFsm>[] roleStates =
    {
        new MonsterIdleState(), new MonsterMoveState(), 
    };
    

    /// <summary>
    /// 启动一个状态机
    /// </summary>
    public MonsterRoleFsm()
    {
        FsmComponent fsmComponent = GameEntry.Fsm;
        
        //创建有限状态机
        //状态机持有者: 当前实例
        //有哪些状态: roleStates[...]
        m_Fsm = fsmComponent.CreateFsm(this, roleStates);
    }

    /// <summary>
    /// 销毁一个状态机
    /// </summary>
    public void DestroyFsm()
    {
        FsmComponent fsmComponent = GameEntry.Fsm;
        fsmComponent.DestroyFsm(m_Fsm);
    }
}

