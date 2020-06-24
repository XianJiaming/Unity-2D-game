using System;
using System.Collections.Generic;

//辅助角色完成状态
public class RoleHelper
{
    //如果目标角色
    //眩晕
    public static void SetXuanYun(RoleBase role, float time)
    {
        

        role.SetStop(true);
        ViewManager.Get<WndTips>("WndTips").ShowMsg("眩晕", role.fightTipPosition, UnityEngine.Color.gray ,time+0.5f, 70, 40);
        TimeManager.RegistOneTime((id) =>
        {
            role.SetStop(false);
        }, time, true);
    }
    //无敌
    public static void SetWuDi(RoleBase role, float time)
    {
       

        role.SetWd(true);
        ViewManager.Get<WndTips>("WndTips").ShowMsg("不灭", role.fightTipPosition, UnityEngine.Color.yellow, time + 0.5f, 70, 40);
        TimeManager.RegistOneTime((id) =>
        {
            role.SetWd(false);
        }, time, true);
    }
}

