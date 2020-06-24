
using System.Collections;
using System.Collections.Generic;

public class AIModel:ModelBase
{
   
    public RoleBase[] fightRoles;

    

    protected override void  Init()
    {
        //EventManager.RegistEvent(EventType.NewGameStart, OnGameStar);
        fightRoles = new RoleBase[9];
    }

    void OnGameStar(object ob = null)
    {
        
    }

    public void SetAIRole(int index, int id, int level, int wuqiId = 0, int fangjuId = 0)
    {
        RoleBase role = new RoleBase();
        role.SetId(id, level);
        if(wuqiId!=0)
        role.AddEquipMust(new Equip(wuqiId));
        if(fangjuId!=0)
        role.AddEquipMust(new Equip(fangjuId));
        fightRoles[index] = role;
    }

    public void ClearRole()
    {
        for(int i =0;i<fightRoles.Length;i++)
        {
            fightRoles[i] = null;
        }
    }
    //ai角色增加羁绊效果,每回合战斗前触发
    
    Dictionary<int, Entanglement> etls = new Dictionary<int, Entanglement>();

    public void UpdateAiCooperData()
    {
        
        for (int i = 0; i < fightRoles.Length; i++)
        {
            RoleBase role = fightRoles[i];
            if (role != null)
            {
                int cooperId = role.GetCooperId();
                int proId = role.GetProId();

                if(!etls.ContainsKey(cooperId))
                etls.Add(cooperId,new Cooperation( cooperId));

                if(!etls.ContainsKey(proId))
                etls.Add( proId,Profession.CreatePro( proId));
            }

        }
        foreach (var v in etls)
        {
            v.Value.Update(fightRoles);
        }

        
    }
}


