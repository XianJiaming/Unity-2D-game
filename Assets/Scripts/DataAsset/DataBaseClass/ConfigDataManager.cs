//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataClass
{
    using System;
    using System.Collections.Generic;
         
    public abstract class ConfigDataManager<T,T2> where T:new()
    {
        protected static T instance = new T();
        protected string name;
        public Dictionary<int, T2> allDatas = new Dictionary<int, T2>();
        public static T Instance()
        {
            return instance;
        }
        public ConfigDataManager()
        {
            Init();
            
        }
        public virtual void Init()
        {
           
        }
    }

}
