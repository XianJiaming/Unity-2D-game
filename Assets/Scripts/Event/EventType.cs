public enum EventType
{
    Update = 0, //每一帧调用

    PreRoleUpdate = 1,  //准备队列的角色增加或者减少

    RoleDie = 2,

    FightRoleUpdate , //战斗队列发生变化,用于战斗前

    EtlDataUpdate , //羁绊数据发生变化
    

    ShopDataUpdate ,  //商店数据变化完成

    LevelUp,  //人口等级提升

    MoneyUpdate, //金钱发生变化时

    FightEnd, // 战斗结束

    InputClick,// 点击输入时

    FightStart, // 战斗开始前

    NextLevel, //新的回合开始了

    RoleBeAttack, //某个角色被攻击了    

    PlayerHpUpdate, //玩家血量变化

    FightFail, //战斗失败
    FightVectory, //战斗胜利

    GameFial, //游戏结束 玩家失败
    GameVectory, //玩家取得最终胜利了
    GameOver, //玩家退出游戏( 回到主界面)

    NewGameStart, // 开始游戏

    ApplicationQuit, //要关闭程序了

    AddEquip, //装备获取完毕

    RemoveEquip, // 装备移出完毕

    EquipUpdate, //装备格子有装备变化

    RoleAct, //角色开始行动

    IsAnyOneActUp, //某人在行动的 bool 更新时

    RoleMiss, //战斗中 某个角色miss了
}
