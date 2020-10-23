using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsUtil
{
    public static Collider2D OverlapBox(Vector3 position, BoxCollider2D collider, float angle)
    {
        collider.enabled = false;
        Collider2D retCollider = Physics2D.OverlapBox(position, collider.size, angle);
        collider.enabled = true;

        return retCollider;
    }
}
