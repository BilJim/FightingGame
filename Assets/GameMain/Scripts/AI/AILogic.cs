using GameFramework.Fsm;

public class AILogic
{
    //AI逻辑 控制的怪物对象
    public MonsterEntity monster;

    //当前 AI 状态
    public E_AI_STATE nowAiState;

    public AILogic(MonsterEntity monster)
    {
        this.monster = monster;
    }
}