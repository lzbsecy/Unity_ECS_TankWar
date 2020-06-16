using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Identity : ComponentBase
{
    public int camp;
    public bool isAIControl = false;
    public bool isDead;
    public string tag;
    [System.NonSerialized] public Entity master;
}
