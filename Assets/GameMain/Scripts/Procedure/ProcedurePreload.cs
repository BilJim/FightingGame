using System.Collections.Generic;
using GameFramework.Event;
using GameFramework.Procedure;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

public class ProcedurePreload : ProcedureBase
{
    /// <summary>
    /// 要加载的数据表资源名
    /// </summary>
    public static readonly string[] DataTableNames = new string[]
    {
        "Monster",
    };

    //加载情况标记
    private Dictionary<string, bool> m_LoadedFlag = new Dictionary<string, bool>();

    protected override void OnEnter(ProcedureOwner procedureOwner)
    {
        base.OnEnter(procedureOwner);
        GameEntry.Event.Subscribe(LoadDataTableSuccessEventArgs.EventId, OnLoadDataTableSuccess);
        GameEntry.Event.Subscribe(LoadDataTableFailureEventArgs.EventId, OnLoadDataTableFailure);

        m_LoadedFlag.Clear();

        PreloadResources();
    }

    protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
    {
        GameEntry.Event.Unsubscribe(LoadDataTableSuccessEventArgs.EventId, OnLoadDataTableSuccess);
        GameEntry.Event.Unsubscribe(LoadDataTableFailureEventArgs.EventId, OnLoadDataTableFailure);

        base.OnLeave(procedureOwner, isShutdown);
    }

    protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

        foreach (KeyValuePair<string, bool> loadedFlag in m_LoadedFlag)
        {
            if (!loadedFlag.Value)
            {
                return;
            }
        }

        //暂时先直接加载游戏场景
        procedureOwner.SetData<VarString>("NextSceneName", "Game");
        ChangeState<ProcedureChangeScene>(procedureOwner);
    }

    private void PreloadResources()
    {
        // Preload data tables
        foreach (string dataTableName in DataTableNames)
        {
            LoadDataTable(dataTableName);
        }
    }

    private void LoadDataTable(string dataTableName)
    {
        //从二进制文件中加载数据
        string dataTableAssetName = AssetUtility.GetDataTableAsset(dataTableName, true);
        m_LoadedFlag.Add(dataTableAssetName, false);
        GameEntry.DataTable.LoadDataTable(dataTableName, dataTableAssetName, this);
    }

    private void OnLoadDataTableSuccess(object sender, GameEventArgs e)
    {
        LoadDataTableSuccessEventArgs ne = (LoadDataTableSuccessEventArgs)e;
        if (ne.UserData != this)
        {
            return;
        }

        m_LoadedFlag[ne.DataTableAssetName] = true;
        Log.Info("Load data table '{0}' OK.", ne.DataTableAssetName);
    }

    private void OnLoadDataTableFailure(object sender, GameEventArgs e)
    {
        LoadDataTableFailureEventArgs ne = (LoadDataTableFailureEventArgs)e;
        if (ne.UserData != this)
        {
            return;
        }

        Log.Error("Can not load data table '{0}' from '{1}' with error message '{2}'.", ne.DataTableAssetName,
            ne.DataTableAssetName, ne.ErrorMessage);
    }
}