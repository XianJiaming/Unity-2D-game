using System;
using System.Collections.Generic;


//战斗结束后 获得 5 金币

//战斗结束后 玩家提升5点 hp

//战斗开始时，我方随机一名角色释放技能

//战斗开始时，我方所有单位无敌3s

//随机获得2个5费单位

//这里只是用于读取天赋数据，玩家的天赋是存在 playermodel里的
public class TalentModel:ModelBase
{
    PlayerModel playerModel;
    RoleShopModel shopModel;

    //存所有的天赋id
    public List<int> talentIds = new List<int>();

    //玩家还没有选的天赋id
    public List<int> canUseTalent = new List<int>();

    protected override void Init()
    {
        playerModel = ModelManager.Get<PlayerModel>("PlayerModel");


        var talentData = DataClass.ConfigTalentManager.Instance().allDatas;
        
        foreach(var v in talentData)
        {
            canUseTalent.Add(v.Key);
            talentIds.Add(v.Key);
        }

        EventManager.RegistEvent(EventType.NewGameStart, OnNewGame);
    }

    void OnNewGame(dynamic d = null)
    {
        canUseTalent.Clear();
        foreach (var v in talentIds)
        {
            canUseTalent.Add(v);
        }
    }
}
    
namespace MyTalent
{
    //一次性天赋作用完 后直接移出
    public class Talent
    {
        
        public static Talent CreatTalent(int id)
        {
            var talent = Activator.CreateInstance(Type.GetType($"MyTalent.Talent{id.ToString()}")) as Talent;
            talent.Init(id);
            return talent;
        }

        public int id;
        public string name;
        public string desc;

        public virtual void Effect()
        {

        }
        //移出该天赋
        public virtual void RemoveEffect()
        {

        }
        //该方法请手动调用
        public void Init(int id)
        {
            var datas = DataClass.ConfigTalentManager.Instance().allDatas[id];
            this.id = id;
            name = datas.name;
            desc = datas.desc;
        }

    }
    //战斗结束后获得5金币

    public class Talent10 : Talent
    {
       
        public override void Effect()
        {
            base.Effect();
            EventManager.RegistEvent(EventType.FightEnd, GetMoney);
        }

        void GetMoney(dynamic dy = null)
        {
             ModelManager.Get<PlayerModel>("PlayerModel").AddMoney(5);
        }

        public override void RemoveEffect()
        {
            base.RemoveEffect();
            EventManager.UnRegistEvent(EventType.FightEnd, GetMoney);
        }
    }

    //战斗开始时，我方随机一名角色蓝条加满
    public class Talent11 : Talent
    {
        public override void Effect()
        {
           
            base.Effect();   
            EventManager.RegistEvent(EventType.FightStart, UseSkill);
           
        }

        void UseSkill(dynamic dy = null)
        {
            var playerModel = ModelManager.Get<PlayerModel>("PlayerModel");
            var fightRoles = playerModel.fightRoles;
            

            List<RoleBase> roles = new List<RoleBase>();

            foreach (var role in fightRoles)
            {
                if (role != null)
                {
                    roles.Add(role);
                }
            }

            if(roles.Count>0)
            {
                int index = UnityEngine.Random.Range(0, roles.Count);

                RoleBase choseRole = roles[index];

                choseRole.AddMp((int)choseRole.attributes[6]);
                ViewManager.Get<WndTips>("WndTips").ShowMsg("一技之长", choseRole.fightTipPosition, UnityEngine.Color.yellow, 1);
            }
           
        }

        public override void RemoveEffect()
        {
            base.RemoveEffect();
            EventManager.UnRegistEvent(EventType.FightStart, UseSkill);
        }
    }
    //随机获得2个5费单位
    public class Talent12:Talent
    {
        public override void Effect()
        {
            base.Effect();
            
            var playerModel = ModelManager.Get<PlayerModel>("PlayerModel");
            var shopModel = ModelManager.Get<RoleShopModel>("RoleShopModel");

            var cost5roles = shopModel.roleCostMap[5];
            var cost3roles = shopModel.roleCostMap[3];

            int id1 = cost5roles[UnityEngine.Random.Range(0, cost5roles.Count)];
            int id2 = cost3roles[UnityEngine.Random.Range(0, cost5roles.Count)];

            playerModel.AddRolePre(id1);
            playerModel.AddRolePre(id2);
            //生效之后 移出该 一次性天赋
            playerModel.talents.Remove(this);
        }
    }

    //无敌 (战斗开始前的继承)
    public class Talent13:Talent
    {
        float time = 5f;
        public override void Effect()
        {
            base.Effect();
            EventManager.RegistEvent(EventType.FightStart, OnFightStart);
        }

