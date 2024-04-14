public class CommonGame : GameBase
{
    private float m_ElapseSeconds = 0f;

    public override GameMode GameMode => GameMode.Common;

    public override void Update(float elapseSeconds, float realElapseSeconds)
    {
        base.Update(elapseSeconds, realElapseSeconds);

        m_ElapseSeconds += elapseSeconds;
        
    }
}
