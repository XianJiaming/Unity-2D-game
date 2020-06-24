
using System.Collections.Generic;

public class ConstConfig
{
    public const int startHp = 50;

    public const int startMoney = 30;

    public const int equipRewardLevel = 3;
    public const int talentRewardLevel = 6;
    //角色移动速度
    public static float moveSpeed = 0.35f;

    public static readonly Dictionary<int, string> levelColor = new Dictionary<int, string>()
    {
        {1,"white"},{2,"#3BFF00"},{3,"blue"},{4,"red"},{5,"yellow"}
    };

    public const string typeColor = "#8A3468";
    public const string attributeColor = "#43020B";
    public const string skillColor = "#23098A";

    public const string AttackEffect = "BeAttack";

    //战斗结算界面
    public const string WndFightOverBg = "WndFightOver";

    //游戏初始界面音乐名字
    public const string WndGameStartBg = "WndGameStart";

    //游戏 战斗准备界面bg音乐
    public const string WndPrepareBg = "WndPrepare";

    //游戏 战斗界面bg 音乐
    public const string WndFightBg = "WndFight";

    //玩家升级音效
    public const string RoleUpLevel = "RoleUpLevel"; //

    //玩家人口升级音效
    public const string PlayerUpMus = "PlayerUp";//

    //点击ui音效
    public const string UIClick = "UIClick";//

    //拖拽角色 放下时音效
    public const string DropRole = "DropRole"; //

    //穿戴装备音效
    public const string UpEquip = "UpEquip"; //

    //普通攻击击中时的音效
    public const string Attack = "Attack";

    //
    public const string Tips = "Tips";
}