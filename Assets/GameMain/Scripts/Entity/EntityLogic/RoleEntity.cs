using UnityEngine;

/// <summary>
/// 角色对象基类
/// </summary>
public abstract class RoleEntity : TargetableObject
{
    public SpriteRenderer roleSprite;

    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        roleSprite = transform.Find("Role").GetComponentInChildren<SpriteRenderer>();
        Animator = transform.Find("Role").GetComponentInChildren<Animator>();
    }


    protected override void OnShow(object userData)
    {
        base.OnShow(userData);
    }

    protected override void OnHide(bool isShutdown, object userData)
    {
        base.OnHide(isShutdown, userData);
    }

    protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(elapseSeconds, realElapseSeconds);
    }

    /// <summary>
    /// 判断朝向是否是右边
    /// </summary>
    public bool BodyIsRight()
    {
        return !roleSprite.flipX;
    }
}