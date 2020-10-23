using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GunComponent : ComponentBase
{
    public GameObject bulletPrefab;
    public int shootDir = 0; // 0
    public float shootCd = 0.8f; // 2.0f
    public float fireOffset = 25f;
    public GunData gunData;
    public float timeCount; // 0f
    public static string[] actions = { "rnf_left", "rnf_right", "rnf_up", "rnf_down" };
    public int test = 123;
}
