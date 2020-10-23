using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [System.NonSerialized]public Entity target;
    public float cameraMoveSpeed;

    public Vector3 vision;
    public Vector2 moveDir;
    public Vector3 targetPos;
    public Vector3 cameraPos;
    
    void Start()
    {
        vision = new Vector3(35, 25, 0);
        moveDir = new Vector2(0, 0);
        SetTarget(PlayerMgr.getInstance().player);
    }

    void Update()
    {
        if (target != null)
        {
            Follow();
        }
    }

    public void SetTarget(Entity target)
    {
        Debug.Log("target " + target.identity.tag);
        this.target = target;
        cameraMoveSpeed = target.moveComponent.moveSpeed;
    }

    void Follow()
    {
        //Debug.Log("Follow", this.target.gameobjectComponent.transform);
        targetPos = target.gameobjectComponent.transform.position;

        // x axis follow
        if (Mathf.Abs(targetPos.x - transform.position.x) >= vision.x / 2)
        {
            moveDir.x = Mathf.Sign(targetPos.x - transform.position.x);
            transform.Translate(new Vector3(cameraMoveSpeed * Time.deltaTime * moveDir.x, 0, 0), Space.World);
        }

        // y axis follow
        if (Mathf.Abs(targetPos.y - transform.position.y) >= vision.y / 2)
        {
            moveDir.y = Mathf.Sign(targetPos.y - transform.position.y);
            transform.Translate(new Vector3(0, cameraMoveSpeed * Time.deltaTime * moveDir.y, 0), Space.World);
        }
    }
}
