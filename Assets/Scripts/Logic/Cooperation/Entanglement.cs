using System;
using System.Collections.Generic;
using UnityEngine;
using DataClass;

public abstract class Entanglement
{
    protected dynamic configData;

    protected int id;
    public int GetId()
    {
        return id;
    }
    public string name;

    //当前战斗队列中属于该羁绊的生效角色们
    public List<RoleBase> roles = new List<RoleBase>();

    public int etlCount; //羁绊数值（即，为该羁绊的所有不同的角色的数量）（相同角色不能让数量+1）
    public int effectIndex = -1; //羁绊生效的索引
    public int maxCount = 0;//最大羁绊生效数量

    //生效范围（所有角色还是该羁绊角色)
    public int[] range;

    public abstract void Init(int id);
   
    public Entanglement( int id)
    {
        this.id = id;
        Init(id);
    }

    protected string desc;
    
    public string GetDesc()
    {
        return desc;
    }

    protected abstract void InitDesc();
   

    

    //角色移出时 不需要为该角色修改属性（因为玩家不再拥有该角色）
    //roles 战斗队列中的所有角色, 此时roles 已经不包括 removeRole
    public void RemoveNoEffect(RoleBase[] allFightRoles, RoleBase removeRole)
    {
      
        //移出当前该羁绊里的角色的所有羁绊， 
        
        foreach (var role in roles)
        {
            if (role != removeRole && role != null)
                RemoveEffect(role, effectIndex);
        }
        //然后重新添加
        //先计算 有效角色数量（即 剔除重复的）

        Update(allFightRoles);


    }

    //初始化时调用，在战斗队列中 找出该羁绊的角色并获得当前羁绊的效果（前提是这些角色身上没有该羁绊效果)
    public void Update(RoleBase[] fightRoles)
    {

        UpdateCooperCount(fightRoles);
        UpdateEffectIndex();
        UpdateRoles(fightRoles);

        if(roles.Count>0)
        foreach (var role in roles)
        {
            if (role != null)
                GetEffect(role);
        }

    }

    //更新还属于该羁绊角色的属性 
    //添加了一个新角色，这个角色要单独处理
    // fightRoles 战斗队列中的角色 newRole本次添加的新角色
    public void AddUpdate(RoleBase[] fightRoles, RoleBase newRole)
    {
        //计算羁绊有校数值
        
        UpdateCooperCount(fightRoles);
        
        //更新羁绊里的角色数量，再找出生效索引，然后在具体生效。
        //羁绊里有角色才搞这些

        int orEffectIndex = effectIndex;
        UpdateEffectIndex();
        int newEffectIndex = effectIndex;


        //如果羁绊生效索引没有变
        if (orEffectIndex == newEffectIndex)
        {
            UpdateRoles(fightRoles);
            GetEffect(newRole);
        }
        else
        {
            foreach (var role in roles)
            {
                if (role != newRole)
                    RemoveEffect(role, orEffectIndex);
            }

            UpdateRoles(fightRoles);

            foreach (var role in roles)
            {
                if (role != null)
                    GetEffect(role);
            }

        }
    }

    //当有角色移出时，需要对剩下的角色做羁绊效果调整，同时该角色也会更新属性
    //第一个参数 是 战斗队列中的所有角色
    public void RemoveUpdate(RoleBase[] fightRoles, RoleBase removeRole)
    {
        RemoveEffect(removeRole, effectIndex);

        RemoveNoEffect(fightRoles, removeRole);

    }

    //更新该羁绊作用的当前角色,包括重复的对象
    protected abstract void UpdateRoles(RoleBase[] fightRoles);

    //获得羁绊效果时，此时该角色已经没有羁绊效果了
    public abstract void GetEffect(RoleBase role);

    public abstract void RemoveEffect(RoleBase role, int effectIndex);
   
