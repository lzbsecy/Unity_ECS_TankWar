using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI
{
    protected GameObject entity;
    protected InputComponent input;

    public virtual void Start(GameObject entity){
        this.entity = entity;
        this.input = entity.GetComponent<InputComponent>();
        Debug.Log("AI base Start method");
    }

    public virtual void Update(){

    }
}
