using System.Collections.Generic;
using System;
namespace DataClass
{

    public class ConfigTalentManager : ConfigDataManager<ConfigTalentManager,ConfigTalent>

    {
    
    public override void Init( )
    {
    name = "ConfigTalent";
    ConfigTalent config = null;
    
    config = new ConfigTalent();
    config.id = 10;
    config.name = "财源滚滚";
    config.desc = "每场战斗结束后获得额外5金币";
    allDatas.Add( config.id, config)
;
    config = new ConfigTalent();
    config.id = 11;
    config.name = "一技之长";
    config.desc = "每场战斗开始时我方随机1名角色满蓝";
    allDatas.Add( config.id, config)
;
    config = new ConfigTalent();
    config.id = 12;
    config.name = "天降奇兵";
    config.desc = "随机获得1个5费角色和1个3费角色";
    allDatas.Add( config.id, config)
;
    config = new ConfigTalent();
    config.id = 13;
    config.name = "绝对防御";
    config.desc = "每次战斗开始时我方所有角色无敌5秒";
    allDatas.Add( config.id, config)
;
    config = new ConfigTalent();
    config.id = 14;
    config.name = "忍辱负重";
    config.desc = "每次战败损失的生命值减少50%";
    allDatas.Add( config.id, config)
;
    config = new ConfigTalent();
    config.id = 15;
    config.name = "孰能生巧";
    config.desc = "每次释放技能角色的蓝条需求量减少10%";
    allDatas.Add( config.id, config)
;
    config = new ConfigTalent();
    config.id = 16;
    config.name = "回光返照";
    config.desc = "我方每阵亡一名角色剩余的角色血量回复20%";
    allDatas.Add( config.id, config)
;
    config = new ConfigTalent();
    config.id = 17;
    config.name = "左右逢源";
    config.desc = "商店里的棋子售价减少1";
    allDatas.Add( config.id, config)
;
    config = new ConfigTalent();
    config.id = 18;
    config.name = "愈战愈勇";
    config.desc = "我方每阵亡一名角色剩余角色的全属性增加10%";
    allDatas.Add( config.id, config)
;
    config = new ConfigTalent();
    config.id = 19;
    config.name = "身轻如燕";
    config.desc = "我方所有角色闪避率增加10%";
    allDatas.Add( config.id, config)
;
    config = new ConfigTalent();
    config.id = 20;
    config.name = "坚不可摧";
    config.desc = "战斗开始时我方第一排中间的角色生命值翻倍";
    allDatas.Add( config.id, config)
;
    config = new ConfigTalent();
    config.id = 21;
    config.name = "装备精通";
    config.desc = "我方所有角色都可穿戴任何等级的装备";
    allDatas.Add( config.id, config)
;
    base.Init();
    }
}
}