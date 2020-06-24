using System.Collections.Generic;
using System;
namespace DataClass
{

    public class ConfigRoleManager : ConfigDataManager<ConfigRoleManager,ConfigRole>

    {
    
    public override void Init( )
    {
    name = "ConfigRole";
    ConfigRole config = null;
    
    config = new ConfigRole();
    config.id = 50;
    config.name = "青帝";
    config.attributes = new int[][]{new int[]{95,36,40,36,30,400,100},new int[]{140,40,40,40,30,700,100},new int[]{200,50,50,50,40,1000,100}};
    config.cooperId = 105;
    config.proId = 201;
    config.cost = 5;
    config.skillId = 500;
    allDatas.Add( config.id, config)
;
    config = new ConfigRole();
    config.id = 49;
    config.name = "女娲";
    config.attributes = new int[][]{new int[]{80,36,40,36,30,400,150},new int[]{120,50,50,50,30,720,150},new int[]{180,60,60,60,40,900,150}};
    config.cooperId = 103;
    config.proId = 201;
    config.cost = 5;
    config.skillId = 490;
    allDatas.Add( config.id, config)
;
    config = new ConfigRole();
    config.id = 44;
    config.name = "蚩尤";
    config.attributes = new int[][]{new int[]{95,36,40,36,15,450,100},new int[]{130,40,40,40,15,700,100},new int[]{185,50,50,50,15,1000,100}};
    config.cooperId = 104;
    config.proId = 202;
    config.cost = 5;
    config.skillId = 440;
    allDatas.Add( config.id, config)
;
    config = new ConfigRole();
    config.id = 43;
    config.name = "孙悟空";
    config.attributes = new int[][]{new int[]{100,36,20,36,20,425,100},new int[]{150,36,20,36,20,640,100},new int[]{200,36,20,36,20,1200,100}};
    config.cooperId = 101;
    config.proId = 202;
    config.cost = 5;
    config.skillId = 430;
    allDatas.Add( config.id, config)
;
    config = new ConfigRole();
    config.id = 35;
    config.name = "判官";
    config.attributes = new int[][]{new int[]{90,34,10,34,20,425,125},new int[]{135,34,10,34,20,640,100},new int[]{180,34,58,34,20,1000,75}};
    config.cooperId = 102;
    config.proId = 203;
    config.cost = 5;
    config.skillId = 350;
    allDatas.Add( config.id, config)
;
    config = new ConfigRole();
    config.id = 34;
    config.name = "通天教主";
    config.attributes = new int[][]{new int[]{100,30,20,25,25,425,100},new int[]{150,30,20,25,25,640,100},new int[]{200,35,20,25,25,1300,100}};
    config.cooperId = 100;
    config.proId = 203;
    config.cost = 5;
    config.skillId = 340;
    allDatas.Add( config.id, config)
;
    config = new ConfigRole();
    config.id = 15;
    config.name = "鲲鹏";
    config.attributes = new int[][]{new int[]{100,36,40,36,30,800,120},new int[]{140,40,40,40,30,1000,120},new int[]{200,60,60,60,40,1500,100}};
    config.cooperId = 106;
    config.proId = 206;
    config.cost = 5;
    config.skillId = 150;
    allDatas.Add( config.id, config)
;
    config = new ConfigRole();
    config.id = 48;
    config.name = "九尾狐";
    config.attributes = new int[][]{new int[]{85,35,40,35,30,500,80},new int[]{115,35,40,35,35,700,80},new int[]{155,32,40,35,40,1000,80}};
    config.cooperId = 106;
    config.proId = 201;
    config.cost = 4;
    config.skillId = 480;
    allDatas.Add( config.id, config)
;
    config = new ConfigRole();
    config.id = 42;
    config.name = "钟馗";
    config.attributes = new int[][]{new int[]{80,32,10,32,20,400,100},new int[]{120,32,10,32,20,600,100},new int[]{160,32,10,32,20,800,100}};
    config.cooperId = 102;
    config.proId = 202;
    config.cost = 4;
    config.skillId = 420;
    allDatas.Add( config.id, config)
;
    config = new ConfigRole();
    config.id = 41;
    config.name = "后羿";
    config.attributes = new int[][]{new int[]{90,20,50,20,20,375,100},new int[]{135,20,50,20,20,565,100},new int[]{180,20,50,20,20,750,100}};
    config.cooperId = 100;
    config.proId = 202;
    config.cost = 4;
    config.skillId = 410;
    allDatas.Add( config.id, config)
;
    config = new ConfigRole();
    config.id = 33;
    config.name = "应龙";
    config.attributes = new int[][]{new int[]{80,30,20,28,20,425,100},new int[]{120,30,20,28,20,630,100},new int[]{160,30,20,28,20,850,100}};
    config.cooperId = 101;
    config.proId = 203;
    config.cost = 4;
    config.skillId = 330;
    allDatas.Add( config.id, config)
;
    config = new ConfigRole();
    config.id = 32;
    config.name = "无天佛祖";
    config.attributes = new int[][]{new int[]{85,30,25,35,25,425,120},new int[]{120,30,25,35,25,680,120},new int[]{160,30,25,30,25,850,120}};
    config.cooperId = 104;
    config.proId = 204;
    config.cost = 4;
    config.skillId = 320;
    allDatas.Add( config.id, config)
;
    config = new ConfigRole();
    config.id = 25;
    config.name = "吕洞宾";
    config.attributes = new int[][]{new int[]{90,35,40,35,30,450,80},new int[]{140,35,40,35,35,640,80},new int[]{180,32,40,35,40,850,80}};
    config.cooperId = 105;
    config.proId = 204;
    config.cost = 4;
    config.skillId = 250;
    allDatas.Add( config.id, config)
;
    config = new ConfigRole();
    config.id = 21;
    config.name = "相柳";
    config.attributes = new int[][]{new int[]{90,35,20,35,20,450,80},new int[]{120,35,20,35,20,640,80},new int[]{160,32,25,35,25,850,80}};
    config.cooperId = 104;
    config.proId = 205;
    config.cost = 4;
    config.skillId = 210;
    allDatas.Add( config.id, config)
;
    config = new ConfigRole();
    config.id = 20;
    config.name = "夸父";
    config.attributes = new int[][]{new int[]{90,35,20,35,20,450,150},new int[]{110,35,20,35,20,600,150},new int[]{150,40,25,35,25,900,130}};
    config.cooperId = 103;
    config.proId = 205;
    config.cost = 4;
    config.skillId = 200;
    allDatas.Add( config.id, config)
;
    config = new ConfigRole();
    config.id = 47;
    config.name = "嫦娥";
    config.attributes = new int[][]{new int[]{70,28,30,28,25,350,100},new int[]{100,28,30,28,25,520,100},new int[]{140,28,30,28,25,700,100}};
    config.cooperId = 105;
    config.proId = 201;
    config.cost = 3;
    config.skillId = 470;
    allDatas.Add( config.id, config)
;
    config = new ConfigRole();
    config.id = 46;
    config.name = "太子长琴";
    config.attributes = new int[][]{new int[]{72,28,30,28,25,360,120},new int[]{105,28,30,28,25,500,120},new int[]{150,28,30,28,25,715,100}};
    config.cooperId = 103;
    config.proId = 201;
    config.cost = 3;
    config.skillId = 460;
    allDatas.Add( config.id, config)
;
    config = new ConfigRole();
    config.id = 45;
    config.name = "孟婆";
    config.attributes = new int[][]{new int[]{70,28,10,28,25,350,105},new int[]{105,28,10,28,25,525,105},new int[]{140,28,10,28,25,700,105}};
    config.cooperId = 102;
    config.proId = 201;
    config.cost = 3;
    config.skillId = 450;
    allDatas.Add( config.id, config)
;
    config = new ConfigRole();
    config.id = 40;
    config.name = "红孩儿";
    config.attributes = new int[][]{new int[]{75,30,20,24,20,350,80},new int[]{115,30,20,24,20,525,80},new int[]{150,30,20,24,20,700,80}};
    config.cooperId = 101;
    config.proId = 202;
    config.cost = 3;
    config.skillId = 400;
    allDatas.Add( config.id, config)
;
    config = new ConfigRole();
    config.id = 39;
    config.name = "哪吒";
    config.attributes = new int[][]{new int[]{65,24,25,20,20,300,100},new int[]{95,24,25,20,20,450,100},new int[]{130,24,25,20,20,600,100}};
    config.cooperId = 100;
    config.proId = 202;
    config.cost = 3;
    config.skillId = 390;
    allDatas.Add( config.id, config)
;
    config = new ConfigRole();
    config.id = 31;
    config.name = "太乙真人";
    config.attributes = new int[][]{new int[]{70,30,30,28,25,350,100},new int[]{100,30,30,28,25,520,100},new int[]{140,30,30,28,25,700,100}};
    config.cooperId = 105;
    config.proId = 203;
    config.cost = 3;
    config.skillId = 310;
    allDatas.Add( config.id, config)
;
    config = new ConfigRole();
    config.id = 30;
    config.name = "白骨精";
    config.attributes = new int[][]{new int[]{70,24,10,30,20,350,125},new int[]{105,24,10,30,20,525,125},new int[]{140,24,10,30,20,700,125}};
    config.cooperId = 101;
    config.proId = 203;
    config.cost = 3;
    config.skillId = 300;
    allDatas.Add( config.id, config)
;
    config = new ConfigRole();
    config.id = 24;
    config.name = "白泽";
    config.attributes = new int[][]{new int[]{70,30,30,28,30,350,100},new int[]{100,30,30,28,35,520,100},new int[]{140,30,30,28,40,700,100}};
    config.cooperId = 106;
    config.proId = 204;
    config.cost = 3;
    config.skillId = 240;
    allDatas.Add( config.id, config)
;
    config = new ConfigRole();
    config.id = 23;
    config.name = "姜子牙";
    config.attributes = new int[][]{new int[]{80,24,20,24,25,325,100},new int[]{120,24,20,24,25,490,100},new int[]{160,24,20,24,25,650,100}};
    config.cooperId = 100;
    config.proId = 204;
    config.cost = 3;
    config.skillId = 230;
    allDatas.Add( config.id, config)
;
    config = new ConfigRole();
    config.id = 38;
    config.name = "穷奇";
    config.attributes = new int[][]{new int[]{62,24,30,24,20,320,100},new int[]{79,24,30,24,20,445,100},new int[]{125,24,30,24,25,620,100}};
    config.cooperId = 106;
    config.proId = 202;
    config.cost = 2;
    config.skillId = 380;
    allDatas.Add( config.id, config)
;
    config = new ConfigRole();
    config.id = 29;
    config.name = "陆压道人";
    config.attributes = new int[][]{new int[]{60,24,30,24,20,300,100},new int[]{85,24,30,24,20,450,100},new int[]{125,24,30,24,25,600,100}};
    config.cooperId = 105;
    config.proId = 203;
    config.cost = 2;
    config.skillId = 290;
    allDatas.Add( config.id, config)
;
    config = new ConfigRole();
    config.id = 28;
    config.name = "箕伯";
    config.attributes = new int[][]{new int[]{60,24,30,24,20,300,100},new int[]{80,24,30,24,20,450,100},new int[]{120,24,30,24,25,600,100}};
    config.cooperId = 104;
    config.proId = 203;
    config.cost = 2;
    config.skillId = 280;
    allDatas.Add( config.id, config)
;
    config = new ConfigRole();
    config.id = 27;
    config.name = "李靖";
    config.attributes = new int[][]{new int[]{60,26,10,24,20,300,75},new int[]{90,26,10,24,20,450,75},new int[]{120,26,10,24,20,600,75}};
    config.cooperId = 100;
    config.proId = 203;
    config.cost = 2;
    config.skillId = 270;
    allDatas.Add( config.id, config)
;
    config = new ConfigRole();
    config.id = 19;
    config.name = "玄龟";
    config.attributes = new int[][]{new int[]{50,35,30,24,20,300,100},new int[]{75,35,30,24,20,480,100},new int[]{120,40,30,40,25,680,100}};
    config.cooperId = 106;
    config.proId = 205;
    config.cost = 2;
    config.skillId = 190;
    allDatas.Add( config.id, config)
;
    config = new ConfigRole();
    config.id = 14;
    config.name = "罗刹";
    config.attributes = new int[][]{new int[]{70,24,10,24,20,300,100},new int[]{105,24,10,24,20,450,100},new int[]{140,24,10,24,20,600,100}};
    config.cooperId = 102;
    config.proId = 206;
    config.cost = 2;
    config.skillId = 140;
    allDatas.Add( config.id, config)
;
    config = new ConfigRole();
    config.id = 13;
    config.name = "申公豹";
    config.attributes = new int[][]{new int[]{55,26,10,26,25,310,100},new int[]{77,26,10,26,25,460,100},new int[]{110,30,10,26,25,620,100}};
    config.cooperId = 101;
    config.proId = 206;
    config.cost = 2;
    config.skillId = 130;
    allDatas.Add( config.id, config)
;
    config = new ConfigRole();
    config.id = 12;
    config.name = "青鸾";
    config.attributes = new int[][]{new int[]{50,22,10,22,20,300,100},new int[]{75,22,10,22,20,450,100},new int[]{100,22,15,22,20,600,100}};
    config.cooperId = 101;
    config.proId = 206;
    config.cost = 2;
    config.skillId = 120;
    allDatas.Add( config.id, config)
;
    config = new ConfigRole();
    config.id = 37;
    config.name = "巨灵神";
    config.attributes = new int[][]{new int[]{55,20,20,20,20,260,100},new int[]{65,20,20,20,20,400,100},new int[]{100,30,20,30,20,500,100}};
    config.cooperId = 105;
    config.proId = 202;
    config.cost = 1;
    config.skillId = 0;
    allDatas.Add( config.id, config)
;
    config = new ConfigRole();
    config.id = 36;
    config.name = "夜叉";
    config.attributes = new int[][]{new int[]{50,20,5,20,20,250,100},new int[]{75,20,5,20,20,375,100},new int[]{100,20,5,20,20,500,100}};
    config.cooperId = 102;
    config.proId = 202;
    config.cost = 1;
    config.skillId = 0;
    allDatas.Add( config.id, config)
;
    config = new ConfigRole();
    config.id = 26;
    config.name = "精卫";
    config.attributes = new int[][]{new int[]{50,20,25,20,20,250,100},new int[]{60,20,30,20,20,340,100},new int[]{100,20,35,20,20,500,100}};
    config.cooperId = 103;
    config.proId = 203;
    config.cost = 1;
    config.skillId = 0;
    allDatas.Add( config.id, config)
;
    config = new ConfigRole();
    config.id = 22;
    config.name = "神农";
    config.attributes = new int[][]{new int[]{60,28,10,28,25,325,100},new int[]{90,28,10,28,25,490,100},new int[]{120,28,10,28,25,650,100}};
    config.cooperId = 100;
    config.proId = 204;
    config.cost = 1;
    config.skillId = 0;
    allDatas.Add( config.id, config)
;
    config = new ConfigRole();
    config.id = 18;
    config.name = "巴蛇";
    config.attributes = new int[][]{new int[]{55,20,20,20,20,270,100},new int[]{65,20,20,20,20,390,100},new int[]{85,20,20,20,20,480,100}};
    config.cooperId = 106;
    config.proId = 205;
    config.cost = 1;
    config.skillId = 0;
    allDatas.Add( config.id, config)
;
    config = new ConfigRole();
    config.id = 17;
    config.name = "小钻风";
    config.attributes = new int[][]{new int[]{40,25,15,15,15,275,100},new int[]{60,25,15,15,15,415,100},new int[]{80,25,15,15,15,550,100}};
    config.cooperId = 101;
    config.proId = 205;
    config.cost = 1;
    config.skillId = 0;
    allDatas.Add( config.id, config)
;
    config = new ConfigRole();
    config.id = 16;
    config.name = "吴刚";
    config.attributes = new int[][]{new int[]{40,25,10,25,20,250,100},new int[]{60,25,10,25,20,375,100},new int[]{80,25,10,25,20,500,100}};
    config.cooperId = 100;
    config.proId = 205;
    config.cost = 1;
    config.skillId = 0;
    allDatas.Add( config.id, config)
;
    config = new ConfigRole();
    config.id = 11;
    config.name = "土行孙";
    config.attributes = new int[][]{new int[]{55,22,25,20,20,225,100},new int[]{80,22,25,20,20,335,100},new int[]{110,22,25,20,20,450,100}};
    config.cooperId = 100;
    config.proId = 206;
    config.cost = 1;
    config.skillId = 0;
    allDatas.Add( config.id, config)
;
    config = new ConfigRole();
    config.id = 10;
    config.name = "雷震子";
    config.attributes = new int[][]{new int[]{50,20,30,20,20,225,100},new int[]{75,20,30,20,20,335,100},new int[]{100,20,30,20,20,450,100}};
    config.cooperId = 100;
    config.proId = 206;
    config.cost = 1;
    config.skillId = 0;
    allDatas.Add( config.id, config)
;
    base.Init();
    }
}
}