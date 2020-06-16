using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MoveSystem : SystemBase
{
    public MoveSystem(GameWorld world) : base(world)
    {

    }

    public void Update(MoveComponent move, GameobjectComponent gameobjectComponent)
    {
        if(move == null || move.enable == false)
        {
            return;
        }
        Transform transform = gameobjectComponent.transform;
        BoxCollider2D collider = gameobjectComponent.collider;
        if (move.needMove == true)
        {
            Vector3 nextPosition = transform.position + transform.up * move.moveSpeed * Time.fixedDeltaTime;
            if (move.collisionDetection == true)
            {
                collider.enabled = false;
                Collider2D otherCollider = Physics2D.OverlapBox(nextPosition, collider.size, 0);
                if (otherCollider != null && !otherCollider.tag.Equals("tree"))
                {
                    nextPosition = transform.position;
                }
            }
            transform.position = nextPosition;
            
            if(collider.enabled == false)
            {
                collider.enabled = true;
            }

        }
    }    
}
