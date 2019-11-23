using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frozen:Buff
{
    private const float speedDownValue = 15;
    public override void Enter(GameObject gameObject)
    {
        Debug.Log("frozen buff enter");
        TankController enemyTankCtrller =  gameObject.GetComponent<TankController>();
        if (enemyTankCtrller)
        {
            enemyTankCtrller.moveSpeed -= speedDownValue;
            if (enemyTankCtrller.moveSpeed < 0)
            {
                enemyTankCtrller.moveSpeed = 0f;
            }
            Debug.Log("frozen tank, move speed down" + enemyTankCtrller.moveSpeed.ToString());
        }
    }
}
