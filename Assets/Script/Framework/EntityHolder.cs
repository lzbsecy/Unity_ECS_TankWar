using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHolder : MonoBehaviour
{
    [System.NonSerialized]public Entity entity;
    public EntityHolder(Entity entity)
    {
        this.entity = entity;
    }
}
