using System;
using System.Collections;
using System.Collections.Generic;
using DataClass;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WndRoleShop:ViewBase
{
    //-------------------------------------------------------------------
    class ShopRoleItem
    {
        GameObject roleGo;

        //RoleBase role;
        int roleId;

        int index;
        Text nameText;
        Text costText;
        Image toneImage;
        Text cooperText;
        Image bgImage;
        Image buyIcon;

        public void Init(GameObject roleGo)
        {
            costText = UIUtil.GetText(roleGo, "price");
            nameText = UIUtil.GetText(roleGo,"Role/name");
            toneImage = UIUtil.GetImage(roleGo, "Role/tone/tone");
            cooperText = UIUtil.GetText(roleGo, "cooper");
            bgImage = UIUtil.GetImage(roleGo, "bg");
            buyIcon = UIUtil.GetImage(roleGo, "buy");
            this.roleGo = roleGo;
            //role = new RoleBase();
            //role.SetGo(UIUtil.GetGameObject(roleGo, "Role"));
            GameObject roleGoo = UIUtil.GetGameObject(roleGo, "Role");
            GameObject buyBtn = UIUtil.GetGameObject(roleGo, "buy");

            UIUtil.SetUIOnClick(buyBtn, OnClickBuy);
            UIUtil.SetUIOnClick(roleGoo, OnClickRole);
           
        }
        void ShowCanLevelView()
        {
            PlayerModel playerModel = ModelManager.Get("PlayerModel") as PlayerModel;
            bool ress = playerModel.CanRoleUpLevel(roleId, 1, 2);
            if(ress)
            {
               
                bgImage.DOColor(Color.red, 0.8f);
            }
            else
            {

                bgImage.color = Color.white;
            }

        }

        void OnClickBuy(object ob = null)
        {
            PlayerModel playerModel = ModelManager.Get("PlayerModel") as PlayerModel;
            int emptyIndex = playerModel.GetEmptyPreIndex();

            if(emptyIndex == -1 && !playerModel.CanRoleUpLevel(roleId, 1 ,2))
            {
                WndTips.ShowTips("您的备战区已满，不能再获得角色");
            }
            else
            {
                (ModelManager.Get("RoleShopModel") as RoleShopModel).OnBuyRole(index);
            }
 
        }

        void OnClickRole(GameObject go = null)
        {
            //属性数据
            var attributeData = ConfigAttributeManager.Instance().allDatas;
            //角色数据
            var roleData = ConfigRoleManager.Instance().allDatas[roleId];
            var roleAttributeData = roleData.attributes[0];

            var attributeSb = new System.Text.StringBuilder();
            for (int i = 0; i < 7; i++)
            {       
                attributeSb.Append($"{attributeData[i].name}:{roleAttributeData[i].ToString()}  ");
                
            }

            string skillDesc = null;
            if(roleData.skillId!=0)
            {
                var datas = ConfigSkillManager.Instance().allDatas[roleData.skillId];
                skillDesc = $"{datas.name}:{datas.desc}";
            }
                
            string cooperName = ConfigCooperationManager.Instance().allDatas[roleData.cooperId].name;
            string proName = ConfigProfessionManager.Instance().allDatas[roleData.proId].name;
          
            string content = $"<size=50><color={ConstConfig.levelColor[roleData.cost]}>{roleData.name}</color></size>\n<color=#8A3468>{cooperName} {proName}</color>\n\n{attributeSb.ToString()}\n\n{skillDesc}";
            ViewManager.Get<WndTips>("WndTips").ShowInfo(content);
        }


        public void SetInfo( int roleId, int index)
        {
            if(roleId  == 0)
            {
                roleGo.SetActive(false);
                return;
            }
            else
            {
                roleGo.SetActive(true);
                this.roleId = roleId;
                this.index = index;
               
                UpdateView();
            }
            
        }

        void UpdateView( )
        {
            var datas = ConfigRoleManager.Instance().allDatas[roleId];
            nameText.text = datas.name.ToString();

            costText.text = (datas.cost- RoleShopModel.zhekou).ToString();
            UpdateCooperText();

            toneImage.sprite = AssetLoader.instance.tones[datas.cost - 1];

            ShowCanLevelView();

            UpdateBugIcon();
        }
        void UpdateCooperText()
        {
           
            string cooperName = RoleBase.GetCooperName(roleId);
            string proName = RoleBase.GetPreName(roleId);

            cooperText.text = $"[{cooperName}][{proName}]";
        }

        void UpdateBugIcon()
        {
            var datas = ConfigRoleManager.Instance().allDatas[roleId];
            int moeny = ModelManager.Get<PlayerModel>("PlayerModel").GetMoney();
            int price = datas.cost - RoleShopModel.zhekou;

            if( price > moeny)
            {
                buyIcon.color = Color.gray;
            }
            else
            {
                buyIcon.color = Color.white;
            }
        }
    }
    //-------------------------------------------------------------------------------
    RoleShopModel roleShopModel;
    RoleShopModel shopModel;
    RectTransform shopRect;
    Transform listTrs;

    GameObject lockGo;
    GameObject unLockGo;

    ShopRoleItem[] roleItemArr;
    public override void Init()
    {
        base.Init();
        layer = GameLayer.UI;
    }

    public override void OnFirstOpen()
    {
        base.OnFirstOpen();

        shopRect = GetRectTransform("shop");
        shopRect.localScale = new Vector3(1, 0, 1);
        roleItemArr = new ShopRoleItem[RoleShopModel.Max_Role_Count];

        AddUIButton("shop/closeBtn", OnClickCloseBtn);
        AddUIButton("shop/refresh", OnClickRefresh);

        shopModel = ModelManager.Get("RoleShopModel") as RoleShopModel;

        var itemGo = GetGameObject("shop/item");
        var listRect = GetRectTransform("shop/list");

        lockGo = GetGameObject("shop/lock/lock");
        unLockGo = GetGameObject("shop/lock/unLock");

        UIUtil.SetUIOnClick(lockGo, UnLock);
        UIUtil.SetUIOnClick(unLockGo, Lock);

        listTrs = GetTransform("shop/list");

        for(int i=0;i<RoleShopModel.Max_Role_Count;i++)
        {
            ShopRoleItem roleItem = new ShopRoleItem();

            var go = GameObject.Instantiate(itemGo, listRect);
            go.name = i.ToString();
            
            
            roleItem.Init( go );
            roleItemArr[i] = roleItem;
        }

        TimeManager.Regist((id) => { GameObject.Destroy( GetComponent<HorizontalLayoutGroup>("shop/list")); }
        , 0.5f, 0, 1);

        roleShopModel = ModelManager.Get<RoleShopModel>("RoleShopModel");

        itemGo.SetActive(false);
        
    }
    //锁住, (开锁的ui）
    void Lock(GameObject go)
    {
        roleShopModel.isLock = true;
        UpdateLockUI();
    }
    //开锁
    void UnLock(GameObject go)
    {
        roleShopModel.isLock = false;
        UpdateLockUI();
    }

    void UpdateLockUI()
    {
        if(roleShopModel.isLock)
        {
            lockGo.SetActive(true);
            unLockGo.SetActive(false);
        }
        else
        {
            lockGo.SetActive(false);
            unLockGo.SetActive(true);
        }
    }

    public override void OnShow()
    {
        base.OnShow();

        AddEvent();

        UpdateView();

        ShowShopView();
    }

    void ShowShopView()
    {
        shopRect.DOScaleY(1, 0.8f);
    }

    void HideShopView()
    {
        shopRect.localScale = new Vector3(1, 0, 1);
    }


    public override void OnHide()
    {
        base.OnHide();

        RemoveEvent();

        HideShopView();
    }

    public override void OnDispose()
    {
        base.OnDispose();
    }

    void OnShopDateUpdate(object o = null)
    {
        UpdateView();
    }

    void UpdateView()
    {
        for(int i =0;i<roleItemArr.Length;i++)
        {
           
            roleItemArr[i].SetInfo(shopModel.roles[i], i);
        }

        UpdateLockUI();
    }

    void OnClickCloseBtn(object ob = null)
    {
        SetVisible(false);
    }

    void OnClickRefresh(object ob = null)
    {

        shopModel.OnClickRefresh();
    }

    void AddEvent()
    {
        EventManager.RegistEvent(EventType.ShopDataUpdate, OnShopDateUpdate);
    }
    void RemoveEvent()
    {
        EventManager.UnRegistEvent(EventType.ShopDataUpdate, OnShopDateUpdate);
    }
}

