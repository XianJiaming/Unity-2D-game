using System;
using System.Collections.Generic;

//技能特效
namespace MyFightEffect {


    public class EffectFactory
    {
        public static SkillEffect CreatInstance(int id)
        {
            return Activator.CreateInstance(Type.GetType($"MyFightEffect.Effect{id.ToString()}")) as SkillEffect;
        }
    }



    public abstract class SkillEffect
    {
        WndTips wndTips;

        protected string name;
        RoleBase owner;
        protected int time;
        protected int value;
        public void Init( int time,int value)
        {
            wndTips = ViewManager.Get<WndTips>("WndTips");

            this.time = time;

            this.value = value;
        }

        public virtual void Effect(RoleBase owner,RoleBase role)
        {

            if(time == 0)
            {
                if(name != null)
                wndTips.ShowMsg(name, role.fightTipPosition, UnityEngine.Color.yellow, 1);
            }
            else
            {
                if (name != null)
                    wndTips.ShowMsg(name, role.fightTipPosition, UnityEngine.Color.yellow, time +1, 50, 40);
            }
        }     
    }

    //眩晕
    public class Effect10 : SkillEffect
    {
//         public Effect10()
//         {
//             name = "眩晕";
//         }

        public override void Effect(RoleBase owner,  RoleBase role)
        {
            RoleHelper.SetXuanYun(role, time);
        }
    }
    //减速
    public class Effect11 : SkillEffect
    {
        public Effect11()
        {
            name = "减速";
        }

        public override void Effect(RoleBase owner,  RoleBase role)
        {
            base.Effect(owner, role);
            int changeValue = (int)(role.attributes[2] * (1.0f * value) / 100);
            role.attributes[2] -= changeValue;

            int timeId = TimeManager.Regist(
                (int id) =>
                {
                    if(FightManager.isFight)
                    role.attributes[2] += changeValue;
                },
                time, 0, 1, true);

        }
    }

    //秒杀
    public class Effect12 : SkillEffect
    {
        public Effect12()
        {
            name = "斩杀";
        }
        public override void Effect(RoleBase owner, RoleBase role)
        {
            base.Effect(owner, role);
            role.Die();
        }
    }

    //无敌
    public class Effect13 : SkillEffect
    {
        public Effect13()
        {
            name = "不灭";
        }
        public override void Effect(RoleBase owner, RoleBase role)
        {
            base.Effect(owner, role);
            role.SetWd(true);
            int timeId = TimeManager.Regist(
                (int id) =>
                {
                    role.SetWd(false);
                   
                },
                time, 0, 1, true);
        }
    }


    //治疗
    public class Effect14 : SkillEffect
    {

        public override void Effect(RoleBase owner, RoleBase role)
        {
            base.Effect(owner, role);
            role.GetTreat(value);
        }
    }

    //伤害
    public class Effect15 : SkillEffect
    {
        public override void Effect(RoleBase owner, RoleBase role)
        {
            int hurtValue = (int)(value * owner.skillParam);
            owner.SkillValue += hurtValue;
            owner.WholeHurtValue += hurtValue;
            role.BeSkillAttack(hurtValue);

        }
    }

    //沉默
    public class Effect16 : SkillEffect
    {
        public Effect16()
        {
            name = "无言";
        }
        public override void Effect(RoleBase owner, RoleBase role)
        {
            base.Effect(owner, role);
            role.SetSilent(true);
            int timeId = TimeManager.RegistOneTime(
                (int id) =>
                {
                    role.SetSilent(false);
                },
                time, true);
        }
    }

    //强化,一定时间内目标 所有属性提升xx%
    public class Effect17 : SkillEffect
    {

        public Effect17()
        {
            name = "祝福";
        }
        

        public override void Effect(RoleBase owner, RoleBase role)
        {
            base.Effect(owner, role);
            
            int[] attributes = new int[5];

            
            for (int i=0;i<5;i++)
            {
                attributes[i] = (int)(role.attributes[i] * value  / 100);

                role.attributes[i] += attributes[i];
            }

            TimeManager.RegistOneTime((id) =>
            {
                
                if(FightManager.isFight == false )
                {
                    //如果战斗已经结束 或者角色已经挂了 就不处理
                    return;
                }
                for (int i = 0; i < 5; i++)
                {                   
                    role.attributes[i] -= attributes[i];
                }
            }, time, true);

        }
    }


    //狂暴,造成的伤害提升x%,受到的伤害增加(x+15)%,持续m秒
    public class Effect18 : SkillEffect
    {
        
        public override void Effect(RoleBase owner, RoleBase role)
        {
            base.Effect(owner, role);
            float param = value * 1.0f / 100;
            owner.skillParam += param;
            owner.attackValueParam += param;
            owner.getHurtParam += (param + 0.15f);

            TimeManager.RegistOneTime((id) =>
            {
                if(FightManager.isFight)
                {
                    owner.skillParam -= param;
                    owner.attackValueParam -= param;
                    owner.getHurtParam -= (param + 0.15f);
                }
               
            }, time, true);

        }
    }
    //复活x名角色
    public class Effect19 : SkillEffect
    {
        public Effect19()
        {
            name = "重生";
        }
        public override void Effect(RoleBase owner, RoleBase role)
        {
            base.Effect(owner, role);
            List<RoleBase> target;
            if(owner.fightListType == EnumType.FightListType.Ai)
            {
                target = FightManager.instance.aiDieRoles;
            }
            else if(owner.fightListType == EnumType.FightListType.Player)
            {
                target = FightManager.instance.playerDieRoles;
            }
            else
            {
                target = null;
                UnityEngine.Debug.LogError("复活 目标队列参数不正确");
            }
            if(target.Count!=0)
            {
                
                RoleBase[] roles = DataHelp.GetRandom<RoleBase>(target, value);
                
                foreach (var ro in roles)
                {

                    FightManager.instance.ResuscitateFightRole(ro);
                }
            }
           
            
        }
    }
}



