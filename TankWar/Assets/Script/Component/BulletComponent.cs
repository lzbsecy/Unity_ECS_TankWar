using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BulletComponent : ComponentBase
{
    public float damage; // = 10.0f
    public float shootDistance = 100f; // = 100.0f
    public float currentDistance = 0;
    public string buffName;
    public bool dead; //  = false
}
