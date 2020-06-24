using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using DataClass;
using System;

// 0	"力量"
// 1	"守御"
// 2	"速度"
// 3	"抗性"
// 4	"精神"
// 5	"生命"
// 6	"法力"
// 7	"当前生命"
// 8	"当前内力"


public class RoleBase
{
    public static string GetCooperName(int roleId)
    {
        var datas = ConfigRoleManager.Instance().allDatas[roleId];
        int cooperId = datas.cooperId;
        return ConfigCooperationManager.Instance().allDatas[cooperId].name;       
    }
    public static string GetPreName(int roleId)
    {
        var datas = ConfigRoleManager.Instance().allDatas[roleId];
        int proId = datas.proId;
        return ConfigProfessionManager.Instance().allDatas[proId].name;
    }


    //每个人两个格子,一个武器，一个防具
    // 0 武器 1 防具
    //装备相关------------------------------------------------------------------------------------------------
    public Action OnUpdateEquip;

    Equip[] equips = new Equip[2];
    public Equip GetEquip(int index)
    {
        return equips[index];
    }
    //给ai调用的,ai穿戴装备无视等级
    public void AddEquipMust(Equip e)
    {
        equips[e.equipType] = e;
        e.Load(this);
    }
    //ai调用
    public void RemoveAIEquip()
    {
        equips[0]?.UnLoad(this);
        equips[1]?.UnLoad(this);
    }
    //为false时无视等级
    public static bool isLevelEffect = true;
    //获得新装备
    public bool AddEquip(Equip equip)
    {
        Equip curEquip = equips[equip.equipType];

        if (curEquip != null)
        {
            //curEquip.UnLoad(this);
            if(curEquip.level > equip.level)
            {
                WndTips.ShowTips("不可替换为低等级的装备！");
                return false;
            }
            else
            {
                AudioManager.instance.PlayEffect(ConstConfig.UpEquip);

                EquipModel equipModel = ModelManager.Get<EquipModel>("EquipModel");

                curEquip.UnLoad(this);

                equipModel.AddEquip(curEquip);

                equips[equip.equipType] = equip;

                equip.Load(this);

                OnUpdateEquip?.Invoke();

                return true;
            }
           
        }
        else if (equip.level > cost && isLevelEffect == true)
        {
            WndTips.ShowTips("装备等级不能低于角色品质.");
            return false;
        }
        else
        {
            AudioManager.instance.PlayEffect(ConstConfig.UpEquip);

            equips[equip.equipType] = equip;

            equip.Load(this);

            OnUpdateEquip?.Invoke();
            
            return true;
        }
        //return curEquip;
    }
    //卸载装备,一次性将身上的所有装备送回背包（目前是贩卖角色时调用
    public void RemoveAllEquip( )
    {
        var model = ModelManager.Get<EquipModel>("EquipModel");
        for (int i=0;i<equips.Length;i++)
        {
            if (equips[i] == null)
                continue;

            var equip = RemoveEquip(i);

            model.AddEquip(equip);                       
        }
    }

    Equip RemoveEquip(int index)
    {
        var equip = equips[index];
        equips[index] = null;
        equip.UnLoad(this);

        OnUpdateEquip?.Invoke();

        return equip;
    }

    
    //装备相关------------------------------------------------------------------------------------------------


    //public const string roleModelPath = "Prefab/RoleModel/";
    //属性
    int id;
    
    public string name;
    //所有的属性存在这里
    public float[] attributes = new float[9];
    
    //种族与职业羁绊id
    public int[] etlId = new int[2];
    public int GetCooperId()
    {
        return etlId[0];
    }
    public int GetProId()
    {
        return etlId[1];
    }

    //战斗偏移
    public int FightOffset
    {
        private set;
        get;
    }

    public int GetRoleId()
    {
        return id;
    }
    int level;
    public int GetLevel( )
    {
        return level;
    }
    public int cost
    { //初始价值
        private set;
        get;
    }
    int price = 0;
    public int GetPrice()
    {
        return price;
    }
    //本次战斗该角色造成的总伤害
    int wholeHurtValue;
    public int WholeHurtValue
    {
        set
        {
            wholeHurtValue = value;            
        }
        get
        {
            return wholeHurtValue;
        }
    }

