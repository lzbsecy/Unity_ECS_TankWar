using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager
{
    private const string GUN_PATH = "Assets/Resources/Data/Gun/";
    private const string BASE_PATH = "Assets/Resources/Data/Base/";
    private readonly Dictionary<int, GunData> guns = new Dictionary<int, GunData>();
    private readonly Dictionary<int, Bodywork> bases = new Dictionary<int, Bodywork>();
    private readonly ResManager resManager;

    private static EquipmentManager instance = null;
    
    public static EquipmentManager GetInstance(){
        if (instance == null)
        {
            instance = new EquipmentManager();
        }
        return instance;
    }

    EquipmentManager(){
        resManager = ResManager.getInstance();
        InitGuns();
        InitBase();
    }

    void InitGuns(){
        DirectoryInfo gunDirectory = new DirectoryInfo(GUN_PATH);
        FileInfo[] files = gunDirectory.GetFiles("*", SearchOption.AllDirectories);
        foreach (var item in files)
        {
            if (item.Name.EndsWith(".json"))
            {
                int gunNo = int.Parse(item.Name.Replace(item.Extension, ""));
                guns.Add(gunNo, resManager.LoadGun(gunNo));
            }
        }
    }

    void InitBase(){
        DirectoryInfo gunDirectory = new DirectoryInfo(BASE_PATH);
        FileInfo[] files = gunDirectory.GetFiles("*", SearchOption.AllDirectories);
        foreach (var item in files)
        {
            if (item.Name.EndsWith(".json"))
            {
                int baseNo = int.Parse(item.Name.Replace(item.Extension, ""));
                bases.Add(baseNo, resManager.LoadBase(baseNo));
            }
        }
    }

    public Bodywork GetBodywork(int baseNo){
        return bases[baseNo];
    }

    public GunData GetGun(int gunNo){
        return guns[gunNo];
    }
}
