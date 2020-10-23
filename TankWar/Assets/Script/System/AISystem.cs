using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AISystem : SystemBase
{
    public AISystem(GameWorld world) : base(world)
    {

    }

    public void Update(Identity identity, AiComponent aicompont, InputComponent input)
    {
        if(aicompont == null || aicompont.enable == false)
        {
            return;
        }
       
        if(identity.isAIControl == true)
        {
            if(identity.tag == "tank")
            {
                MoveAI.moveAI(aicompont, input);
            }
            else if(identity.tag == "gun")
            {
                ShootAI.shootAI(aicompont, input);
            }            
        }
  
    }
}
