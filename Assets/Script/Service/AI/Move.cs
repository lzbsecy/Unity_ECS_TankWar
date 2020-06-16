using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class MoveAI : Singleton<MoveAI>
{
    private static Dictionary<int, string> stateToAction = new Dictionary<int, string>();

    public MoveAI()
    {
        stateToAction[1] = "move_up";
        stateToAction[2] = "move_left";
        stateToAction[3] = "move_down";
        stateToAction[4] = "move_right";
    }
    public static void moveAI(AiComponent aicomponent, InputComponent input)
    {
        aicomponent.timeCount += Time.deltaTime;
        if (aicomponent.timeCount % aicomponent.changeDirCd == 0) 
        {
            aicomponent.Dir = Random.Range(0, 4);
            if (aicomponent.lastMoveState != "") 
            {
                InputSystem.ReleaseAction(input, aicomponent.lastMoveState);
            }
            switch (aicomponent.Dir)
            {
                case 0:
                    InputSystem.PressAction(input, "move_up");
                    aicomponent.lastMoveState = "move_up";
                    break;
                case 1:
                    InputSystem.PressAction(input, "move_left");
                    aicomponent.lastMoveState = "move_left";
                    break;
                case 2:
                    InputSystem.PressAction(input, "move_down");
                    aicomponent.lastMoveState = "move_down";
                    break;
                case 3:
                    InputSystem.PressAction(input, "move_right");
                    aicomponent.lastMoveState = "move_right";
                    break;
                default:
                    break;
            }
        }        
    }
    
}
