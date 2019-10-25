using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 10;
    private Animator anim;
    public float dir = 0;
    public GameObject gun;

    private float x, y;
    void Start()
    {
        anim = GetComponent<Animator>();
        gun = GameObject.FindGameObjectWithTag("gun");
    }

    void Update()
    {
    }

    private void FixedUpdate()
    {
        move();
        gunMove();
    }

    void move()
    {
        if (Input.GetKey(KeyCode.W))
        {
            dir = 0;
            anim.SetInteger("state", 1);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.Translate(Vector3.up * moveSpeed * Time.fixedDeltaTime, Space.World);
            
        }
        else if (Input.GetKey(KeyCode.S))
        {
            dir = 2;
            anim.SetInteger("state", 1);
            transform.rotation = Quaternion.Euler(0, 0, 180);
            transform.Translate(Vector3.down * moveSpeed * Time.fixedDeltaTime, Space.World);
           
        }
        else if (Input.GetKey(KeyCode.A))
        {
            dir = 3;
            anim.SetInteger("state", 1);
            transform.rotation = Quaternion.Euler(0, 0, 90);
            transform.Translate(Vector3.left * moveSpeed * Time.fixedDeltaTime, Space.World);
            
        }
        else if (Input.GetKey(KeyCode.D))
        {
            dir = 1;
            anim.SetInteger("state", 1);
            transform.rotation = Quaternion.Euler(0, 0, 270);
            transform.Translate(Vector3.right * moveSpeed * Time.fixedDeltaTime, Space.World);
            
        }
        else
        {
            anim.SetInteger("state", 0);
        }
     
    }
    void gunMove()
    {
        x = transform.position.x;
        y = transform.position.y;
        gun.transform.position = new Vector3(x, y, 0);
    }

}
