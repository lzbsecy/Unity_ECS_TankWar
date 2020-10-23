using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class AiComponent : ComponentBase
{
    public int Dir; //移动方向，boss逻辑里表示攻击对象所在方位
    public int EnemyDir; //攻击对象的方位
    public float timeCount; //计时
    public float timeCountShoot; //记录cd
    public int lastDir; //上一次的方向
    public int count; //计数
    public int StateChangeCount; //记录状态变化次数
    public string lastMoveAction = ""; //上一次移动状态
    public string lastShootAction = ""; //上一次射击状态
    public string lastShootAction1 = ""; //上一次射击状态
    public int ShootStateCount; //状态计数
    public float changeDirCd = 2.0f;  //改变方向CD
    public float shootCD = 0.8f;  //射击CD
    public bool finishMove=false; //记录动作是否结束
    public bool finishShoot=false; //记录动作是否结束
    public int MoveState=0; //状态机
    public int AIId; //标识ai种类
    public Collider2D[] collider; //视野目标
    public Vector2 stadia=new Vector2 (300f,300f); //视距
    public GameObject AttackableObject; //攻击对象
    public AiComponent bodyworkAIcomponent;//基座
    public Vector2 EnemyPosition;
    public Vector2 PositionSelf;
}
