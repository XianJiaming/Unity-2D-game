using System;
using System.Collections.Generic;

namespace MyEquipEffect
{
    //装备特殊效果父类
    public abstract class EquipEffect
    {
        public string name;
        public string desc;

        public void Init(int id)
        {
            var datas = DataClass.ConfigEquipEffectManager.Instance().allDatas[id];
            name = datas.name;
            desc = datas.desc;
        }

        public static EquipEffect CreatEffect(int id)
        {
            EquipEffect efct = Activator.CreateInstance(Type.GetType($"MyEquipEffect.EquipEffect{id.ToString()}")) as EquipEffect;
            efct.Init(id);
            return efct;
        }

        //在装备的穿戴和卸载的时候会被回调
        public abstract void OnEquipUp(RoleBase owner);
        public abstract void OnEquipDown(RoleBase owner);

        //self 装备穿戴者 ,可以不使用这个方法
        public virtual void Effect(RoleBase self, RoleBase[] target) { }
        

    }

    //普通攻击吸血 10%
    public class EquipEffect101 : EquipEffect
    {
        protected float param = 0.1f;
        public override void Effect(RoleBase self, RoleBase[] target)
        {
            //击中了就回血
            if(self.isAttackSuccess&&self.isFight)
            self.GetTreat((int)(self.HurtValue * param));
        }

        public override void OnEquipUp(RoleBase owner)
        {
            owner.afterAttack += Effect;
        }
        public override void OnEquipDown(RoleBase owner)
        {
            owner.afterAttack -= Effect;
        }

    }

    //普通攻击吸血 20%
    public class EquipEffect102 : EquipEffect101
    {
        public EquipEffect102()
        {
            param = 0.2f;
        }


    }

    //普通攻击吸血 30%
    public class EquipEffect103 : EquipEffect101
    {
        public EquipEffect103()
        {
            param = 0.3f;
        }
    }

    //技能吸血 10 %
    public class EquipEffect111 : EquipEffect
    {
        protected float param = 0.1f;

        public override void Effect(RoleBase self, RoleBase[] target)
        {
            self.GetTreat((int)(self.SkillValue*param));
        }

        public override void OnEquipDown(RoleBase owner)
        {
            owner.afterSkill -= Effect;
        }

        public override void OnEquipUp(RoleBase owner)
        {
            
            owner.afterSkill += Effect;
        }
    }

    //技能吸血 20 %
    public class EquipEffect112 : EquipEffect111
    {
        public EquipEffect112()
        {
            param = 0.2f;
        }

        
    }

    //技能吸血 30 %
    public class EquipEffect113 : EquipEffect111
    {
        public EquipEffect113()
        {
            param = 0.3f;
        }

    }

    //普通攻击额外回蓝20点
    public class EquipEffect12 : EquipEffect
    {
        protected int value = 20;
        public override void Effect(RoleBase self, RoleBase[] target)
        {
            if(self.isFight)
            self.AddMp(value);

        }

        public override void OnEquipDown(RoleBase owner)
        {
            owner.afterAttack -= Effect;
        }

        public override void OnEquipUp(RoleBase owner)
        {
            
            owner.afterAttack += Effect;
        }
    }
    //普通攻击额外回蓝30点
    public class EquipEffect121 : EquipEffect12
    {
        public EquipEffect121()
        {
            value = 30;
        }
    }

    //普通攻击让目标减少 5%的生命
    public class EquipEffect13 : EquipEffect
    {
        protected float param = 0.05f;

        public override void Effect(RoleBase self, RoleBase[] target)
        {
            foreach(var role in target)
            {
                if(role.isFight)
                {
                    int value = (int)(role.attributes[5] * param);
                    role.GetHurt(value);
                }
               

            }
        }

        public override void OnEquipDown(RoleBase owner)
        {
            owner.afterAttack -= Effect;
        }

        public override void OnEquipUp(RoleBase owner)
        {
            
            owner.afterAttack += Effect;
        }
    }
    //普通攻击让目标减少 10%的生命
    public class EquipEffect131 : EquipEffect13
    {
        public EquipEffect131()
        {
            param = 0.1f;
        }
    }

    //技能数值提升50%
    public class EquipEffect14 : EquipEffect
    {
        protected float param = 0.5f;

        public override void OnEquipDown(RoleBase owner)
        {
            owner.skillParam -= param;
        }

        public override void OnEquipUp(RoleBase owner)
        {
            
            owner.skillParam += param;
        }
    }

    //技能数值提升30%
    public class EquipEffect141 : EquipEffect14
    {
        public EquipEffect141()
        {
            param = 0.3f;
        }
    }

    //技能数值提升70%
    public class EquipEffect142 : EquipEffect14
    {
        public EquipEffect142()
        {
            param = 0.7f;
        }
    }

    //重击。每次普通攻击让目标30%概率眩晕2s
    public class EquipEffect15 : EquipEffect
    {
        protected int param = 30;
        protected float time = 2f;

        public override void Effect(RoleBase self, RoleBase[] target)
        {

            int tempInt = UnityEngine.Random.Range(1, 101);
            if(tempInt<= param)
            {
                foreach(RoleBase role in target)
                {
                    if(role.isFight)
                    RoleHelper.SetXuanYun(role, time);
                    
                }
            }
        }

        public override void OnEquipDown(RoleBase owner)
        {
            owner.afterAttack -= Effect;
        }
        public override void OnEquipUp(RoleBase owner)
        {
            owner.afterAttack += Effect;
        }
    }

    //重击。每次普通攻击让目标50%概率眩晕1.2s
    public class EquipEffect151: EquipEffect15
    {
        public EquipEffect151()
        {
            param = 50;
            time = 1.2f;
        }
    }

