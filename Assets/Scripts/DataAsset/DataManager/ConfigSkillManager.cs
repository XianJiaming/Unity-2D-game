using System.Collections.Generic;
using System;
namespace DataClass
{

    public class ConfigSkillManager : ConfigDataManager<ConfigSkillManager,ConfigSkill>

    {
    
    public override void Init( )
    {
    name = "ConfigSkill";
    ConfigSkill config = null;
    
    config = new ConfigSkill();
    config.id = 500;
    config.name = "春哺万物";
    config.targetType = new int[]{0,3};
    config.targetQueue = 0;
    config.level1 = new Dictionary<int,int[]>{{14,new int[]{0,100}},{17,new int[]{5,20}}};
    config.level2 = new Dictionary<int,int[]>{{14,new int[]{0,300}},{17,new int[]{5,30}}};
    config.level3 = new Dictionary<int,int[]>{{14,new int[]{0,500}},{17,new int[]{5,50}}};
    config.selfEffect = "null";
    config.wholeEffect = "500Whole";
    config.targetEffect = "500Target";
    config.desc = "我方随机3个目标获得100/300/500治疗，并在5秒内全属性提升20/30/50%";
    allDatas.Add( config.id, config)
;
    config = new ConfigSkill();
    config.id = 490;
    config.name = "万物不朽";
    config.targetType = new int[]{5,0};
    config.targetQueue = 0;
    config.level1 = new Dictionary<int,int[]>{{19,new int[]{0,1}}};
    config.level2 = new Dictionary<int,int[]>{{19,new int[]{0,2}}};
    config.level3 = new Dictionary<int,int[]>{{19,new int[]{0,4}}};
    config.selfEffect = "490Self";
    config.wholeEffect = "490Whole";
    config.targetEffect = "null";
    config.desc = "随机复活我方1/2/4名角色";
    allDatas.Add( config.id, config)
;
    config = new ConfigSkill();
    config.id = 480;
    config.name = "魅惑";
    config.targetType = new int[]{0,1};
    config.targetQueue = 1;
    config.level1 = new Dictionary<int,int[]>{{10,new int[]{4,0}}};
    config.level2 = new Dictionary<int,int[]>{{10,new int[]{5,0}}};
    config.level3 = new Dictionary<int,int[]>{{10,new int[]{8,0}}};
    config.selfEffect = "null";
    config.wholeEffect = "null";
    config.targetEffect = "480Target";
    config.desc = "敌方随机一名角色眩晕4/5/8秒";
    allDatas.Add( config.id, config)
;
    config = new ConfigSkill();
    config.id = 470;
    config.name = "月桂祝福";
    config.targetType = new int[]{2,1};
    config.targetQueue = 0;
    config.level1 = new Dictionary<int,int[]>{{14,new int[]{0,150}}};
    config.level2 = new Dictionary<int,int[]>{{14,new int[]{0,300}}};
    config.level3 = new Dictionary<int,int[]>{{14,new int[]{0,500}}};
    config.selfEffect = "null";
    config.wholeEffect = "null";
    config.targetEffect = "470Target";
    config.desc = "我方第一排角色治疗150/300/500生命";
    allDatas.Add( config.id, config)
;
    config = new ConfigSkill();
    config.id = 460;
    config.name = "琴瑟之佑";
    config.targetType = new int[]{4,0};
    config.targetQueue = 0;
    config.level1 = new Dictionary<int,int[]>{{13,new int[]{3,0}},{17,new int[]{2,20}}};
    config.level2 = new Dictionary<int,int[]>{{13,new int[]{3,0}},{17,new int[]{2,50}}};
    config.level3 = new Dictionary<int,int[]>{{13,new int[]{3,0}},{17,new int[]{2,80}}};
    config.selfEffect = "460Self";
    config.wholeEffect = "null";
    config.targetEffect = "460Target";
    config.desc = "我方一名角色3秒免疫伤害，并全属性增加20/50/80%";
    allDatas.Add( config.id, config)
;
    config = new ConfigSkill();
    config.id = 440;
    config.name = "逐鹿";
    config.targetType = new int[]{4,0};
    config.targetQueue = 1;
    config.level1 = new Dictionary<int,int[]>{{15,new int[]{0,300}}};
    config.level2 = new Dictionary<int,int[]>{{15,new int[]{0,500}}};
    config.level3 = new Dictionary<int,int[]>{{15,new int[]{0,1000}}};
    config.selfEffect = "440Self";
    config.wholeEffect = "null";
    config.targetEffect = "440Target";
    config.desc = "对敌方单体造成300/500/1000伤害";
    allDatas.Add( config.id, config)
;
    config = new ConfigSkill();
    config.id = 430;
    config.name = "斗战齐天";
    config.targetType = new int[]{3,0};
    config.targetQueue = 1;
    config.level1 = new Dictionary<int,int[]>{{15,new int[]{0,100}},{18,new int[]{4,30}}};
    config.level2 = new Dictionary<int,int[]>{{15,new int[]{0,200}},{18,new int[]{5,60}}};
    config.level3 = new Dictionary<int,int[]>{{15,new int[]{0,400}},{18,new int[]{7,100}}};
    config.selfEffect = "430Self";
    config.wholeEffect = "430Whole";
    config.targetEffect = "430Target";
    config.desc = "对方随机一列敌人受到100/200/400伤害，之后自身伤害提升30/60/100%，承受伤害提升45/75/115%，持续4/5/7秒";
    allDatas.Add( config.id, config)
;
    config = new ConfigSkill();
    config.id = 420;
    config.name = "除魔剑刃";
    config.targetType = new int[]{0,2};
    config.targetQueue = 1;
    config.level1 = new Dictionary<int,int[]>{{15,new int[]{0,200}}};
    config.level2 = new Dictionary<int,int[]>{{15,new int[]{0,350}}};
    config.level3 = new Dictionary<int,int[]>{{15,new int[]{0,600}}};
    config.selfEffect = "null";
    config.wholeEffect = "null";
    config.targetEffect = "420Target";
    config.desc = "对敌方随机2名角色造成200/350/600伤害";
    allDatas.Add( config.id, config)
;
    config = new ConfigSkill();
    config.id = 410;
    config.name = "逐日之矢";
    config.targetType = new int[]{3,0};
    config.targetQueue = 1;
    config.level1 = new Dictionary<int,int[]>{{15,new int[]{0,200}}};
    config.level2 = new Dictionary<int,int[]>{{15,new int[]{0,300}}};
    config.level3 = new Dictionary<int,int[]>{{15,new int[]{0,500}}};
    config.selfEffect = "null";
    config.wholeEffect = "null";
    config.targetEffect = "410Target";
    config.desc = "对敌方随机一列目标造成200/300/500的伤害";
    allDatas.Add( config.id, config)
;
    config = new ConfigSkill();
    config.id = 390;
    config.name = "三头六臂";
    config.targetType = new int[]{5,0};
    config.targetQueue = 0;
    config.level1 = new Dictionary<int,int[]>{{18,new int[]{4,30}}};
    config.level2 = new Dictionary<int,int[]>{{18,new int[]{5,45}}};
    config.level3 = new Dictionary<int,int[]>{{18,new int[]{6,70}}};
    config.selfEffect = "390Self";
    config.wholeEffect = "null";
    config.targetEffect = "null";
    config.desc = "在4/5/6秒内自身伤害提升30/45/70%，受到的伤害提升45/60/85%";
    allDatas.Add( config.id, config)
;
    config = new ConfigSkill();
    config.id = 380;
    config.name = "古兽猛击";
    config.targetType = new int[]{4,0};
    config.targetQueue = 1;
    config.level1 = new Dictionary<int,int[]>{{15,new int[]{0,100}}};
    config.level2 = new Dictionary<int,int[]>{{15,new int[]{0,200}}};
    config.level3 = new Dictionary<int,int[]>{{15,new int[]{0,200}}};
    config.selfEffect = "null";
    config.wholeEffect = "null";
    config.targetEffect = "380Target";
    config.desc = "对敌方单体目标造成100/200/300伤害";
    allDatas.Add( config.id, config)
;
    config = new ConfigSkill();
    config.id = 350;
    config.name = "无魂笔";
    config.targetType = new int[]{4,0};
    config.targetQueue = 1;
    config.level1 = new Dictionary<int,int[]>{{12,new int[]{0,0}}};
    config.level2 = new Dictionary<int,int[]>{{12,new int[]{0,0}}};
    config.level3 = new Dictionary<int,int[]>{{12,new int[]{0,0}}};
    config.selfEffect = "350Self";
    config.wholeEffect = "null";
    config.targetEffect = "350Target";
    config.desc = "直接斩杀对方一名角色";
    allDatas.Add( config.id, config)
;
    config = new ConfigSkill();
    config.id = 340;
    config.name = "诛仙剑阵";
    config.targetType = new int[]{1,0};
    config.targetQueue = 1;
    config.level1 = new Dictionary<int,int[]>{{15,new int[]{0,150}}};
    config.level2 = new Dictionary<int,int[]>{{15,new int[]{0,300}}};
    config.level3 = new Dictionary<int,int[]>{{15,new int[]{0,500}}};
    config.selfEffect = "340Self";
    config.wholeEffect = "340Whole";
    config.targetEffect = "340Target";
    config.desc = "对敌方所有目标造成150/300/500伤害";
    allDatas.Add( config.id, config)
;
    config = new ConfigSkill();
    config.id = 330;
    config.name = "神龙庇护";
    config.targetType = new int[]{0,2};
    config.targetQueue = 0;
    config.level1 = new Dictionary<int,int[]>{{13,new int[]{2,0}}};
    config.level2 = new Dictionary<int,int[]>{{13,new int[]{3,0}}};
    config.level3 = new Dictionary<int,int[]>{{13,new int[]{5,0}}};
    config.selfEffect = "null";
    config.wholeEffect = "null";
    config.targetEffect = "330Target";
    config.desc = "我方随机2名角色无敌2/3/5秒";
    allDatas.Add( config.id, config)
;
    config = new ConfigSkill();
    config.id = 320;
    config.name = "黑莲灭世";
    config.targetType = new int[]{0,5};
    config.targetQueue = 1;
    config.level1 = new Dictionary<int,int[]>{{16,new int[]{3,0}},{11,new int[]{3,30}}};
    config.level2 = new Dictionary<int,int[]>{{16,new int[]{4,0}},{11,new int[]{4,50}}};
    config.level3 = new Dictionary<int,int[]>{{16,new int[]{6,0}},{11,new int[]{6,80}}};
    config.selfEffect = "null";
    config.wholeEffect = "320Whole";
    config.targetEffect = "320Target";
    config.desc = "对敌方随机5个目标造成3/4/6秒的沉默和30/50/80%的减速";
    allDatas.Add( config.id, config)
;
    config = new ConfigSkill();
    config.id = 310;
    config.name = "金仙制衡";
    config.targetType = new int[]{3,1};
    config.targetQueue = 1;
    config.level1 = new Dictionary<int,int[]>{{15,new int[]{0,100}},{11,new int[]{2,30}}};
    config.level2 = new Dictionary<int,int[]>{{15,new int[]{0,200}},{11,new int[]{3,30}}};
    config.level3 = new Dictionary<int,int[]>{{15,new int[]{0,300}},{11,new int[]{4,30}}};
    config.selfEffect = "null";
    config.wholeEffect = "null";
    config.targetEffect = "310Target";
    config.desc = "敌方第一列的角色造成100/200/300伤害，并减速30%持续2/3/4秒";
    allDatas.Add( config.id, config)
;
    config = new ConfigSkill();
    config.id = 250;
    config.name = "八仙姿";
    config.targetType = new int[]{2,1};
    config.targetQueue = 1;
    config.level1 = new Dictionary<int,int[]>{{15,new int[]{0,150}},{16,new int[]{2,0}}};
    config.level2 = new Dictionary<int,int[]>{{15,new int[]{0,250}},{16,new int[]{3,0}}};
    config.level3 = new Dictionary<int,int[]>{{15,new int[]{0,400}},{16,new int[]{4,0}}};
    config.selfEffect = "null";
    config.wholeEffect = "null";
    config.targetEffect = "250Target";
    config.desc = "对敌方第一排的角色造成150/250/400伤害和持续2/3/4秒沉默";
    allDatas.Add( config.id, config)
;
    config = new ConfigSkill();
    config.id = 240;
    config.name = "祥兽福泽";
    config.targetType = new int[]{4,0};
    config.targetQueue = 0;
    config.level1 = new Dictionary<int,int[]>{{17,new int[]{3,30}}};
    config.level2 = new Dictionary<int,int[]>{{17,new int[]{4,50}}};
    config.level3 = new Dictionary<int,int[]>{{17,new int[]{5,50}}};
    config.selfEffect = "null";
    config.wholeEffect = "null";
    config.targetEffect = "240Target";
    config.desc = "我方第一位角色全属性增加30/50/50%，持续3/4/5秒";
    allDatas.Add( config.id, config)
;
    config = new ConfigSkill();
    config.id = 230;
    config.name = "封神";
    config.targetType = new int[]{2,1};
    config.targetQueue = 0;
    config.level1 = new Dictionary<int,int[]>{{17,new int[]{2,20}}};
    config.level2 = new Dictionary<int,int[]>{{17,new int[]{3,30}}};
    config.level3 = new Dictionary<int,int[]>{{17,new int[]{4,40}}};
    config.selfEffect = "null";
    config.wholeEffect = "230Whole";
    config.targetEffect = "230Target";
    config.desc = "我方第一排角色全属性增加20/30/40%，持续2/3/4s";
    allDatas.Add( config.id, config)
;
    config = new ConfigSkill();
    config.id = 210;
    config.name = "蛇神之怒";
    config.targetType = new int[]{3,0};
    config.targetQueue = 1;
    config.level1 = new Dictionary<int,int[]>{{15,new int[]{0,150}},{11,new int[]{3,20}}};
    config.level2 = new Dictionary<int,int[]>{{15,new int[]{0,250}},{11,new int[]{3,40}}};
    config.level3 = new Dictionary<int,int[]>{{15,new int[]{0,400}},{11,new int[]{3,60}}};
    config.selfEffect = "null";
    config.wholeEffect = "210Whole";
    config.targetEffect = "210Target";
    config.desc = "对敌方随机一列目标造成150/250/400伤害，并减速20/40/60%，持续3秒";
    allDatas.Add( config.id, config)
;
    config = new ConfigSkill();
    config.id = 200;
    config.name = "地震天撼";
    config.targetType = new int[]{1,0};
    config.targetQueue = 1;
    config.level1 = new Dictionary<int,int[]>{{10,new int[]{2,0}}};
    config.level2 = new Dictionary<int,int[]>{{10,new int[]{3,0}}};
    config.level3 = new Dictionary<int,int[]>{{10,new int[]{5,0}}};
    config.selfEffect = "200Self";
    config.wholeEffect = "200Whole";
    config.targetEffect = "200Target";
    config.desc = "眩晕敌方全体角色2/3/5秒";
    allDatas.Add( config.id, config)
;
    config = new ConfigSkill();
    config.id = 150;
    config.name = "大鹏展翅";
    config.targetType = new int[]{2,0};
    config.targetQueue = 1;
    config.level1 = new Dictionary<int,int[]>{{10,new int[]{1,0}},{15,new int[]{0,150}}};
    config.level2 = new Dictionary<int,int[]>{{10,new int[]{2,0}},{15,new int[]{0,300}}};
    config.level3 = new Dictionary<int,int[]>{{10,new int[]{3,0}},{15,new int[]{0,500}}};
    config.selfEffect = "null";
    config.wholeEffect = "null";
    config.targetEffect = "150Target";
    config.desc = "敌方随机一排角色受到150/300/500伤害，并眩晕1/2/3秒";
    allDatas.Add( config.id, config)
;
    config = new ConfigSkill();
    config.id = 140;
    config.name = "地狱恫吓";
    config.targetType = new int[]{0,3};
    config.targetQueue = 1;
    config.level1 = new Dictionary<int,int[]>{{15,new int[]{0,80}},{10,new int[]{1,0}}};
    config.level2 = new Dictionary<int,int[]>{{15,new int[]{0,120}},{10,new int[]{1,0}}};
    config.level3 = new Dictionary<int,int[]>{{15,new int[]{0,200}},{10,new int[]{1,0}}};
    config.selfEffect = "null";
    config.wholeEffect = "140Whole";
    config.targetEffect = "null";
    config.desc = "随机三个目标造成80/120/200伤害以及1秒的眩晕";
    allDatas.Add( config.id, config)
;
    config = new ConfigSkill();
    config.id = 450;
    config.name = "迷魂汤";
    config.targetType = new int[]{0,1};
    config.targetQueue = 1;
    config.level1 = new Dictionary<int,int[]>{{15,new int[]{0,100}},{10,new int[]{2,0}}};
    config.level2 = new Dictionary<int,int[]>{{15,new int[]{0,200}},{10,new int[]{3,0}}};
    config.level3 = new Dictionary<int,int[]>{{15,new int[]{0,300}},{10,new int[]{4,0}}};
    config.selfEffect = "null";
    config.wholeEffect = "null";
    config.targetEffect = "450Target";
    config.desc = "对随机一名角色造成100/200/300伤害，并沉默2/3/4秒";
    allDatas.Add( config.id, config)
;
    config = new ConfigSkill();
    config.id = 400;
    config.name = "三昧真火";
    config.targetType = new int[]{2,1};
    config.targetQueue = 1;
    config.level1 = new Dictionary<int,int[]>{{15,new int[]{0,100}}};
    config.level2 = new Dictionary<int,int[]>{{15,new int[]{0,200}}};
    config.level3 = new Dictionary<int,int[]>{{15,new int[]{0,300}}};
    config.selfEffect = "null";
    config.wholeEffect = "400Whole";
    config.targetEffect = "null";
    config.desc = "对第一排角色造成100/200/300伤害";
    allDatas.Add( config.id, config)
;
    config = new ConfigSkill();
    config.id = 300;
    config.name = "白骨冤魂";
    config.targetType = new int[]{0,1};
    config.targetQueue = 1;
    config.level1 = new Dictionary<int,int[]>{{15,new int[]{0,200}}};
    config.level2 = new Dictionary<int,int[]>{{15,new int[]{0,300}}};
    config.level3 = new Dictionary<int,int[]>{{15,new int[]{0,400}}};
    config.selfEffect = "null";
    config.wholeEffect = "null";
    config.targetEffect = "300Target";
    config.desc = "对敌方随机一名角色造成200/300/400伤害";
    allDatas.Add( config.id, config)
;
    config = new ConfigSkill();
    config.id = 290;
    config.name = "请宝贝转身";
    config.targetType = new int[]{0,1};
    config.targetQueue = 1;
    config.level1 = new Dictionary<int,int[]>{{15,new int[]{0,150}}};
    config.level2 = new Dictionary<int,int[]>{{15,new int[]{0,250}}};
    config.level3 = new Dictionary<int,int[]>{{15,new int[]{0,400}}};
    config.selfEffect = "null";
    config.wholeEffect = "null";
    config.targetEffect = "290Target";
    config.desc = "对敌方随机一名角色造成150/250/400伤害";
    allDatas.Add( config.id, config)
;
    config = new ConfigSkill();
    config.id = 280;
    config.name = "风回流转";
    config.targetType = new int[]{2,1};
    config.targetQueue = 1;
    config.level1 = new Dictionary<int,int[]>{{15,new int[]{0,80}},{11,new int[]{2,20}}};
    config.level2 = new Dictionary<int,int[]>{{15,new int[]{0,150}},{11,new int[]{3,20}}};
    config.level3 = new Dictionary<int,int[]>{{15,new int[]{0,250}},{11,new int[]{4,20}}};
    config.selfEffect = "null";
    config.wholeEffect = "null";
    config.targetEffect = "280Target";
    config.desc = "对敌方第一排目标造成80/150/250伤害，并减速20%持续2/3/4秒，";
    allDatas.Add( config.id, config)
;
    config = new ConfigSkill();
    config.id = 270;
    config.name = "镇压八方";
    config.targetType = new int[]{4,0};
    config.targetQueue = 1;
    config.level1 = new Dictionary<int,int[]>{{10,new int[]{1,0}}};
    config.level2 = new Dictionary<int,int[]>{{10,new int[]{2,0}}};
    config.level3 = new Dictionary<int,int[]>{{10,new int[]{4,0}}};
    config.selfEffect = "null";
    config.wholeEffect = "null";
    config.targetEffect = "270Target";
    config.desc = "敌方第一名角色眩晕1/2/4秒";
    allDatas.Add( config.id, config)
;
    config = new ConfigSkill();
    config.id = 190;
    config.name = "洛水负书";
    config.targetType = new int[]{2,0};
    config.targetQueue = 0;
    config.level1 = new Dictionary<int,int[]>{{13,new int[]{1,0}}};
    config.level2 = new Dictionary<int,int[]>{{13,new int[]{2,0}}};
    config.level3 = new Dictionary<int,int[]>{{13,new int[]{3,0}}};
    config.selfEffect = "null";
    config.wholeEffect = "null";
    config.targetEffect = "330Target";
    config.desc = "我方随机1排角色无敌1/2/3秒";
    allDatas.Add( config.id, config)
;
    config = new ConfigSkill();
    config.id = 130;
    config.name = "道友请留步";
    config.targetType = new int[]{4,0};
    config.targetQueue = 1;
    config.level1 = new Dictionary<int,int[]>{{11,new int[]{2,30}},{15,new int[]{0,100}}};
    config.level2 = new Dictionary<int,int[]>{{11,new int[]{2,40}},{15,new int[]{0,200}}};
    config.level3 = new Dictionary<int,int[]>{{11,new int[]{2,50}},{15,new int[]{0,300}}};
    config.selfEffect = "null";
    config.wholeEffect = "null";
    config.targetEffect = "130Target";
    config.desc = "敌方第一位角色受到100/200/300伤害，并减速30/40/50%持续2秒";
    allDatas.Add( config.id, config)
;
    config = new ConfigSkill();
    config.id = 120;
    config.name = "玉露还精";
    config.targetType = new int[]{4,0};
    config.targetQueue = 0;
    config.level1 = new Dictionary<int,int[]>{{14,new int[]{0,150}}};
    config.level2 = new Dictionary<int,int[]>{{14,new int[]{0,300}}};
    config.level3 = new Dictionary<int,int[]>{{14,new int[]{0,500}}};
    config.selfEffect = "null";
    config.wholeEffect = "null";
    config.targetEffect = "120Target";
    config.desc = "我方第一位角色治疗150/300/500生命";
    allDatas.Add( config.id, config)
;
    base.Init();
    }
}
}