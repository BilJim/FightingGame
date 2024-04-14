using System;
using GameFramework.Localization;
using GameFramework.Procedure;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

/// <summary>
/// 流程启动器，最先启动的流程
/// </summary>
public class ProcedureLaunch : ProcedureBase
{
    protected override void OnEnter(ProcedureOwner procedureOwner)
    {
        base.OnEnter(procedureOwner);

        //todo 构建信息：发布版本时，把一些数据以 Json 的格式写入 Assets/GameMain/Configs/BuildInfo.txt，供游戏逻辑读取
        //GameEntry.BuiltinData.InitBuildInfo();

        //todo 语言配置：设置当前使用的语言，如果不设置，则默认使用操作系统语言
        InitLanguageSettings();

        //todo 变体配置：根据使用的语言，通知底层加载对应的资源变体
        InitCurrentVariant();

        //todo 声音配置：根据用户配置数据，设置即将使用的声音选项
        InitSoundSettings();

        //todo 默认字典：加载默认字典文件 Assets/GameMain/Configs/DefaultDictionary.xml
        // 此字典文件记录了资源更新前使用的各种语言的字符串，会随 App 一起发布，故不可更新
        //GameEntry.BuiltinData.InitDefaultDictionary();
    }

    protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

        // 运行一帧即切换到 Splash 展示流程
        ChangeState<ProcedureSplash>(procedureOwner);
    }

    private void InitLanguageSettings()
    {
    }

    private void InitCurrentVariant()
    {
    }

    private void InitSoundSettings()
    {
    }
}