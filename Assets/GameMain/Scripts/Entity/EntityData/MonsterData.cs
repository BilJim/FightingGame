using System;
using UnityEngine;

[Serializable]
public class MonsterData : TargetableObjectData
{
    public MonsterData(int entityId, int typeId, CampType camp) : base(entityId, typeId, camp)
    {
        HP = 100;
    }

    public override int MaxHP { get; }
    
    //移动速度
    public float moveSpeed = 3;
    //跳跃速度
    public float jumpSpeed = 10;
    //重力加速度
    public float gSpeed = 50f;

    //出生点
    public Vector2 bornPos;
    //移动方向
    public Vector2 movePos;
}