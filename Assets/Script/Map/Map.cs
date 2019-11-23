using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{

    public GameObject stone;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                Instantiate(stone, new Vector3(32.0f + (float)i * 32.0f, 32.0f + (float)j * 32.0f, 0), transform.rotation);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
