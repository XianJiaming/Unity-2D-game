using System.Collections.Generic;
using UnityEngine;


//获得奖励在playerModel里
public class PlayerModel:ModelBase
{
    

    public List<MyTalent.Talent> talents;

    //战斗队列的角色，用来上场的，但是战斗中不应该修改这个
    public  RoleBase[] fightRoles;

    //我此时拥有的未放入战斗队列的角色
    public RoleBase[] preRoles;

    public PlayerData playerData;

    //当前人口等级,与最大上阵人数一致
    public int peopleLevel ;
    public const int MaxPeopleLevel = 9;
    public const int  Max_Pre_Role_Count = 8;

    protected override void  Init()
    {
        base.Init();

        EventManager.RegistEvent(EventType.NewGameStart, OnNewGame);
        EventManager.RegistEvent(EventType.GameOver, OnGameOver);
        EventManager.RegistEvent(EventType.FightEnd, GetReward);
        EventManager.RegistEvent(EventType.FightVectory, OnVectory);
        
    }

    public void OnNewGame( object ob = null)
    {
        talents = new List<MyTalent.Talent>();
        fightRoles = new RoleBase[9];
        preRoles = new RoleBase[Max_Pre_Role_Count];

        playerData = new PlayerData(ConstConfig.startMoney, ConstConfig.startHp);

        peopleLevel = 2;

    }

    void OnGameOver(dynamic dy)
    {
        ClearTalent();
    }

    public void TalantEffect()
    {
        foreach(var t in talents)
        {
            t.Effect();
        }
    }
    //清除所有的天赋效果
    void ClearTalent()
    {
        var tempTalents = new List<MyTalent.Talent>();

        foreach (var v in talents)
        {
            tempTalents.Add(v);
        }
        foreach( var v in tempTalents)
        {
            v.RemoveEffect();
        }
    }

    //添加新天赋并产生效果
    public void AddTalent(int id )
    {
        var talent = MyTalent.Talent.CreatTalent(id);

        AddTalent(talent);
    }
    public void AddTalent(MyTalent.Talent talent)
    {
        talents.Add(talent);

        talent.Effect();

        ModelManager.Get<TalentModel>("TalentModel").canUseTalent.Remove(talent.id);
    }

    public int GetPreRoleCount()
    {
        int count = 0;
        for(int i=0;i< preRoles.Length;i++)
        {
            if (preRoles[i] != null)
                count++;
        }

        return count;
    }

    public int GetFightRoleCount( )
    {
        int count = 0;
        for(int i=0;i<fightRoles.Length;i++)
        {
            if( fightRoles[i] != null )
            {
                count++;
            }
        }
        return count;
    }

    public int GetMoney( )
    {
        return playerData.GetMoney();
    }
    /// <summary>
    /// 传入money的变化值,返回是否变化成功
    /// </summary>
    /// <param name="value"></param>
    public bool SetMoney(int value)
    {
        
        if(value>0)
        {
            playerData.AddMoney(value);
            return true;
        }
        else
        {
            return playerData.ReduceMoney( -value);
        }
  
    }
    //增加钱
    public void AddMoney(int value)
    {
        playerData.AddMoney(value);
    }

    public int GetHp()
    {
        return playerData.GetHp();
    }

    /// <summary>
    /// 根据value的正负决定 是增加还是减少
    /// </summary>
    /// <param name="value">变化值（必须标明正负)</param>
    public void SetHp(int value)
    {
        if(value>0)
        {
            playerData.AddHp(value);

        }
        else
        {
            playerData.ReduceHp(-value);
        }
    }