    //本回合造成的普通攻击伤害
    public int HurtValue
    {
        private set;
        get;
    }
    //本回合技能的数值
    public int SkillValue
    {
        set;
        get;
    }
    /// <summary>
    /// 技能数据参数，技能数值会乘以这个参数
    /// </summary>
    public float skillParam = 1.0f; //技能数值参数，影响技能数值

    //是否在战斗中
    public bool isFight = false;

    //UI go 组件
    
    Text nameText;
    GameObject uiGo;
    //行动计时
    bool isStop = false; //为true就 会暂停
    float actTime = 0f;

    //战斗相关
    public RoleBase[] team;
    RoleBase[] enemy;
    //战斗时的索引
    public int positionIndex = -1;

    //技能
    public SkillBase skill;

    bool isSilent = false; //被沉默后就不能回蓝了
    public void SetSilent(bool value)
    {
        isSilent = value;
    }

    public void SetTeamAndEnemy(RoleBase[] team, RoleBase[] enemy)
    {
        this.team = team;
        this.enemy = enemy;
    }
    //战斗前做初始化，血回满，蓝量清空之类的
    public void BeginFight(int wholeHurtValue = 0)
    {
        isFight = true;

        isWd = false;
        isSilent = false;

        BeforeFight();
        HurtValue = 0;
        SkillValue = 0;
        //本场战斗伤害统计归0
        WholeHurtValue = wholeHurtValue;

        actTime = 0;
        //hpImg.gameObject.SetActive(true);
        //attributes[7] = attributes[5];
        SetHp(attributes[5]);
        //内力归0
        attributes[8] = 0;

        


    }
    //角色新建时 从配置表里读数据,传入的是 数据项的id，不是角色id
    public void SetId( int id , int level = 1)
    {
//         if(!ConfigRoleManager.Instance().allDatas.ContainsKey(id))
//         {
//             Debug.Log(id);
//         }
        var data = ConfigRoleManager.Instance().allDatas[id];
        this.id = id;
        name = data.name;
        cost = data.cost;

        SetLevel(level);
        etlId[0] = data.cooperId;
        etlId[1] = data.proId;
        

        for (int i = 0; i < data.attributes[level - 1].Length; i++)
        {
            attributes[i] = data.attributes[level-1][i];
        }
        

        //创建技能对象
        if(data.skillId != 0)
        {
             skill = new SkillBase();
             skill.SetId(data.skillId, level, this);
        }
       
        //获取fight gameobject资源 并且实例化，实例化后隐藏
        //fightRole = ObjectPool.Instance().GetRoleGo(name);
    }
    
    //被停了  可以用来 眩晕
    public void SetStop(bool value)
    {
        isStop = value;
    }

//     public void SetUIGo(GameObject go)
//     {
//         this.uiGo = go;
//         nameText = go.transform.Find("name").GetComponent<Text>();
//         
//         
//     }
   
    //设置战斗中的角色模型
    //----------------------------------------------------战斗相关
    public GameObject fightRole;
    Text hpText;
    Text mpText;
    Text fightNameText;
    Image hpImage;
    Image mpImage;

    Image kuang0Image;
    Image kuang1Image;
    Image toneImage;

    GameObject wuqi;
    GameObject fangju;

