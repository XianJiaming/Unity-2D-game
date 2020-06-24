using System;
using System.Collections.Generic;
using System.Linq;
using DataClass;
using DG.Tweening;
using UnityEngine;

public class SkillBase
{
    int level; //技能等级 与角色等级同步
    int id;
    List<MyFightEffect.SkillEffect> effects = new List<MyFightEffect.SkillEffect>();
    public string name
    {
        get;
        private set;
    }
    int[] targetParam;
    int targetQueue;

    RoleBase owner;


    public void SetId( int id, int level, RoleBase owner )
    {
        if(!ConfigSkillManager.Instance().allDatas.ContainsKey(id))
        {
            Debug.LogError("该技能id不存在" + id);
            return;
        }

        Dictionary<int, int[]>[] values = new Dictionary<int, int[]>[3];
        this.owner = owner;
        var datas = ConfigSkillManager.Instance().allDatas[id];
        this.id = id;
        name = datas.name;
        targetParam = datas.targetType;
       
        this.level = level;
        targetQueue = datas.targetQueue;
        values[0] = datas.level1;
        values[1] = datas.level2;
        values[2] = datas.level3;

        if(level == 1)
        {
            foreach(var v in datas.level1)
            {
                var effect = MyFightEffect.EffectFactory.CreatInstance(v.Key);
                effect.Init(v.Value[0], v.Value[1]);
                effects.Add(effect);
            }
        }
        else if(level == 2)
        {
            foreach (var v in datas.level2)
            {
                var effect = MyFightEffect.EffectFactory.CreatInstance(v.Key);
                effect.Init(v.Value[0], v.Value[1]);
                effects.Add(effect);
            }
        }
        else if(level == 3)
        {
            foreach (var v in datas.level3)
            {
                var effect = MyFightEffect.EffectFactory.CreatInstance(v.Key);
                effect.Init(v.Value[0], v.Value[1]);
                effects.Add(effect);
            }
        }
        else
        {
            Debug.LogError("技能等级配置错误");
        }

        for(int i =0;i<values.Length;i++)
        {

        }


        selfEffect = datas.selfEffect;
       
        wholeEffect = datas.wholeEffect;
        targetEffect = datas.targetEffect;

        InitDesc();


    }
    //数据变动
    void UseData(RoleBase owner,RoleBase target)
    {
        if (target != null)
        {
            for (int j = 0; j < effects.Count; j++)
            {
                effects[j].Effect(owner,target);
            }
        } 
    }
    string selfEffect;
    
    string wholeEffect;
    string targetEffect;

    //自身go
    float targetTime = 0.2f;

    //自己表现
    void UseSelfGo( RoleBase[] target)
    {
        GameObject fightRole = owner.fightRole;
        Vector3 orPosition = fightRole.transform.position;

        //表示作用范围
        int length = target.Length;
        //此时1个目标
        if (length == 1)
        {
            var oneTarget = target[0];
            
            if(oneTarget == null)
            {
                EndUse();
            }
            else
            fightRole.transform.DOMove(owner.GetTargetPosition(oneTarget), ConstConfig.moveSpeed).OnComplete(
           () =>
           {
               fightRole.transform.DOMove(orPosition, ConstConfig.moveSpeed).OnComplete(() => { EndUse(); });

               UseEnemyEffect(oneTarget); //特效

               UseEnemyGo(oneTarget);

               UseData(owner, oneTarget); //数据

               
           }
           );
        }
        //此时多个目标
        else if (length >1)
        {
            //影响目标数据
            for(int i=0;i<target.Length;i++)
            {
                
                var role = target[i];
                if (role == null) continue;
                
                UseEnemyEffect(role); //特效

                UseEnemyGo(role);

                UseData(owner, role); //数据
            }
            TimeManager.RegistOneTime((id) => { EndUse(); }, targetTime);
            
            //
        }
    }
   
    float selfEffectTime;
    //释放自身特效，并且设置特效时间
    void UseSelfEffect()
    {
        selfEffectTime = 0.4f;
        if (selfEffect == "null") return;

        var effectGo =  ObjectPool.GetGo(selfEffect);

        var effect = effectGo.GetComponent<ParticleSystem>();

        selfEffectTime = effect.main.duration;

        var sP = RectTransformUtility.WorldToScreenPoint(Camera.main, owner.fightRole.transform.position);

        var worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(sP.x, sP.y, 10));

        effectGo.transform.position = worldPoint;
    }
    //全屏特效
    void UseWholeEffect( )
    {
        if (wholeEffect.Equals( "null")) return;
        ObjectPool.GetGo(wholeEffect); //全屏特效
    }
    //敌方特效
    void UseEnemyEffect(RoleBase role )
    {
        if (targetEffect == "null") return;

        if(role==null)
        {
            Debug.LogError("播放enemy特效时 role 为空");
            return;
        }

        var effectGo = ObjectPool.GetGo(targetEffect);

        var sP = RectTransformUtility.WorldToScreenPoint(Camera.main, role.fightGoPosition);

        var worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(sP.x, sP.y, 10));

        effectGo.transform.position = worldPoint;
    }
    //敌方go
    void UseEnemyGo(RoleBase role)
    {
        //颤抖 自己就不抖了
        if (role == owner) return;

        role.fightRole.transform.DOShakePosition(0.5f,3);
    }
    public RoleBase[] Use(RoleBase[] team, RoleBase[] enemy)
    {
        
        RoleBase[] target = SkillHelp.GetSkillTarget(owner ,team, enemy, targetParam, targetQueue);
        
        UseSelfEffect();

        TimeManager.RegistOneTime(
            (id) => {
                UseWholeEffect();
                UseSelfGo(target);
            }
            , selfEffectTime * 0.6f);

        //是否需要自身特效？ 等待自身特效结束 是否需要全屏特效 是否需要敌方特效 等待敌方特效结束处理数据
        return target;
    }
   


    void EndUse()
    {
        FightManager.instance.isAnyoneAct = false;
    }

    string desc;
    public string GetDesc()
    {
        return desc;
    }

    void InitDesc()
    {
        var datas = ConfigSkillManager.Instance().allDatas[id];
        string skillDesc = datas.desc;
        desc = $"\n技能:{name}\n{skillDesc}";
    }

} 
   

