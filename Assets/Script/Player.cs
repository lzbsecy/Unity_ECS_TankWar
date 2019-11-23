using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject player;

    public void SetPlayer(GameObject gameObject){
        this.player = gameObject;
    }
}
