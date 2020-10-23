using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Entity 
{
    public GameobjectComponent gameobjectComponent;

    public Identity identity;
    public InputComponent inputComponent = null;
    public MoveComponent moveComponent = null;
    
    public TankComponent tankComponent = null;
    public GunComponent gunComponent = null;
    public AiComponent aiComponent = null;
    public BulletComponent bulletComponent = null;
    public BuffComponent buffComponent = null;
    

    public Entity()
    {
        gameobjectComponent = new GameobjectComponent() { entity = this };
        identity = new Identity() { entity = this };


        //// pawn
        //inputComponent = new InputComponent() { entity = this };
        //moveComponent = new MoveComponent() { entity = this };
        //aiComponent = new AiComponent() { entity = this };

        //// tank bodywork or gun
        //tankComponent = new TankComponent() { entity = this };
        //gunComponent = new GunComponent() { entity = this };
        //buffComponent = new BuffComponent() { entity = this };

        //// bullet
        //bulletComponent = new BulletComponent() { entity = this };
        
    }

    public void LinkComponentToEntity()
    {
        if (gameobjectComponent != null)
        {
            gameobjectComponent.entity = this;
        }
        if (identity != null)
        {
            identity.entity = this;
        }
        if (inputComponent != null)
        {
            inputComponent.entity = this;
        }
        if (moveComponent != null)
        {
            moveComponent.entity = this;
        }
        if (tankComponent != null)
        {
            tankComponent.entity = this;
        }
        if (gunComponent != null)
        {
            gunComponent.entity = this;
        }
        if (aiComponent != null)
        {
            aiComponent.entity = this;
        }
        if (buffComponent != null)
        {
            buffComponent.entity = this;
        }
        if (bulletComponent != null)
        {
            bulletComponent.entity = this;
        }
    }
}
