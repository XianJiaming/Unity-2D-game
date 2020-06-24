using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ObjectPool
{
    
    //go
    public static Dictionary<string, List<GameObject>> goMap = new Dictionary<string, List<GameObject>>();

    //暂时只用于特效
    public static GameObject GetGo( string name)
    {
        GameObject go = null;
        if(!goMap.ContainsKey(name))
        {
            goMap.Add(name, new List<GameObject>());
        }
        

        if(goMap[name].Count == 0)
        {
            //此处暂时为特效路径
            go = GameObject.Instantiate(ResourceManager.LoadAsset<GameObject>(ResourceManager.effectPath + name, name));
            var v = go.AddComponent<OnEffectStop>();
            v.SetName(name);
            
        }
        else
        {
            go = goMap[name][0];
            goMap[name].Remove(go);
        }
        go.SetActive(true);
        
       

        return go;
        
    }

    public static void SaveGo(string name, GameObject go )
    {
        go.SetActive(false);
        goMap[name].Add(go);
    }

    public static void Clear()
    {
        foreach(var list in goMap.Values)
        {
            foreach(var go in list)
            {
                GameObject.Destroy(go);
            }
            list.Clear();
        }

        
    }
}
