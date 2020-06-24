using System;
using System.Collections.Generic;

public class DataHelp
{

    //从list中返回一个随机 元素
    public static T GetRandom<T>(List<T> list)
    {
        int index = UnityEngine.Random.Range(0, list.Count);

        return list[index];

    }

    //从list里返回一个N大小的随机数组(并且不重复) 
    public static T[] GetRandom<T>(List<T> list, int count)
    {
        T[] array;
        if (count<list.Count)
        {
            array = new T[count];
            List<T> tempList = new List<T>();
            foreach (var v in list)
            {
                tempList.Add(v);
            }                   
            //执行count次
            for (int i = 0; i < count; i++)
            {
                int randomIndex = UnityEngine.Random.Range(0, tempList.Count);
                array[i] = tempList[randomIndex];
                tempList.RemoveAt(randomIndex);
            }
        }
        else
        {
            array = list.ToArray();
        }

        return array;
    }
   

    //从一个字典里取随机元素
    public static T[] GetRandomValueInDic<Key,T>(Dictionary<Key, T> dic, int count )
    {
        T[] array = null;

        List<T> tempList = new List<T>(count);

        foreach(var v in dic)
        {
            tempList.Add(v.Value);
        }

        return array = GetRandom<T>(tempList, count);

    }

    public static Key[] GetRandomKeyInDic<Key, T>(Dictionary<Key, T> dic, int count)
    {
        Key[] array = null;

        List<Key> tempList = new List<Key>(count);

        foreach (var v in dic)
        {
            tempList.Add(v.Key);
        }

        return array = GetRandom<Key>(tempList, count);

    }

//     public static void DeBugLog<T>(IEnumerable<T> ts)
//     {
//         
//         foreach(var v in ts)
//         {
//             UnityEngine.Debug.Log(v);
//         }
//         UnityEngine.Debug.Log("输出完毕");
//     }
}
