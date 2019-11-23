using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float moveSpeed = 10.0f ;
    public float damage = 10.0f;
    public float shootDistance = 100.0f;
    public float currentDistance;
    public string buffName;
    public GameObject masterObj;
    private bool dead = false;
    private Animator animator;

    void Start()
    {   
        currentDistance = 0.0f;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
    }
    
    private void FixedUpdate()
    {
        currentDistance = currentDistance + moveSpeed * Time.fixedDeltaTime;
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
        if(dead == false)
        {
            move();
        }

        if (currentDistance > shootDistance  || info.normalizedTime >= 1.0f)
        {
            Destroy(gameObject);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "brick" || other.gameObject.tag == "stone")
        {
            dead = true;
            if (other.gameObject.tag == "brick")
            {
                Destroy(other.gameObject);
            }
        }
        else if(other.gameObject != this.masterObj && other.gameObject.GetComponent<TankController>() != null)
        {
            dead = true;
            other.gameObject.GetComponent<Life>().ChangeHP(-this.damage);
            other.gameObject.GetComponent<BuffComponent>().AddBuff(buffName);
            Debug.Log(" collide with other tank ");
        }

        if (dead == true)
        {
            animator.SetBool("destroy", true);
            GetComponent<BoxCollider2D>().enabled = false; // disable collider to avoid mutiple times collision.
        }
    }

    void move()
    {
        transform.Translate(transform.up * moveSpeed * Time.deltaTime, Space.World);
    }
}
