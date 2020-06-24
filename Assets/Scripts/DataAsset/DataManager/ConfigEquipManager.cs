using System.Collections.Generic;
using System;
namespace DataClass
{

    public class ConfigEquipManager : ConfigDataManager<ConfigEquipManager,ConfigEquip>

    {
    
    public override void Init( )
    {
    name = "ConfigEquip";
    ConfigEquip config = null;
    
    config = new ConfigEquip();
    config.id = 1;
    config.name = "望舒剑";
    config.attributes = new Dictionary<int,int>{{0,25},{2,30}};
    config.equipType = 0;
    config.level = 5;
    config.effects = new int[]{12,151,19};
    allDatas.Add( config.id, config)
;
    config = new ConfigEquip();
    config.id = 2;
    config.name = "盘古开天斧";
    config.attributes = new Dictionary<int,int>{{0,100}};
    config.equipType = 0;
    config.level = 5;
    config.effects = new int[]{102,121,13};
    allDatas.Add( config.id, config)
;
    config = new ConfigEquip();
    config.id = 3;
    config.name = "金箍棒";
    config.attributes = new Dictionary<int,int>{{0,25},{2,15}};
    config.equipType = 0;
    config.level = 4;
    config.effects = new int[]{16,19};
    allDatas.Add( config.id, config)
;
    config = new ConfigEquip();
    config.id = 4;
    config.name = "乾坤图";
    config.attributes = new Dictionary<int,int>{{4,20}};
    config.equipType = 0;
    config.level = 4;
    config.effects = new int[]{113,14};
    allDatas.Add( config.id, config)
;
    config = new ConfigEquip();
    config.id = 5;
    config.name = "风火轮";
    config.attributes = new Dictionary<int,int>{{2,15}};
    config.equipType = 0;
    config.level = 3;
    config.effects = new int[]{161,171};
    allDatas.Add( config.id, config)
;
    config = new ConfigEquip();
    config.id = 6;
    config.name = "碧玉琵琶";
    config.attributes = new Dictionary<int,int>{{4,10}};
    config.equipType = 0;
    config.level = 3;
    config.effects = new int[]{112,12};
    allDatas.Add( config.id, config)
;
    config = new ConfigEquip();
    config.id = 7;
    config.name = "雷公锤";
    config.attributes = new Dictionary<int,int>{{0,100},{1,-50}};
    config.equipType = 0;
    config.level = 2;
    config.effects = new int[]{13,15};
    allDatas.Add( config.id, config)
;
    config = new ConfigEquip();
    config.id = 8;
    config.name = "极乐弓";
    config.attributes = new Dictionary<int,int>{{2,10}};
    config.equipType = 0;
    config.level = 2;
    config.effects = new int[]{19,20};
    allDatas.Add( config.id, config)
;
    config = new ConfigEquip();
    config.id = 9;
    config.name = "嗜血匕";
    config.attributes = new Dictionary<int,int>{{2,10}};
    config.equipType = 0;
    config.level = 1;
    config.effects = new int[]{101};
    allDatas.Add( config.id, config)
;
    config = new ConfigEquip();
    config.id = 10;
    config.name = "梭罗派";
    config.attributes = new Dictionary<int,int>{{0,25}};
    config.equipType = 0;
    config.level = 1;
    config.effects = new int[]{15};
    allDatas.Add( config.id, config)
;
    config = new ConfigEquip();
    config.id = 11;
    config.name = "太虚神甲";
    config.attributes = new Dictionary<int,int>{{1,15},{3,30}};
    config.equipType = 1;
    config.level = 5;
    config.effects = new int[]{18,22};
    allDatas.Add( config.id, config)
;
    config = new ConfigEquip();
    config.id = 12;
    config.name = "女娲石";
    config.attributes = new Dictionary<int,int>{{5,400}};
    config.equipType = 1;
    config.level = 5;
    config.effects = new int[]{21,24};
    allDatas.Add( config.id, config)
;
    config = new ConfigEquip();
    config.id = 13;
    config.name = "昆仑镜";
    config.attributes = new Dictionary<int,int>{{1,20}};
    config.equipType = 1;
    config.level = 4;
    config.effects = new int[]{23,111};
    allDatas.Add( config.id, config)
;
    config = new ConfigEquip();
    config.id = 14;
    config.name = "天罗伞";
    config.attributes = new Dictionary<int,int>{{1,20},{2,15},{3,20},{4,10}};
    config.equipType = 1;
    config.level = 4;
    config.effects = new int[]{0};
    allDatas.Add( config.id, config)
;
    config = new ConfigEquip();
    config.id = 15;
    config.name = "乾坤罩";
    config.attributes = new Dictionary<int,int>{{1,40},{2,15}};
    config.equipType = 1;
    config.level = 3;
    config.effects = new int[]{19};
    allDatas.Add( config.id, config)
;
    config = new ConfigEquip();
    config.id = 16;
    config.name = "神农鼎";
    config.attributes = new Dictionary<int,int>{{5,200}};
    config.equipType = 1;
    config.level = 3;
    config.effects = new int[]{24};
    allDatas.Add( config.id, config)
;
    config = new ConfigEquip();
    config.id = 17;
    config.name = "八卦阵";
    config.attributes = new Dictionary<int,int>{{4,10}};
    config.equipType = 1;
    config.level = 2;
    config.effects = new int[]{171};
    allDatas.Add( config.id, config)
;
    config = new ConfigEquip();
    config.id = 18;
    config.name = "紫金花狐貂";
    config.attributes = new Dictionary<int,int>{{1,40},{3,40}};
    config.equipType = 1;
    config.level = 2;
    config.effects = new int[]{112};
    allDatas.Add( config.id, config)
;
    config = new ConfigEquip();
    config.id = 19;
    config.name = "缥缈衣";
    config.attributes = new Dictionary<int,int>{{2,15}};
    config.equipType = 1;
    config.level = 1;
    config.effects = new int[]{17};
    allDatas.Add( config.id, config)
;
    config = new ConfigEquip();
    config.id = 20;
    config.name = "太极盾";
    config.attributes = new Dictionary<int,int>{{-1,-1}};
    config.equipType = 1;
    config.level = 1;
    config.effects = new int[]{21,22};
    allDatas.Add( config.id, config)
;
    base.Init();
    }
}
}