    /// <summary>
    /// 交换2个队列中的角色数据
    /// </summary>
    /// <param name="role">具体角色</param>
    /// <param name="fightIndex">即将落户的战斗队列索引</param>
    /// <param name="preIndex">准备队列索引</param>
    public bool ExChangeRolesBetPreAndFight( int fightIndex, int preIndex)
    {
        RoleBase fightRole = fightRoles[fightIndex];
        if( fightRole == null)
        {
            int count = GetFightRoleCount();
            if( count >= peopleLevel )
            {
                WndTips.ShowTips("战斗队列角色已达上限，不可再上阵角色！");
                
                return false;
            }
        }
        RoleBase preRole = preRoles[preIndex];
        
        RemoveFightRole(fightIndex, EnumType.RoleUpdateType.FightToPre); //减少属性并更新羁绊
        SetPreRole(fightRole, preIndex);//pre 来自fight
        
        AddFightRole(preRole, fightIndex);//判断该角色是否需要获取羁绊属性，更新羁绊
      
        return true;
    }
    //交换战斗队列中的角色数据
    public void ExChangeRolesBetFight( int newIndex, int orIndex)
    {
        RoleBase tempRole = fightRoles[newIndex];

        SetFightRole(fightRoles[orIndex], newIndex, EnumType.RoleUpdateType.Fight);
        SetFightRole(tempRole, orIndex, EnumType.RoleUpdateType.Fight);      

    }
    //交换准备队列中的角色数据
    public void ExChangeRolesBetPre( int newIndex, int orIndex)
    {
        RoleBase tempRole = preRoles[newIndex];
        SetPreRole(preRoles[orIndex], newIndex);
        SetPreRole(tempRole, orIndex);      
    }

    //人口升级了
     void UpdateLevel( int value = 1 )
    {
        

        peopleLevel += value;

        EventManager.ExecuteEvent(EventType.LevelUp);
    }

    public void TryUpPeopleLevel()
    {
        //先判等级
        if(peopleLevel >= MaxPeopleLevel)
        {
            WndTips.ShowTips("人口已达最大限度，不可继续升级!");
            return;
        }

        bool can = SetMoney(-4 * peopleLevel);
        if(can)
        {
            
            UpdateLevel();
        }
        else
        {
            WndTips.ShowTips("余额不足，不可升级人口！");
            
        }
    }


    //-----------------------------------------------------------------------------------羁绊
  
    //-----------------------------------------------------------------------------------羁绊

