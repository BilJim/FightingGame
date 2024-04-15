using GameFramework.Fsm;
using UnityGameFramework.Runtime;

/// <summary>
/// 角色状态机
/// 初始化时
/// </summary>
public class RoleFsm
{

    public IFsm<RoleFsm> m_Fsm = null;

    //角色状态机列表
    public FsmState<RoleFsm>[] roleStates =
    {
        new IdleState(), new MoveState(), 
        new JumpState(), new JumpAtkState(),
        new AtkOneState(), new AtkTwoState(), new AtkThreeState(),
        new FootAtkOneState(), new FootAtkTwoState(),
        new HitState(), new HitFlyState(),
        new ThrowState(), new PickUpState(), new DefendState()
    };
    

    /// <summary>
    /// 启动一个状态机
    /// </summary>
    public RoleFsm()
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

