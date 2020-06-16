using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ShootAI : Singleton<ShootAI>
{
    private static Dictionary<int, string> stateToAction = new Dictionary<int, string>();

    public ShootAI()
    {
        stateToAction[1] = "rnf_up";
        stateToAction[2] = "rnf_left";
        stateToAction[3] = "rnf_down";
        stateToAction[4] = "rnf_right";
    }
    public static void shootAI(AiComponent aicomponent, InputComponent input)
    {
        if (aicomponent.finishShoot)
        {
            InputSystem.ReleaseAction(input, aicomponent.lastShootState);
            aicomponent.finishShoot = false;
        }
        if (aicomponent.timeCount % aicomponent.shootCD == 0)
        {
            aicomponent.finishShoot = true;
            switch (aicomponent.Dir)
            {
                case 0:
                    InputSystem.PressAction(input, "rnf_up");
                    aicomponent.lastShootState = "rnf_up";
                    break;
                case 1:
                    InputSystem.PressAction(input, "rnf_left");
                    aicomponent.lastShootState = "rnf_left";
                    break;
                case 2:
                    InputSystem.PressAction(input, "rnf_down");
                    aicomponent.lastShootState = "rnf_down";
                    break;
                case 3:
                    InputSystem.PressAction(input, "rnf_right");
                    aicomponent.lastShootState = "rnf_right";
                    break;
                default:
                    break;
            }
        }
    }
}
