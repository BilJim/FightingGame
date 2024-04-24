using System;
using FightingGame;
using GameFramework.DataTable;

[Serializable]
public class MonsterData : TargetableObjectData
{
    public MonsterData(int entityId, int typeId, CampType camp) : base(entityId, typeId, camp)
    {
        HP = 100;


        IDataTable<DATAMonster> dtMonster = GameEntry.DataTable.GetDataTable<DATAMonster>();
        Data = dtMonster.GetDataRow(typeId);
    }

    public override int MaxHP => Data.MaxHP;

    public DATAMonster Data { get; private set; }
}