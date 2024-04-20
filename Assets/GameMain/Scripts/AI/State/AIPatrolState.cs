using GameFramework.Fsm;
using UnityEngine;
using UnityGameFramework.Runtime;

/// <summary>
/// 巡逻
/// </summary>
public class AIPatrolState : AIBaseState
{
    //移动范围
    private int rangeW = 2;
    private int rangeH = 2;

    //目标点
    private Vector3 targetPos;

    //是否到达
    private bool isArrive = true;
    private bool isWait = false;
    private float waitTime = 1f;
    private float nowWaitTime = 0;

    protected override void OnEnter(IFsm<AIFsm> fsm)
    {
        base.OnEnter(fsm);
    }

    protected override void OnUpdate(IFsm<AIFsm> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
        if (isWait)
        {
            nowWaitTime += elapseSeconds;
            Log.Info($"WaitTime: {nowWaitTime}");
            if (nowWaitTime >= waitTime)
            {
                isWait = false;
                nowWaitTime = 0;
            }
            return;
        }
        if (isArrive)
        {
            GetTargetPos();
            isArrive = false;
        }
        MonsterBaseState monsterState = monster.roleFsm.m_Fsm.CurrentState as MonsterBaseState;
        monsterState.Move(elapseSeconds, targetPos - monster.transform.position);
        //到达目标点位置
        if (Vector3.Distance(monster.transform.position, targetPos) < 0.5f)
        {
            isArrive = true;
            isWait = true;
            monsterState.Idle();
        }
    }

    //获取要前往的巡逻目标点
    private void GetTargetPos()
    {
        Vector2 bornPos = monster.GetData<MonsterData>().bornPos;
        targetPos.x = Random.Range(bornPos.x - rangeW, bornPos.x + rangeW);
        targetPos.y = Random.Range(bornPos.y - rangeH, bornPos.y + rangeH);
    }
}