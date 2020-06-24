﻿using System;
using System.Collections.Generic;
using UnityEngine;

// CooperationModel 和 WndCooper 实际上不止种族 还有 职业
public  class CooperationModel:ModelBase
{
    //当前战斗队列中的羁绊
    public Dictionary<int, Entanglement> playerEtgs = new Dictionary<int, Entanglement>();
    

    PlayerModel playerModel;

    protected override  void Init()
    {
        base.Init();
        playerModel = ModelManager.Get("PlayerModel") as PlayerModel;
        
        EventManager.RegistEvent(EventType.FightRoleUpdate, OnFightListUpdate);
        EventManager.RegistEvent(EventType.NewGameStart, ClearEtgs);

    }

    public void ClearEtgs(dynamic dy = null)
    {
        playerEtgs.Clear();
    }

    //
    public Cooperation GetPlayerCooper(int id)
    {
        if(!playerEtgs.ContainsKey(id))
        {
            playerEtgs.Add(id, new Cooperation(id));
        }
       
        return playerEtgs[id] as Cooperation;
    }

    public Profession GetPlayerPro(int id)
    {
        if (!playerEtgs.ContainsKey(id))
        {
            playerEtgs.Add(id, Profession.CreatePro(id));
        }

        return playerEtgs[id] as Profession;
    }

    //当战斗队列变化时
    void OnFightListUpdate( dynamic ob)
    {
        if(ob.type == EnumType.RoleUpdateType.RemoveFight ) //更新剩下的羁绊，但移出的角色不修改属性
        {
            OnReduceFight( ob.role);
        }
        else if(ob.type == EnumType.RoleUpdateType.FightToPre) //更新剩下的羁绊，移出的角色需要修改属性
        {
            OnFightToPre(ob.role);
        }
        else if( ob.type == EnumType.RoleUpdateType.AddFight) //添加了新角色
        {
            OnAddFight(ob.role);
        }
    }
    
    //更新当前队列中所有的羁绊数据（要求所有的角色都应该是 同样的羁绊数据处理状态)
    //可用于初始化
//     public void UpdatePlayerCooperData( )
//     {
//         
//         var playerRoles = playerModel.fightRoles;
//         for (int i = 0; i < playerRoles.Length; i++)
//         {
//             RoleBase role = playerRoles[i];
//             if (role != null)
//             {
//                 int cooperId = role.GetCooperId();
//                 if (playerCoopers.ContainsKey(cooperId))
//                 {
// 
//                 }
//                 else
//                 {
//                     playerCoopers.Add(cooperId, new Cooperation(cooperId));
// 
//                 }
//                 playerCoopers[cooperId].roles.Add(role);
//             }
// 
//         }
//         
//         foreach (var v in playerCoopers.Values)
//         {            
//             v.Update(playerRoles);
//         }
//         //通知羁绊更新完成 （显示更新)
//         EventManager.ExecuteEvent(EventType.CooperDataUpdate);
//     }

    //角色减少，该角色不需要更新属性，角色从战斗队列直接被删除，不再被玩家拥有
    void OnReduceFight(RoleBase role )
    {
        //先判断是不是有这个羁绊
        if(role == null)
        {           
            return;
        }
        
        for(int i=0;i<role.etlId.Length;i++)
        {
            int id = role.etlId[i];
            playerEtgs[id].RemoveNoEffect(playerModel.fightRoles, role);
        }
                 
        EventManager.ExecuteEvent(EventType.EtlDataUpdate);
    }

    //fightTopre 减少属性 角色从战斗队列回到准备队列 需要减少属性
    void OnFightToPre(RoleBase role )
    {
        if(role == null)
        {
           
            return;
        }
        else
        {
            for (int i = 0; i < role.etlId.Length; i++)
            {
                int id = role.etlId[i];
                playerEtgs[id].RemoveUpdate(playerModel.fightRoles, role);
            }
                       

            EventManager.ExecuteEvent(EventType.EtlDataUpdate);
        }
       

    }
    //战斗队列角色增加了,让这个角色获得羁绊
    void OnAddFight(RoleBase role)
    {
        if(role == null)
        {
            return;
        }
        else
        {
            Cooperation etl1 = GetPlayerCooper(role.GetCooperId());
            Profession etl2 = GetPlayerPro(role.GetProId());

            etl1.AddUpdate(playerModel.fightRoles, role);
            etl2.AddUpdate(playerModel.fightRoles, role);

            EventManager.ExecuteEvent(EventType.EtlDataUpdate);
        }
    }

}


