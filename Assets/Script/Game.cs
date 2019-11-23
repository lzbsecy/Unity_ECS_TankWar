using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject tankPrefab;
    public EntityFactory factory;
    void Awake()
    {

        Player playerMgr = GetComponent<Player>();
        factory = GetComponent<EntityFactory>();
        // GameObject player = Instantiate(tankPrefab, new Vector3(10, 15, 0), new Quaternion());
        GameObject player = factory.NewTank(tankPrefab, new Vector3(-10, -15, 0));
        factory.NewEnemyTank(tankPrefab, new Vector3(-55, -45, 0));
        playerMgr.SetPlayer(player);

    }

}
