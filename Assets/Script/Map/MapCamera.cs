using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCamera : MonoBehaviour
{
    public GameObject player;
    public float cameraMoveSpeed;

    public Vector3 vision = new Vector3(35, 25, 0);
    private Vector2 moveDir = new Vector2(0, 0);
    public Vector3 playerPos;
    public Vector3 cameraPos;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Game").GetComponent<Player>().player;
        cameraMoveSpeed = player.GetComponent<TankController>().moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        if (player != null)
        {
            follow();
        }
    }

    void follow()
    {
        playerPos = player.transform.position;   
        
        // x axis follow
        if (Mathf.Abs(playerPos.x - transform.position.x) >= vision.x / 2)
        {
            moveDir.x = Mathf.Sign(playerPos.x - transform.position.x);   
            transform.Translate(new Vector3(cameraMoveSpeed * Time.deltaTime * moveDir.x, 0, 0), Space.World);
        }
        
        // y axis follow
        if (Mathf.Abs(playerPos.y - transform.position.y) >= vision.y / 2)
        {
            moveDir.y = Mathf.Sign(playerPos.y - transform.position.y);
            transform.Translate(new Vector3(0, cameraMoveSpeed * Time.deltaTime * moveDir.y, 0), Space.World);
        }
    }
}
