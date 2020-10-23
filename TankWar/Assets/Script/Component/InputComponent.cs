using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class InputComponent : ComponentBase
{
    public bool isAIControl = false;
    public enum STATE
    {
        PRESSED,
        HOLD,
        RELEASED,
        CLEAER
    }
    public static Dictionary<string, string> inputMap;
    public Dictionary<string, STATE> inputStates;

    public List<string> tempList; // cache keys of items that need to change input state

    public InputComponent()
    {
        if (inputMap == null)
        {
            inputMap = new Dictionary<string, string>();
            inputMap.Add("w", "move_up");
            inputMap.Add("s", "move_down");
            inputMap.Add("a", "move_left");
            inputMap.Add("d", "move_right");
            // "rnf" = "rotate and fire"
            inputMap.Add("up", "rnf_up");
            inputMap.Add("down", "rnf_down");
            inputMap.Add("left", "rnf_left");
            inputMap.Add("right", "rnf_right");
        }


        inputStates = new Dictionary<string, InputComponent.STATE>(); ;
        tempList = new List<string>();
        foreach (var map in inputMap)
        {
            inputStates.Add(map.Value, InputComponent.STATE.CLEAER);
        }
    }
}
