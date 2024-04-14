public class PlayerData : TargetableObjectData
{
    public PlayerData(int entityId, int typeId, CampType camp) : base(entityId, typeId, camp)
    {
        HP = 100;
    }

    public override int MaxHP { get; }
}