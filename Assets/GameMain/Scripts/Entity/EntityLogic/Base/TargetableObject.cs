using UnityEngine;
using UnityGameFramework.Runtime;

/// <summary>
/// 可作为目标的实体类
/// </summary>
public abstract class TargetableObject : Entity
{
    [SerializeField]
    private TargetableObjectData m_TargetableObjectData = null;
    
    //死亡判断
    public bool IsDead => m_TargetableObjectData.HP <= 0;

    public T GetData<T>() where T : TargetableObjectData
    {
        return m_TargetableObjectData as T;
    }
    
    //受到伤害
    public void ApplyDamage(Entity attacker, int damageHP)
    {
        m_TargetableObjectData.HP -= damageHP;

        if (m_TargetableObjectData.HP <= 0)
        {
            OnDead(attacker);
        }
    }

    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
    }

    protected override void OnShow(object userData)
    {
        base.OnShow(userData);
        m_TargetableObjectData = userData as TargetableObjectData;
        if (m_TargetableObjectData == null)
        {
            Log.Error("Targetable object data is invalid.");
            return;
        }
    }
    
    //角色死亡
    protected virtual void OnDead(Entity attacker)
    {
        GameEntry.Entity.HideEntity(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        Entity entity = other.gameObject.GetComponent<Entity>();
        if (entity == null)
            return;

        if (entity is TargetableObject && entity.Id >= Id)
            // 碰撞事件由 Id 小的一方处理，避免重复处理
            return;
        //todo 碰撞处理
    }
}