        public override void RemoveEffect()
        {
            base.RemoveEffect();
            EventManager.UnRegistEvent(EventType.FightStart, OnFightStart);
        }

        protected virtual void OnFightStart(dynamic dy)
        {
            var roles = FightManager.instance.playerRoles;
            foreach(var role in roles)
            {
                if(role!= null)
                {
                    RoleHelper.SetWuDi(role, time);
                }
            }
        }
    }

    //战败损失的血量减少50%
    public class Talent14 : Talent
    {
        public override void Effect()
        {
            base.Effect();
            FightManager.instance.failReduceHpParam -= 0.5f;
        }
        public override void RemoveEffect()
        {
            base.RemoveEffect();
            FightManager.instance.failReduceHpParam += 0.5f;
        }
    }

    //每次释放技能后 该角色的最大蓝量减 10%
    public class Talent15: Talent
    {
        public override void Effect()
        {
            base.Effect();

            EventManager.RegistEvent(EventType.FightStart, OnFightStart);

        }

        public override void RemoveEffect()
        {
            base.RemoveEffect();
            EventManager.UnRegistEvent(EventType.FightStart, OnFightStart);
        }

        void OnFightStart(dynamic d)
        {
            foreach(var role in FightManager.instance.playerRoles)
            {
                if(role!=null)
                {
                    //使用技能后执行该方法

                    role.afterSkill += EfMp;
                    role.afterFight += Remove;
                }
               
            }
            
        }

        void EfMp(RoleBase owner , RoleBase[] target)
        {
            owner.attributes[6] = (int)(owner.attributes[6] *0.9f); 
        }
        void Remove(RoleBase role)
        {
            role.afterSkill -= EfMp;
            role.afterFight -= Remove;
        }
    }

    // 死一个 全部回血20%
    public class Talent16 : Talent
    {
        public override void Effect()
        {
            base.Effect();
            EventManager.RegistEvent(EventType.RoleDie, OnDie);
        }
        public override void RemoveEffect()
        {
            base.RemoveEffect();
            EventManager.UnRegistEvent(EventType.RoleDie, OnDie);
        }

        protected virtual void OnDie(dynamic d)
        {

            RoleBase role = d as RoleBase;

            if(role.fightListType == EnumType.FightListType.Player)
            {
                foreach(var r in FightManager.instance.playerRoles)
                {
                    if(r!= null)
                    {
                        if(r.isFight)
                        {

                            r.GetTreat((int)(r.attributes[5] * 0.2f));
                        }
                        
                    }
                }
            }
        }
    }

    //价格-1
    public class Talent17 : Talent
    {
        public override void Effect()
        {
            base.Effect();
            RoleShopModel.zhekou = 1;
        }

        public override void RemoveEffect()
        {
            base.RemoveEffect();
            RoleShopModel.zhekou = 0;
        }
    }

    //每当我方场上有人挂时，我方其余角色 全属性+10%
    public class Talent18: Talent16
    {
        protected override void OnDie(dynamic role)
        {
            RoleBase r = role as RoleBase;
            if(r.fightListType == EnumType.FightListType.Player)
            {
                foreach (var v in FightManager.instance.playerRoles)
                {
                    if (v != null && v.isFight)
                    {
                        for(int i=0;i< 5;i++)
                        {
                            v.attributes[i] *= 1.1f;
                        }
                    }
                }
            }
        }
    }


    public class Talent19:Talent13
    {
        protected override void OnFightStart(dynamic dy)
        {
            WndTips.ShowTips("我方所有角色闪避率增加10%");
            var roles = FightManager.instance.playerRoles;
            foreach (var role in roles)
            {
                if (role != null)
                {
                    role.missValue += 0.1f;
                }
            }
        }
    }

    //我方第一排第二位的角色生命值翻倍
    public class Talent20:Talent13
    {
       
        protected override void OnFightStart(dynamic dy)
        {
            RoleBase role = FightManager.instance.playerRoles[0];
            if(role!=null)
            {
                role.attributes[5] *= 2;
                role.attributes[7] = role.attributes[5];
                ViewManager.Get<WndTips>("WndTips").ShowMsg("坚不可摧", role.fightTipPosition, UnityEngine.Color.yellow, 1, 70, 40);
            }
        }
    }

    //装备无视等级
    public class Talent21:Talent
    {
        public override void Effect()
        {
            base.Effect();
            RoleBase.isLevelEffect = false;
        }
        public override void RemoveEffect()
        {
            base.RemoveEffect();
            RoleBase.isLevelEffect = true;
        }
    }
}