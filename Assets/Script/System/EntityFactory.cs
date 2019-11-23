using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFactory : MonoBehaviour
{
    
    public GameObject NewEntity(GameObject prefab, Vector3 position){
        return Instantiate(prefab, position, new Quaternion());
    }

    public GameObject NewTank(GameObject prefab, Vector3 position){
        GameObject tank = NewEntity(prefab, position);
        tank.GetComponent<AIComponent>().AddAI(new MoveAI());
        return tank;
    }

    public GameObject NewEnemyTank(GameObject prefab, Vector3 position){
        GameObject tank = NewTank(prefab, position);
        tank.GetComponent<InputComponent>().isAIControl = true;
        tank.GetComponent<AIComponent>().isRunning = true;
        return tank;
    }
}