    /// <summary>
    /// 准备队列增加角色,（交换战斗队列与准备队列的角色时不走该接口
    /// </summary>
    /// <param name="role">角色</param>
    /// <param name="idx">角色即将所在准备队列中的索引</param>
    /// <param name="orIdx">角色在战斗队列中的索引</param>
    public bool AddRolePre( RoleBase role)
    {       
        //判断准备队列里有多少个角色
            int count = 0;
            for (int i = 0; i < preRoles.Length; i++)
            {
                if (preRoles[i] != null)
                    count++;
            }
            //小于最大的角色数量
            if (count < Max_Pre_Role_Count)
            {
                //找个空位置给他
                for (int i = 0; i < preRoles.Length; i++)
                {
                    if (preRoles[i] == null)
                    {
                        preRoles[i] = role;
                       
                        EventManager.ExecuteEvent(EventType.PreRoleUpdate, i);

                        TryRoleUpLevel(role.GetRoleId(), role.GetLevel());

                        break;
                    }
                }
                 return true;
            }
            else//角色满了
            {
                //先判断是否可以合成
                bool res = TryRoleUpLevel(role.GetRoleId(), role.GetLevel(),2);
                if(!res)
                WndTips.ShowTips("玩家准备区域拥有的角色达到最大数量");
                
                return res;
            }                    
    }
    //外界调用
   public bool CanRoleUpLevel(int roleId, int level, int composeCount = 3)
   {
        if (level == 3)
        {
            return false;
        }

        //找到可以升级的 n 个角色
        //2个队列中的索引
        List<int> preRs = new List<int>(composeCount);
        List<int> fightRs = new List<int>(composeCount);

        for (int i = 0; i < preRoles.Length; i++)
        {
            var role = preRoles[i];
            if (role == null)
                continue;

            if (role.GetRoleId() == roleId 
                && role.GetLevel() == level)
            {
                preRs.Add(i);
            }
        }
        for (int i = 0; i < fightRoles.Length; i++)
        {
            var role = fightRoles[i];
            if (role == null)
                continue;

            if (role.GetRoleId() == roleId && role.GetLevel() == level)
            {
                fightRs.Add(i);
            }
        }
        //移出角色
        if (preRs.Count + fightRs.Count == composeCount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //判断该角色是否可以升级,穿入该角色的id与等级,角色升级是直接生成一个新角色，还不是在原角色基础上修改
    bool TryRoleUpLevel(int roleId, int level, int composeCount = 3)
    {
        if(level == 3)
        {
            return false;
        }

        //找到可以升级的 n 个角色
        //2个队列中的索引
        List<int> preRs = new List<int>(composeCount);
        List<int> fightRs = new List<int>(composeCount);

        for(int i = 0;i< preRoles.Length;i++)
        {
            var role = preRoles[i];
            if (role == null)
                continue;

            if(role.GetRoleId() == roleId && role.GetLevel() == level)
            {
                preRs.Add(i);
            }
        }
        for(int i=0;i< fightRoles.Length;i++)
        {
            var role = fightRoles[i];
            if (role == null)
                continue;

            if (role.GetRoleId() == roleId && role.GetLevel() == level)
            {
                fightRs.Add(i);
            }
        }
        //移出角色
        if(preRs.Count + fightRs.Count == composeCount)
        {
            for(int i = 0;i<preRs.Count;i++)
            {
                preRoles[preRs[i]].RemoveAllEquip();
                RemoveRolePre(preRs[i], preRoles[ preRs[i] ].GetRoleId());
            }
            for(int i=0;i<fightRs.Count;i++)
            {
                RemoveFightRole(fightRs[i], EnumType.RoleUpdateType.RemoveFight);
            }
        }
        else
        {
            return false;
        }
        //获得升级后的角色，放到战斗队列中找到的第一个该角色
        //此处新生成的角色是因升级而成，所以因该派发 升级 事件,进行升级的特殊响应
        int index;
       
        if( fightRs.Count>0)
        {
            index = fightRs[0];
            RoleBase role = RoleFactory.CreateRole( roleId , level + 1);
            
            AddFightRole(role, index);
        }
        else
        {
           
            AddRolePre(roleId, level + 1);
        }

        AudioManager.instance.PlayEffect(ConstConfig.RoleUpLevel);

        return true;
    }


    //指定位置上覆盖一个新角色，原来该位置要保证为空
    public void AddFightRole( RoleBase role, int index)
    {
        if( fightRoles[index] != null)
        {
            Debug.LogError($"索引为{index}的位置上角色不为空，不能直接赋值新角色");
            return;
        }
               
        SetFightRole(role, index, EnumType.RoleUpdateType.AddFight);
        if(role!=null)
        TryRoleUpLevel(role.GetRoleId(), role.GetLevel());
    }
    public void SetPreRole( RoleBase role , int setIndex )
    {
        preRoles[setIndex] = role;
        EventManager.ExecuteEvent(EventType.PreRoleUpdate, setIndex);
    }

    void SetFightRole( RoleBase role, int setIndex, EnumType.RoleUpdateType type)
    {
        fightRoles[setIndex] = role;
        EventManager.ExecuteEvent(EventType.FightRoleUpdate, new UpdateRoleData(setIndex, type, role));
    }
  
    //玩家获得新角色,准备队列中增加新角色, 购买时 以id传参
    public void AddRolePre( int roleId, int level = 1)
    {
        RoleBase role = RoleFactory.CreateRole(roleId, level);      
        AddRolePre(role);
    }
    //准备队列中移出角色
    public void RemoveRolePre(int index ,int roleId = 0)
    {
        if(roleId != 0)
            if(preRoles[index].GetRoleId() != roleId)
            {
                Debug.LogError("移除的角色索引与id不一致");
            }
        
        preRoles[index] = null;
        EventManager.ExecuteEvent(EventType.PreRoleUpdate,index);
            
    }
    //移出角色,分两种，第一种需要移出的改属性，第二种不需要
    public void RemoveFightRole( int removeIndex, EnumType.RoleUpdateType type)
    {   

        RoleBase role = fightRoles[removeIndex];
        
        if ( role == null)
        {
            return;
        }
        if(type == EnumType.RoleUpdateType.RemoveFight)
        role.RemoveAllEquip();

        fightRoles[removeIndex] = null;
        EventManager.ExecuteEvent(EventType.FightRoleUpdate, new UpdateRoleData(removeIndex, type, role));
           
    }

    public void SellRole(int index,EnumType.RoleListType listType)
    {
        int price = 0;
        if(listType == EnumType.RoleListType.Fight)
        {
            price = fightRoles[index].GetPrice();
            RemoveFightRole(index, EnumType.RoleUpdateType.RemoveFight);
        }
        else if( listType == EnumType.RoleListType.Pre)
        {
            preRoles[index].RemoveAllEquip();
            price = preRoles[index].GetPrice();
            RemoveRolePre(index);
        }

        price -= RoleShopModel.zhekou;

        SetMoney( price);
       
    }

    public const int MaxEquipReward = 3;
    //获得关卡奖励(不包括金币，金币奖励在 playerData类中处理)
    void GetReward(dynamic dy)
    {
        GameLevelModel gameLevel = ModelManager.Get<GameLevelModel>("GameLevelModel");
        EquipModel equipModel = ModelManager.Get<EquipModel>("EquipModel");
        int curLevel = gameLevel.GetLevel();
        if( curLevel%ConstConfig.equipRewardLevel == 0)
        {
            //获得装备奖励
            //第一个装备的id为1,随机直接int里随机
           
            //获得随机3件装备
            List<Equip> equipIds = new List<Equip>(MaxEquipReward);

            //概率 30 30 20 10 10
            int gailv1 = 30;//1-30
            int gailv2 = 60;//31-60
            int gailv3 = 80;//61-80
            int gailv4 = 90;//81-90
            int gailv5 = 100;//91-100

            for(int i = 0;i< MaxEquipReward; i++)
            {
                int randomRes = Random.Range(1, 101);

                int resLevel = 0;

                if(randomRes>=1 && randomRes<=gailv1)
                {
                    resLevel = 1;
                }
                else if(randomRes>= gailv1+1 && randomRes<=gailv2)
                {
                    resLevel = 2;
                }
                else if(randomRes>= gailv2+1 && randomRes <= gailv3)
                {
                    resLevel = 3;
                }
                else if(randomRes >=gailv3+1 && randomRes <=gailv4)
                {
                    resLevel = 4;
                }
                else if(randomRes >= gailv4+1 && randomRes <= gailv5)
                {
                    resLevel = 5;
                }
                var targetIds = EquipModel.equipIds[resLevel];

                int id = targetIds[Random.Range(0, targetIds.Count)];
               
                Equip e = new Equip(id);
                equipIds.Add(e);
                equipModel.AddEquip(e);
            }

            ViewManager.Get<WndFightOver>("WndFightOver").ShowEquipReward(equipIds);

        }
        if( curLevel%ConstConfig.talentRewardLevel == 0)
        {
            if (ModelManager.Get<TalentModel>("TalentModel").canUseTalent.Count > 0)
                ViewManager.Get("WndTalent").SetVisible(true);          
        }
    }

    void OnVectory(dynamic dy)
    {
        AddMoney(5);
    }
    //拿一个空位置索引
    public int GetEmptyPreIndex( )
    {
        for(int i=0;i<preRoles.Length;i++)
        {
            if(preRoles[i] == null)
            {
                return i;
            }
        }
        return -1;
    }

}

public struct UpdateRoleData
{
    public int index;
    public EnumType.RoleUpdateType type;
    public RoleBase role;
    public UpdateRoleData(int index, EnumType.RoleUpdateType type, RoleBase role = null)
    {
        this.index = index;
        this.type = type;
        this.role = role;
    }

}


