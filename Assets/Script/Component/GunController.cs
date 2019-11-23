using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float shootDir = 0;
    public float shootCd = 2.0f;
    public float fireOffset;
    private InputComponent input;
    private Gun _gun;
    private float timeCount = 0f;
    private string[] actions = {"rnf_left", "rnf_right", "rnf_up", "rnf_down"};
    private EquipmentManager EquipmentMgr;

    void Start()
    {
        input = transform.parent.gameObject.GetComponent<InputComponent>();

        EquipmentMgr = EquipmentManager.GetInstance();
        EquipGun(EquipmentMgr.GetGun(1));
    }

    void Update()
    {   
        SetDirection();
        timeCount += Time.deltaTime;
        if (timeCount >= shootCd)
        {
            for (int i = 0; i < actions.GetLength(0); i++)
            {
                if (input.GetHoldAction(actions[i]) == true)
                {
                    timeCount = 0;
                    Fire();
                    break;
                }
            }
        }
        
    }

    void SetDirection()
    {
        bool rnf_left = input.GetHoldAction("rnf_left");
        bool rnf_right = input.GetHoldAction("rnf_right");
        bool rnf_up = input.GetHoldAction("rnf_up");
        bool rnf_down = input.GetHoldAction("rnf_down");

        float axisH = 0;
        float axisV = 0;

        if (rnf_left == true)
        {
            axisH = -1;
        }
        else if(rnf_right == true)
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
            shootDir = 0;
        }
        else if(axisV > 0 && axisH > 0)
        {
            shootDir = 1;
        }
        else if (axisH > 0 && axisV == 0)
        {
            shootDir = 2;
        }
        else if (axisV < 0 && axisH > 0)
        {
            shootDir = 3;
        }
        else if (axisV < 0 && axisH == 0)
        {
            shootDir = 4;
        }
        else if (axisV < 0 && axisH < 0)
        {
            shootDir = 5;
        }
        else if (axisH < 0 && axisV == 0)
        {
            shootDir = 6;
        }
        else if(axisV > 0 && axisH < 0)
        {
            shootDir = 7;
        }
        transform.rotation = Quaternion.Euler(0, 0, 360 - shootDir * 45);
    }

    void Fire()
    {
        Vector3 bulletPos = new Vector3(transform.position.x, transform.position.y, 0);
        bulletPos = this.transform.position + this.transform.up * fireOffset;
        GameObject bullet = Instantiate(bulletPrefab, bulletPos, transform.rotation);
        bullet.GetComponent<Bullet>().buffName = _gun.bulletBuff;
        bullet.GetComponent<Bullet>().masterObj = this.transform.parent.gameObject;
    }

    void EquipGun(Gun gun){
        this._gun = gun;
        var sprite = Resources.Load<Sprite>(gun.spritePath);
        GetComponent<SpriteRenderer>().sprite = sprite;
    }
}
