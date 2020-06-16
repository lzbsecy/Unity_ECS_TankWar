using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PlayerMgr : Singleton<PlayerMgr>
{
    public Entity player;

    public void SetPlayer(Entity gameObject){
        this.player = gameObject;
    }
}
