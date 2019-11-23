using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResManager : MonoBehaviour
{
    private const string BASE_PATH = "Data/Base/";
    private const string GUN_PATH = "Data/Gun/";

    public Base LoadBase(int gunNo)
    {
        return JsonUtility.FromJson<Base>(Resources.Load<TextAsset>(BASE_PATH + gunNo.ToString()).text);
    }

    public Gun LoadGun(int gunNo)
    {
        return JsonUtility.FromJson<Gun>(Resources.Load<TextAsset>(GUN_PATH + gunNo.ToString()).text);
    }
}
