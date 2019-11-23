using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    private InputComponent input;
    private Animator animator;
    private new Rigidbody2D rigidbody;
    private new BoxCollider2D collider;
    private Base _base;
    private GameObject gun;
    public float moveSpeed = 10;
    public float dir = 0;
    private bool needMove = true;
    private Dictionary<string, int> actionDict = new Dictionary<string, int>();
    private EquipmentManager EquipmentMgr;
    

    void Awake(){

    }

    void Start()
    {
        this.gun = GetGun();

        input = GetComponent<InputComponent>();
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();

        actionDict.Add("move_up", 0);
        actionDict.Add("move_left", 1);
        actionDict.Add("move_down", 2);
        actionDict.Add("move_right", 3);
        
        // animator.SetInteger("init", 0); // enter balck base animations
        animator.SetInteger("state", 0); // enter the idle animation

        EquipmentMgr = EquipmentManager.GetInstance();
        EquipBase(EquipmentMgr.GetBase(1));
    }

    void Update()
    {
    }

    private void FixedUpdate()
    {
        move();
        FixGun();
    }

    void move()
    {
        foreach (var item in actionDict)
        {
            if (input.GetHoldAction(item.Key))
            {
                dir = item.Value;
            }
        }

        needMove = false;
        foreach (var item in actionDict)
        {
            if (input.GetHoldAction(item.Key))
            {
                needMove = true;
                break;
            }
        }

        if (needMove == true){
            Debug.Log("need move is true");
            transform.rotation = Quaternion.Euler(0, 0, dir * 90);
            // rigidbody.MoveRotation(dir * 90);
            MoveForward();
        }else{
            dir = -1;
            animator.SetInteger("state", 0);
        }
        
    }

    void MoveForward(){
        animator.SetInteger("state", 1);
        // transform.Translate(transform.up * moveSpeed * Time.fixedDeltaTime, Space.World); 
        Vector3 nextPosition = transform.position + transform.up * moveSpeed * Time.fixedDeltaTime;

        collider.enabled = false;
        Collider2D otherCollider =  Physics2D.OverlapBox(nextPosition, collider.size, 0);
        collider.enabled = true;
        if (otherCollider != null && otherCollider.tag != "tree") // hit something, cannot pass
        {
            nextPosition = transform.position;
        }
        
        rigidbody.MovePosition(nextPosition);
    }

    GameObject GetGun(){
        Transform[] children = transform.GetComponentsInChildren<Transform>();
        foreach (var t in children)
        {
            if (t.tag == "gun")
            {
                return t.gameObject;
            }
        }
        return null;
    }

    void FixGun()
    {
        // gun.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        gun.transform.position = transform.position;
    }

    void EquipBase(Base b){
        this._base = b;
        animator.SetInteger("init", b.animTrigger);
        var sprite = Resources.Load<Sprite>(b.spritePath);
        GetComponent<SpriteRenderer>().sprite = sprite;
        
        this.moveSpeed = b.moveSpeed;
        GetComponent<Life>().SetHP(b.hp);
    }
}
