using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TankComponent : ComponentBase
{
    public Bodywork bodywork;
    public GameObject gun;
    [System.NonSerialized] public Entity gunEntity;
    public static Dictionary<string, int> moveActionDict = new Dictionary<string, int>();
}
