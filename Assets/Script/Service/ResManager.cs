using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ResManager : Singleton<ResManager>
{
    private const string BASE_PATH = "Data/Base/";
    private const string GUN_PATH = "Data/Gun/";

    public Bodywork LoadBase(int gunNo)
    {
        return JsonUtility.FromJson<Bodywork>(Resources.Load<TextAsset>(BASE_PATH + gunNo.ToString()).text);
    }

    public GunData LoadGun(int gunNo)
    {
        return JsonUtility.FromJson<GunData>(Resources.Load<TextAsset>(GUN_PATH + gunNo.ToString()).text);
    }
}
