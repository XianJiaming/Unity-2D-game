using System.Collections.Generic;
using System;
namespace DataClass
{

    public class ConfigCooperationManager : ConfigDataManager<ConfigCooperationManager,ConfigCooperation>

    {
    
    public override void Init( )
    {
    name = "ConfigCooperation";
    ConfigCooperation config = null;
    
    config = new ConfigCooperation();
    config.id = 100;
    config.name = "人";
    config.counts = new int[]{3,6,9};
    config.attributes = new Dictionary<int,int[]>{{0,new int[]{10,30,50}},{1,new int[]{10,30,50}},{5,new int[]{10,30,50}},{3,new int[]{10,30,50}}};
    config.range = new int[]{1,1,2};
    config.type = 2;
    allDatas.Add( config.id, config)
;
    config = new ConfigCooperation();
    config.id = 101;
    config.name = "妖";
    config.counts = new int[]{3,6};
    config.attributes = new Dictionary<int,int[]>{{3,new int[]{40,80}}};
    config.range = new int[]{1,2};
    config.type = 1;
    allDatas.Add( config.id, config)
;
    config = new ConfigCooperation();
    config.id = 102;
    config.name = "鬼";
    config.counts = new int[]{2,4};
    config.attributes = new Dictionary<int,int[]>{{1,new int[]{40,80}}};
    config.range = new int[]{2,2};
    config.type = 1;
    allDatas.Add( config.id, config)
;
    config = new ConfigCooperation();
    config.id = 103;
    config.name = "神";
    config.counts = new int[]{2,4};
    config.attributes = new Dictionary<int,int[]>{{4,new int[]{15,25}}};
    config.range = new int[]{2,2};
    config.type = 1;
    allDatas.Add( config.id, config)
;
    config = new ConfigCooperation();
    config.id = 104;
    config.name = "魔";
    config.counts = new int[]{2,3};
    config.attributes = new Dictionary<int,int[]>{{0,new int[]{25,50}}};
    config.range = new int[]{1,1};
    config.type = 2;
    allDatas.Add( config.id, config)
;
    config = new ConfigCooperation();
    config.id = 105;
    config.name = "仙";
    config.counts = new int[]{2,4};
    config.attributes = new Dictionary<int,int[]>{{2,new int[]{25,40}}};
    config.range = new int[]{1,2};
    config.type = 1;
    allDatas.Add( config.id, config)
;
    config = new ConfigCooperation();
    config.id = 106;
    config.name = "兽";
    config.counts = new int[]{2,4,6};
    config.attributes = new Dictionary<int,int[]>{{5,new int[]{200,400,800}}};
    config.range = new int[]{1,1,1};
    config.type = 1;
    allDatas.Add( config.id, config)
;
    base.Init();
    }
}
}