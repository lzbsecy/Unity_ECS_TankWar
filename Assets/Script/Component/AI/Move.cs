using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAI : AI
{
    private float count = 0;
    private int time = 3000;
    private int dir = 1;
    public override void Start(GameObject entity)
    {
        base.Start(entity);
    }

    public override void Update()
    {
        count = count + Time.deltaTime;
        if (count >= (float)(time / 1000))
        {
            count = 0;
            if (dir == 1)
            {
                dir = 3;
                input.ReleaseAction("move_right");
                input.PressAction("move_left");
                Debug.Log("enmey tank ai: move left");
            }
            else
            {
                dir = 1;
                input.ReleaseAction("move_left");
                input.PressAction("move_right");
                Debug.Log("enmey tank ai: move right");
            }
        }


    }
}
