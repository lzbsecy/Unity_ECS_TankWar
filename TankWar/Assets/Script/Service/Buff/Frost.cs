using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frost : Buff
{
    public static float speedDown = 15.0f;
    public double timer = 0.0f;
    public double time = 20.0f;
    private MoveComponent moveComponent;
    public override void Enter(Entity entity)
    {
        Debug.Log("frozen buff enter");
        
        this.entity = entity;
        moveComponent = entity.moveComponent;
        moveComponent.moveSpeed -= speedDown;
        if (moveComponent.moveSpeed < 0)
        {
            moveComponent.moveSpeed = 0f;
        }
        
        Debug.Log("frozen tank, move speed down" + moveComponent.moveSpeed.ToString());
    }

    public override void Update()
    {
        timer += Time.deltaTime;
        if (timer >= time)
        {
            moveComponent.moveSpeed += speedDown;
            finish = true;
        }
    }
}
