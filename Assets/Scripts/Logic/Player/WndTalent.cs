using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

//给玩家展示可选天赋
class WndTalent : ViewBase
{
    RectTransform listTrasf;

    GameObject talentItemGo;

    TalentModel talentModel;

    //Dictionary<GameObject, MyTalent.Talent> goTalents = new Dictionary<GameObject, MyTalent.Talent>();

    GameObject[] talentGos = new GameObject[3];

    PlayerModel playerModel;

    public override void Init()
    {
        base.Init();
        falseType = FalseType.hide;
        layer = GameLayer.UI;
        
    }

    public override void OnFirstOpen() {
        base.OnFirstOpen();

        playerModel = ModelManager.Get<PlayerModel>("PlayerModel");

        talentItemGo = GetGameObject("container/list/talentItem");
        listTrasf = GetRectTransform("container/list");

        talentModel = ModelManager.Get<TalentModel>("TalentModel");
        //int count = talentModel.talentIds.Count;

        //生成go，并且让go具备点击效果
        //随机让go绑定3个天赋

        //var talants = DataHelp.GetRandom<int>(talentModel.canUseTalent, 3);

        for (int i=0;i<3;i++)
        {
            var itemGo = GameObject.Instantiate(talentItemGo, listTrasf);
            talentGos[i] = itemGo;
        }

        talentItemGo.SetActive(false);

        
    }

    void UpdateTalent()
    {
        var talants = DataHelp.GetRandom(talentModel.canUseTalent, 3);
        for (int i = 0; i < talants.Length; i++)
        {
            var itemGo = talentGos[i];
            itemGo.SetActive(true);
            int id = talants[i];
            MyTalent.Talent talent = MyTalent.Talent.CreatTalent(id);

            UIUtil.GetText(itemGo, "Text").text = talent.desc;
            UIUtil.GetText(itemGo, "nameText").text = talent.name;
            // goTalents.Add(itemGo, talent);

            //选择之后
            UIUtil.SetUIOnClick(itemGo, (g) =>
            {
                playerModel.AddTalent(talent);
                
                SetVisible(false);

            });
        }
        for(int i = talants.Length; i<3;i++)
        {
            talentGos[i].SetActive(false);
        }
    }

    public override void OnShow() {
        base.OnShow();

        UpdateTalent();
    }

    public override void OnHide() {
        base.OnHide();
    }

    public override void OnDispose() {
        base.OnDispose();
    }

    void UpdateView( )
    {
        
    }

    void OnClickTalent( )
    {

    }
}

