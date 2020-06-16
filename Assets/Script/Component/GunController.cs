using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float shootDir = 0;
    public float shootCd = 2.0f;
    public float fireOffset;
    private InputComponent input;
    private GunData _gun;
    private float timeCount = 0f;
    private string[] actions = {"rnf_left", "rnf_right", "rnf_up", "rnf_down"};

    void Start()
    {
        
    }

    void Update()
    {   
        
        
    }

    
}
