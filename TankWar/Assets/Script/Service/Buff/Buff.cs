using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff
{
    protected Entity entity;
    public bool finish = false;
    public virtual void Enter(Entity entity) {

    }

    public virtual void Update() { }
}