    public void SetFightGo(GameObject go)
    {
        fightRole = go;
        
        fightTipPosition.x = fightRole.transform.position.x;
        fightTipPosition.y = fightRole.transform.position.y + 5;
        fightTipPosition.z = fightRole.transform.position.z;

        fightGoPosition = fightRole.transform.position;

        
        hpImage = UIUtil.GetImage(fightRole, "hp");
        mpImage = UIUtil.GetImage(fightRole, "mp");

        kuang0Image = UIUtil.GetImage(fightRole, "Role/kuang0");
        kuang1Image = UIUtil.GetImage(fightRole, "Role/kuang1");
        toneImage = UIUtil.GetImage(fightRole, "Role/tone/tone");

        wuqi = UIUtil.GetGameObject(fightRole, "Role/wuqiIcon");
        fangju = UIUtil.GetGameObject(fightRole, "Role/fangjuIcon");

        fightNameText = UIUtil.GetText(fightRole, "Role/name");
        fightRole.SetActive(true);
        UpdateFightGo();
    }
    void UpdateFightGo()
    {
        UpdateHpView();
        UpdateMpView();


        string cooperName = RoleBase.GetCooperName(GetRoleId());
        string proName = RoleBase.GetPreName(GetRoleId());
        fightNameText.text = $"<color={ConstConfig.levelColor[cost]}>{name}</color>\n【{cooperName}】\n【{proName}】";

        //更新等级 费用view
        toneImage.sprite = AssetLoader.instance.tones[cost - 1];
        int level = GetLevel();
        kuang0Image.sprite = AssetLoader.instance.kuang0s[level - 1];
        kuang1Image.sprite = AssetLoader.instance.kuang1s[level - 1];
        //更新装备ui显示
        if (null != GetEquip(0))
        {
            wuqi.SetActive(true);
        }
        else
        {
            wuqi.SetActive(false);
        }

        if (null != GetEquip(1))
        {
            fangju.SetActive(true);
        }
        else
        {
            fangju.SetActive(false);
        }

    }

    void UpdateHpView()
    {
       
        if(hpImage)
        {
            hpImage.fillAmount = attributes[7] / attributes[5];
            
        }
            
    }
    void UpdateMpView()
    {
        if(mpImage)
            mpImage.fillAmount = attributes[8] / attributes[6];
    }


    //----------------------------------------------------战斗相关
    public Vector3 fightTipPosition = Vector3.zero;
    


    public float missValue = 0.1f;
    //闪避成功了吗
    bool Miss()
    {
        return UnityEngine.Random.Range(0, 1.0f) < missValue;
    }
    //受到的普通攻击伤害
    int normalHurtValue = 0;
    //对外挨打的接口，是普通攻击的
    public Action<int, RoleBase> afterBeNormalAttack;

    void AfterBeNomAtk(RoleBase attacker)
    {
        afterBeNormalAttack?.Invoke(normalHurtValue, attacker);
    }
    //被普通攻击打了
    public bool BeAttack(int hurtValue, RoleBase attacker = null)
    {
        if(Miss())
        {
            attacker.HurtValue = 0;
            EventManager.ExecuteEvent(EventType.RoleMiss, this);
            return false;
        }
        else
        {
            EventManager.ExecuteEvent(EventType.RoleBeAttack, new RoleBase[] { this, attacker});
            float param;
            float shouyu = attributes[1];            
            param = (1 - shouyu / (100 + shouyu));
            normalHurtValue = (int)(hurtValue * param);
            GetHurt(normalHurtValue);
            attacker.HurtValue = normalHurtValue;
            attacker.WholeHurtValue += normalHurtValue;

            AfterBeNomAtk(attacker);
            return true;
        }
        
        
    }
    //被技能打了
    public void BeSkillAttack(int hurtValue )
    {
        
        int getHurtValue = (int)(hurtValue * (1 - attributes[3] / (100 + attributes[3])));
        GetHurt(getHurtValue);
    }
    //是否无敌
    bool isWd = false;
    public void SetWd(bool value)
    {
        isWd = value;
    }
    /// <summary>
    /// 承受伤害的参数
    /// </summary>
    public float getHurtParam = 1.0f;

    public Action afterGetHurt;
    void AfterGetHurt()
    {
        afterGetHurt?.Invoke();
    }

    //受到伤害,无论什么伤害都回走这个接口
    public void GetHurt(int value)
    {
        //对value取绝对值保证为负
        value = Mathf.Abs(value);


        int hurtValue = (int)(value * getHurtParam);

        if (isWd) hurtValue = 0;

        int mp = Mathf.Min(20, hurtValue / 5);
        AddMp(mp);

        var wndTips = ViewManager.Get("WndTips") as WndTips;
        wndTips.ShowMsg(hurtValue.ToString(), fightTipPosition, Color.red);



        SetHp(attributes[7] - hurtValue);
        AfterGetHurt();
    }
    //获得治疗
    public void GetTreat(int value )
    {
        SetHp(attributes[7] + value);
        var wndTips = ViewManager.Get("WndTips") as WndTips;

        wndTips.ShowMsg($"HP+{ value.ToString()}", fightTipPosition, Color.green);
    }

