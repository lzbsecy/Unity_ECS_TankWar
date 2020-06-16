using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class AiComponent : ComponentBase
{
    public int Dir; //移动方向
    public float timeCount; //计时
    public string lastMoveState; //上一次移动状态
    public string lastShootState; //上一次射击状态
    public float changeDirCd;  //改变方向CD
    public float shootCD;  //射击CD
    public bool finishShoot; //记录是否触发
}
