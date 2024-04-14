using UnityEngine;

public abstract class EntityData
{
    //本项目暂无实际作用，
    //关于 EntityId 可用于的作用：
    //0 为无效
    //正值用于和服务器通信的实体（如玩家角色、NPC、怪等，服务器只产生正值）
    //负值用于本地生成的临时实体（如特效、FakeObject等）
    [SerializeField] private int m_Id = 0;

    //用于读取配置表中某一行的数据
    [SerializeField] private int m_TypeId = 0;

    [SerializeField] private Vector3 m_Position = Vector3.zero;

    [SerializeField] private Quaternion m_Rotation = Quaternion.identity;

    public EntityData(int entityId, int typeId)
    {
        m_Id = entityId;
        m_TypeId = typeId;
    }

    //实体编号
    public int Id => m_Id;

    //实体类型编号
    public int TypeId => m_TypeId;

    //实体位置
    public Vector3 Position
    {
        get => m_Position;
        set => m_Position = value;
    }

    //实体朝向
    public Quaternion Rotation
    {
        get => m_Rotation;
        set => m_Rotation = value;
    }
}