using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float moveSpeed = 10.0f ;
    public float maxFlyDistance = 100.0f;
    public float currentDistance;
    private bool dead = false;
    private Animator anima;

    void Start()
    {   
        currentDistance = 0.0f;
        anima = GetComponent<Animator>();
    }

    void Update()
    {
    }
    private void FixedUpdate()
    {
        currentDistance = currentDistance + moveSpeed * Time.fixedDeltaTime;
        AnimatorStateInfo info = anima.GetCurrentAnimatorStateInfo(0);
        if(dead == false)
        {
            move();
        }

        if (currentDistance > maxFlyDistance  || info.normalizedTime >= 1.0f)
        {
            Destroy(gameObject);
        }   
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "stone" || other.gameObject.tag == "brick")
        {
            anima.SetBool("destroy", true);
            dead = true;
            if(other.gameObject.tag == "brick")
            {
                Destroy(other.gameObject);
            }
        }
    }

    void move()
    {
        transform.Translate(transform.up * moveSpeed * Time.deltaTime, Space.World);
    }
}
