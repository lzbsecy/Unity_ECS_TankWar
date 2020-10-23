using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ShootAI : Singleton<ShootAI>
{
    private static Dictionary<int, string> stateToAction = new Dictionary<int, string>();


    public ShootAI()
    {
        stateToAction[1] = "rnf_up";
        stateToAction[2] = "rnf_left";
        stateToAction[3] = "rnf_down";
        stateToAction[4] = "rnf_right";
    }

    public static void shootAI(AiComponent aicomponent, InputComponent input)
    {
        aicomponent.bodyworkAIcomponent = aicomponent.entity.identity.master.aiComponent;
        aicomponent.EnemyDir = aicomponent.bodyworkAIcomponent.EnemyDir;
        aicomponent.AIId = aicomponent.bodyworkAIcomponent.AIId;
        aicomponent.shootCD = aicomponent.entity.gunComponent.shootCd;
        switch (aicomponent.AIId)
        {
            case 0:
                EnemyShoot(aicomponent, input);
                break;
            case 1:
                Boss1Shoot(aicomponent, input);
                break;
            case 2:
                Boss2Shoot(aicomponent, input);
                break;

            default:
                break;
        }
    }

    public static void EnemyShoot(AiComponent aicomponent, InputComponent input)
    {
        if (aicomponent.finishShoot)
        {
            aicomponent.timeCountShoot += Time.deltaTime;
            if (aicomponent.timeCountShoot >= aicomponent.shootCD)
            {
                if(aicomponent.lastShootAction!="")
                {
                    InputSystem.ReleaseAction(input, aicomponent.lastShootAction);
                    aicomponent.lastShootAction = "";
                }
                aicomponent.finishShoot = false;
                aicomponent.timeCountShoot = 0;
            }
        }
        else
        {
            aicomponent.finishShoot = true;
            switch (aicomponent.Dir)
            {
                case 0:
                    InputSystem.PressAction(input, "rnf_up");
                    aicomponent.lastShootAction = "rnf_up";
                    break;
                case 1:
                    InputSystem.PressAction(input, "rnf_left");
                    aicomponent.lastShootAction = "rnf_left";
                    break;
                case 2:
                    InputSystem.PressAction(input, "rnf_down");
                    aicomponent.lastShootAction = "rnf_down";
                    break;
                case 3:
                    InputSystem.PressAction(input, "rnf_right");
                    aicomponent.lastShootAction = "rnf_right";
                    break;
                default:
                    break;
            }
        }
    }
    /*
     * MoveState=0 根据玩家方向选择横向或者纵向持续来回移动并不断扫射 5s 接状态2
     *           1 四方向原地射击 接状态1
     *           2 玩家靠近时变换到战场的四个点其中之一，四方向射击一次
     *           3 玩家坐标越过中线时，对称跳跃到玩家另一侧
     *           4 暴走状态 射速以及移速加快 接状态1或2
     *           5 开始搜寻状态 玩家最后一次miss的方位
     *           6 目标丢失，停顿
     *           状态12重复四次没有状态2时接状态2
     *           血量到1/5时接状态4
     */
    public static void Boss1Shoot(AiComponent aicomponent, InputComponent input)
    {
        aicomponent.MoveState = aicomponent.bodyworkAIcomponent.MoveState;
        aicomponent.timeCountShoot += Time.deltaTime;

        
        switch (aicomponent.MoveState)
        {
            case 1:
                skill_MoveShoot(aicomponent, input);
                break;
            case 2:
                skill_Attack(aicomponent, input);
                break;
            case 3:
                skill_QuickMove(aicomponent, input);
                break;
            case 4:
                //暴走状态
                break;
            case 5:
                //搜寻状态
                break;
            default:
                break;
        }
    }
    public static void Boss2Shoot(AiComponent aicomponent, InputComponent input)
    {

    }

    //扫射
    public static void skill_MoveShoot(AiComponent aicomponent, InputComponent input)
    {
       
        if (aicomponent.finishShoot)
        {
            aicomponent.timeCountShoot += Time.deltaTime;
            if (aicomponent.timeCountShoot >= aicomponent.shootCD)
            {
                aicomponent.timeCountShoot = 0;
                aicomponent.finishShoot = false;
                if(aicomponent.lastShootAction!="")
                {
                    InputSystem.ReleaseAction(input, aicomponent.lastShootAction);
                    aicomponent.lastShootAction = "";
                }
            }
        }
        else
        {
            aicomponent.Dir = aicomponent.bodyworkAIcomponent.Dir;
            aicomponent.finishShoot = true;
            switch (aicomponent.Dir) 
            {
                case 1:
                    InputSystem.PressAction(input, "rnf_up");
                    aicomponent.lastShootAction = "rnf_up";
                    break;
                case 2:
                    InputSystem.PressAction(input, "rnf_right");
                    aicomponent.lastShootAction = "rnf_right";
                    break;
                case 3:
                    InputSystem.PressAction(input, "rnf_down");
                    aicomponent.lastShootAction = "rnf_down";
                    break;
                case 4:
                    InputSystem.PressAction(input, "rnf_left");
                    aicomponent.lastShootAction = "rnf_left";
                    break;
                default:
                    break;
            }
        }
    }
    //追击，正前方三方向散射子弹
    public static void skill_Attack(AiComponent aicomponent, InputComponent input)
    {

        if(aicomponent.finishShoot)
        {
            aicomponent.timeCountShoot += Time.deltaTime;
            if (aicomponent.timeCountShoot >= aicomponent.shootCD)
            {
                aicomponent.timeCountShoot = 0;
                aicomponent.finishShoot = false;

                if(aicomponent.lastShootAction!="")
                {
                    InputSystem.ReleaseAction(input, aicomponent.lastShootAction);
                    aicomponent.lastShootAction = "";
                }
                if (aicomponent.lastShootAction1 != "")
                {
                    InputSystem.ReleaseAction(input, aicomponent.lastShootAction1);
                    aicomponent.lastShootAction1 = "";
                }
                aicomponent.ShootStateCount += 1;
                if(aicomponent.ShootStateCount>=3)
                {
                    aicomponent.ShootStateCount = 0;
                }
            }
        }
        else
        {
            switch(aicomponent.EnemyDir)
            {
                case 1:
                    if (aicomponent.ShootStateCount == 0)
                    {
                        InputSystem.PressAction(input, "rnf_left");
                        InputSystem.PressAction(input, "rnf_up");
                        aicomponent.lastShootAction = "rnf_left";
                        aicomponent.lastShootAction1 = "rnf_up";
                    }
                    else if (aicomponent.ShootStateCount == 1)
                    {
                        InputSystem.PressAction(input, "rnf_up");
                        aicomponent.lastShootAction = "rnf_up";
                    }
                    else if (aicomponent.ShootStateCount == 2)
                    {
                        InputSystem.PressAction(input, "rnf_up");
                        InputSystem.PressAction(input, "rnf_right");
                        aicomponent.lastShootAction = "rnf_up";
                        aicomponent.lastShootAction1 = "rnf_right";
                    }
                    aicomponent.finishShoot = true;
                    break;
                case 2:
                    if (aicomponent.ShootStateCount == 0)
                    {
                        InputSystem.PressAction(input, "rnf_up");
                        InputSystem.PressAction(input, "rnf_right");
                        aicomponent.lastShootAction = "rnf_up";
                        aicomponent.lastShootAction1 = "rnf_right";
                    }
                    else if (aicomponent.ShootStateCount == 1)
                    {
                        InputSystem.PressAction(input, "rnf_right");
                        aicomponent.lastShootAction = "rnf_right";
                    }
                    else if (aicomponent.ShootStateCount == 2)
                    {
                        InputSystem.PressAction(input, "rnf_right");
                        InputSystem.PressAction(input, "rnf_down");
                        aicomponent.lastShootAction = "rnf_right";
                        aicomponent.lastShootAction1 = "rnf_down";
                    }

                    aicomponent.finishShoot = true;
                    break;
                case 3:
                    if (aicomponent.ShootStateCount == 0)
                    {
                        InputSystem.PressAction(input, "rnf_down");
                        InputSystem.PressAction(input, "rnf_right");
                        aicomponent.lastShootAction = "rnf_down";
                        aicomponent.lastShootAction1 = "rnf_right";
                    }
                    else if (aicomponent.ShootStateCount == 1)
                    {
                        InputSystem.PressAction(input, "rnf_down");
                        aicomponent.lastShootAction = "rnf_down";
                    }
                    else if (aicomponent.ShootStateCount == 2)
                    {
                        InputSystem.PressAction(input, "rnf_down");
                        InputSystem.PressAction(input, "rnf_left");
                        aicomponent.lastShootAction = "rnf_down";
                        aicomponent.lastShootAction1 = "rnf_left";
                    }

                    aicomponent.finishShoot = true;
                    break;
                case 4:
                    if (aicomponent.ShootStateCount == 0)
                    {
                        InputSystem.PressAction(input, "rnf_down");
                        InputSystem.PressAction(input, "rnf_left");
                        aicomponent.lastShootAction = "rnf_down";
                        aicomponent.lastShootAction1 = "rnf_left";
                    }
                    else if (aicomponent.ShootStateCount == 1)
                    {
                        InputSystem.PressAction(input, "rnf_left");
                        aicomponent.lastShootAction = "rnf_left";
                    }
                    else if (aicomponent.ShootStateCount == 2)
                    {
                        InputSystem.PressAction(input, "rnf_left");
                        InputSystem.PressAction(input, "rnf_up");
                        aicomponent.lastShootAction = "rnf_left";
                        aicomponent.lastShootAction1 = "rnf_up";
                    }


                    aicomponent.finishShoot = true;
                    break;
                default:
                    break;
            }
        }
    }
    public static void skill_QuickMove(AiComponent aicomponent,InputComponent input)
    {
        if (aicomponent.finishShoot)
        {
            aicomponent.timeCountShoot += Time.deltaTime;
            if (aicomponent.timeCountShoot >= aicomponent.shootCD)
            {
                aicomponent.timeCountShoot = 0;
                aicomponent.finishShoot = false;
                InputSystem.ReleaseAction(input, aicomponent.lastShootAction);
                aicomponent.lastShootAction = "";
            }
        }
        else
        {
            Debug.Log("直射");
            aicomponent.Dir = aicomponent.bodyworkAIcomponent.Dir;
            aicomponent.finishShoot = true;
            switch (aicomponent.Dir)
            {
                case 1:
                    InputSystem.PressAction(input, "rnf_up");
                    aicomponent.lastShootAction = "rnf_up";
                    break;
                case 2:
                    InputSystem.PressAction(input, "rnf_right");
                    aicomponent.lastShootAction = "rnf_right";
                    break;
                case 3:
                    InputSystem.PressAction(input, "rnf_down");
                    aicomponent.lastShootAction = "rnf_down";
                    break;
                case 4:
                    InputSystem.PressAction(input, "rnf_left");
                    aicomponent.lastShootAction = "rnf_left";
                    break;
                default:
                    break;
            }
        }
    }
}
