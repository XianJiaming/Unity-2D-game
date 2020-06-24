using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

class WndTips:ViewBase
{
    RectTransform selfRect;

    GameObject fightTipsGo;

    GameObject detailInfoGo;
    Image detailBg;

    RectTransform detailInfoRect;
    Text detailInfoText;

    List<RectTransform> tipRectList;
    List<RectTransform> curTipRectList = new List<RectTransform>();

    public override void Init() {
        base.Init();
        layer = GameLayer.Tips;
        SetVisible(true);

        EventManager.RegistEvent(EventType.FightEnd, AfterFight);
        EventManager.RegistEvent(EventType.RoleMiss, OnRoleMiss);
    }

    public override void OnFirstOpen()
    {
        tipsGo = GetGameObject("tips");
        tipsGo.SetActive(false);

        orTipsVector = tipsGo.transform.position;

        tipRectList = new List<RectTransform>();
        fightTipsGo = GetGameObject("fightTips");
        fightTipsGo.SetActive(false);

        detailInfoGo = GetGameObject("info");
        detailBg = GetImage("info/bg");
        detailInfoGo.SetActive(false);
        detailInfoText = GetText("info/text");
        detailInfoRect = GetRectTransform("info");

        selfRect = go.GetComponent<RectTransform>();
    }

    public override void OnShow() { }

    public override void OnHide() { }

    public override void OnDispose() { }


    void OnRoleMiss(dynamic dy)
    {
        RoleBase role = dy as RoleBase;

        ShowMsg("Miss", role.fightTipPosition, Color.yellow,1f);
    }
    //战斗中会使用这个方法
    public void ShowMsg( string content , Vector3 position, Color color,float time = 2f, int size = 35, int offY = 80)
    {
        var tipsP =  RectTransformUtility.WorldToScreenPoint(Camera.main, position);

        RectTransform tip;
        if (tipRectList.Count == 0)
        {
            tip = GameObject.Instantiate(fightTipsGo, go.transform).GetComponent<RectTransform>();
            
        }
        else
        {
            tip = tipRectList[tipRectList.Count - 1];
            
            tipRectList.Remove(tip);
        }
        
        tip.gameObject.SetActive(true);
        curTipRectList.Add(tip);

        Text t = tip.GetComponent<Text>();
        Outline outline = tip.GetComponent<Outline>();
        outline.effectColor = color;
        t.text = content;
        t.fontSize = size;
        t.color = Color.white;
        //time秒后颜色消失
        
        t.DOColor(Color.clear, time).OnComplete(
            () =>
            {
                tip.gameObject.SetActive(false);
                tipRectList.Add(tip);
                curTipRectList.Remove(tip);
            });
        

        tip.position = tipsP;

        t.transform.DOLocalMoveY(t.transform.localPosition.y + offY, time);
    }

    public void ShowEtlInfo(Entanglement etl)
    {       
        ShowInfo($"<size=30>{etl.GetDesc()}</size>");
    }

    public enum RoleInfoType
    {
        Pre,
        Fight
    }

    public void ShowRoleInfo(RoleBase role, RoleInfoType type = RoleInfoType.Pre)
    {
        if(role == null)
        {
            return;
        }

        int level = role.GetLevel();
        var attributeData = DataClass.ConfigAttributeManager.Instance().allDatas;
        var roleAttributeData = DataClass.ConfigRoleManager.Instance().allDatas[role.GetRoleId()].attributes[ level - 1];
        var attributeSb = new System.Text.StringBuilder();

        if( type == RoleInfoType.Pre)
        for (int i = 0; i < 7; i++)
        {
            float nowValue = role.attributes[i]; //当前属性
            int orValue = roleAttributeData[i]; //原属性

            if ( nowValue > orValue)
            {
                attributeSb.Append($"{attributeData[i].name}:<color=yellow>{nowValue.ToString()}</color>  ");
            }
            else
            {
                attributeSb.Append($"{attributeData[i].name}:{nowValue.ToString()}  ");
            }
            
        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                float nowValue = role.attributes[i]; //当前属性
                int orValue = roleAttributeData[i]; //原属性

                if (nowValue > orValue)
                {
                    attributeSb.Append($"{attributeData[i].name}:<color=yellow>{nowValue.ToString()}</color>  ");
                }
                else
                {
                    attributeSb.Append($"{attributeData[i].name}:{nowValue.ToString()}  ");
                }
            }

            attributeSb.Append($"{attributeData[5].name}:{role.attributes[7].ToString()}/{role.attributes[5].ToString()} {attributeData[6].name}:{role.attributes[8].ToString()}/{role.attributes[6].ToString()}" );

        }


        string cooperName = DataClass.ConfigCooperationManager.Instance().allDatas[role.GetCooperId()].name;
        string proName = DataClass.ConfigProfessionManager.Instance().allDatas[role.GetProId()].name;
        string equipContent = $"{role.GetEquip(0)?.desc}\n{role.GetEquip(1)?.desc}";

        string content = $"<size=50><color={ConstConfig.levelColor[role.cost]}>{role.name}</color></size>\n<color={ConstConfig.typeColor}>等级:{level.ToString()}【{cooperName}】 【{proName}】</color>\n\n{attributeSb.ToString()} {role.ShowMissCirtInfo()}\n<color={ConstConfig.skillColor}>{role.skill?.GetDesc()}</color>\n{equipContent}";

        ShowInfo(content);
    }

    void CanCloseInfo(object ob=null )
    {
        if( !RectTransformUtility.RectangleContainsScreenPoint(detailInfoText.rectTransform, Input.mousePosition))
        {
            detailInfoGo.SetActive(false);
            EventManager.UnRegistEvent(EventType.InputClick, CanCloseInfo);
        }
    }
    //显示一些详尽信息 通过点击消失
    public void ShowInfo( string content)
    {
        detailInfoGo.SetActive(true);
        //text 宽度不变 高度由组件控制
        detailInfoText.text = content;

        //bg高度
        var v = Vector2.zero;
        v.x = detailBg.rectTransform.rect.width;
        v.y = detailInfoText.preferredHeight + 60;
        
        
        detailBg.rectTransform.sizeDelta = v ;
        EventManager.RegistEvent(EventType.InputClick, CanCloseInfo);
    }
    static Vector3 orTipsVector;
    static GameObject tipsGo;
    static List<GameObject> tipsGos = new List<GameObject>();

    //显示提示信息，会自动消失
    public static void ShowTips(string content )
    {
        AudioManager.instance.PlayEffect(ConstConfig.Tips);

        GameObject tempTipsGo = null;
        if(tipsGos.Count>0)
        {
            tempTipsGo = tipsGos[0];
            
            tipsGos.Remove(tempTipsGo);
        }
        else
        {
            var wndtips = ViewManager.Get<WndTips>("WndTips");
            tempTipsGo = GameObject.Instantiate(tipsGo, wndtips.rect);            
        }
        tempTipsGo.SetActive(true);
        tempTipsGo.transform.SetAsLastSibling();
        tempTipsGo.transform.position = orTipsVector;
        tempTipsGo.transform.Find("text").GetComponent<Text>().text = content;

        tempTipsGo.transform.DOLocalMoveY(tempTipsGo.transform.localPosition.y + 120, 2f).OnComplete(
            () => {
                tempTipsGo.SetActive(false);
                tipsGos.Add(tempTipsGo);
            }
            );
        
    }

    void AfterFight(dynamic dy)
    {
        foreach(var v in curTipRectList)
        {
            v.gameObject.SetActive(false);
        }
        
    }
}
