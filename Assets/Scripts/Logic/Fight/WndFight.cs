using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WndFight:ViewBase
{
    Text speedText;

    Vector3[] aiPos;
    Vector3[] playerPos;

    RectTransform fightPosition;
    RectTransform[] playerPositions;
    RectTransform[] aiPositions;

    GameObject[] playerRoleGos;
    GameObject[] aiRoleGos;

    PlayerModel playerModel;
    AIModel aiModel;

    WndTips wndTips;

    FightManager fightManager;
    public override void Init()
    {
        base.Init();
        layer = GameLayer.Fight;

        playerModel =  ModelManager.Get("PlayerModel") as PlayerModel;
        aiModel = ModelManager.Get("AIModel") as AIModel;
    }
    
    public override void OnFirstOpen()
    {
        wndTips = ViewManager.Get<WndTips>("WndTips");

        aiPos = new Vector3[3];
        playerPos = new Vector3[3];

        Transform aiPs = GetTransform("aiPs");
        Transform playerPs = GetTransform("playerPs");

        for(int i=0;i<3;i++)
        {
            aiPos[i] = aiPs.GetChild(i).position;
            playerPos[i] = playerPs.GetChild(i).position;
        }

        base.OnFirstOpen();

        GameObject fightRoleGo = GetGameObject("fightRoleGo");
        

        fightManager = FightManager.instance;
        
        int count = FightManager.Max_Fight_List_Number;


        fightPosition = GetRectTransform("fightPosition");
        
        playerPositions = new RectTransform[count];
        for (int i = 0; i < count; i++)
        {
            playerPositions[i] = fightPosition.Find($"player{i.ToString()}").GetComponent<RectTransform>();
        }
        playerRoleGos = new GameObject[count];
        for (int i = 0; i < count; i++)
        {
            var go = GameObject.Instantiate(fightRoleGo, playerPositions[i]);
            go.transform.localPosition = Vector2.zero;
            playerRoleGos[i] = go;
        }

        aiPositions = new RectTransform[count];
        for (int i = 0; i < count; i++)
        {
            aiPositions[i] = fightPosition.Find($"ai{i.ToString()}").GetComponent<RectTransform>();
        }

        aiRoleGos = new GameObject[count];
        for (int i = 0; i < count; i++)
        {
            var go = GameObject.Instantiate(fightRoleGo, aiPositions[i]);
            go.transform.localPosition = Vector2.zero;
            aiRoleGos[i] = go;
        }

        foreach(var go in playerRoleGos)
        {
            UIUtil.SetUIOnClick(go, OnClickRole);
        }
        foreach (var go in aiRoleGos)
        {
            UIUtil.SetUIOnClick(go, OnClickRole);
        }
        speedText = GetText("speed/speed");
        AddUIButton("speed", OnClickSpeed);


        GameObject.Destroy(fightRoleGo);
    }

    void OnClickRole(GameObject go)
    {
        RoleBase role = UIListener.GetUIListener(go).param as RoleBase;
        wndTips.ShowRoleInfo(role, WndTips.RoleInfoType.Fight);


    }

    void RolesToFightPos()
    {

        foreach(RoleBase role in playerModel.fightRoles)
        {
            if(role!=null)
            {
                int index = UnityEngine.Random.Range(0, 3);
                var orPos = role.fightRole.transform.position;
                //送到初始位置
                role.fightRole.transform.position = playerPos[index];
                //然后飞过来
                role.fightRole.transform.DOMove(orPos, 0.8f);
            }
        }

        foreach (RoleBase role in aiModel.fightRoles)
        {
            if (role != null)
            {
                int index = UnityEngine.Random.Range(0, 3);
                var orPos = role.fightRole.transform.position;
                //送到初始位置
                role.fightRole.transform.position = aiPos[index];
                //然后飞过来
                role.fightRole.transform.DOMove(orPos, 0.8f);
            }
        }
    }

    public override void OnShow()
    {
        base.OnShow();

        AudioManager.instance.PlayBGM(ConstConfig.WndFightBg);
        UpdateSpeedText();

        AddEvent();

        fightManager.BeginFight(playerModel.fightRoles, aiModel.fightRoles, playerRoleGos, aiRoleGos);

        RolesToFightPos();

        TimeManager.Regist((id) =>
        {
            fightManager.StartFight();
            

        }, 1.2f, 0, 1);


        for(int i=0;i<9;i++)
        {
            RoleBase pRole = playerModel.fightRoles[i];
            RoleBase aRole = aiModel.fightRoles[i];

            UIListener.GetUIListener(playerRoleGos[i]).param = pRole;

            UIListener.GetUIListener(aiRoleGos[i]).param = aRole;
        }
    }

    void UpdateSpeedText()
    {
        if (ConstConfig.moveSpeed == 0.35f)
        {
            speedText.text = "正常速度";
        }
        else if(ConstConfig.moveSpeed == 0.20f)
        {
            speedText.text = "二倍速";
        }
        else if(ConstConfig.moveSpeed == 0.10f)
        {
            speedText.text = "三倍速";
        }
    }

    void OnClickSpeed(GameObject g)
    {
        if (ConstConfig.moveSpeed == 0.35f)
        {
            ConstConfig.moveSpeed = 0.20f;
        }
        else if (ConstConfig.moveSpeed == 0.20f)
        {
            ConstConfig.moveSpeed = 0.10f;
        }
        else if (ConstConfig.moveSpeed == 0.10f)
        {
            ConstConfig.moveSpeed = 0.35f;
        }

        UpdateSpeedText();
    }

    public override void OnHide() {
        RemoveEvent();
    }

    public override void OnDispose() { }

    void OnFightOver(object ob = null )
    {
        SetVisible(false);
    }

    void AddEvent( )
    {
        EventManager.RegistEvent(EventType.RoleAct, OnRoleAct);
        EventManager.RegistEvent(EventType.FightEnd, OnFightOver);
        EventManager.RegistEvent(EventType.RoleBeAttack, OnRoleBeAttack);
    }
    void RemoveEvent( )
    {
        EventManager.UnRegistEvent(EventType.RoleAct, OnRoleAct);
        EventManager.UnRegistEvent(EventType.FightEnd, OnFightOver);
        EventManager.UnRegistEvent(EventType.RoleBeAttack, OnRoleBeAttack);
    }

    void OnRoleAct( dynamic role)
    {
        GameObject go = role.fightRole;
        go.transform.parent.SetAsLastSibling();

    }
    //挨打时生成特效
    void OnRoleBeAttack(dynamic ob )
    {
        //播放声音
        int number = UnityEngine.Random.Range(1, 4);
        AudioManager.instance.PlayEffect($"{ ConstConfig.Attack}{number.ToString()}");

        var role = ob[0] as RoleBase;
        var attacker = ob[1] as RoleBase;

       
        if (attacker.isCrit)
        {
            var sP = RectTransformUtility.WorldToScreenPoint(Camera.main, role.fightGoPosition);

            var worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(sP.x, sP.y, 10));

            ObjectPool.GetGo("Crit").transform.position = worldPoint;
        }
        else
        {
            var effectGo = ObjectPool.GetGo(ConstConfig.AttackEffect);

            //effectGo.transform.position = fightRole.transform.position;

            //将一个UI坐标专成一个3d物体的坐标
            //后面设置为10是 因为相机的z为 -10， 要让z之和为0，具体原因未知

            //相机为 perspective Canvas render model 为 camera

            var sP = RectTransformUtility.WorldToScreenPoint(Camera.main, role.fightGoPosition);

            var worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(sP.x, sP.y, 10));

            effectGo.transform.position = worldPoint;

        }
    }

}
