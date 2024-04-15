using GameFramework.Fsm;
using UnityEngine;
using UnityGameFramework.Runtime;

/// <summary>
/// 站立状态
/// </summary>
public class JumpState : FsmState<RoleFsm>
{
    //创建有限状态机时调用
    protected override void OnInit(IFsm<RoleFsm> fsm)
    {
        base.OnInit(fsm);
        Log.Info("创建站立状态");
    }

    //进入有限状态机时调用
    protected override void OnEnter(IFsm<RoleFsm> fsm)
    {
        base.OnEnter(fsm);
        Log.Info("进入站立状态");
    }

    //有限状态机的固定轮询调用逻辑
    protected override void OnUpdate(IFsm<RoleFsm> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
        Log.Info("轮询站立状态");
        
        //按一下 W 切换到 MoveState
        if (Input.GetKeyDown(KeyCode.W))
            //把这个状态机下的状态调整为 MoveState
            ChangeState<MoveState>(fsm);
    }

    //离开有限状态机时调用
    protected override void OnLeave(IFsm<RoleFsm> fsm, bool isShutdown)
    {
        base.OnLeave(fsm, isShutdown);
        Log.Info("离开站立状态");
    }

    //销毁有限状态机时调用
    protected override void OnDestroy(IFsm<RoleFsm> fsm)
    {
        base.OnDestroy(fsm);
        Log.Info("销毁站立状态");
    }
}