using System;
using System.Collections.Generic;

//济世 ,每3秒加点血
public class Profession201 : Profession
{
    int[] valueParam = new int[] { 50, 100, 200 };
    float time = 2f;
    FightManager fightManager;

    public Profession201(int id):base(id)
    {

        fightManager = FightManager.instance;
        EventManager.RegistEvent(EventType.FightStart, BeforeFight);
    }

    List<RoleBase> tempRoles = new List<RoleBase>();

    //战斗前调用
    void BeforeFight(dynamic dy = null)
    {
        tempRoles.Clear();

        if (effectIndex != -1)
        {           
            foreach (RoleBase role in roles)
            {
                role.afterDie += RemoveTempRoles;

                tempRoles.Add(role);
            }

            timerId = TimeManager.Regist(AddHp, 0, time, 50, true);

            EventManager.RegistEvent(EventType.FightEnd, RemoveTimer);
        }
        

    }
    void RemoveTempRoles(RoleBase role)
    {
        tempRoles.Remove(role);
        role.afterDie -= RemoveTempRoles;
    }

    void AddHp(int id)
    {       
        foreach (RoleBase role in tempRoles)
        {          
            if (role.isFight)
            {
                role.GetTreat(valueParam[effectIndex]);
            }      
        }
    }
    //战斗结束了，就要移出 计时器
    int timerId;

    void RemoveTimer(dynamic dy = null)
    {

        TimeManager.Remove(timerId);
        EventManager.UnRegistEvent(EventType.FightEnd, RemoveTimer);
    }


    public override void GetEffect(RoleBase role)
    {
        
    }

    public override void RemoveEffect(RoleBase role, int effectIndex)
    {
        
    }


}
//斗战
public class Profession202 : Profession
{
    int[] valueParam = new int[] { 10, 20,30 };
    float[] critParam = new float[] {0.1f,0.2f, 0.3f}; //暴击率

    public Profession202(int id) : base(id)
    {

    }

    public override void GetEffect(RoleBase role)
    {
        if (role == null | effectIndex ==-1) return;
        role.attackValueParam += valueParam[effectIndex] * 1.0f / 100;
        role.critRate += critParam[effectIndex];
    }

    public override void RemoveEffect(RoleBase role, int effectIndex)
    {
        if (role == null | effectIndex == -1) return;
        role.attackValueParam -= valueParam[effectIndex] * 1.0f / 100;
        role.critRate -= critParam[effectIndex];
    }
}

//奇术
public class Profession203 : Profession
{
    float[] valueParam = new float[] { 0.15f, 0.30f, 0.45f };

    public Profession203(int id) : base(id)
    {

    }

    public override void GetEffect(RoleBase role)
    {
        if (role == null | effectIndex == -1) return;
        role.skillParam += valueParam[effectIndex];
    }

    public override void RemoveEffect(RoleBase role, int effectIndex)
    {
        if (role == null | effectIndex == -1) return;
        role.skillParam -= valueParam[effectIndex];
    }


}

//大智
public class Profession204 : Profession
{
    RoleBase role;
    int[] valueParam = new int[] { 3, 5 };

    List<RoleBase> tempRoles = new List<RoleBase>();

    public Profession204(int id) : base(id)
    {
        EventManager.RegistEvent(EventType.FightStart, BeforeFight);
    }

    //战斗前调用
    void BeforeFight(dynamic dy =null )
    {
        tempRoles.Clear();
        if (effectIndex!= -1)
        {
            foreach (RoleBase role in roles)
            {
                role.afterDie += RemoveTempRoles;
                tempRoles.Add(role);
            }

            timerId = TimeManager.Regist(AddMp, 0, 1, 300);
            EventManager.RegistEvent(EventType.IsAnyOneActUp, StopTimer);
            EventManager.RegistEvent(EventType.FightEnd, RemoveTimer);
        }
       
    }

    void RemoveTempRoles(RoleBase role)
    {
        tempRoles.Remove(role);
        role.afterDie -= RemoveTempRoles;
    }

    void AddMp(int id)
    {
        foreach (RoleBase role in tempRoles)
        {
            if(role.isFight)
            {

                role.AddMp(valueParam[effectIndex]);
            }
            
        }
    }

    void StopTimer(dynamic dy = null)
    {
        //有人在行动
        if (FightManager.instance.isAnyoneAct)
        {
            TimeManager.GetTimer(timerId).isStop = true;
        }
        else
        {
            TimeManager.GetTimer(timerId).isStop = false;
        }
    }

    //战斗结束了，就要移出 计时器
    int timerId;
    
    void RemoveTimer(dynamic dy = null)
    {

        TimeManager.Remove(timerId);
        EventManager.UnRegistEvent(EventType.IsAnyOneActUp, StopTimer);
        EventManager.UnRegistEvent(EventType.FightEnd, RemoveTimer);
    }
 
    public override void GetEffect(RoleBase role)
    {
       
    }

    public override void RemoveEffect(RoleBase role, int effectIndex)
    {
     
    }
}


//坚御
public class Profession205 : Profession
{
    //减少受到的伤害
    float[] valueParam = new float[] { 0.1f, 0.2f, 0.3f};

    public Profession205(int id) : base(id)
    {

    }

    public override void GetEffect(RoleBase role)
    {
        if (role == null | effectIndex == -1) return;
        role.getHurtParam -= valueParam[effectIndex];
    }

    public override void RemoveEffect(RoleBase role, int effectIndex)
    {
        if (role == null | effectIndex == -1) return;
        role.getHurtParam += valueParam[effectIndex];
    }
}


//灵动
public class Profession206 : Profession
{
    //提升的速度
    int[] valueParam = new int[] { 10, 20, 30 };
    //提升的闪避
    float[] missParam = new float[] { 0.05f, 0.10f, 0.15f };

    public Profession206(int id) : base(id)
    {

    }

    public override void GetEffect(RoleBase role)
    {
        if (role == null | effectIndex == -1) return;
        role.attributes[2] += valueParam[effectIndex];
        role.missValue += missParam[effectIndex];
    }

    public override void RemoveEffect(RoleBase role, int effectIndex)
    {
        if (role == null | effectIndex == -1) return;
        role.attributes[2] -= valueParam[effectIndex];
        role.missValue -= missParam[effectIndex];
    }
}
