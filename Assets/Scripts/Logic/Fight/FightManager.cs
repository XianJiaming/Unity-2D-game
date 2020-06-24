using System;
using System.Collections.Generic;
using UnityEngine;

class FightManager
{
    public const int Max_Fight_List_Number = 9;

    public static bool isFight = false;

    public static FightManager instance = new FightManager();
    private FightManager()
    {
        Init();

    }
    public List<RoleBase> playerDieRoles = new List<RoleBase>();
    public List<RoleBase> aiDieRoles = new List<RoleBase>();

    bool _isAnyoneAct = false;
    public bool isAnyoneAct {
        set
        {
            _isAnyoneAct = value;
            EventManager.ExecuteEvent(EventType.IsAnyOneActUp);
        }
        get
        {
            return _isAnyoneAct;
        }
    } 
    //战斗时浅拷贝
    public RoleBase[] playerRoles;

    public RoleBase[] aiRoles;



    public void Init( )
    {
        playerRoles = new RoleBase[Max_Fight_List_Number];
        aiRoles = new RoleBase[Max_Fight_List_Number];

        

        //SetPositionData();
    }

    //参加战斗的2方队列
    void InitFightList(RoleBase[] player, RoleBase[] ai)
    {
        for (int i = 0; i < playerRoles.Length; i++)
        {
            playerRoles[i] = player[i];
            var role = playerRoles[i];
            if (role != null)
            {
                /*role.SetGo(playerGos[i]);*/
                role.BeginFight();
                role.CopyAllValue();
                role.SetFightIndex(EnumType.FightListType.Player, i);
            
            }
            
        }
        GameLevelModel gameLevel = ModelManager.Get<GameLevelModel>("GameLevelModel");
        //如果进入随机关卡了 每一关敌人的全属性就+value%
        int curDis = gameLevel.GetLevel() - GameLevelModel.MaxLevel;

        float value = 10f;

        for (int i = 0; i < aiRoles.Length; i++)
        {
            aiRoles[i] = null;

            aiRoles[i] = ai[i];

            var role = aiRoles[i];

            if (role != null)
            {
                if(curDis >0)
                for(int j=0;j<9;j++)
                {
                    role.attributes[j] *= (1 + curDis * value / 100);
                }

                role.BeginFight();
                role.SetFightIndex(EnumType.FightListType.Ai, i);
            }
         
        }

    }
    GameObject[] playerRoleGos;
    GameObject[] aiRoleGos;
    public void BeginFight(RoleBase[] player, RoleBase[] ai,GameObject[] playerRoleGos, GameObject[] aiRoleGos)
    {
        isFight = true;
        curTime = 0;
        isOverTime = false;
        

        aiDieRoles.Clear();
        playerDieRoles.Clear();

        InitFightList(player, ai);
        SetGos(playerRoleGos, aiRoleGos);
    }

    public void StartFight()
    {
        InitNeedEnd();
        AddEvent();
        EventManager.ExecuteEvent(EventType.FightStart);
    }

     void SetGos(GameObject[] playerRoleGos, GameObject[] aiRoleGos)
    {
        this.playerRoleGos = playerRoleGos;
        this.aiRoleGos = aiRoleGos;

        for (int i = 0; i < 9; i++)
        {
            var role = playerRoles[i];
            if (role != null)
            {
                //role.SetFightPosition(playerPositions[i], i);
                role.SetFightGo(playerRoleGos[i]);
                role.SetTeamAndEnemy(playerRoles, aiRoles);

            }
            else
            {
                playerRoleGos[i].SetActive(false);
            }
            role = aiRoles[i];
            if (role != null)
            {
                //role.SetFightPosition(aiPositions[i], i);
                role.SetFightGo(aiRoleGos[i]);
                role.SetTeamAndEnemy(aiRoles, playerRoles);

            }
            else
            {
                aiRoleGos[i].SetActive(false);
            }
        }

    }
    //玩家是否胜利
    bool isPlayerSuccess = false;

    //战斗结束后 判断谁赢了
    void EndFight()
    {
        //先处理本次战斗结果的相关结算
        //再level++
        //判断是玩家赢了吗
        isFight = false;
        isPlayerSuccess = false;
        AudioManager.instance.BgAudioSource.pitch = 1.0f;
        Time.timeScale = 1f;
        for( int i=0;i<playerRoles.Length;i++)
        {
            if( playerRoles[i]!=null)
            {
                isPlayerSuccess = true;
                break;
            }
        }

        var pm = ModelManager.Get<PlayerModel>("PlayerModel");
        //当前并无角色战斗结束后逻辑        

        for (int i = 0; i < pm.fightRoles.Length; i++)
        {
            //if (playerRoles[i] != null)
            pm.fightRoles[i]?.EndFight();               
        }

        //释放ai角色的go资源
        for (int i = 0; i < aiRoles.Length; i++)
        {
            var airole = aiRoles[i];
            if (airole != null)
            {
                
                 airole.fightRole = null;
                 airole.RemoveAIEquip();
                
                    
            }
               
        }
        foreach (var v in aiDieRoles)
        {
            if(v!= null)
            {
                v.fightRole = null;
                v.RemoveAIEquip();
            }
            
        }



        LevelEnd(isPlayerSuccess);

        EventManager.ExecuteEvent(EventType.FightEnd);

        NextLevel();
    }
    //战斗结束后根据 关卡 判断是否继续游戏
    void NextLevel( )
    {
        int curLevel = (ModelManager.Get("GameLevelModel") as GameLevelModel).GetLevel();

        int hp = (ModelManager.Get("PlayerModel") as PlayerModel).GetHp();

        if (curLevel != 100 && hp>0)
        {
            
            EventManager.ExecuteEvent(EventType.NextLevel);
        }           
        else if(curLevel == 100 && hp>0)
        {
            EventManager.ExecuteEvent(EventType.GameVectory);
            EventManager.ExecuteEvent(EventType.GameOver);
            
        }
    }

