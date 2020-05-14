using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using System;

[GenerateAuthoringComponent]
public struct MoveSpeed : IComponentData
{
    public float moveSpeed;
}
