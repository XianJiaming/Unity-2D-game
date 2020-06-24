using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class WndCooper:ViewBase
{
    //------------------------------------------
    class EtlItem
    {
        static Dictionary<int, string> effectColor = new Dictionary<int, string>()
        {
            {-1,"white"},{0,"green"},{1,"red"},{2,"yellow"},{3,"yellow"}
        };


        
        Entanglement info;
        GameObject go;
        Image iconImage;
        Text nameText;
        Text numberText;
        public EtlItem(GameObject go)
        {
            this.go = go;
            iconImage = UIUtil.GetImage(go, "icon");
            nameText = UIUtil.GetText(go, "name");
            numberText = UIUtil.GetText(go, "number");
            UIUtil.SetUIOnClick(go, OnClick);
            
        }

        void OnClick(GameObject go)
        {
            ViewManager.Get<WndTips>("WndTips").ShowEtlInfo(info);
        }

        public void SetInfo(Entanglement info)
        {
            this.info = info;
            UpdateView();
        }
        public void UpdateView()
        {

            if( info.etlCount == 0)
            {
                SetVisible(false);
                return;
            }

            SetVisible(true);
            nameText.text = info.name;
            numberText.text = $"<color={effectColor[info.effectIndex]}>{info.etlCount}</color>/{info.maxCount}";
            UpdateImage();
        }
        public void SetVisible(bool value)
        {           
            go.SetActive(value);
            
        }

        void UpdateImage( )
        {
            iconImage.sprite = AssetLoader.instance.GetSprite(info.GetId().ToString());
        }
    }
    //------------------------------------------


    Transform cooperListTrs;
    GameObject cooperViewItem;
    List<EtlItem> etlItems = new List<EtlItem>();
    PlayerModel playerModel;

    public override void Init()
    {
        base.Init();
        playerModel = ModelManager.Get("PlayerModel") as PlayerModel;
        layer = GameLayer.UI;
        EventManager.RegistEvent(EventType.GameFial, SetFalse);
        EventManager.RegistEvent(EventType.FightStart, SetFalse);
        
        EventManager.RegistEvent(EventType.GameOver, SetFalse);
    }


    public override void OnFirstOpen()
    {
        base.OnFirstOpen();
        cooperViewItem = GetGameObject("cooperView");
        cooperListTrs = GetTransform("cooperList");
        cooperViewItem.SetActive(false);
    }

    public override void OnShow()
    {
        base.OnShow();
        UpdateView();
        AddEvent();
    }
    void AddEvent()
    {
        
        EventManager.RegistEvent(EventType.EtlDataUpdate, UpdateView);
    }
    void RemoveEvent()
    {
        EventManager.UnRegistEvent(EventType.EtlDataUpdate, UpdateView);
    }
     
    
    void SetTrue(dynamic ob = null)
    {
        SetVisible(true);
    }
    void SetFalse(dynamic ob = null)
    {
        SetVisible(false);
    }

    int ComparisonEntanglement(Entanglement a, Entanglement b)
    {
        //b生效高些
        if( a.effectIndex < b.effectIndex)
        {
            return 1;
        }
        else if(a.effectIndex > b.effectIndex)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }

    void UpdateView(object ob = null )
    {
        CooperationModel cooperModel = ModelManager.Get("CooperationModel") as CooperationModel;

       

        List<Entanglement> etls = new List<Entanglement>();
        foreach(var v in cooperModel.playerEtgs.Values)
        {
            etls.Add(v);
        }

        etls.Sort(ComparisonEntanglement);


        int etgCounts = etls.Count;

        while (etgCounts> etlItems.Count)
        {
            etlItems.Add(new EtlItem(GameObject.Instantiate(cooperViewItem, cooperListTrs)));

        }
        
        int index = 0;
        foreach(var cooper in etls)
        {
            
            etlItems[index].SetInfo(cooper);
            
            index++;
        }
        for(int i=index;i<etlItems.Count;i++)
        {
            etlItems[i].SetVisible(false);
        }
    }

    public override void OnHide()
    {
        base.OnHide();
        RemoveEvent();
    }

    public override void OnDispose()
    {
        base.OnDispose();
    }
}