    //升一级
    //角色没有升级，触发升级时就是原角色被销毁，创建一个新角色
    void SetLevel(int value)
    {
        level = value;
        price = level * cost;
    }

    void SetHp( int value)
    {
        attributes[7] = value;
        if(attributes[7] > attributes[5])
        {
            attributes[7] = attributes[5];
        }

        UpdateHpView();

        if (attributes[7] <= 0)
            Die();
    }
    void SetHp(float value)
    {
        SetHp( (int)(value));
    }

    void SetMp(int value)
    {
        attributes[8] = value;
        UpdateMpView();
    }
    void SetMp(float value)
    {
        SetMp((int)value);
    }
    //计算出我产出的普通攻击伤害
    public bool isCrit
    { get; set; } = false;

    int GetAttackValue()
    {
        //计算暴击
        isCrit = UnityEngine.Random.Range(0, 1.0f)<=critRate;
        if (!isCrit)
        {
           
            return (int)(attributes[0] * attackValueParam);
        }
            
        else
            return (int)(attributes[0] * attackValueParam * critValue);
    }

    //暴击率
    public float critRate = 0.2f;
    /// <summary>
    /// 暴击伤害比率
    /// </summary>
    public float critValue = 2.0f;
    //普通攻击数值比率 修改用于提升普通攻击伤害
    public float attackValueParam { set; get; } = 1.0f;

    public bool isAttackSuccess { private set; get; }

    public void Attack(RoleBase target )
    {
       
        if (target == null)
        {
            //Debug.LogError(name+"攻击的目标为空");
            AfterAct();
            return;
        }

        //暴击初始
        isCrit = false;

        Vector3 orPosition = fightGoPosition;
            
        fightRole.transform.DOMove(GetTargetPosition(target), ConstConfig.moveSpeed).OnComplete(
            ( ) =>
            {
                fightRole.transform.DOMove(orPosition, ConstConfig.moveSpeed).OnComplete(AfterAct);
                int mpValue = GetAddMp();

                //并判断是否暴击
                int hurtValue = GetAttackValue();    //获取输出伤害

                //打中没
                isAttackSuccess = target.BeAttack(hurtValue, this);

                if (isAttackSuccess)
                    AddMp(mpValue); //内力增加
                    //AddMp(80);
                AfterAttack(target);

            }
            );
    }
    int GetAddMp()
    {
        //计算普通攻击回蓝
        return (int)(attributes[4]*0.8f);
    }

    //自己到目标的哪个位置
    public Vector3 fightGoPosition;
    public Vector3 GetTargetPosition(RoleBase target)
    {

        Vector3 targetPosition = Vector3.zero;
        targetPosition.x = target.fightGoPosition.x;
        targetPosition.y = target.fightGoPosition.y + FightOffset;
        targetPosition.z = target.fightGoPosition.z;


        return targetPosition;
    }

    //增加mp
    public void AddMp(int value)
    {
        if (isSilent)
            return;


        SetMp(attributes[8] + value);
        if (attributes[8] > attributes[6])
            SetMp(attributes[6]);
    }

    //获得攻击目标
    RoleBase GetTarget()
    {
        RoleBase target = null;

        int yushu = positionIndex % 3;


        for(int i = 0;i<3;i++)
        {
            var e = enemy[yushu + 3 * i];
            if (e != null)
            {
                target = e;
                return target;
            }
            //说明对面那个位置为空
            if (target == null)
            {
                for (int j = 0+i*3; j <= 2+i*3; j++)
                {
                    if (enemy[j] != null)
                    {
                        target = enemy[j];
                        return target;
                    }
                }
            }
        }
        if(target == null)
        {
            Debug.LogError("战斗中的攻击目标为空");
        }
        return target;
    }
    //战斗结束时 所有属性回到战斗前
    float tempAttackParam;
    float tempSkillParam;
    float[] tempAttributes = new float[9];
    float tempGetHurtParam;
    float tempMissValue;
    float tempCritRate;
    float tempCritValue;