    //根据人数来找生效的索引
    protected void UpdateEffectIndex()
    {
        int length = configData.counts.Length;

        effectIndex = -2;

        if (etlCount < configData.counts[0])
        {
            effectIndex = -1;

        }
        else if (etlCount > configData.counts[length - 1])
        {
            effectIndex = length - 1;
        }
        else
        {
            for (int i = 0; i < length; i++)
            {

                if (etlCount == configData.counts[i])
                {
                    effectIndex = i;
                    break;
                }
                if (etlCount < configData.counts[i])
                {
                    effectIndex = i - 1;
                    break;
                }
            }
        }


    }

    //更新羁绊数值,当前的羁绊有校数值
    public abstract void UpdateCooperCount(RoleBase[] fightRoles);
}


//--------------------------------------------------------------------------------------------------------------------
//种族羁绊
public class Cooperation : Entanglement
{
    
    public Cooperation(int id) : base(id)
    {

    }
    //用属性id，找到每个属性对应 羁绊等级的 数值，静态数值不能做更改
    public Dictionary<int, int[]> attributes;

    //百分比还是具体数值
    public int type;
    
    //构造方法里根据id赋予初始数据
    //将其初始化，可由外调用
    public override void Init( int id)
    {
        configData = ConfigCooperationManager.Instance().allDatas[id];
        maxCount = configData.counts[configData.counts.Length - 1];
     
        name = configData.name;
        attributes = configData.attributes;
        range = configData.range;
        type = configData.type;

        InitDesc();

        //下面的代码可以让羁绊回到出厂设置
        effectIndex = -1;
        etlCount = 0;
        roles.Clear();
    }
   
    protected override void InitDesc()
    {
        System.Text.StringBuilder namesb = new System.Text.StringBuilder("\n");

        var allRoles = ConfigRoleManager.Instance().allDatas;

        foreach (var role in allRoles.Values)
        {
            if (role.cooperId == id)
            {
                namesb.Append($"<color={ConstConfig.levelColor[role.cost]}>[{role.name}]</color> ");
            }
        }


        ConfigCooperation cooperData =ConfigCooperationManager.Instance().allDatas[GetId()];
        var attriData =ConfigAttributeManager.Instance().allDatas;
        var sb = new System.Text.StringBuilder();

        string type = null;
        if (cooperData.type == 1)
        {
            type = " ";
        }
        else if (cooperData.type == 2)
        {
            type = "% ";
        }

        for (int i = 0; i < cooperData.counts.Length; i++)
        {
            sb.Append($"<color=blue>人口:</color>{cooperData.counts[i]}\n");
            foreach (var v in cooperData.attributes)
            {
                sb.Append($"<color=yellow>{ attriData[v.Key].name}:</color>{v.Value[i]}{type}");
            }
            int range = cooperData.range[i];
            if (range == 1)
                sb.Append("\n<color=blue>范围:</color>只对该羁绊的角色生效\n");
            else if (range == 2)
                sb.Append("\n<color=blue>范围:</color>对我方所有角色生效\n");


        }
        sb.Append(namesb);

        desc = sb.ToString();
    }

    protected override void UpdateRoles(RoleBase[] fightRoles)
    {
        int rangeType = -1;
        if (effectIndex != -1)
            rangeType = range[effectIndex];
        else
        {
            //当前羁绊没有生效，所以不存在对角色产生作用
            roles.Clear();
            return;
        }

        this.roles.Clear();
        //只对该羁绊的角色生效
        if (rangeType == 1)
        {

            for (int i = 0; i < fightRoles.Length; i++)
            {
                if (fightRoles[i]?.GetCooperId() == id)
                {
                    this.roles.Add(fightRoles[i]);
                }
            }
        }
        //对所有角色生效
        else if (rangeType == 2)
        {
            foreach (RoleBase r in fightRoles)
            {
                this.roles.Add(r);
            }
        }


    }

