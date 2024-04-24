using System.Collections;
using GameFramework.Fsm;
using UnityEngine;
using UnityGameFramework.Runtime;

/// <summary>
/// 移动
/// </summary>
public class AIMoveState : AIBaseState
{
    public Vector2 targetPos;

    public Vector2 curTargetPos;

    //是否是追逐状态
    private bool isChasing;

    //到达目标点后的等待状态
    private bool isWait;
    private float nowWaitTime = 0;

    protected override void OnEnter(IFsm<AIFsm> fsm)
    {
        base.OnEnter(fsm);
        nowWaitTime = 0;
        isWait = false;
        GameEntry.Coroutine.StartCoroutine(MoveWaitTime(monsterData.MoveWaitTime));
        targetPos = fsm.GetData<VarVector2>("targetPos");
        curTargetPos = targetPos;
    }

    protected override void OnUpdate(IFsm<AIFsm> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
        //接近玩家
        if (Vector3.Distance(monster.transform.position, player.transform.position) <= monsterData.AtkRange)
            //靠近玩家后追击玩家
            isChasing = true;
        else
            isChasing = false;

        if (isChasing)
        {
            //敌人在玩家右边
            if (monster.transform.position.x >= player.transform.position.x)
                curTargetPos = player.transform.position + Vector3.right * 0.8f;
            else
                curTargetPos = player.transform.position + Vector3.left * 0.8f;
        }
        else
        {
            curTargetPos = targetPos;
            if (isWait)
            {
                nowWaitTime += elapseSeconds;
                if (nowWaitTime >= monsterData.MoveWaitTime)
                    //切换巡逻状态，重新计算下一个点位
                    ChangeAIState<AIPatrolState>();
                return;
            }
        }

        Move(elapseSeconds, curTargetPos - (Vector2)monster.transform.position);
        //到达目标点位置
        if (Vector3.Distance(monster.transform.position, curTargetPos) < 0.15f)
        {
            if (isChasing)
            {
                ChangeAIState<AIAttackState>();
                return;
            }

            isWait = true;
            ChangeAIState<AIPatrolState>();
        }
    }

    protected override void OnLeave(IFsm<AIFsm> fsm, bool isShutdown)
    {
        base.OnLeave(fsm, isShutdown);
        animator.SetBool("isMoving", false);
    }

    public void Move(float elapseSeconds, Vector2 movePos)
    {
        if (movePos == Vector2.zero)
        {
            ChangeAIState<AIPatrolState>();
            return;
        }
        //移动
        monster.transform.Translate(movePos.normalized * monsterData.MoveSpeed * elapseSeconds);
        //转向
        if (movePos.x > 0)
            roleSprite.flipX = false;
        else if (movePos.x < 0)
            roleSprite.flipX = true;
    }

    private IEnumerator MoveWaitTime(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        isWait = false;
        animator.SetBool("isMoving", true);
    }
}