using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : SystemBase
{
    public LevelSystem(GameWorld world) : base(world)
    {
        Entity player = world.entitySystem.NewTank(world.tankPrefab, new Vector3(-10, -15, 0));
        player.identity.tag = "player";
        PlayerMgr.getInstance().SetPlayer(player);

        GameObject randomPrefab = world.brick;
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                int random = Random.Range(0, 3);
                if(random == 1)
                {
                    randomPrefab = world.river;
                }
                else if(random == 2)
                {
                    randomPrefab = world.brick;
                }
                world.entitySystem.CreateFrom(randomPrefab, new Vector3(32.0f + i * 32.0f, 32.0f + j * 32.0f, 0));
            }
        }
    }

    public void Init(LevelComponent level)
    {

    }

    public void Update()
    {
        
    }
}
