using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameobjectComponent : ComponentBase
{
    public GameObject gameObject;
    public Transform transform;
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public Rigidbody2D rigidbody;
    public BoxCollider2D collider;

    public void SetGameObject(GameObject gameObject)
    {
        this.gameObject = gameObject;
        this.transform = gameObject.transform;
        this.spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        this.animator = gameObject.GetComponent<Animator>();
        this.rigidbody = gameObject.GetComponent<Rigidbody2D>();
        this.collider = gameObject.GetComponent<BoxCollider2D>();
    }

    public void Destroy()
    {
        GameObject.Destroy(gameObject);
        this.transform = null;
        this.spriteRenderer = null;
        this.animator = null;
        this.rigidbody = null;
        this.collider = null;
    }
}
