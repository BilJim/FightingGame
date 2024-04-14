using System.Collections.Generic;
using GameFramework.Procedure;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

/// <summary>
/// 开始游戏后的主流程
/// </summary>
public class ProcedureMain : ProcedureBase
{

    private GameBase m_CurrentGame = null;
    private bool m_GotoMenu = false;
    //返回菜单的延迟时间
    private float m_GotoMenuDelaySeconds = 0f;
    //游戏中切换场景的延迟时间
    private const float GameOverDelayedSeconds = 2f;

    //返回菜单
    public void GotoMenu()
    {
        m_GotoMenu = true;
    }
    
    protected override void OnInit(ProcedureOwner procedureOwner)
    {
        base.OnInit(procedureOwner);
        //目前只有普通模式
        m_CurrentGame = new CommonGame();
    }
    
    protected override void OnDestroy(ProcedureOwner procedureOwner)
    {
        base.OnDestroy(procedureOwner);
        m_CurrentGame = null;
    }
    
    protected override void OnEnter(ProcedureOwner procedureOwner)
    {
        base.OnEnter(procedureOwner);
        m_GotoMenu = false;
        m_CurrentGame.Initialize();
    }

    protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
    {
        if (m_CurrentGame != null)
        {
            m_CurrentGame.Shutdown();
            m_CurrentGame = null;
        }

        base.OnLeave(procedureOwner, isShutdown);
    }
    
    protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

        if (m_CurrentGame != null && !m_CurrentGame.GameOver)
        {
            m_CurrentGame.Update(elapseSeconds, realElapseSeconds);
            return;
        }

        if (!m_GotoMenu)
        {
            m_GotoMenu = true;
            m_GotoMenuDelaySeconds = 0;
        }

        m_GotoMenuDelaySeconds += elapseSeconds;
        if (m_GotoMenuDelaySeconds >= GameOverDelayedSeconds)
        {
            //切换场景
            procedureOwner.SetData<VarString>("NextSceneName", "Menu");
            ChangeState<ProcedureChangeScene>(procedureOwner);
        }
    }
}