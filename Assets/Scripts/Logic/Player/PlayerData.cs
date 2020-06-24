using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{

    int money;
    int Hp;

    public int GetHp()
    {
        return Hp;
    }

    void SetHp(int value)
    {
        Hp = value;
        EventManager.ExecuteEvent(EventType.PlayerHpUpdate);

    }


    public void AddHp(int value)
    {
        SetHp(GetHp() + value);
    }

    public void ReduceHp(int value)
    {
        SetHp(GetHp() - value);
        if(GetHp()<=0 )
        {
            //玩家失败
            EventManager.ExecuteEvent(EventType.GameFial);
            //玩家回到主界面
            EventManager.ExecuteEvent(EventType.GameOver);
        }
    }


    public PlayerData(int Money, int Hp = 20)
    {
        money = Money;
        this.Hp = Hp;
        AddEvent();
    }

    void AddEvent()
    {
        EventManager.RegistEvent(EventType.FightEnd, OnNextLevel);
    }

    //下一关时获得金币
    void OnNextLevel(object ob =null)
    {
        int curtMoney = GetMoney();
        int lixi = curtMoney / 10;
        //每一关固定5金币 //每10金币1利息，利息无上限
        AddMoney(5 + lixi);
    }


    public int GetMoney()
    {
        return money;
    }

    void SetMoney(int value)
    {
        money = value;
        EventManager.ExecuteEvent(EventType.MoneyUpdate);
    }

    public void AddMoney(int value)
    {
        SetMoney(money + value);
    }
    public bool ReduceMoney(int value)
    {
        if(money - value <0)
        {

            return false;
        }
        else
        {
            SetMoney(money - value);
            return true;
        }

    }



}
