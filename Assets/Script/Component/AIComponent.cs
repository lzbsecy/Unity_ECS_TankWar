using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIComponent : MonoBehaviour
{
    private List<AI> ais = new List<AI>();
    public bool isRunning = false;

    void Start()
    {
        foreach (var ai in ais)
        {
            ai.Start(this.gameObject);
        }
    }

    void Update()
    {
        if (isRunning == false)
        {
            return ;
        }
        
        foreach (var ai in ais)
        {
            ai.Update();
        }
    }

    public void AddAI(AI ai){
        ais.Add(ai);
    }
}
