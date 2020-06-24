using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Object = UnityEngine.Object;

public class ResourceManager
{



    const string rolePath = "Prefab/RoleModel/";

    public const string effectPath = "Prefab/Effects/";

    public const string EquipPath = "Equip/";

    public static ResourceManager instance = new ResourceManager();
    private ResourceManager()
    {
        Init();
    } 
    //加载过的资源都存在这个字典里
    public static Dictionary<string,Object> assetMap = new Dictionary<string, UnityEngine.Object>();

    //path是完整路径
    public static T LoadAsset<T>(string path, string name) where T: UnityEngine.Object
    {
        T asset = null;
        if (!assetMap.ContainsKey(name))
        {
            asset = Resources.Load<T>(path);
            assetMap.Add(name, asset);
        }
        else
        {
            asset = assetMap[name] as T;
        }

            
        if (asset == null) Debug.LogError($"加载的资源为空——{path}");
      
        return asset;
    }


    void Init()
    {

        
    }

}
