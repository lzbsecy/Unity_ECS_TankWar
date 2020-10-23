using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI
{    
    private static AI instance = null;

    public static AI GetInstance()
    {
        if (instance == null)
        {
            instance = new AI();
        }
        return instance;
    }

    public AI()
    {
        Debug.Log("AI base construction.");
    }

    public virtual void Update(){

    }
}

class Singleton<T> where T : class, new()
{
    private static T _instance;
    private static readonly object syslock = new object();

    public static T getInstance()
    {
        if (_instance == null)
        {
            lock (syslock)
            {
                if (_instance == null)
                {
                    _instance = new T();
                }
            }
        }
        return _instance;
    }
}
