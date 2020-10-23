using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    private float currentHP = 100.0f;
    private float maxHP = 100.0f;
    private bool isDead = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ChangeHP(float damage){
        currentHP = currentHP + damage;
        if (currentHP >= maxHP)
        {
            currentHP = maxHP;
        }
        if (currentHP <= 0)
        {
            currentHP = 0;
            isDead = true;
            Destroy(this.gameObject);
            // TODO: execute death action, like playing death animation and destroy gameobject.
        }

    }

    public void SetHP(float hp){
        this.currentHP = hp;
        this.maxHP = hp;
    }

}