    //神速。每次行动结束 速度+4
    public class EquipEffect16 : EquipEffect
    {
        protected RoleBase owner;
        protected int value = 4;
        //战斗结束后增益的速度要还回去
        //每回合结束时调用该方法
        public override void Effect(RoleBase self, RoleBase[] target)
        {          
            self.attributes[2] += value;
            
        }
            
     


        public override void OnEquipDown(RoleBase owner)
        {
            this.owner = null;
           
            owner.afterAct -= Effect;
            
        }

        public override void OnEquipUp(RoleBase owner)
        {
           
            this.owner = owner;
            owner.afterAct += Effect;
        }
    }

    //神速。每次行动结束 速度+2
    public class EquipEffect161 : EquipEffect16
    {
        public EquipEffect161()
        {
            value = 2;
        }
    }

    //提高闪避率 10%
    public class EquipEffect17 : EquipEffect
    {
        protected float param = 0.1f;

        public override void OnEquipDown(RoleBase owner)
        {
            owner.missValue -= param;
        }

        public override void OnEquipUp(RoleBase owner)
        {
            owner.missValue += param;
        }
    }
    //提高闪避率 20%
    public class EquipEffect171: EquipEffect17
    {
        public EquipEffect171()
        {
            param = 0.2f;
        }
    }

    //反弹受到的普通攻击50%的伤害(真实伤害)
    public class EquipEffect18 : EquipEffect
    {
        void MyEffect(int value ,RoleBase attacker)
        {
            if(attacker.isFight)
            attacker.GetHurt( (int)(value * 0.5f));
        }

        public override void OnEquipDown(RoleBase owner)
        {
            owner.afterBeNormalAttack -= MyEffect;
        }

        public override void OnEquipUp(RoleBase owner)
        {
            owner.afterBeNormalAttack += MyEffect;
        }
    }

    //提升暴击率20%
    public class EquipEffect19 : EquipEffect
    {
        public override void OnEquipDown(RoleBase owner)
        {
            owner.critRate -= 0.2f;
        }

        public override void OnEquipUp(RoleBase owner)
        {
            owner.critRate += 0.2f;
        }
    }
    
    //提升暴击伤害 50%
    public class EquipEffect20 : EquipEffect
    {
        public override void OnEquipDown(RoleBase owner)
        {
            owner.critValue -= 0.5f;
        }

        public override void OnEquipUp(RoleBase owner)
        {
            owner.critValue += 0.5f;
        }
    }

    //受到伤害时守御增加10，上限100
    public class EquipEffect21 : EquipEffect
    {
        RoleBase owner;
        int value = 10;
        int curValue = 0;
        int max = 100;
        public override void OnEquipDown(RoleBase owner)
        {
            
            owner.afterGetHurt -= AddShouYu;

            this.owner = null;

            EventManager.UnRegistEvent(EventType.FightStart, Clear);
        }

        public override void OnEquipUp(RoleBase owner)
        {
            this.owner = owner;
            owner.afterGetHurt += AddShouYu;
            EventManager.RegistEvent(EventType.FightStart, Clear);
        }

        void AddShouYu()
        {
            if (owner == null) return;
            if(curValue <max)
            {
                if(owner.isFight)
                {
                    owner.attributes[1] += value;
                    curValue += value;
                }
                
            }
            
        }
        void Clear(dynamic d)
        {
            curValue = 0;
        }

    }

    //受到伤害时抗性加10,上限100
    public class EquipEffect22 : EquipEffect
    {
        RoleBase owner;
        int value = 10;
        int curValue = 0;
        int max = 100;
    
        void Clear(dynamic d)
        {
            curValue = 0;
        }

        public override void OnEquipDown(RoleBase owner)
        {
            
            owner.afterGetHurt -= AddKangXing;
            this.owner = null;
            EventManager.UnRegistEvent(EventType.FightStart, Clear);
        }


        public override void OnEquipUp(RoleBase owner)
        {
            this.owner = owner;
            owner.afterGetHurt += AddKangXing;
            EventManager.RegistEvent(EventType.FightStart, Clear);
        }

        void AddKangXing()
        {
            if (owner == null) return;

            if (curValue < max)
            {
                if(owner.isFight)
                {
                    owner.attributes[3] += value;
                    curValue += value;
                }
                
            }
        }

    }

    //速度-50% 守御提升50%
    public class EquipEffect23 : EquipEffect
    {
        int sudu = 0;
        int shouyu = 0;
        

        public override void OnEquipDown(RoleBase owner)
        {
            owner.attributes[1] -= shouyu;
            owner.attributes[2] += sudu;
        }

        public override void OnEquipUp(RoleBase owner)
        {

            var datas = DataClass.ConfigRoleManager.Instance().allDatas[owner.GetRoleId()];
            //原始速度
            int orSudu = datas.attributes[owner.GetLevel() - 1][2];
            int orShouyu = datas.attributes[owner.GetLevel() - 1][1];

            sudu = (int)(orShouyu * 0.5f);
            shouyu = (int)(orShouyu * 0.5f);

            owner.attributes[1] += shouyu;
            owner.attributes[2] -= sudu;
        }
    }

    //释放技能后回复400点血
    public class EquipEffect24 : EquipEffect
    {
        protected int value = 400;
        
        public override void Effect(RoleBase self, RoleBase[] target)
        {
            self.GetTreat( value);
        }

        public override void OnEquipUp(RoleBase owner)
        {
            owner.afterSkill += Effect;
        }
        public override void OnEquipDown(RoleBase owner)
        {
            owner.afterSkill -= Effect;
        }
    }

    //释放技能后回复200点血
    public class EquipEffect241: EquipEffect24
    {
        public EquipEffect241()
        {
            value = 200;
        }

    }
}
