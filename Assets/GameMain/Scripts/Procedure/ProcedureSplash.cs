using GameFramework.Procedure;
using GameFramework.Resource;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

/// <summary>
/// 游戏启动流程 - 尚未进入游戏界面的流程
/// </summary>
public class ProcedureSplash : ProcedureBase
{
    protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

        // TODO: 这里可以播放一个 Splash 动画
        // ...

        if (GameEntry.Base.EditorResourceMode)
        {
            // 编辑器模式 直接加载项目内资源
            Log.Info("Editor resource mode detected.");
            // ChangeState<ProcedurePreload>(procedureOwner);
            //暂时先直接加载游戏场景
            procedureOwner.SetData<VarString>("NextSceneName", "Game");
            ChangeState<ProcedureChangeScene>(procedureOwner);
        }
        // else if (GameEntry.Resource.ResourceMode == ResourceMode.Package)
        // {
        //     // 单机模式。要加载的AB包资源在 StreamingAssets 下
        //     Log.Info("Package resource mode detected.");
        //     ChangeState<ProcedureInitResources>(procedureOwner);
        // }
        // else
        // {
        //     // 可更新模式
        //     Log.Info("Updatable resource mode detected.");
        //     ChangeState<ProcedureCheckVersion>(procedureOwner);
        // }
    }
}
