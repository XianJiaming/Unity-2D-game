using System;
using System.Collections.Generic;

//继承该类边可显示某数据
public abstract class ItemView<T>
{
    protected T info;
    public int index;
    public ItemView( )
    {
        
    }

    //init里获得 view的各个组件
    public abstract void Init(UnityEngine.GameObject go, int index);


    public virtual void SetInfo(T info)
    {
        this.info = info;
        UpdateView();
    }

    public abstract void UpdateView();
    

    
}
    


