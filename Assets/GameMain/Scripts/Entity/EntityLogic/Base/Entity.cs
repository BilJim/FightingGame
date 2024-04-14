using System;
using UnityEngine;
using UnityGameFramework.Runtime;

public abstract class Entity : EntityLogic
{
    [SerializeField]
    private EntityData m_EntityData = null;
    
    public int Id => Entity.Id;
    
    //动画控制器
    public Animator Animator
    {
        get;
        private set;
    }

    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        //获取动画控制器，如果有的话
        Animator = GetComponent<Animator>();
    }

    /// <summary>
    /// 实体回收
    /// </summary>
    protected void OnRecycle()
    {
        base.OnRecycle();
    }

    protected override void OnShow(object userData)
    {
        base.OnShow(userData);
    }

    protected override void OnHide(bool isShutdown, object userData)
    {
        base.OnHide(isShutdown, userData);
    }
}