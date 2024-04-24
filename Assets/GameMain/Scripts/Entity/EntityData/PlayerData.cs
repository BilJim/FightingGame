using System;
using UnityEngine;

[Serializable]
public class PlayerData : TargetableObjectData
{
    public PlayerData(int entityId, int typeId, CampType camp) : base(entityId, typeId, camp)
    {
        HP = 100;
    }

    public override int MaxHP { get; }
    
    //移动速度
    public float moveSpeed = 8;
    //移动方向
    public Vector2 moveDir = Vector2.zero;
    //跳跃速度
    public float jumpSpeed = 10;
    //重力加速度
    public float gSpeed = 50f;
}