    //让一个角色移出羁绊效果
    public override void RemoveEffect(RoleBase role, int effectIndex )
    {
        if (effectIndex == -1 | role == null)
            return;
       
        //数值
        if (type == 1)
        {
            foreach (var v in attributes)
            {
                role.attributes[v.Key] -= v.Value[effectIndex];
            }
        }
        //百分比
        else if (type == 2)
        {
            foreach (var v in attributes)
            {
               
                float changeVlu = DataClass.ConfigRoleManager.Instance().allDatas[role.GetRoleId()].attributes[role.GetLevel() - 1][v.Key]
                    * v.Value[effectIndex] * 1.0f / 100;

                role.attributes[v.Key] -= changeVlu;

            }
        }
        else
        {
            Debug.LogError("羁绊 数值 种类数据配置错误");
        }

    }
    //让一个角色获得羁绊效果
    public override void GetEffect(RoleBase role)
    {       

        if (effectIndex == -1)
            return;

        if(type == 1)
        {
            foreach (var v in attributes)
            {
                role.attributes[v.Key] += v.Value[effectIndex];
            }
        }
        else if(type == 2)
        {
            
            foreach (var v in attributes)
            { 
                float changeVlu = ConfigRoleManager.Instance().allDatas[role.GetRoleId()].attributes[role.GetLevel() - 1][v.Key] 
                    * v.Value[effectIndex]*1.0f/100;

               

                role.attributes[v.Key] += changeVlu;
            }
        }
        else
        {
            Debug.LogError("羁绊 数值 种类数据配置错误");
        }
        
    }

    //更新羁绊数值,当前的羁绊有校数值
    public override void UpdateCooperCount(RoleBase[] fightRoles)
    {
        etlCount = 0;
        List<int> roleIdList = new List<int>();
        for (int i = 0; i < fightRoles.Length; i++)
        {
            RoleBase role = fightRoles[i];
            if (role != null)
            {
                int cooperId = role.GetCooperId();
                int roleId = role.GetRoleId();
                if (!roleIdList.Contains(roleId) && cooperId == id)
                {
                    roleIdList.Add(roleId);
                }
            }
        }
        etlCount = roleIdList.Count;
    }
}



public abstract class Profession : Entanglement
{
    public static Profession CreatePro(int id)
    {
        return Activator.CreateInstance(Type.GetType($"Profession{id.ToString()}"),id) as Profession;
    }


    public Profession(int id) : base(id)
    {

    }

    protected override void InitDesc()
    {
        System.Text.StringBuilder namesb = new System.Text.StringBuilder("\n");

        var allRoles = ConfigRoleManager.Instance().allDatas;

        foreach (var role in allRoles.Values)
        {
            if (role.proId == id)
            {
                namesb.Append($"<color={ConstConfig.levelColor[role.cost]}>[{role.name}]</color> ");
            }
        }


        desc = $"{configData.desc}\n{namesb.ToString()}";
    }

    //更新羁绊数值,当前的羁绊有校数值
    public override void UpdateCooperCount(RoleBase[] fightRoles)
    {
        etlCount = 0;
        List<int> roleIdList = new List<int>();
        for (int i = 0; i < fightRoles.Length; i++)
        {
            RoleBase role = fightRoles[i];
            if (role != null)
            {
                int proId = role.GetProId();
                int roleId = role.GetRoleId();
                if (!roleIdList.Contains(roleId) && proId == id)
                {
                    roleIdList.Add(roleId);
                }
            }
        }
        etlCount = roleIdList.Count;

    }



    public override void Init(int id)
    {
        configData = ConfigProfessionManager.Instance().allDatas[id];

        name = configData.name;

        maxCount = configData.counts[configData.counts.Length - 1];
       
        range = configData.range;

        InitDesc();
    }

  
    
    //更新羁绊的作用效果角色
    protected override void UpdateRoles(RoleBase[] fightRoles)
    {
        roles.Clear();
        //先拿作用类型
        int rangeType = -1;
        if (effectIndex != -1)
            rangeType = range[effectIndex];
        else
        {
            //当前羁绊没有生效，所以不存在对角色产生作用
                      
            return;
        }

        
        //只对该羁绊的角色生效
        if (rangeType == 1)
        {
            for (int i = 0; i < fightRoles.Length; i++)
            {
                if (fightRoles[i]?.GetProId() == id)
                {
                    roles.Add(fightRoles[i]);
                }
            }
        }
        //对所有角色生效
        else if (rangeType == 2)
        {

            foreach (RoleBase r in fightRoles)
            {
                if(r!=null)
                roles.Add(r);
            }
        }
    }
}

