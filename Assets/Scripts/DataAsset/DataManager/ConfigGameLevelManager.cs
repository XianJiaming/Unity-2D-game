using System.Collections.Generic;
using System;
namespace DataClass
{

    public class ConfigGameLevelManager : ConfigDataManager<ConfigGameLevelManager,ConfigGameLevel>

    {
    
    public override void Init( )
    {
    name = "ConfigGameLevel";
    ConfigGameLevel config = null;
    
    config = new ConfigGameLevel();
    config.id = 1;
    config.level = 1;
    config.roles = new Dictionary<int,int[]>{{0,new int[]{11,1,0,0}},{3,new int[]{10,1,0,0}}};
    allDatas.Add( config.id, config)
;
    config = new ConfigGameLevel();
    config.id = 2;
    config.level = 2;
    config.roles = new Dictionary<int,int[]>{{1,new int[]{19,1,0,0}},{3,new int[]{18,1,0,0}}};
    allDatas.Add( config.id, config)
;
    config = new ConfigGameLevel();
    config.id = 3;
    config.level = 3;
    config.roles = new Dictionary<int,int[]>{{1,new int[]{27,1,0,0}},{0,new int[]{22,2,0,0}},{2,new int[]{23,1,0,0}}};
    allDatas.Add( config.id, config)
;
    config = new ConfigGameLevel();
    config.id = 4;
    config.level = 4;
    config.roles = new Dictionary<int,int[]>{{1,new int[]{46,1,0,0}},{3,new int[]{26,2,6,0}},{5,new int[]{28,1,0,0}}};
    allDatas.Add( config.id, config)
;
    config = new ConfigGameLevel();
    config.id = 5;
    config.level = 5;
    config.roles = new Dictionary<int,int[]>{{0,new int[]{37,2,0,0}},{1,new int[]{38,2,0,0}},{2,new int[]{39,1,5,0}},{4,new int[]{40,1,0,19}}};
    allDatas.Add( config.id, config)
;
    config = new ConfigGameLevel();
    config.id = 6;
    config.level = 6;
    config.roles = new Dictionary<int,int[]>{{0,new int[]{45,2,0,0}},{2,new int[]{46,1,0,16}},{6,new int[]{29,2,0,0}},{8,new int[]{26,1,0,0}}};
    allDatas.Add( config.id, config)
;
    config = new ConfigGameLevel();
    config.id = 7;
    config.level = 7;
    config.roles = new Dictionary<int,int[]>{{1,new int[]{17,1,0,20}},{3,new int[]{18,2,0,0}},{5,new int[]{19,2,0,0}},{7,new int[]{20,1,9,20}}};
    allDatas.Add( config.id, config)
;
    config = new ConfigGameLevel();
    config.id = 8;
    config.level = 8;
    config.roles = new Dictionary<int,int[]>{{1,new int[]{22,2,6,0}},{4,new int[]{23,2,0,0}},{6,new int[]{25,1,0,0}},{8,new int[]{32,1,4,0}}};
    allDatas.Add( config.id, config)
;
    config = new ConfigGameLevel();
    config.id = 9;
    config.level = 9;
    config.roles = new Dictionary<int,int[]>{{0,new int[]{18,3,0,20}},{1,new int[]{19,2,0,20}},{2,new int[]{20,2,0,20}},{6,new int[]{21,2,10,0}},{8,new int[]{28,3,10,0}}};
    allDatas.Add( config.id, config)
;
    config = new ConfigGameLevel();
    config.id = 10;
    config.level = 10;
    config.roles = new Dictionary<int,int[]>{{1,new int[]{48,1,0,0}},{4,new int[]{31,2,0,17}},{6,new int[]{29,2,7,0}},{7,new int[]{25,1,5,15}},{8,new int[]{47,2,9,0}}};
    allDatas.Add( config.id, config)
;
    config = new ConfigGameLevel();
    config.id = 11;
    config.level = 11;
    config.roles = new Dictionary<int,int[]>{{0,new int[]{39,1,0,0}},{2,new int[]{30,2,0,0}},{3,new int[]{32,2,8,0}},{5,new int[]{28,2,8,0}},{7,new int[]{44,1,7,0}}};
    allDatas.Add( config.id, config)
;
    config = new ConfigGameLevel();
    config.id = 12;
    config.level = 12;
    config.roles = new Dictionary<int,int[]>{{1,new int[]{48,2,0,0}},{3,new int[]{45,2,0,19}},{5,new int[]{46,2,0,19}},{6,new int[]{19,2,0,16}},{7,new int[]{47,2,5,0}}};
    allDatas.Add( config.id, config)
;
    config = new ConfigGameLevel();
    config.id = 13;
    config.level = 13;
    config.roles = new Dictionary<int,int[]>{{1,new int[]{19,2,0,12}},{3,new int[]{23,2,6,17}},{4,new int[]{24,2,6,17}},{5,new int[]{25,2,6,17}},{7,new int[]{32,2,4,14}}};
    allDatas.Add( config.id, config)
;
    config = new ConfigGameLevel();
    config.id = 14;
    config.level = 14;
    config.roles = new Dictionary<int,int[]>{{0,new int[]{13,2,0,18}},{1,new int[]{15,2,9,20}},{2,new int[]{12,2,0,18}},{3,new int[]{10,3,3,0}},{5,new int[]{11,3,8,0}},{7,new int[]{14,3,1,15}}};
    allDatas.Add( config.id, config)
;
    config = new ConfigGameLevel();
    config.id = 15;
    config.level = 15;
    config.roles = new Dictionary<int,int[]>{{0,new int[]{48,2,9,0}},{1,new int[]{15,2,9,11}},{2,new int[]{24,3,9,15}},{6,new int[]{38,2,10,20}},{7,new int[]{19,3,10,15}},{8,new int[]{18,2,10,15}}};
    allDatas.Add( config.id, config)
;
    config = new ConfigGameLevel();
    config.id = 16;
    config.level = 16;
    config.roles = new Dictionary<int,int[]>{{1,new int[]{50,2,1,0}},{3,new int[]{46,2,0,17}},{5,new int[]{48,2,0,13}},{6,new int[]{45,2,6,17}},{7,new int[]{49,2,6,14}},{8,new int[]{47,2,0,17}}};
    allDatas.Add( config.id, config)
;
    config = new ConfigGameLevel();
    config.id = 17;
    config.level = 17;
    config.roles = new Dictionary<int,int[]>{{1,new int[]{22,3,0,0}},{3,new int[]{25,2,6,0}},{4,new int[]{23,3,6,4}},{5,new int[]{24,3,6,14}},{6,new int[]{29,3,5,0}},{7,new int[]{50,2,1,14}},{8,new int[]{31,3,5,0}}};
    allDatas.Add( config.id, config)
;
    config = new ConfigGameLevel();
    config.id = 18;
    config.level = 18;
    config.roles = new Dictionary<int,int[]>{{1,new int[]{24,2,0,0}},{3,new int[]{38,2,8,0}},{4,new int[]{43,2,3,15}},{5,new int[]{40,2,8,0}},{6,new int[]{21,2,8,0}},{7,new int[]{44,2,2,15}},{8,new int[]{32,2,8,0}}};
    allDatas.Add( config.id, config)
;
    config = new ConfigGameLevel();
    config.id = 19;
    config.level = 19;
    config.roles = new Dictionary<int,int[]>{{0,new int[]{18,2,10,20}},{1,new int[]{21,2,10,20}},{2,new int[]{19,2,10,20}},{3,new int[]{16,2,10,20}},{4,new int[]{20,2,10,20}},{5,new int[]{17,2,10,20}},{7,new int[]{44,2,5,17}}};
    allDatas.Add( config.id, config)
;
    config = new ConfigGameLevel();
    config.id = 20;
    config.level = 20;
    config.roles = new Dictionary<int,int[]>{{0,new int[]{20,2,0,17}},{2,new int[]{25,2,0,17}},{3,new int[]{26,2,9,14}},{4,new int[]{32,2,9,14}},{5,new int[]{46,2,9,14}},{6,new int[]{24,2,9,14}},{7,new int[]{49,2,9,14}},{8,new int[]{23,2,9,14}}};
    allDatas.Add( config.id, config)
;
    config = new ConfigGameLevel();
    config.id = 21;
    config.level = 21;
    config.roles = new Dictionary<int,int[]>{{0,new int[]{30,2,0,12}},{2,new int[]{33,2,10,12}},{3,new int[]{43,2,10,20}},{4,new int[]{40,2,5,20}},{5,new int[]{12,2,2,12}},{6,new int[]{13,2,10,20}},{7,new int[]{41,2,8,20}},{8,new int[]{39,2,5,20}}};
    allDatas.Add( config.id, config)
;
    config = new ConfigGameLevel();
    config.id = 22;
    config.level = 22;
    config.roles = new Dictionary<int,int[]>{{0,new int[]{50,2,1,20}},{2,new int[]{48,2,0,11}},{3,new int[]{26,3,8,15}},{4,new int[]{20,2,2,15}},{5,new int[]{47,3,6,17}},{6,new int[]{46,2,6,17}},{7,new int[]{49,2,6,12}},{8,new int[]{45,3,6,17}}};
    allDatas.Add( config.id, config)
;
    config = new ConfigGameLevel();
    config.id = 23;
    config.level = 23;
    config.roles = new Dictionary<int,int[]>{{0,new int[]{24,2,10,20}},{1,new int[]{15,2,10,11}},{2,new int[]{48,3,5,12}},{3,new int[]{38,2,8,20}},{4,new int[]{19,3,8,20}},{5,new int[]{18,2,10,20}},{6,new int[]{47,3,6,14}},{8,new int[]{32,2,1,14}}};
    allDatas.Add( config.id, config)
;
    config = new ConfigGameLevel();
    config.id = 24;
    config.level = 24;
    config.roles = new Dictionary<int,int[]>{{0,new int[]{27,2,4,14}},{1,new int[]{34,2,1,14}},{2,new int[]{23,2,6,14}},{3,new int[]{11,2,3,14}},{4,new int[]{16,2,2,14}},{5,new int[]{10,2,7,14}},{6,new int[]{39,2,5,14}},{7,new int[]{41,2,8,14}},{8,new int[]{22,3,6,16}}};
    allDatas.Add( config.id, config)
;
    config = new ConfigGameLevel();
    config.id = 25;
    config.level = 25;
    config.roles = new Dictionary<int,int[]>{{0,new int[]{31,2,4,17}},{1,new int[]{50,2,1,11}},{2,new int[]{32,3,6,14}},{3,new int[]{20,2,2,17}},{4,new int[]{47,3,6,14}},{5,new int[]{46,2,6,17}},{6,new int[]{26,2,5,14}},{7,new int[]{49,2,6,12}},{8,new int[]{25,3,5,14}}};
    allDatas.Add( config.id, config)
;
    base.Init();
    }
}
}