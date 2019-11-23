using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffComponent : MonoBehaviour
{
    private List<Buff> buffs = new List<Buff>();

    void Start()
    {
    }

    void Update()
    {
        foreach (var buff in buffs)
        {
            buff.Update();
        }
    }

    public void AddBuff(string buffName){
        Buff buff = NewBuff(buffName);
        buff.Enter(this.gameObject);
        buffs.Add(buff);
    }

    Buff NewBuff(string buffName){
        if (buffName == "Frozen")
        {
            return new Frozen();
        }
        return null;
    }
}
