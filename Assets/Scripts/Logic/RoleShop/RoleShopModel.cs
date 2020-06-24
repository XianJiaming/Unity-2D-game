﻿using System;
using System.Collections.Generic;
using DataClass;
using UnityEngine;

//这里存了所有的角色 费用为key
public class RoleShopModel:ModelBase
{
    public bool isLock = false;

    public int[] roles;   //随机给出的角色池子，用id代替

    public const int Max_Role_Count = 5;

    //不同费用role的id Map，key为费用，value为id 数组
    public Dictionary<int, List<int>> roleCostMap = new Dictionary<int, List<int>>();
    PlayerModel playerModel;

    protected override void Init()
    {
        base.Init();

        playerModel = ModelManager.Get("PlayerModel") as PlayerModel;
        roles = new int[Max_Role_Count];
        //数据初始化
        for (int i=1;i<= Max_Role_Count; i++)
        {
            roleCostMap.Add(i, new List<int>());
        }

        var roleDatas = ConfigRoleManager.Instance().allDatas;
        foreach(var v in roleDatas)
        {
            roleCostMap[v.Value.cost].Add(v.Key);
        }

        AddEvent();
    }

    void AddEvent()
    {
        EventManager.RegistEvent(EventType.NewGameStart, OnNewGame);
        EventManager.RegistEvent(EventType.NextLevel, OnNextLevel);
       // EventManager.RegistEvent(EventType.RefreshShop, RefreshShopData);
    }
    public static int zhekou = 0;
    public void OnBuyRole(int index)
    {
       
        int id = roles[index];

        int cost = ConfigRoleManager.Instance().allDatas[id].cost;

        int price = cost - zhekou;

        if (playerModel.SetMoney(-price))
        {
            roles[index] = 0; //买了后 id 设置为0

            playerModel.AddRolePre(id);

            EventManager.ExecuteEvent(EventType.ShopDataUpdate);
        }
        else
        {
            WndTips.ShowTips("您的余额不足以购买该角色!");
        }
    }

    const int refreshCost = 2;
    public void OnClickRefresh()
    {        
        if(playerModel.SetMoney(-refreshCost))
        {
            RefreshShopData();
        }
        else
        {
            WndTips.ShowTips("您的金币不足以刷新商店");
        }

    }

    void OnNewGame(object ob = null)
    {
        isLock = false;
        RefreshShopData();
    }

    void OnNextLevel(object ob = null)
    {
        if (!isLock)
        {
            RefreshShopData();
            
        }
        //每一关判定刷新后 解锁
        isLock = false;
    }

    void RefreshShopData()
    {
        

        PlayerModel playerModel = ModelManager.Get("PlayerModel") as PlayerModel;

        int level = playerModel.peopleLevel;
        var data = ConfigRoleShopManager.Instance().allDatas[level];
        var rates = data.rate;
        //取得5个等级的概率范围
        int rate1 = rates[0]; //15
        int rate2 = rates[1] + rate1; //30
        int rate3 = rate2 + rates[2]; //45
        int rate4 = rate3 + rates[3]; //65
        int rate5 = rate4 + rates[4]; //90

        int[] levels = new int[Max_Role_Count];

        for(int i=0; i<Max_Role_Count;i++)
        {
            int x = UnityEngine.Random.Range(1, 100);
            if(x>=1 && x<= rate1)
            {
                levels[i] = 1;
            }
            else if(x> rate1 && x<= rate2)
            {
                levels[i] = 2;
            }
            else if(x > rate2 && x <= rate3)
            {
                levels[i] = 3;
            }
            else if(x > rate3 && x <= rate4)
            {
                levels[i] = 4;
            }
            else if( x > rate4 && x <= rate5)
            {

                levels[i] = 5;
            }
        }

        for(int i=0;i < levels.Length;i++)
        {
            var idList = roleCostMap[levels[i]];
            int index = UnityEngine.Random.Range(0, idList.Count);
            int id = idList[index]; //取出一个随机id

            roles[i] = id;


        }

        EventManager.ExecuteEvent(EventType.ShopDataUpdate);
    }

}
