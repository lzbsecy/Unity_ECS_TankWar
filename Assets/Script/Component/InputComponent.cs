using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputComponent : MonoBehaviour
{

    public bool isAIControl = false;
    private enum STATE
    {
        PRESSED,
        HOLD,
        RELEASED,
        CLEAER
    }
    private Dictionary<string, string> inputMap = new Dictionary<string, string>();
    private Dictionary<string, STATE> inputStates = new Dictionary<string, STATE>();

    private List<string> tempList = new List<string>(); // cache keys of items that need to change input state

    // Start is called before the first frame update
    void Start()
    {
        inputMap.Add("w", "move_up");
        inputMap.Add("s", "move_down");
        inputMap.Add("a", "move_left");
        inputMap.Add("d", "move_right");

        // "rnf" = "rotate and fire"
        inputMap.Add("up", "rnf_up");
        inputMap.Add("down", "rnf_down");
        inputMap.Add("left", "rnf_left");
        inputMap.Add("right", "rnf_right");

        foreach (var map in inputMap)
        {
            inputStates.Add(map.Value, STATE.CLEAER);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isAIControl == false)
        {
            foreach (var map in inputMap)
            {
                if (Input.GetKeyDown(map.Key))
                {
                    tempList.Add(map.Value);
                }
            }
            ProcessInputStates(STATE.PRESSED);

            foreach (var map in inputMap)
            {
                if (Input.GetKeyUp(map.Key))
                {
                    tempList.Add(map.Value);
                }
            }
            ProcessInputStates(STATE.RELEASED);
        }
        
        foreach (var state in inputStates) // set pressed keys to STATE.HOLD
        {
            if (inputStates[state.Key] == STATE.PRESSED)
            {
                tempList.Add(state.Key);
            }
        }
        ProcessInputStates(STATE.HOLD);


        foreach (var state in inputStates) // set released keys to STATE.CLEAER
        {
            if(inputStates[state.Key] == STATE.RELEASED)
            {
                tempList.Add(state.Key);
            }
        }
        ProcessInputStates(STATE.CLEAER);
        

    }

    private void ProcessInputStates(STATE state){
        for (int i = 0; i < tempList.Count; i++)
        {
            inputStates[tempList[i]] = state;
        }
        tempList.Clear();
    }

    public void PressAction(string action){
        inputStates[action] = STATE.PRESSED;
    }

    public void ReleaseAction(string action){
        inputStates[action] = STATE.RELEASED;
    }

    public bool GetPressAction(string action){
        if (inputStates.ContainsKey(action))
        {
            return inputStates[action] == STATE.PRESSED;
        }
        
        return false;
    }
    
    public bool GetHoldAction(string action){
        if (inputStates.ContainsKey(action))
        {
            return inputStates[action] == STATE.HOLD;
        }
        
        return false;
    }

    public bool GetReleaseAction(string action){
        if (inputStates.ContainsKey(action))
        {
            return inputStates[action] == STATE.RELEASED;
        }
        
        return false;
    }



}
