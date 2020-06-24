using System.Collections.Generic;
using System;
namespace DataClass
{

    public class ConfigEquipEffectManager : ConfigDataManager<ConfigEquipEffectManager,ConfigEquipEffect>

    {
    
    public override void Init( )
    {
    name = "ConfigEquipEffect";
    ConfigEquipEffect config = null;
    
    config = new ConfigEquipEffect();
    config.id = 101;
    config.name = "嗜血";
    config.desc = "普通攻击时回复造成伤害的10%的生命值";
    allDatas.Add( config.id, config)
;
    config = new ConfigEquipEffect();
    config.id = 102;
    config.name = "嗜血";
    config.desc = "普通攻击时回复造成伤害的20%的生命值";
    allDatas.Add( config.id, config)
;
    config = new ConfigEquipEffect();
    config.id = 103;
    config.name = "嗜血";
    config.desc = "普通攻击时回复造成伤害的30%的生命值";
    allDatas.Add( config.id, config)
;
    config = new ConfigEquipEffect();
    config.id = 111;
    config.name = "逆鳞";
    config.desc = "技能造成伤害时回复造成伤害的10%";
    allDatas.Add( config.id, config)
;
    config = new ConfigEquipEffect();
    config.id = 112;
    config.name = "逆鳞";
    config.desc = "技能造成伤害时回复造成伤害的20%";
    allDatas.Add( config.id, config)
;
    config = new ConfigEquipEffect();
    config.id = 113;
    config.name = "逆鳞";
    config.desc = "技能造成伤害时回复造成伤害的30%";
    allDatas.Add( config.id, config)
;
    config = new ConfigEquipEffect();
    config.id = 121;
    config.name = "灵通";
    config.desc = "普通攻击后额外回复30点Mp";
    allDatas.Add( config.id, config)
;
    config = new ConfigEquipEffect();
    config.id = 12;
    config.name = "灵通";
    config.desc = "普通攻击后额外回复20点Mp";
    allDatas.Add( config.id, config)
;
    config = new ConfigEquipEffect();
    config.id = 13;
    config.name = "斩龙";
    config.desc = "普通攻击造成目标最大生命值5%的真实伤害";
    allDatas.Add( config.id, config)
;
    config = new ConfigEquipEffect();
    config.id = 131;
    config.name = "斩龙";
    config.desc = "普通攻击造成目标最大生命值10%的真实伤害";
    allDatas.Add( config.id, config)
;
    config = new ConfigEquipEffect();
    config.id = 142;
    config.name = "破浪";
    config.desc = "技能数值提升70%";
    allDatas.Add( config.id, config)
;
    config = new ConfigEquipEffect();
    config.id = 141;
    config.name = "破浪";
    config.desc = "技能数值提升30%";
    allDatas.Add( config.id, config)
;
    config = new ConfigEquipEffect();
    config.id = 14;
    config.name = "破浪";
    config.desc = "技能数值提升50%";
    allDatas.Add( config.id, config)
;
    config = new ConfigEquipEffect();
    config.id = 15;
    config.name = "定身";
    config.desc = "每次普通攻击30%概率使目标眩晕2s";
    allDatas.Add( config.id, config)
;
    config = new ConfigEquipEffect();
    config.id = 151;
    config.name = "定身";
    config.desc = "每次普通攻击50%概率使目标眩晕1.2s";
    allDatas.Add( config.id, config)
;
    config = new ConfigEquipEffect();
    config.id = 161;
    config.name = "神速";
    config.desc = "每次行动结束后速度提升2点";
    allDatas.Add( config.id, config)
;
    config = new ConfigEquipEffect();
    config.id = 16;
    config.name = "神速";
    config.desc = "每次行动结束后速度提升4点";
    allDatas.Add( config.id, config)
;
    config = new ConfigEquipEffect();
    config.id = 171;
    config.name = "缥缈";
    config.desc = "闪避率提升20%";
    allDatas.Add( config.id, config)
;
    config = new ConfigEquipEffect();
    config.id = 17;
    config.name = "缥缈";
    config.desc = "闪避率提升10%";
    allDatas.Add( config.id, config)
;
    config = new ConfigEquipEffect();
    config.id = 18;
    config.name = "天罡";
    config.desc = "受到普通攻击时，返弹50%的真实伤害";
    allDatas.Add( config.id, config)
;
    config = new ConfigEquipEffect();
    config.id = 19;
    config.name = "狂兽";
    config.desc = "暴击率提升20%";
    allDatas.Add( config.id, config)
;
    config = new ConfigEquipEffect();
    config.id = 20;
    config.name = "裂石";
    config.desc = "暴击伤害提升50%";
    allDatas.Add( config.id, config)
;
    config = new ConfigEquipEffect();
    config.id = 21;
    config.name = "磐石";
    config.desc = "受到伤害时守御增加10（上限为100）";
    allDatas.Add( config.id, config)
;
    config = new ConfigEquipEffect();
    config.id = 22;
    config.name = "强魂";
    config.desc = "受到伤害时抗性增加10（上限为100）";
    allDatas.Add( config.id, config)
;
    config = new ConfigEquipEffect();
    config.id = 23;
    config.name = "逆流";
    config.desc = "速度降低50%守御增加50%";
    allDatas.Add( config.id, config)
;
    config = new ConfigEquipEffect();
    config.id = 24;
    config.name = "回灵";
    config.desc = "每次释放技能时回复400生命";
    allDatas.Add( config.id, config)
;
    config = new ConfigEquipEffect();
    config.id = 241;
    config.name = "回灵";
    config.desc = "每次释放技能时回复200生命";
    allDatas.Add( config.id, config)
;
    base.Init();
    }
}
}