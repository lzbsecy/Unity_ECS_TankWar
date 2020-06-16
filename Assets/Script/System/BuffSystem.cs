using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffSystem : SystemBase
{
    public BuffSystem(GameWorld world) : base(world)
    {
    }


    public void Update(BuffComponent buffComponent)
    {
        if(buffComponent == null || buffComponent.enable == false)
        {
            return;
        }

        foreach (Buff buff in buffComponent.buffs)
        {
            if (!buff.finish)
            {
                buff.Update();
            }
        }
    }

    public void AddBuff(Entity entity, string buffName)
    {
        if (buffName == "Frozen")
        {
            Frost frost = new Frost();
            frost.Enter(entity);
            entity.buffComponent.buffs.Add(frost);
        }
    }
}
