using System.Collections.Generic;
using System;
namespace DataClass
{

    public class ConfigProfessionManager : ConfigDataManager<ConfigProfessionManager,ConfigProfession>

    {
    
    public override void Init( )
    {
    name = "ConfigProfession";
    ConfigProfession config = null;
    
    config = new ConfigProfession();
    config.id = 201;
    config.name = "济世";
    config.counts = new int[]{2,4,6};
    config.range = new int[]{1,1,2};
    config.desc = "<color=yellow>【2】</color>:我方【济世】角色每2秒回复50点生命值\n<color=yellow>【4】</color>:我方【济世】每2秒回复100生命值\n<color=yellow>【6】</color>:我方所有角色每2秒回复200生命值";
    allDatas.Add( config.id, config)
;
    config = new ConfigProfession();
    config.id = 202;
    config.name = "斗战";
    config.counts = new int[]{2,4,7};
    config.range = new int[]{1,1,2};
    config.desc = "<color=yellow>【2】</color>:我方【斗战】角色普通攻击伤害增加10%,暴击率增加10%。\n<color=yellow>【4】</color>:我方【斗战】角色普通攻击伤害增加20%,暴击率增加20%。\n<color=yellow>【7】</color>:我方所有角色普通攻击伤害增加30%,暴击率增加30%。";
    allDatas.Add( config.id, config)
;
    config = new ConfigProfession();
    config.id = 203;
    config.name = "奇术";
    config.counts = new int[]{2,5,8};
    config.range = new int[]{1,1,2};
    config.desc = "<color=yellow>【2】</color>:我方【奇术】角色技能数值增加15%\n<color=yellow>【5】</color>:我方【奇术】角色技能数值增加30%\n<color=yellow>【8】</color>:我方所有角色技能数值增加45%";
    allDatas.Add( config.id, config)
;
    config = new ConfigProfession();
    config.id = 204;
    config.name = "大智";
    config.counts = new int[]{2,4};
    config.range = new int[]{2,2};
    config.desc = "<color=yellow>【2】</color>:我方所有角色每秒回复3法力值\n<color=yellow>【4】</color>我方所有角色每秒回复5法力值";
    allDatas.Add( config.id, config)
;
    config = new ConfigProfession();
    config.id = 205;
    config.name = "坚御";
    config.counts = new int[]{2,4,6};
    config.range = new int[]{1,1,1};
    config.desc = "<color=yellow>【2】</color>:我方【坚御】角色受到伤害时减免总伤害的10%\n<color=yellow>【4】</color>:我方【坚御】角色受到伤害时减免总伤害的20%\n<color=yellow>【6】</color>:我方【坚御】角色受到伤害时减免总伤害的30%";
    allDatas.Add( config.id, config)
;
    config = new ConfigProfession();
    config.id = 206;
    config.name = "灵动";
    config.counts = new int[]{2,4,6};
    config.range = new int[]{1,1,2};
    config.desc = "<color=yellow>【2】</color>:我方【灵动】角色速度增加 10,闪避增加 5%\n<color=yellow>【4】</color>:我方【灵动】角色速度增加 20,闪避增加 10%\n<color=yellow>【6】</color>:我方所有角色速度增加 30,闪避增加 15%";
    allDatas.Add( config.id, config)
;
    base.Init();
    }
}
}