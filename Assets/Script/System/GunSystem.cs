using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSystem : SystemBase
{
    public GunSystem(GameWorld world) : base(world)
    {
    }

    public void InitGun(int gunID, GunComponent gunComponent, GameobjectComponent gameobjectComponent)
    {
        if(gunComponent == null || gunComponent.enable == false)
        {
            return;
        }
        Debug.Log("GunSystem.Init");
        EquipGun(EquipmentManager.GetInstance().GetGun(1), gunComponent, gameobjectComponent);
    }

    public void Update(GunComponent gunComponent,InputComponent input, GameobjectComponent gameobjectComponent)
    {
        if (gunComponent == null || gunComponent.enable == false)
        {
            return;
        }

        SetDirection(gunComponent, input, gameobjectComponent.transform);
        gunComponent.timeCount += Time.deltaTime;
        if (gunComponent.timeCount >= gunComponent.shootCd)
        {
            for (int i = 0; i < GunComponent.actions.GetLength(0); i++)
            {
                if (InputSystem.GetHoldAction(input, GunComponent.actions[i]) == true)
                {
                    gunComponent.timeCount = 0;
                    Fire(gunComponent, gameobjectComponent.transform);
                    break;
                }
            }
        }
    }

    protected void SetDirection(GunComponent gunComponent, InputComponent input, Transform transform)
    {
        bool rnf_left = InputSystem.GetHoldAction(input, "rnf_left");
        bool rnf_right = InputSystem.GetHoldAction(input, "rnf_right");
        bool rnf_up = InputSystem.GetHoldAction(input, "rnf_up");
        bool rnf_down = InputSystem.GetHoldAction(input, "rnf_down");

        float axisH = 0;
        float axisV = 0;

        if (rnf_left == true)
        {
            axisH = -1;
        }
        else if (rnf_right == true)
        {
            axisH = 1;
        }
        else
        {
            axisH = 0;
        }

        if (rnf_up == true)
        {
            axisV = 1;
        }
        else if (rnf_down == true)
        {
            axisV = -1;
        }
        else
        {
            axisV = 0;
        }

        if (axisV > 0 && axisH == 0)
        {
            gunComponent.shootDir = 0;
        }
        else if (axisV > 0 && axisH > 0)
        {
            gunComponent.shootDir = 1;
        }
        else if (axisH > 0 && axisV == 0)
        {
            gunComponent.shootDir = 2;
        }
        else if (axisV < 0 && axisH > 0)
        {
            gunComponent.shootDir = 3;
        }
        else if (axisV < 0 && axisH == 0)
        {
            gunComponent.shootDir = 4;
        }
        else if (axisV < 0 && axisH < 0)
        {
            gunComponent.shootDir = 5;
        }
        else if (axisH < 0 && axisV == 0)
        {
            gunComponent.shootDir = 6;
        }
        else if (axisV > 0 && axisH < 0)
        {
            gunComponent.shootDir = 7;
        }
        transform.rotation = Quaternion.Euler(0, 0, 360 - gunComponent.shootDir * 45);
        
    }

    protected void Fire(GunComponent gunComponent, Transform transform)
    {
        Vector3 bulletPos = transform.position + transform.up * gunComponent.fireOffset;
        Entity bullet = world.entitySystem.CreateFrom(gunComponent.bulletPrefab, bulletPos);
        
        bullet.gameobjectComponent.transform.rotation = transform.rotation;
        bullet.bulletComponent.buffName = gunComponent.gunData.bulletBuff;
        bullet.identity.master = gunComponent.entity;
    }

    protected void EquipGun(GunData gunData, GunComponent gunComponent, GameobjectComponent gameobjectComponent)
    {
        gunComponent.gunData = gunData;
        var sprite = Resources.Load<Sprite>(gunData.spritePath);
        gameobjectComponent.spriteRenderer.sprite = sprite;
    }
}