    public void CopyAllValue()
    {

        tempAttackParam = attackValueParam;
        tempSkillParam = skillParam;

        for (int i = 0; i < 9; i++)
        {
            tempAttributes[i] = attributes[i];
        }
        tempGetHurtParam = getHurtParam;
        tempMissValue = missValue;
        tempCritRate = critRate;
        tempCritValue = critValue;
    }

    public void BackAllValue()
    {
        attackValueParam = tempAttackParam;
        skillParam = tempSkillParam;
        for (int i = 0; i < 9; i++)
        {
            attributes[i] = tempAttributes[i];
        }
        getHurtParam = tempGetHurtParam  ;
        missValue = tempMissValue  ;
        critRate = tempCritRate;
        critValue = tempCritValue ;

    }

    public event Action<RoleBase> afterFight;
    //战斗结束时调用
    public void EndFight()
    {
        fightRole = null;
        BackAllValue();
        afterFight?.Invoke(this);
    }
    public event Action<RoleBase> afterDie;
    void AfterDie()
    {
        afterDie?.Invoke(this);
    }
    //角色死亡时 告诉别人他挂了
    public void Die()
    {
        if(isFight == false)
        {
            return;
        }

        isFight = false;
        
        EventManager.ExecuteEvent(EventType.RoleDie, this);
        AfterDie();

        fightRole = null;
    }

    //使用速度属性来判断是否可以执行
    public bool CanAct()
    {
        return actTime >= 75/(attributes[2]+50);
    }


    public event Action<RoleBase> beforeFight; //战斗开始时调用
    public event Action beforeAct;
    public event Action<RoleBase, RoleBase[]> afterAct;
    public event Action<RoleBase,RoleBase[]> afterAttack;
    public event Action<RoleBase, RoleBase[]> afterSkill;


    void BeforeFight( )
    {
        beforeFight?.Invoke(this);
    }

    void AfterAttack(RoleBase target)
    {
        afterAttack?.Invoke(this, new RoleBase[] { target });
    }
    void AfterSkill(RoleBase[] target)
    {
        afterSkill?.Invoke(this, target);
    }

    void BeforeAct()
    {
        beforeAct?.Invoke();
    }
    void AfterAct()
    {
        afterAct?.Invoke(this, null);
        //普通攻击固定回复5点蓝
        AddMp(10);
        FightManager.instance.isAnyoneAct = false;
    }
    //开始行动，判断是攻击还是使用技能
    public void Act()
    {
        FightManager.instance.isAnyoneAct = true;
        actTime = 0f;

        BeforeAct();

        EventManager.ExecuteEvent(EventType.RoleAct, this);

        //使用技能
        if(attributes[8] >= attributes[6] && skill!=null)
        {
            UseSkill();
        }
        else
        {
            Attack(GetTarget());
        }      
    }
   
    public void UseSkill()
    {
        SkillValue = 0;
        var wndTips = ViewManager.Get<WndTips>("WndTips");

        wndTips.ShowMsg(skill.name, fightTipPosition, UnityEngine.Color.blue, 1,60, 25);

        var target = skill.Use(team, enemy);

        //用完技能后蓝条归0
        SetMp(0);

        AfterSkill(target);
    }

    public void Update()
    {
        //如果别人在行动就停止
        if (FightManager.instance.isAnyoneAct) return;
        //如果自己不能行动就停止
        if (isStop) return;
        

        actTime += Time.deltaTime;
        if (CanAct())
            Act();
    }
    //该角色是ai还是player
    public EnumType.FightListType fightListType;

    public void SetFightIndex(EnumType.FightListType type, int index )
    {
        int tempOffset = 2;
        positionIndex = index;
        fightListType = type;
        if(fightListType == EnumType.FightListType.Ai)
        {
            FightOffset = tempOffset;
        }
        else if(fightListType == EnumType.FightListType.Player)
        {
            FightOffset = -tempOffset;
        }
    }
    public void SetFightPositionIndex(int index)
    {
        positionIndex = index;
    }

    public string ShowMissCirtInfo()
    {
        return $"<color=red>暴击率:{critRate:P0} 闪避率:{missValue:P0}</color>";
    }
}
