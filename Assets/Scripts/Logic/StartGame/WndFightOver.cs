using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

//战斗结束的时候， 根据玩家胜利和失败，做不同现实
class WndFightOver : ViewBase
{

    Text descText;
    PlayerModel playerModel;

    GameObject equipListGo;
    GameObject equipItemGo;

    List<GameObject> equipItems = new List<GameObject>();

    public override void Init()
    {
        base.Init();

        playerModel = ModelManager.Get<PlayerModel>("PlayerModel");

        EventManager.RegistEvent(EventType.GameFial, ShowGameOverView);
        EventManager.RegistEvent(EventType.FightFail, ShowFightFailView);
        EventManager.RegistEvent(EventType.FightVectory, ShowFightVectoryView);
        EventManager.RegistEvent(EventType.GameVectory, ShowGameVectoryView);

        layer = GameLayer.Tips;
    }

    public override void OnFirstOpen()
    {
        base.OnFirstOpen();

        descText = GetText("desc");

        equipListGo = GetGameObject("equipList");
        equipListGo.SetActive(false);
        equipItemGo = GetGameObject("equipList/equipItem");

        for (int i = 0; i < PlayerModel.MaxEquipReward; i++)
        {
            
            equipItems.Add(GameObject.Instantiate(equipItemGo, equipListGo.transform));
        }

        equipItemGo.SetActive(false);
        AddUIButton("sureBtn", OnClickSureBtn);
    }

    public override void OnShow()
    {
        base.OnShow();
        AudioManager.instance.PlayBGM(ConstConfig.WndFightOverBg);
    }

    public override void OnHide()
    {
        base.OnHide();

        equipListGo.SetActive(false);
    }

    public override void OnDispose()
    {
        base.OnDispose();
    }

    public override void SetVisible(bool value)
    {
        base.SetVisible(value);
        if(value)
        {
            go.transform.SetAsFirstSibling();
        }
    }

    void OnGameOver(object ob =null )
    {
        SetVisible(true);
    }
    
    void OnClickSureBtn(UnityEngine.GameObject ob)
    {
        SetVisible(false);

        if(Main.isGame)
        {
            ViewManager.Get("WndPrepare").SetVisible(true);
            ViewManager.Get("WndCooper").SetVisible(true);
        }

        

    }

    void ShowFightVectoryView(object ob = null )
    {
        SetVisible(true);
        descText.text = ShowSettlement()+"\n战斗胜利！";
    }
    void ShowFightFailView(dynamic d)
    {
        SetVisible(true);
        descText.text = ShowSettlement() + $"本局战斗失败损失血量 ：{reduceHpValue.ToString()}\n\n战斗失败！";
    }
    void ShowGameVectoryView(object ob = null)
    {
        SetVisible(true);
        descText.text = ShowSettlement() + "\n恭喜你打通了所有关卡！";
    }
    void ShowGameOverView(object ob = null )
    {
        descText.text += "\n生命值归0，游戏结束！";
    }
    //展示伤害统计
    string ShowSettlement()
    {
        int curLevel = ModelManager.Get<GameLevelModel>("GameLevelModel").GetLevel();
        var sb = new System.Text.StringBuilder($"当前关卡-----{curLevel}\n");
        var roles = playerModel.fightRoles;

        foreach(var role in roles)
        {
            if(role!= null)
            {
                sb.Append($"{role.name}:{role.WholeHurtValue.ToString()}\n");
            }
        }

        //金币

        return sb.ToString();
    }

    int reduceHpValue = 0;

    public void SetReduceHp(int value)
    {
        reduceHpValue = value;
        
    }


    //显示装备奖励信息
    public void ShowEquipReward(List<Equip> equips)
    {
        equipListGo.SetActive(true);
        //添加点击事件
        for(int i=0;i< equipItems.Count;i++)
        {
            //点击后显示装备icon 再点击显示装备信息
            var uiL = UIUtil.SetUIOnClick(equipItems[i],  ShowEquip );
            equipItems[i].GetComponent<Image>().color = Color.white;
            equipItems[i].transform.GetChild(0).gameObject.SetActive(false);
            uiL.param = equips[i];
        }

    }
    //被点击后调用
    void ShowEquip(GameObject go)
    {
        //image透明度变化

        
        var uiL = UIListener.GetUIListener(go);
        Equip e = uiL.param as Equip;
        //显示装备图片
        //image显示
        var image = go.GetComponent<Image>();
        image.DOColor(Color.clear, 1f).OnComplete(
            () =>
            {
                var equipImage = go.transform.GetChild(0).GetComponent<Image>();
                equipImage.gameObject.SetActive(true);
                equipImage.sprite = ResourceManager.LoadAsset<Sprite>(ResourceManager.EquipPath + e.id.ToString(), e.id.ToString());
            }
            );
        //添加点击显示信息
        UIUtil.SetUIOnClick(go, (g) => { ViewManager.Get<WndTips>("WndTips").ShowInfo(e.desc); });
    }
}
