using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class WndEquip:ViewBase
{

//-----------------------------------------------------------
    class EquipItem:ItemView<Equip>
    {

        GameObject itemGo;
        GameObject gridGo;
        //Text nameText;
        Image icon;
        Image bgImage;
        public override void Init(GameObject go, int index)
        {
            gridGo = go;
            itemGo = gridGo.transform.Find("bagItem").gameObject;
            this.index = index;//该item对应的哪个格子
            //nameText = UIUtil.GetText(itemGo, "name");
            icon = UIUtil.GetImage(itemGo, "icon");
            var uiDrag = UIDrag.GetUIDrag(icon.gameObject);
            uiDrag.AddBeginDrag(OnBeginDrag);
            uiDrag.AddDrag(OnDrag);
            uiDrag.AddEndDrag(OnEndDrag);
            bgImage = itemGo.GetComponent<Image>();

            UIUtil.SetUIOnClick(icon.gameObject, OnClick);
        }

        Vector3 orPosition; //原始坐标

        void OnBeginDrag(GameObject go)
        {
            orPosition = itemGo.transform.position;
            gridGo.transform.SetAsLastSibling();
        }

        void OnClick(GameObject go )
        {
            //显示信息
            ViewManager.Get<WndTips>("WndTips").ShowInfo(info.desc);
            //显示售卖
            ViewManager.Get<WndEquip>("WndEquip").ShowSellButton(index, info.Price);
        }

        void OnDrag(GameObject go)
        {
            //仅在ui的世界坐标和 鼠标的屏幕坐标一致时有用
            itemGo.transform.position = Input.mousePosition;

        }


        void OnEndDrag(GameObject go)
        {
            itemGo.transform.position = orPosition;
            var mousePosition = Input.mousePosition;

            var rect = ViewManager.Get<WndEquip>("WndEquip").bagListRect;

            if (RectTransformUtility.RectangleContainsScreenPoint(rect, mousePosition))
            {
                return;
            }

           
            var wndPrepare = ViewManager.Get<WndPrepare>("WndPrepare");
            

            int preRoleLength = wndPrepare.preRoleItems.Length;
            int fightRoleLength = wndPrepare.fightRoleItems.Length;
            //处理准备队列
            for (int i = 0; i < preRoleLength; i++)
            {
                var preItem = wndPrepare.preRoleItems[i];
                //遍历 准备队列的item
                if (preItem.role != null)
                {
                    bool isHere = RectTransformUtility.RectangleContainsScreenPoint(preItem.roleGo.transform as RectTransform, Input.mousePosition);
                    if (isHere)
                    {
                        //
                        var equipModel = ModelManager.Get("EquipModel") as EquipModel;

                        var playerModel = ModelManager.Get("PlayerModel") as PlayerModel;

                        bool res = preItem.role.AddEquip(equipModel.GetEquip(index));
                        if(res) equipModel.RemoveEquip(index);
                        //equipModel.AddEquip(downEquip);

                        

                        return;
                    }
                }
            }
            //处理战斗队列
            for (int i = 0; i < fightRoleLength; i++)
            {
                if (wndPrepare.fightRoleItems[i].role != null)
                {
                    bool isHere = RectTransformUtility.RectangleContainsScreenPoint(wndPrepare.fightPositions[i], Input.mousePosition);
                    if (isHere)
                    {
                        //
                        var equipModel = ModelManager.Get("EquipModel") as EquipModel;

                        var playerModel = ModelManager.Get("PlayerModel") as PlayerModel;

                        
                        bool res = playerModel.fightRoles[i].AddEquip(equipModel.GetEquip(index));
                        if (res) equipModel.RemoveEquip(index);

                        


                        return;
                    }
                }
            }

        }

        public override void UpdateView()
        {
            if(info!=null)
            {
                itemGo.SetActive(true);
                UpdateBgImage();

                icon.sprite = ResourceManager.LoadAsset<Sprite>(ResourceManager.EquipPath + info.id.ToString(), info.id.ToString());
            }
            else
            {
                itemGo.SetActive(false);
            }
        }

        static Dictionary<int, string> levelColor = new Dictionary<int, string>
        {
            {1,"white" }, {2, "green"}, {3, "blue"}, {4, "red"}, {5, "yellow"}
        };

        void UpdateBgImage()
        {
            bgImage.sprite = ResourceManager.LoadAsset<Sprite>(ResourceManager.EquipPath + levelColor[info.level], ConstConfig.levelColor[info.level]);
        }
    }

    //-----------------------------------------------------------
    GameObject bagItemGo;
    RectTransform bagListRect;
    EquipModel equipModel;
    EquipItem[] equipItems = new EquipItem[18];

    GameObject bagContainer;
    RectTransform bagContainerRect;

    WndPrepare wndPrepare;

    GameObject sellEquipGo;
    Text equipPrice;

   

    //     Vector3 originPoint;
    //     Vector3 endPoint;


    public override void Init()
    {
        base.Init();
        equipModel = ModelManager.Get("EquipModel") as EquipModel;
        layer = GameLayer.UI;
//         originPoint = new Vector3(0, -306f, 0);
//         endPoint = new Vector3(0, 303.6f, 0);

    }

    float bagMoveHeight;

   

    public override void OnFirstOpen()
    {
        base.OnFirstOpen();

        wndPrepare = ViewManager.Get<WndPrepare>("WndPrepare");

        bagContainer = GetGameObject("bagContainer");
        bagContainerRect = bagContainer.GetComponent<RectTransform>();

        bagMoveHeight = bagContainerRect.rect.height +
            GetRectTransform("bagContainer/closeBtn").rect.height + 10;

        bagContainerRect.anchoredPosition = new Vector2(0, -bagMoveHeight);

        sellEquipGo = GetGameObject("bagContainer/sell");
        equipPrice = GetText("bagContainer/sell/text");

        GameObject bagGrid = GetGameObject("bagContainer/bagList/grid");

        bagListRect = GetRectTransform("bagContainer/bagList");

        AddUIButton("bagContainer/closeBtn", (go) => { SetVisible(false); });

        //18个装备格子
        for (int i = 0; i < 18; i++)
        {
            var equipItem = new EquipItem();
            GameObject go = GameObject.Instantiate(bagGrid, bagListRect);
           
            equipItem.Init(go,i);
            equipItem.SetInfo(equipModel.GetEquip(i));
            equipItems[i] = equipItem;

           
        }
        bagGrid.SetActive(false);

        TimeManager.Regist((id) => { GameObject.Destroy(GetComponent<GridLayoutGroup>("bagContainer/bagList")); }
        , 0.5f, 0, 1);

    }

    //从下面跑到上面
    void StartToEnd()
    {
        //未做自适应

        bagContainerRect.DOAnchorPosY(0,0.8f);
    }
    void EndToStart()
    {
        bagContainerRect.DOAnchorPosY(-bagMoveHeight, 0.8f);
    }

    void UpdateView( )
    {
        for(int i=0;i<18;i++)
        {
            equipItems[i].SetInfo(equipModel.GetEquip(i));
        }

    }

    void ShowSellButton( int index, int price)
    {
        //显示
        sellEquipGo.SetActive(true);
        equipPrice.text = price.ToString();
        //
        UIUtil.SetUIOnClick( sellEquipGo, (g)=> { SellEquip(index); });
        EventManager.RegistEvent(EventType.InputClick, CanCloseSell);
    }

    void SellEquip(int index)
    {
        equipModel.SellEquip(index);
        sellEquipGo.SetActive(false);
    }

    void CanCloseSell(object ob = null)
    {
        if (!RectTransformUtility.RectangleContainsScreenPoint(sellEquipGo.transform as RectTransform, Input.mousePosition))
        {
            sellEquipGo.SetActive(false);
            EventManager.UnRegistEvent(EventType.InputClick, CanCloseSell);
        }
    }

    public override void OnShow()
    {
        base.OnShow();
        AddEvent();
        UpdateView();
        StartToEnd();
    }


    public override void OnHide()
    {
        base.OnHide();
        RemoveEvent();

    }

    public override void SetVisible(bool value)
    {
        
        if(value == false)
        {
           
            EndToStart();
            TimeManager.Regist(
                (id) =>
                {                 
                    base.SetVisible(false);
                }, 0.8f, 0, 1);
        }
        else
        {          
            base.SetVisible(true);
        }
        
    }

    public override void OnDispose()
    {
        base.OnDispose();
    }

    void OnEquipUpdate(dynamic index)
    {
        equipItems[index].SetInfo( equipModel.GetEquip(index));
    }

    void AddEvent()
    {
        EventManager.RegistEvent(EventType.EquipUpdate, OnEquipUpdate);
    }
    void RemoveEvent( )
    {
        EventManager.UnRegistEvent(EventType.EquipUpdate, OnEquipUpdate);
    }

    
}
    

