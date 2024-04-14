
using System;
using UnityEngine;

/// <summary>
/// 可作为目标的实体数据类
/// </summary>
[Serializable]
public abstract class TargetableObjectData : EntityData
{
    //阵营
    [SerializeField]
    private CampType m_Camp = CampType.Unknown;

    //当前生命
    [SerializeField]
    private int m_HP = 0;

    public TargetableObjectData(int entityId, int typeId, CampType camp) : base(entityId, typeId)
    {
        m_Camp = camp;
        m_HP = 0;
    }

    //角色阵营
    public CampType Camp => m_Camp;

    //当前生命
    public int HP
    {
        get => m_HP;
        set => m_HP = value;
    }

    //最大生命
    public abstract int MaxHP
    {
        get;
    }

    //生命百分比(0~1)
    public float HPRatio => MaxHP > 0 ? (float)HP / MaxHP : 0f;
}
