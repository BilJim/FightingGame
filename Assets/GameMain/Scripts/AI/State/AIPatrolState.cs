using System.Collections.Generic;
using GameFramework.Fsm;
using UnityEngine;
using UnityGameFramework.Runtime;

/// <summary>
/// 巡逻
/// </summary>
public class AIPatrolState : AIBaseState
{
    //巡逻目标点
    private List<Vector2> targetPosList = new();
    private int nowTargetPosIndex;
    private bool isAdd = true;
    //目标点
    private Vector2 targetPos;

    
    protected override void OnEnter(IFsm<AIFsm> fsm)
    {
        base.OnEnter(fsm);
        targetPosList = monsterData.PatrolPosList;
    }

    protected override void OnUpdate(IFsm<AIFsm> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
        //计算巡逻点位
        GetTargetPos();
        fsm.SetData<VarVector2>("targetPos", targetPos);
        ChangeAIState<AIMoveState>();
    }

    //获取要前往的巡逻目标点
    private void GetTargetPos()
    {
        targetPos = targetPosList[nowTargetPosIndex];
        nowTargetPosIndex = isAdd ? nowTargetPosIndex + 1 : nowTargetPosIndex - 1;
        if (nowTargetPosIndex == targetPosList.Count - 1)
            isAdd = false;
        else if (nowTargetPosIndex == 0)
            isAdd = true;
    }
}