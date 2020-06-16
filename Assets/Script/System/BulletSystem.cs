using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSystem : SystemBase
{
    public BulletSystem(GameWorld world) : base(world)
    {
    }

    public void Init()
    {
    }

    public void Update(Identity identity, BulletComponent bullet, MoveComponent move, GameobjectComponent gameobjectComponent)
    {
        if (bullet == null || bullet.enable == false)
        {
            return ;
        }

        Transform transform = gameobjectComponent.transform;
        BoxCollider2D boxCollider2D = gameobjectComponent.collider;
        Animator animator = gameobjectComponent.animator;

        boxCollider2D.enabled = false;
        Collider2D otherCollider = Physics2D.OverlapBox(transform.position, boxCollider2D.size, transform.rotation.z * Mathf.Deg2Rad);
        if (otherCollider != null)
        {
            OnCollision(otherCollider, identity, bullet, animator, boxCollider2D, move);
        }
        boxCollider2D.enabled = true;

        bullet.currentDistance += move.moveSpeed * Time.fixedDeltaTime;

        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
        if (bullet.currentDistance > bullet.shootDistance)
        {
            world.entitySystem.DestroyEntity(identity.entity);
        }
        else if(info.normalizedTime >= 1.0f)
        {
            world.entitySystem.DestroyEntity(identity.entity);
        }
    }

    protected void OnCollision(Collider2D other, Identity identity, BulletComponent bulletComponent, Animator animator, BoxCollider2D boxCollider2D, MoveComponent move)
    {
        
        Entity otherEntity = other.gameObject.GetComponent<EntityHolder>().entity;
        Identity otherIdentity = otherEntity.identity;

        if (otherIdentity.tag == "brick" || otherIdentity.tag == "stone")
        {
            //identity.isDead = true;
            if (other.gameObject.tag == "brick")
            {
                world.entitySystem.DestroyEntity(otherEntity);
            }
        }
        else if (otherEntity != identity.master && otherIdentity.tag == "tank")
        {
            //identity.isDead = true;
            //other.gameObject.GetComponent<Life>().ChangeHP(-bulletComponent.damage);
            world.buffSystem.AddBuff(otherEntity, bulletComponent.buffName);
            Debug.Log("bullet hit tank");
        }

        if(otherIdentity.tag != "river")
        {
            move.needMove = false;
            identity.isDead = true;
        }

        if (identity.isDead == true)
        {
            animator.SetBool("destroy", true);
            boxCollider2D.enabled = false; // disable collider to avoid mutiple times collision.
        }
    }
    
}
