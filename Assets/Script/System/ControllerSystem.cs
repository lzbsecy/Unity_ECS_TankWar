using UnityEngine;
public class ControllerSystem : SystemBase
{
    public ControllerSystem(GameWorld world) : base(world)
    {
        TankComponent.moveActionDict.Add("move_up", 0);
        TankComponent.moveActionDict.Add("move_left", 1);
        TankComponent.moveActionDict.Add("move_down", 2);
        TankComponent.moveActionDict.Add("move_right", 3);
    }

    public void InitBodywork(int bodyworkID, TankComponent tank, MoveComponent move, GameobjectComponent gameobjectComponent)
    {
        gameobjectComponent.animator.SetInteger("state", 0); // enter the idle animation
        EquipBodywork(EquipmentManager.GetInstance().GetBodywork(bodyworkID), tank, move, gameobjectComponent);
    }

    public Entity CreateGunEntity(GameobjectComponent gameobjectComponent)
    {
        // create the entity for gun
        Transform transform = gameobjectComponent.transform;
        GameObject gunGo = null;
        Transform[] children = transform.GetComponentsInChildren<Transform>();
        foreach (var t in children)
        {
            if (t.tag == "gun")
            {
                gunGo = t.gameObject;
                break;
            }
        }
        Entity gunEntity = world.entitySystem.CreateFor(gunGo);
        //GenerateGun(entity);
        return gunEntity;
    }

    public void Update(TankComponent tank, InputComponent input, MoveComponent move, GameobjectComponent gameobjectComponent)
    {
        if (tank == null || tank.enable == false)
        {
            return;
        }
        //Debug.Log("contorller system update, entity tag: " + tank.entity.identity.tag);
        MoveControl(tank, input, move, gameobjectComponent.transform, gameobjectComponent);    
    }

    void MoveControl(TankComponent tank, InputComponent input, MoveComponent move, Transform transform, GameobjectComponent gameobjectComponent)
    {
        foreach (var item in TankComponent.moveActionDict)
        {
            if (InputSystem.GetHoldAction(input, item.Key))
            {
                move.moveDirection = item.Value;
            }
        }

        move.needMove = false;
        foreach (var item in TankComponent.moveActionDict)
        {
            if (InputSystem.GetHoldAction(input, item.Key))
            {
                move.needMove = true;
                
                break;
            }
        }

        if (move.needMove == true)
        {
            //Debug.Log("need move is true");
            gameobjectComponent.animator.SetInteger("state", 1);
            transform.rotation = Quaternion.Euler(0, 0, move.moveDirection * 90);
        }
        else
        {
            move.moveDirection = -1;
            gameobjectComponent.animator.SetInteger("state", 0);
        }
    }

    //void GenerateGun(Entity entity)
    //{
    //    Entity gunEntity = world.entitySystem.NewPawn(gunPrefab, entity.gameobjectComponent.transform.position);
    //    gunEntity.identity.master = entity;
    //}

    void EquipBodywork(Bodywork bodywork, TankComponent tank, MoveComponent move, GameobjectComponent gameobjectComponent)
    {
        tank.bodywork = bodywork;
        gameobjectComponent.animator.SetInteger("init", bodywork.animTrigger);
        var sprite = Resources.Load<Sprite>(bodywork.spritePath);
        gameobjectComponent.spriteRenderer.sprite = sprite;

        move.moveSpeed = bodywork.moveSpeed;
        //GetComponent<Life>().SetHP(b.hp);
    }
}
