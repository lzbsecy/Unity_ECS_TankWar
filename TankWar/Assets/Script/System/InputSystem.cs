using UnityEngine;
using System.Collections.Generic;
public class InputSystem : SystemBase
{
    public InputSystem(GameWorld world):base(world)
    {

    }

    public void Update(InputComponent input)
    {
        if (input == null || input.enable == false)
        {
            return;
        }
        if (input.isAIControl == false)
        {
            foreach (var map in InputComponent.inputMap)
            {
                if (Input.GetKeyDown(map.Key))
                {
                    input.tempList.Add(map.Value);
                }
            }
            ProcessInputStates(input, InputComponent.STATE.PRESSED);

            foreach (var map in InputComponent.inputMap)
            {
                if (Input.GetKeyUp(map.Key))
                {
                    input.tempList.Add(map.Value);
                }
            }
            ProcessInputStates(input, InputComponent.STATE.RELEASED);
        }

        foreach (var state in input.inputStates) // set pressed keys to STATE.HOLD
        {
            if (input.inputStates[state.Key] == InputComponent.STATE.PRESSED)
            {
                input.tempList.Add(state.Key);
            }
        }
        ProcessInputStates(input, InputComponent.STATE.HOLD);


        foreach (var state in input.inputStates) // set released keys to STATE.CLEAER
        {
            if (input.inputStates[state.Key] == InputComponent.STATE.RELEASED)
            {
                input.tempList.Add(state.Key);
            }
        }
        ProcessInputStates(input, InputComponent.STATE.CLEAER);

    }

    private static void ProcessInputStates(InputComponent input, InputComponent.STATE state)
    {
        for (int i = 0; i < input.tempList.Count; i++)
        {
            input.inputStates[input.tempList[i]] = state;
        }
        input.tempList.Clear();
    }

    public static void PressAction(InputComponent input, string action)
    {
        input.inputStates[action] = InputComponent.STATE.PRESSED;
    }

    public static void ReleaseAction(InputComponent input, string action)
    {
        input.inputStates[action] = InputComponent.STATE.RELEASED;
    }

    public static bool GetPressAction(InputComponent input, string action)
    {
        if (input.inputStates.ContainsKey(action))
        {
            return input.inputStates[action] == InputComponent.STATE.PRESSED;
        }

        return false;
    }

    public static bool GetHoldAction(InputComponent input, string action)
    {
        if (input.inputStates.ContainsKey(action))
        {
            return input.inputStates[action] == InputComponent.STATE.HOLD;
        }

        return false;
    }

    public static bool GetReleaseAction(InputComponent input, string action)
    {
        if (input.inputStates.ContainsKey(action))
        {
            return input.inputStates[action] == InputComponent.STATE.RELEASED;
        }

        return false;
    }
}
