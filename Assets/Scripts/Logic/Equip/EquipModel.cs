using System;
using System.Collections.Generic;

//玩家装备数据
public class EquipModel:ModelBase
{

    //key 是等级 value是装备
    public static Dictionary<int, List<int>> equipIds = new Dictionary<int, List<int>>();
    // key 是 类型 value 是id list
    public static Dictionary<int, List<int>> typeEquipIds = new Dictionary<int, List<int>>();

    Equip[] equipBag = new Equip[18];

    public int GetEquipNumber( )
    {
        int count = 0;
        foreach(var e in equipBag)
        {
            if(e!=null)
            {
                count++;
            }
        }
        return count;
    }

    public bool IsEmpty
    {
        get
        {
            bool res = true;
            foreach(var e in equipBag)
            {
                if(e!=null)
                {
                    res = false;
                    break;
                }
            }

            return res;
        }
    }

    protected override void Init()
    {
        base.Init();

        typeEquipIds.Add(0, new List<int>());
        typeEquipIds.Add(1, new List<int>());

        var allEquipDatas = DataClass.ConfigEquipManager.Instance().allDatas;

        for(int i = 1;i<=5;i++)
        {
            equipIds.Add(i, new List<int>());
        }


        foreach(var e in allEquipDatas)
        {
            equipIds[e.Value.level].Add(e.Key);
            typeEquipIds[e.Value.equipType].Add(e.Key);

        }

        EventManager.RegistEvent(EventType.NewGameStart, Clear);

    }
    //还没有处理装备满了的情况
    public bool AddEquip(Equip equip)
    {
        if(equip==null)
        {
            return false;
        }
        int index = -1;
        for(int i = 0;i<equipBag.Length;i++)
        {
            if( equipBag[i] == null)
            {
                index = i;
                equipBag[i] = equip;
                break;
            }
        }
        if(index!= -1)
        {
            
            EventManager.ExecuteEvent(EventType.EquipUpdate, index);
            return true;
        }
        else
        {
            WndTips.ShowTips("背包已满，装备获取失败!");
            
            return false;
        }
        
    }

    public bool AddEquip(int id)
    {
        return AddEquip(new Equip(id));
    }

    public bool AddEquip(Equip equip, int index)
    {
        if(equipBag[index] == null)
        equipBag[index] = equip;
        else
        {
            UnityEngine.Debug.LogError("该装备位置不为空，不能添加新装备");
            return false;
        }
        EventManager.ExecuteEvent(EventType.EquipUpdate, index);
        return true;
    }

    public Equip RemoveEquip(int index)
    {
        var equip = equipBag[index];
        equipBag[index] = null;
        EventManager.ExecuteEvent(EventType.EquipUpdate, index);
        return equip;
    }

    public Equip GetEquip(int index)
    {
        return equipBag[index];
    }


    public void SellEquip(int index)
    {
        int price = RemoveEquip(index).Price;
        ModelManager.Get<PlayerModel>("PlayerModel").AddMoney(price);
    }

    void Clear(dynamic d)
    {
        for(int i =0;i< equipBag.Length;i++)
        {
            equipBag[i] = null;
        }
    }
}
