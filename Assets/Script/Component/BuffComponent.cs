using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BuffComponent : ComponentBase
{
    public List<Buff> buffs;
    public BuffComponent()
    {
        buffs = new List<Buff>();
    }
}
