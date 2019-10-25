using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject gun_bullet;

    public float shootDir = 0;
    public float shootCd = 0.2f;

    private float timeCount = 0f;

    void Start()
    {
    }

    void Update()
    {   
        timeCount += Time.deltaTime;    
        SetDirection();
        transform.rotation = Quaternion.Euler(0, 0, 360 - shootDir * 45);
        if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            Shoot();
        }
    }

    void SetDirection()
    {
        float axisH = Input.GetAxisRaw("Horizontal");
        float axisV = Input.GetAxisRaw("Vertical");

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
         
    }

    void Shoot()
    {
        Vector3 bulletPos = new Vector3(transform.position.x, transform.position.y, 0);
        if (timeCount >= shootCd)
        {
            bulletPos = this.transform.position + this.transform.up * 3.0f;
            Instantiate(gun_bullet, bulletPos, transform.rotation);
            timeCount = 0.0f;
        }
    }

}
