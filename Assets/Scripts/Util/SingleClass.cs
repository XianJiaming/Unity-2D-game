using System;
using System.Collections.Generic;
using UnityEngine;

public  class  SingleClass<T> where T:new()
{
    private static T instance = new T();
    public static T Instance()
    {
        return instance;
    }

    protected SingleClass()
    {

        Init();
    }
    protected virtual void Init()
    {

    }
}