    //根据玩家胜利与否处理结果
    void LevelEnd( bool isSuccess)
    {
        PlayerModel playerModel = ModelManager.Get("PlayerModel") as PlayerModel;
        if (isSuccess)
        {                       
            EventManager.ExecuteEvent(EventType.FightVectory);
            
        }
        else
        {
            int value = GetFailHurt();
            
            ViewManager.Get<WndFightOver>("WndFightOver").SetReduceHp(value);

            EventManager.ExecuteEvent(EventType.FightFail);

            playerModel.AddMoney(8);

            playerModel.SetHp(-value);
        }
    }
    //战斗失败的血量减少公式
    public float failReduceHpParam = 1.0f;
    int GetFailHurt()
    {
        int value = 0;
        foreach(var role in aiRoles)
        {
            if(role!=null)
            value += role.cost;
        }

        return (int)(value * failReduceHpParam);
    }

    bool needEnd = false;
    void InitNeedEnd( )
    {
        int temp = 0;
        foreach(var v in playerRoles)
        {
            if( v != null)
            {
                temp++;
                break;
            }
        }
        foreach (var v in aiRoles)
        {
            if (v != null)
            {
                temp++;
                break;
            }
        }
        if (temp != 2)
            needEnd = true;
        else
            needEnd = false;

    }

    float curTime = 0;
    float maxFightTime = 90;
    bool isOverTime = false;

    float overTimeBL = 4f;

    void OnOverTime()
    {
        WndTips.ShowTips("所有角色伤害提升500%!\n战斗速度提升1.5倍！");
        Time.timeScale = 1.5f;
        AudioManager.instance.BgAudioSource.pitch = 1.5f;

        foreach (RoleBase role in playerRoles)
        {
            if (role != null)
            {
                role.attackValueParam += overTimeBL;
                role.skillParam += overTimeBL;
            }
        }
        foreach (RoleBase role in aiRoles)
        {
            if (role != null)
            {
                role.attackValueParam += overTimeBL;
                role.skillParam += overTimeBL;
            }
        }
    }


    public void Update(dynamic ob)
    {

        if(needEnd)
        {
            RemoveEvent();

            ObjectPool.Clear();

            TimeManager.RegistOneTime((id) => {  EndFight(); }, 2f);
   
            return;
        }

        //超时处理
        curTime += Time.deltaTime;
        if( curTime >= maxFightTime && !isOverTime)
        {
            isOverTime = true;

            OnOverTime();
        }

       
        for (int i = 0;i< playerRoles.Length;i++)
        {
            if (playerRoles[i] != null)
                playerRoles[i].Update();
        }
        for (int i = 0; i < aiRoles.Length; i++)
        {
            if (aiRoles[i] != null)
                aiRoles[i].Update();
        }
    }

    //角色挂了 修改数据
    void OnRoleDie(object ob)
    {
        if (ob == null) return;
        //有角色挂了，将其移出战斗队列
        RoleBase role = ob as RoleBase;
       
       
        if(role.fightRole!=null)
        role.fightRole.SetActive(false);
        AddRoleDieList(role);
        Remove(role.team, role.positionIndex);

        //数据修改完毕

        //判断是否需要结束战斗
        needEnd = true;
        for(int i=0;i<role.team.Length;i++)
        {
            if(role.team[i] != null)
            {
                needEnd = false;
                break;
            }
        }
    }
    //
    void AddRoleDieList(RoleBase role)
    {
        if(role.fightListType == EnumType.FightListType.Ai)
        {            
            aiDieRoles.Add(role);
        }
        else if( role.fightListType == EnumType.FightListType.Player)
        {
            playerDieRoles.Add(role);
        }
    }
    

    public void Remove(RoleBase[] team,int index)
    {
        team[index] = null;
    }
    /// <summary>
    /// 复活一个挂掉的角色
    /// </summary>
    /// <param name="role"></param>
    public void ResuscitateFightRole(RoleBase role)
    {
        //role.BackAllValue(); //复活后属性要回退到初始状态
        role.BeginFight( role.WholeHurtValue);
        
        //先找个位置
        int index = -1;
        for(int i = 0;i<role.team.Length;i++)
        {
            if(role.team[i] == null)
            {
                index = i;
                break;
            }
        }
        if(index!=-1)
        {
            //送回去
            role.team[index] = role;
            role.SetFightPositionIndex(index);
            if(role.fightListType == EnumType.FightListType.Player)
            {
                UIListener.GetUIListener(playerRoleGos[index]).param = role;
                role.SetFightGo(playerRoleGos[index]);
                
                playerDieRoles.Remove(role);
            }
                
            else if(role.fightListType == EnumType.FightListType.Ai)
            {
                UIListener.GetUIListener(aiRoleGos[index]).param = role;
                role.SetFightGo(aiRoleGos[index]);
                
                aiDieRoles.Remove(role);
            }
        }
    }


    void AddEvent()
    {

        EventManager.RegistEvent(EventType.Update, Update);
        EventManager.RegistEvent(EventType.RoleDie, OnRoleDie);
    }
    void RemoveEvent()
    {
       
        EventManager.UnRegistEvent(EventType.Update, Update);
        EventManager.UnRegistEvent(EventType.RoleDie, OnRoleDie);
    }
}