using System.Collections.Generic;
using System;
namespace DataClass
{

    public class ConfigRoleShopManager : ConfigDataManager<ConfigRoleShopManager,ConfigRoleShop>

    {
    
    public override void Init( )
    {
    name = "ConfigRoleShop";
    ConfigRoleShop config = null;
    
    config = new ConfigRoleShop();
    config.id = 1;
    config.level = 1;
    config.rate = new int[]{70,30,0,0,0};
    allDatas.Add( config.id, config)
;
    config = new ConfigRoleShop();
    config.id = 2;
    config.level = 2;
    config.rate = new int[]{70,20,10,0,0};
    allDatas.Add( config.id, config)
;
    config = new ConfigRoleShop();
    config.id = 3;
    config.level = 3;
    config.rate = new int[]{60,25,15,0,0};
    allDatas.Add( config.id, config)
;
    config = new ConfigRoleShop();
    config.id = 4;
    config.level = 4;
    config.rate = new int[]{50,30,20,0,0};
    allDatas.Add( config.id, config)
;
    config = new ConfigRoleShop();
    config.id = 5;
    config.level = 5;
    config.rate = new int[]{40,30,20,10,0};
    allDatas.Add( config.id, config)
;
    config = new ConfigRoleShop();
    config.id = 6;
    config.level = 6;
    config.rate = new int[]{30,25,30,10,5};
    allDatas.Add( config.id, config)
;
    config = new ConfigRoleShop();
    config.id = 7;
    config.level = 7;
    config.rate = new int[]{25,25,25,15,10};
    allDatas.Add( config.id, config)
;
    config = new ConfigRoleShop();
    config.id = 8;
    config.level = 8;
    config.rate = new int[]{15,20,25,25,15};
    allDatas.Add( config.id, config)
;
    config = new ConfigRoleShop();
    config.id = 9;
    config.level = 9;
    config.rate = new int[]{10,15,20,30,25};
    allDatas.Add( config.id, config)
;
    base.Init();
    }
}
}