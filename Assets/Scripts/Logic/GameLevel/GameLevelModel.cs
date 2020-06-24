using System;
using System.Collections.Generic;
using DataClass;
using UnityEngine;

public class GameLevelModel:ModelBase
{
    //当前等级
    int currentLevel = 1;
    //最大等级用来控制 随机关卡的生成
    public const int MaxLevel = 25;


    public int GetLevel()
    {
        return currentLevel;
    }
    public void SetLevel(int value)
    {
        currentLevel = value;
    }
    //数组，  元素是字典   ， 字典的key是位置  ，value是角色信息
    Dictionary<int, int[]> [] roles;//

    protected override void Init()
    {
        base.Init();
        AddEvent();  
    }

    //游戏开始时加载关卡数据
    void OnNewGame(object ob = null)
    {
        SetLevel(1);
        
        var datas = ConfigGameLevelManager.Instance().allDatas;

        roles = new Dictionary<int, int[]>[datas.Count];
        foreach (var v in datas.Values)
        {
            roles[v.level - 1] = v.roles;
        }
        //初始等级为1
        SetAIRole();

    }

    void AddEvent( )
    {
       
        EventManager.RegistEvent(EventType.NewGameStart, OnNewGame);
        EventManager.RegistEvent(EventType.NextLevel, OnNextLevel);
    }

    public void OnNextLevel(object ob )
    {
        currentLevel++;
        SetAIRole();
    }

    public void SetAIRole()
    {
        


        Dictionary<int, int[]> currentRoles;
        if (currentLevel <= MaxLevel)//maxLevel
        {
            currentRoles = roles[currentLevel - 1];
        }
        else
        {
            //生成随机ai队列

            //先选一个职业id
            var allPro = ConfigProfessionManager.Instance().allDatas;
            List<int> proIds = new List<int>();
            foreach(var v in allPro.Keys)
            {
                proIds.Add(v);
            }
            int proId = proIds[UnityEngine.Random.Range(0, proIds.Count)];

            //根据这个id 凑满该职业的最大数量的角色
            var proData = allPro[proId];
            //该职业生效的最大数量
            int maxRoleNumbers = proData.counts[proData.counts.Length - 1];
            //获取该职业的所有角色id
            List<int> roleIds = new List<int>();

            foreach(var roleData in ConfigRoleManager.Instance().allDatas)
            {
                if(roleData.Value.proId == proId)
                {
                    roleIds.Add(roleData.Key);
                }
            }
            //拿到随机出来的角色id
            var resRoleIds = DataHelp.GetRandom(roleIds, maxRoleNumbers);

           
            //然后再补全剩下的role ID
            var restRoleIds = DataHelp.GetRandomKeyInDic(ConfigRoleManager.Instance().allDatas, 9 - maxRoleNumbers);

            

            int[] ids = new int[9];
            
            Array.ConstrainedCopy(resRoleIds, 0, ids, 0, resRoleIds.Length);
            Array.ConstrainedCopy(restRoleIds, 0, ids, resRoleIds.Length, restRoleIds.Length);

            

            //随机9把武器 随机9件防具

            int[] wuqiIds = DataHelp.GetRandom(EquipModel.typeEquipIds[0], 9);
            int[] fangjuIds = DataHelp.GetRandom(EquipModel.typeEquipIds[1], 9);

            //key 是索引 value 是 id 等级 武器 防具
            currentRoles = new Dictionary<int, int[]>();

            for(int i=0;i<9;i++)
            {
                currentRoles.Add(i, new int[] { ids[i], UnityEngine.Random.Range(2, 4), wuqiIds[i], fangjuIds[i] });
            }

        }
        


        var aiModel = ModelManager.Get("AIModel") as AIModel;
        aiModel.ClearRole();
        //TODO 给ai队列加入装备
        foreach (var v in currentRoles)
        {
            aiModel.SetAIRole(v.Key, v.Value[0], v.Value[1], v.Value[2], v.Value[3]);
        }
        aiModel.UpdateAiCooperData();
    }
}