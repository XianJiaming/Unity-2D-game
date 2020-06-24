﻿using System;
using System.Collections.Generic;
using DataClass;
using MyEquipEffect;

 public  class Equip
{
   
    enum EquipType
    {
        武器 = 0,
        防具 = 1
    }
    //装备特殊效果
    //public List<EquipEffect> effects = new List<EquipEffect>();

    List<EquipEffect> effects = new List<EquipEffect>();

    public int id
    {
        get;
        private set;
    }
    public string name { get; private set; }

    public int equipType
    {
        get;
        private set;
    }
    public int level { private set; get; }

    public int Price
    {
        get
        {
            return level * 2;
        }
       
    }
    Dictionary<int, int> attributes = new Dictionary<int, int>();

    public Equip(int id)
    {
        Init(id);
    }
    //更新初始数据
    public void Init(int id)
    {
        this.id = id;
        var datas = ConfigEquipManager.Instance().allDatas[id];
        name = datas.name;
        equipType = datas.equipType;
        level = datas.level;
        
        foreach(var v in datas.attributes)
        {
            attributes.Add(v.Key, v.Value);
        }
        //TODO 生成特殊效果
        var effectIds = datas.effects;
        foreach(int vid in effectIds)
        {
            if(vid!=0)
            effects.Add(EquipEffect.CreatEffect(vid));
        }

        if (attributes.ContainsKey(-1))
            attributes = null;

        InitDesc();
    }

    //穿戴装备
    public void Load(RoleBase role)
    {
        if(attributes ==null)
        {
            return;
        }
        foreach(var v in attributes)
        {
            role.attributes[v.Key] += v.Value;
        }

        foreach(var e in effects)
        {
            e.OnEquipUp(role);
        }
    }
    //卸载装备
    public void UnLoad(RoleBase role)
    {
        if (attributes == null)
        {
            return;
        }
        foreach (var v in attributes)
        {
            role.attributes[v.Key] -= v.Value;
        }
        foreach (var e in effects)
        {
            e.OnEquipDown(role);
        }
    }
    //初始化描述信息
    // 名字 属性 类型 品阶
    public string desc
    {
        get;
        private set;
    }

    void InitDesc()
    {
        var attributeData = DataClass.ConfigAttributeManager.Instance().allDatas;

        
        System.Text.StringBuilder attrString = new System.Text.StringBuilder();
        //属性
        //#E79617
        attrString.Append($"<color={ConstConfig.attributeColor}>");
        if (attributes!= null)
        {
            foreach(var v in attributes)
            {
                attrString.Append($"{attributeData[v.Key].name}:{v.Value.ToString()}  ");
            }
        }
        attrString.Append("</color>");
        //特殊效果
        //#0749A4
        attrString.Append($"\n<color={ConstConfig.skillColor}>");
        foreach (var v in effects)
        {
            attrString.Append($"\n【{v.name}】{v.desc}");
        }
        attrString.Append("</color>");

        desc = $"\n<size=35><color={ConstConfig.levelColor[level]}>{name}</color></size>  <color={ConstConfig.typeColor}>【{((EquipType)equipType).ToString()}】</color>\n\n{attrString.ToString()}";
    }
}   
 
