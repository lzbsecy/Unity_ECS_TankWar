using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class MoveAI : Singleton<MoveAI>
{
    private static Dictionary<int, string> stateToAction = new Dictionary<int, string>();

    public MoveAI()
    {
        stateToAction[1] = "move_up";
        stateToAction[2] = "move_left";
        stateToAction[3] = "move_down";
        stateToAction[4] = "move_right";
    }
    public static void moveAI(AiComponent aicomponent, InputComponent input)
    {
        switch(aicomponent.AIId)
        {
            case 0:
                EnemyMove(aicomponent, input);
                break;
            case 1:
                Boss1Move(aicomponent, input);
                break;
            case 2:
                Boss2Move(aicomponent, input);
                break;
            default:
                break;
        }
    }
    //普通敌人
    public static void EnemyMove(AiComponent aicomponent, InputComponent input)
    {
        if(PhysicsUtil.OverlapBox(aicomponent.entity.gameobjectComponent.transform.position, aicomponent.entity.gameobjectComponent.collider, 0))
        {
            aicomponent.MoveState = 1;
        }
        if (aicomponent.MoveState == 1)//遇到障碍物，刷新移动状态
        {
            if(aicomponent.lastMoveAction!="")
            {
                InputSystem.ReleaseAction(input, aicomponent.lastMoveAction);
                aicomponent.lastMoveAction = "";
            }

            aicomponent.Dir = Random.Range(0, 4);
            switch (aicomponent.Dir)
            {
                case 0:
                    InputSystem.PressAction(input,"move_up");
                    aicomponent.lastMoveAction = "move_up";
                    break;
                case 1:
                    InputSystem.PressAction(input,"move_left");
                    aicomponent.lastMoveAction = "move_left";
                    break;
                case 2:
                    InputSystem.PressAction(input,"move_down");
                    aicomponent.lastMoveAction = "move_down";
                    break;
                case 3:
                    InputSystem.PressAction(input,"move_right");
                    aicomponent.lastMoveAction = "move_right";
                    break;
                default:
                    break;
            }
            aicomponent.MoveState = 0;
        }
        if (aicomponent.MoveState == 0)
        {
            aicomponent.timeCount += Time.deltaTime;
            if (aicomponent.timeCount >= aicomponent.changeDirCd)
            {
                if (aicomponent.lastMoveAction != "")
                {
                    InputSystem.ReleaseAction(input, aicomponent.lastMoveAction);
                    aicomponent.lastMoveAction = "";
                }
                aicomponent.timeCount = 0;
                aicomponent.Dir = Random.Range(0, 4);
                switch (aicomponent.Dir)
                {
                    case 0:
                        InputSystem.PressAction(input, "move_up");
                        aicomponent.lastMoveAction = "move_up";
                        break;
                    case 1:
                        InputSystem.PressAction(input, "move_left");
                        aicomponent.lastMoveAction = "move_left";
                        break;
                    case 2:
                        InputSystem.PressAction(input, "move_down");
                        aicomponent.lastMoveAction = "move_down";
                        break;
                    case 3:
                        InputSystem.PressAction(input, "move_right");
                        aicomponent.lastMoveAction = "move_right";
                        break;
                    default:
                        break;
                }
            }
        }
    }
    /*
     * MoveState=1 根据玩家方向选择横向或者纵向持续来回移动并不断扫射 5s 接状态2
     *           2 追击
     *           3 长距离移动，横冲直撞
     *           4 暴走状态 射速以及移速加快 接状态1或2
     *           5 开始搜寻状态 玩家最后一次miss的方位
     *           
     *           血量到1/5时接状态4
     */
     //boss1
    public static void Boss1Move(AiComponent aicomponent, InputComponent input)
    {
        updateCollider(aicomponent);
        if(aicomponent.MoveState!=5)
        {
            getEnemyDir(aicomponent, input);
        }   
        if (aicomponent.Dir != aicomponent.lastDir && aicomponent.Dir!=0)//玩家方向发生变化,记录方向
        {
            aicomponent.lastDir = aicomponent.EnemyDir;
        }
        /*if(aicomponent.entity)  //血量
        {
            GOTO_STATE4(aicomponent, input);
        }
        */
        switch (aicomponent.MoveState)
        {
            case 0:
                GOTO_STATE1(aicomponent, input);
                break;
            case 1:
                //中移速，高射速
                aicomponent.timeCount += Time.deltaTime;
                skill_MoveShoot(aicomponent, input);
                break;
            case 2:
                //低移速，高射速
                aicomponent.timeCount += Time.deltaTime;
                skill_Attack(aicomponent, input);
                break;
            case 3:
                //高移速，低射速
                aicomponent.timeCount += Time.deltaTime;
                skill_QuickMove(aicomponent, input);
                break;
            case 4:
                //暴走状态，视距翻倍，移速翻倍，射击cd减半
                aicomponent.stadia = aicomponent.stadia * 2;
                aicomponent.entity.moveComponent.moveSpeed = aicomponent.entity.moveComponent.moveSpeed * 2;
                aicomponent.shootCD = aicomponent.shootCD / 2;
                GOTO_STATE2(aicomponent, input);
                break;
            case 5:
                //搜索状态
                skill_FindEnemy(aicomponent, input); 
                break;
        }
    }

    //boss2
    public static void Boss2Move(AiComponent aicomponent, InputComponent input)
    {

    }


    public static void updateCollider(AiComponent aicomponent) //更新视野内的collider对象
    {
        if(aicomponent.AttackableObject==null)  //无攻击目标，获取攻击对象
        {
            Debug.Log("开始获取对象");
            aicomponent.collider = Physics2D.OverlapBoxAll(aicomponent.entity.gameobjectComponent.transform.position, aicomponent.stadia, 0);
            List<GameObject> attackObject = new List<GameObject> { };
            float mindistance = 99999;
            if (aicomponent.collider[0] != null) //collider非空
            {
                foreach (Collider2D co in aicomponent.collider)
                {
                    //collider对象拥有identity组件和tankcomponent组件，且camp与自己不同，列入可攻击列表
                    if (co.GetComponent<EntityHolder>().entity.identity.enable == true
                        && co.GetComponent<EntityHolder>().entity.tankComponent.enable == true 
                        && co.GetComponent<EntityHolder>().entity.identity.camp != aicomponent.entity.identity.camp
                        )
                    {
                        attackObject.Add(co.gameObject);
                    }
                }
            }
            if(attackObject.Count==0)   //范围内没有可攻击对象
            {
                aicomponent.AttackableObject = null;
            }
            else  //确定攻击目标，选择距离最近的锁定
            {
                Debug.Log("视野内的可攻击对象数目为：" + attackObject.Count);
                foreach (GameObject obj in attackObject)
                {
                    if(mindistance>Vector2.Distance(aicomponent.entity.gameobjectComponent.transform.position,obj.transform.position))
                    {
                        mindistance = Vector2.Distance(aicomponent.entity.gameobjectComponent.transform.position, obj.transform.position);
                        aicomponent.AttackableObject = obj;
                    }
                }
                Debug.Log("锁定对象，开始攻击");
                //aicomponent.MoveState = Random.Range(1, 3);
            }
        }
        else  //有攻击目标，锁定攻击对象，离开视野时刷新
        {
            
            if (Mathf.Abs(aicomponent.entity.gameobjectComponent.transform.position.x
                - aicomponent.AttackableObject.transform.position.x) > aicomponent.stadia.x ||
                Mathf.Abs(aicomponent.entity.gameobjectComponent.transform.position.y
                - aicomponent.AttackableObject.transform.position.y) > aicomponent.stadia.y)    //离开视野范围,清空攻击目标
            {
                Debug.Log("对象miss");
                aicomponent.AttackableObject = null;
            }
        }
       
    }
    public static void getEnemyDir(AiComponent aicomponent, InputComponent input) //获取玩家所在方向
    {
        if(aicomponent.AttackableObject!=null)
        {
            Vector2 EnemyPosition = aicomponent.AttackableObject.transform.position;
            Vector2 PositionSelf = aicomponent.entity.gameobjectComponent.transform.position;

            //Debug.Log(aicomponent.AttackableObject.name+" "+ EnemyPosition + "    self" + PositionSelf);
            /*       1 | 1
             *     4   |   2
             *   ——————+———————
             *     4   |   2 
             *       3 | 3 
             */
            if (EnemyPosition.x > PositionSelf.x && EnemyPosition.y > PositionSelf.y)  //第一象限
            {
                if ((EnemyPosition.x - PositionSelf.x) < (EnemyPosition.y - PositionSelf.y))
                {
                    aicomponent.EnemyDir = 1;
                }
                else
                {
                    aicomponent.EnemyDir = 2;
                }
            }
            if (EnemyPosition.x > PositionSelf.x && EnemyPosition.y < PositionSelf.y)  //第四象限
            {
                if ((EnemyPosition.x - PositionSelf.x) > Mathf.Abs(EnemyPosition.y - PositionSelf.y))
                {
                    aicomponent.EnemyDir = 2;
                }
                else
                {
                    aicomponent.EnemyDir = 3;
                }
            }
            if (EnemyPosition.x < PositionSelf.x && EnemyPosition.y < PositionSelf.y)  //第三象限
            {
                if (Mathf.Abs(EnemyPosition.x - PositionSelf.x) < Mathf.Abs(EnemyPosition.y - PositionSelf.y))
                {
                    aicomponent.EnemyDir = 3;
                }
                else
                {
                    aicomponent.EnemyDir = 4;
                }
            }
            if (EnemyPosition.x < PositionSelf.x && EnemyPosition.y > PositionSelf.y)  //第二象限
            {
                if (Mathf.Abs(EnemyPosition.x - PositionSelf.x) > (EnemyPosition.y - PositionSelf.y))
                {
                    aicomponent.EnemyDir = 4;
                }
                else
                {
                    aicomponent.EnemyDir = 1;
                }
            }
        }
        else
        {
            //无可获取对象     
            clearInputState(aicomponent, input);
            aicomponent.finishMove = false;
            GOTO_STATE5(aicomponent, input);
        }
        
    }
    public static void skill_MoveShoot(AiComponent aicomponent, InputComponent input)//扫射
    {
        if(aicomponent.Dir==0)//初始方向，开始获取攻击方向
        {
            aicomponent.Dir = aicomponent.EnemyDir;
        }
        if(aicomponent.Dir == 1 || aicomponent.Dir == 3)
        {
            //判断其方向，时长3s
            if (aicomponent.AttackableObject.transform.position.x > aicomponent.entity.gameobjectComponent.transform.position.x)//正前方右侧
            {
                //先向右扫射，再向左扫射，时长3s
                if (aicomponent.finishMove == false)//初始为false,向右扫射
                {
                    InputSystem.PressAction(input, "move_right");
                    aicomponent.lastMoveAction = "move_right";
                    aicomponent.finishMove = true;
                }
                if (PhysicsUtil.OverlapBox(aicomponent.entity.gameobjectComponent.transform.position, aicomponent.entity.gameobjectComponent.collider, 0)
                    || aicomponent.timeCount > 3) //技能持续3s 或碰到障碍物 技能结束，切换状态
                {
                    if (aicomponent.lastMoveAction != "")
                    {
                        InputSystem.ReleaseAction(input, aicomponent.lastMoveAction);
                        aicomponent.lastMoveAction = "";
                    }
                    aicomponent.finishMove = false;
                    aicomponent.timeCount = 0;
                    aicomponent.Dir = 0;
                    GOTO_STATE2(aicomponent, input);
                }
            }
            //正前方左侧 先向左移动扫射，再向右扫射，时长5s
            else
            {
                if (aicomponent.finishMove == false)//初始为false,向左扫射
                {
                    InputSystem.PressAction(input, "move_left");
                    aicomponent.lastMoveAction = "move_left";
                    aicomponent.finishMove = true;
                }
                if (aicomponent.timeCount > 3) //技能持续3s
                {
                    if (aicomponent.lastMoveAction != "")
                    {
                        InputSystem.ReleaseAction(input, aicomponent.lastMoveAction);
                        aicomponent.lastMoveAction = "";
                    }
                    aicomponent.finishMove = false;
                    aicomponent.Dir = 0;
                    aicomponent.timeCount = 0;
                    GOTO_STATE2(aicomponent, input);
                }
            }
        }
        else //2和4，竖直方向
        {
            //判断其方向，向上移动扫射还是向下，直到超过玩家的时候变换扫射方向，时长5s
            if (aicomponent.AttackableObject.transform.position.y > aicomponent.entity.gameobjectComponent.transform.position.y)
            {
                //先向上扫射，再向左扫射，时长5s
                if (aicomponent.finishMove==false)//初始为false,向上扫射
                {
                    InputSystem.PressAction(input, "move_up");
                    aicomponent.lastMoveAction = "move_up";
                    aicomponent.finishMove = true;
                }

                //技能持续3s 技能结束，切换状态
                if (aicomponent.timeCount >= 3f) 
                {
                    if (aicomponent.lastMoveAction != "")
                    {
                        InputSystem.ReleaseAction(input, aicomponent.lastMoveAction);
                        aicomponent.lastMoveAction = "";
                    }
                    aicomponent.finishMove = false;
                    aicomponent.Dir = 0;
                    aicomponent.timeCount = 0;
                    GOTO_STATE2(aicomponent, input);
                }
            }
            //向下移动扫射，时长3s
            else
            {
                if (aicomponent.finishMove == false)//初始为false,向下扫射
                {
                    InputSystem.PressAction(input, "move_down");
                    aicomponent.lastMoveAction = "move_down";
                    aicomponent.finishMove = true;
                }

                //技能持续5s 技能结束，切换状态
                if (aicomponent.timeCount > 3) 
                {
                    if (aicomponent.lastMoveAction != "")
                    {
                        InputSystem.ReleaseAction(input, aicomponent.lastMoveAction);
                        aicomponent.lastMoveAction = "";
                    }
                    aicomponent.finishMove = false;
                    aicomponent.Dir = 0;
                    aicomponent.timeCount = 0;
                    GOTO_STATE2(aicomponent, input);
                    
                }
            }
        }
    }
    public static void skill_Attack(AiComponent aicomponent, InputComponent input) //追击
    {
        if (aicomponent.Dir == 0)
        {
            aicomponent.Dir = aicomponent.EnemyDir;
        }
        if(aicomponent.finishMove)
        {
            if(aicomponent.Dir!=aicomponent.EnemyDir)
            {
                aicomponent.Dir = aicomponent.EnemyDir;
                if (aicomponent.lastMoveAction != "")
                {
                    InputSystem.ReleaseAction(input, aicomponent.lastMoveAction);
                    aicomponent.lastMoveAction = "";
                }
                if (aicomponent.timeCount >= 3)
                {
                    GOTO_STATE1(aicomponent, input);
                }
                aicomponent.finishMove = false;
            }
        }
        else
        {
            aicomponent.finishMove = true;
            switch (aicomponent.Dir)
            {
                case 1:
                    InputSystem.PressAction(input, "move_up");
                    aicomponent.lastMoveAction = "move_up";
                    break;
                case 2:
                    InputSystem.PressAction(input, "move_right");
                    aicomponent.lastMoveAction = "move_right";
                    break;
                case 3:
                    InputSystem.PressAction(input, "move_down");
                    aicomponent.lastMoveAction = "move_down";
                    break;
                case 4:
                    InputSystem.PressAction(input, "move_left");
                    aicomponent.lastMoveAction = "move_left";
                    break;
                default:
                    break;
            }
        }
    }
    public static void skill_QuickMove(AiComponent aicomponent, InputComponent input) //高速移动
    {
        if(aicomponent.Dir==0)//0为初始方向，不做动作
        {
            aicomponent.Dir = aicomponent.EnemyDir;//锁定敌人方向
        }
        if(aicomponent.finishMove)
        {
            //遇到障碍物或移动时间超过3s，结束当前状态，进入状态1或者状态2
            if (PhysicsUtil.OverlapBox(aicomponent.entity.gameobjectComponent.transform.position, aicomponent.entity.gameobjectComponent.collider, 0)
                || aicomponent.timeCount >= 3f)
            {
                if (aicomponent.lastMoveAction != "")
                {
                    InputSystem.ReleaseAction(input, aicomponent.lastMoveAction);
                    aicomponent.lastMoveAction = "";
                }
                aicomponent.finishMove = false;
                aicomponent.timeCount = 0;
                aicomponent.Dir = 0;
                if (Random.Range(1, 3) == 1)
                {
                    GOTO_STATE1(aicomponent, input);
                }
                else
                {
                    GOTO_STATE2(aicomponent, input);
                }
                aicomponent.finishMove = false;
            }
        }
        else
        {
            aicomponent.finishMove = true;
            switch (aicomponent.Dir)
            {
                case 1:
                    InputSystem.PressAction(input, "move_up");
                    aicomponent.lastMoveAction = "move_up";
                    break;
                case 2:
                    InputSystem.PressAction(input, "move_right");
                    aicomponent.lastMoveAction = "move_right";
                    break;
                case 3:
                    InputSystem.PressAction(input, "move_down");
                    aicomponent.lastMoveAction = "move_down";
                    break;
                case 4:
                    InputSystem.PressAction(input, "move_left");
                    aicomponent.lastMoveAction = "move_left";
                    break;
                default:
                    break;
            }
        }
    }
    public static void skill_FindEnemy(AiComponent aicomponent, InputComponent input)//搜寻敌人
    {
        //attackableObject存在时
        if(aicomponent.finishMove)
        {
            if (aicomponent.AttackableObject != null)
            {
                if (aicomponent.lastMoveAction != "")
                {
                    InputSystem.ReleaseAction(input, aicomponent.lastMoveAction);
                    aicomponent.lastMoveAction = "";
                }
                aicomponent.finishMove = false;
                GOTO_STATE2(aicomponent, input);//发现敌人，进入追击状态
            }
        }
        //遇到障碍物，更改搜索方向,方向随机选择
        if(PhysicsUtil.OverlapBox(aicomponent.entity.gameobjectComponent.transform.position+aicomponent.entity.gameobjectComponent.transform.up*
            aicomponent.entity.moveComponent.moveSpeed, aicomponent.entity.gameobjectComponent.collider,0))
        {

            Debug.Log("障碍，改变搜索方向");
            if(aicomponent.lastMoveAction!="")
            {
                InputSystem.ReleaseAction(input, aicomponent.lastMoveAction);
                aicomponent.lastMoveAction = "";
            }       
            switch(Random.Range(1,5))
            {
                case 1:
                    InputSystem.PressAction(input, "move_up");
                    aicomponent.lastMoveAction = "move_up";
                    break;
                case 2:
                    InputSystem.PressAction(input, "move_right");
                    aicomponent.lastMoveAction = "move_right";
                    break;
                case 3:
                    InputSystem.PressAction(input, "move_down");
                    aicomponent.lastMoveAction = "move_down";
                    break;
                case 4:
                    InputSystem.PressAction(input, "move_left");
                    aicomponent.lastMoveAction = "move_left";
                    break;

            }
        }
        //用finishMove标记第一次搜索玩家是按照玩家最后一次的方位前进,之后的搜索方向随机
        if (aicomponent.finishMove == false) 
        {
            Debug.Log("向最后一次的方位：" + aicomponent.lastDir + "号位搜索");
            aicomponent.finishMove = true;
            switch (aicomponent.lastDir)
            {
                case 1:
                    InputSystem.PressAction(input, "move_up");
                    aicomponent.lastMoveAction = "move_up";
                    break;
                case 2:
                    InputSystem.PressAction(input, "move_right");
                    aicomponent.lastMoveAction = "move_right";
                    break;
                case 3:
                    InputSystem.PressAction(input, "move_down");
                    aicomponent.lastMoveAction = "move_down";
                    break;
                case 4:
                    InputSystem.PressAction(input, "move_left");
                    aicomponent.lastMoveAction = "move_left";
                    break;
                default:
                    InputSystem.PressAction(input, "move_up");
                    aicomponent.lastMoveAction = "move_up";
                    break;
            }
        }
    }
    //  speed  CD 
    //0   1     1          初始，以玩家的属性为基础展开
    //1   2    0.2
    //2   0.5  0.2
    //3   4     1
    //5   1     1
    public static void GOTO_STATE1(AiComponent aicomponent, InputComponent input)
    {
        Debug.Log("GOTO_1");
        clearInputState(aicomponent, input);
        aicomponent.Dir = 0;
        aicomponent.finishMove = false;
        aicomponent.timeCount = 0;
        if(aicomponent.MoveState==0)
        {
            aicomponent.entity.moveComponent.moveSpeed = aicomponent.entity.moveComponent.moveSpeed * 2;
            aicomponent.entity.tankComponent.gunEntity.gunComponent.shootCd = aicomponent.entity.tankComponent.gunEntity.gunComponent.shootCd / 5;
        }
        else if (aicomponent.MoveState == 2)
        {
            aicomponent.entity.moveComponent.moveSpeed = aicomponent.entity.moveComponent.moveSpeed * 4;
            
        }
        else if(aicomponent.MoveState==3)
        {
            aicomponent.entity.tankComponent.gunEntity.gunComponent.shootCd = aicomponent.entity.tankComponent.gunEntity.gunComponent.shootCd / 5;
            aicomponent.entity.moveComponent.moveSpeed = aicomponent.entity.moveComponent.moveSpeed / 2;
        }
        else if(aicomponent.MoveState==5)
        {
            aicomponent.entity.moveComponent.moveSpeed = aicomponent.entity.moveComponent.moveSpeed * 2;
            aicomponent.entity.tankComponent.gunEntity.gunComponent.shootCd = aicomponent.entity.tankComponent.gunEntity.gunComponent.shootCd / 5;
        }
        aicomponent.MoveState = 1;
        aicomponent.StateChangeCount += 1;
        if (aicomponent.StateChangeCount > 4) 
        {
            aicomponent.StateChangeCount = 0;
            GOTO_STATE3(aicomponent, input);
        }
    }
    public static void GOTO_STATE2(AiComponent aicomponent, InputComponent input)
    {
        Debug.Log("GOTO_2");
        clearInputState(aicomponent, input);
        aicomponent.Dir = 0;
        aicomponent.finishMove = false;
        aicomponent.timeCount = 0;
        if (aicomponent.MoveState == 0) 
        {
            aicomponent.entity.tankComponent.gunEntity.gunComponent.shootCd = aicomponent.entity.tankComponent.gunEntity.gunComponent.shootCd / 5;
            aicomponent.entity.moveComponent.moveSpeed = aicomponent.entity.moveComponent.moveSpeed / 2;
        }
        else if (aicomponent.MoveState == 1)
        {
            aicomponent.entity.moveComponent.moveSpeed = aicomponent.entity.moveComponent.moveSpeed / 4;
        }
        else if (aicomponent.MoveState == 3)
        {
            aicomponent.entity.moveComponent.moveSpeed = aicomponent.entity.moveComponent.moveSpeed / 8;
            aicomponent.entity.tankComponent.gunEntity.gunComponent.shootCd = aicomponent.entity.tankComponent.gunEntity.gunComponent.shootCd / 5; 
        }
        else if (aicomponent.MoveState == 5)
        {
            aicomponent.entity.moveComponent.moveSpeed = aicomponent.entity.moveComponent.moveSpeed / 2;
            aicomponent.entity.tankComponent.gunEntity.gunComponent.shootCd = aicomponent.entity.tankComponent.gunEntity.gunComponent.shootCd / 5;
        }
        aicomponent.MoveState = 2;
        aicomponent.StateChangeCount += 1;
        if (aicomponent.StateChangeCount > 4)
        {
            aicomponent.StateChangeCount = 0;
            GOTO_STATE3(aicomponent, input);
        }
    }
    public static void GOTO_STATE3(AiComponent aicomponent, InputComponent input)
    {
        Debug.Log("GOTO_3");
        clearInputState(aicomponent, input);
        aicomponent.Dir = 0;
        aicomponent.finishMove = false;
        aicomponent.timeCount = 0;
        if (aicomponent.MoveState == 0) 
        {
            aicomponent.entity.moveComponent.moveSpeed = aicomponent.entity.moveComponent.moveSpeed * 4;
        }
        else if (aicomponent.MoveState == 1)
        {
            aicomponent.entity.moveComponent.moveSpeed = aicomponent.entity.moveComponent.moveSpeed * 2;
            aicomponent.entity.tankComponent.gunEntity.gunComponent.shootCd = aicomponent.entity.tankComponent.gunEntity.gunComponent.shootCd * 5;
        }
        else if (aicomponent.MoveState == 2)
        {
            aicomponent.entity.moveComponent.moveSpeed = aicomponent.entity.moveComponent.moveSpeed * 8;
            aicomponent.entity.tankComponent.gunEntity.gunComponent.shootCd = aicomponent.entity.tankComponent.gunEntity.gunComponent.shootCd * 5;
        }
        else if (aicomponent.MoveState == 5)
        {
            aicomponent.entity.moveComponent.moveSpeed = aicomponent.entity.moveComponent.moveSpeed * 4;
        }
        aicomponent.MoveState = 3;
    }
    public static void GOTO_STATE4(AiComponent aicomponent, InputComponent input)
    {
        Debug.Log("GOTO_4");
        clearInputState(aicomponent, input);
        aicomponent.MoveState = 4;
        aicomponent.Dir = 0;
        aicomponent.finishMove = false;
        aicomponent.timeCount = 0;
    }
    public static void GOTO_STATE5(AiComponent aicomponent, InputComponent input)
    {
        Debug.Log("GOTO_5");
        clearInputState(aicomponent, input);
        aicomponent.Dir = 0;

        if (aicomponent.MoveState == 1)
        {
            aicomponent.entity.moveComponent.moveSpeed = aicomponent.entity.moveComponent.moveSpeed / 2;
            aicomponent.entity.tankComponent.gunEntity.gunComponent.shootCd = aicomponent.entity.tankComponent.gunEntity.gunComponent.shootCd * 5;
        }
        else if (aicomponent.MoveState == 2)
        {
            aicomponent.entity.moveComponent.moveSpeed = aicomponent.entity.moveComponent.moveSpeed * 2;
            aicomponent.entity.tankComponent.gunEntity.gunComponent.shootCd = aicomponent.entity.tankComponent.gunEntity.gunComponent.shootCd * 5;
        }
        else if (aicomponent.MoveState == 3)
        {
            aicomponent.entity.moveComponent.moveSpeed = aicomponent.entity.moveComponent.moveSpeed / 4;
        }
        aicomponent.MoveState = 5;
        aicomponent.finishMove = false;

    }


    public static void clearInputState(AiComponent aicomponent, InputComponent input)
    {

        if (aicomponent.lastMoveAction != "")
        {
            InputSystem.ReleaseAction(input, aicomponent.lastMoveAction);
            aicomponent.lastMoveAction = "";
        }
        
        if (aicomponent.entity.tankComponent.gunEntity.aiComponent.lastShootAction != "")
        {
            InputSystem.ReleaseAction(aicomponent.entity.tankComponent.gunEntity.inputComponent, aicomponent.entity.tankComponent.gunEntity.aiComponent.lastShootAction);
            aicomponent.entity.tankComponent.gunEntity.aiComponent.lastShootAction = "";
        }
        if (aicomponent.entity.tankComponent.gunEntity.aiComponent.lastShootAction1 != "")
        {
            InputSystem.ReleaseAction(aicomponent.entity.tankComponent.gunEntity.inputComponent, aicomponent.entity.tankComponent.gunEntity.aiComponent.lastShootAction1);
            aicomponent.entity.tankComponent.gunEntity.aiComponent.lastShootAction1 = "";
        }

    }

}
