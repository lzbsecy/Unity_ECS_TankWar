using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemBase
{
    public GameWorld world;
    protected List<string> filter; // store component field names to filter entities

    public SystemBase(GameWorld world)
    {
        
        this.world = world;
    }

    
    

}
