using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using System;

public class MoveSystem : ComponentSystem
{
    protected override void OnCreate()
    {
        base.OnCreate();
    }

    protected override void OnUpdate()
    {

        Entities.ForEach((ref MoveSpeed moveSpeed, ref Translation translation ) =>
        {
            var deltaTime = Time.DeltaTime;
            translation.Value.x += moveSpeed.moveSpeed * deltaTime;
        });
    }

    
}
