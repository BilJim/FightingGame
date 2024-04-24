using GameFramework.Fsm;
using UnityEngine;

/// <summary>
/// 攻击
/// </summary>
public class AIAttackState : AIBaseState
{

    private float minCDTime = 0.5f;
    private float maxCDTime = 3f;
    private float nowCDTime;
    private float atkRangeY = 0.15f;
    private float atkRangeX = 2.5f;
    
    protected override void OnEnter(IFsm<AIFsm> fsm)
    {
        base.OnEnter(fsm);
        nowCDTime = Random.Range(minCDTime, maxCDTime);
        CheckBodyDir();
    }

    protected override void OnUpdate(IFsm<AIFsm> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
        nowCDTime -= elapseSeconds;
        if (nowCDTime <= 0)
        {
            animator.SetTrigger("atkTrigger");
            nowCDTime = Random.Range(minCDTime, maxCDTime);
        }

        if (Mathf.Abs(monster.transform.position.y - player.transform.position.y) > atkRangeY)
        {
            ChangeAIState<AIPatrolState>();
            return;
        }

        if (monster.BodyIsRight() && (player.transform.position.x - monster.transform.position.x > atkRangeX || player.transform.position.x < monster.transform.position.x))
        {
            ChangeAIState<AIPatrolState>();
            return;
        }

        if (!monster.BodyIsRight() && (monster.transform.position.x - player.transform.position.x > atkRangeX || player.transform.position.x > monster.transform.position.x))
        {
            ChangeAIState<AIPatrolState>();
            return;
        }
    }

    private void CheckBodyDir()
    {
        if (monster.transform.position.x > player.transform.position.x)
            monster.roleSprite.flipX = true;
        else if (monster.transform.position.x < player.transform.position.x)
            monster.roleSprite.flipX = false;
    }
}