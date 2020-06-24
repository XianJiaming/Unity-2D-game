using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WndPrepare : ViewBase
{
    //-----------------------------------------------------------

    public class RoleItem
    {
        public RoleBase role;
        public GameObject roleGo;

        Text nameText;
        Image toneImage;
        Image kuang0Image;
        Image kuang1Image;

        GameObject wuqi;
        GameObject fangju;

        Image roleIcon;

        public EnumType.RoleListType listType;

        public int index;

        public RoleItem( GameObject go, EnumType.RoleListType listType, int index)
        {
            roleGo = go;
            nameText = UIUtil.GetText(roleGo, "name");
            roleIcon = UIUtil.GetImage(roleGo, "icon");
            UIUtil.SetUIOnClick(roleGo, OnClick);

            this.listType = listType;
            this.index = index;

            wuqi = UIUtil.GetGameObject(roleGo, "wuqiIcon");
            fangju = UIUtil.GetGameObject(roleGo, "fangjuIcon");

            toneImage = UIUtil.GetImage(roleGo, "tone/tone");
            kuang0Image = UIUtil.GetImage(roleGo, "kuang0");
            kuang1Image = UIUtil.GetImage(roleGo, "kuang1");
        }

        void OnClick(GameObject go)
        {
            
            //显示角色信息
            ViewManager.Get<WndTips>("WndTips").ShowRoleInfo(role);
            //显示售卖的接口
            ViewManager.Get<WndPrepare>("WndPrepare").UpdateSellText(role , go);

        }
        public void SetRole(RoleBase role)
        {
            if (this.role != null)
            {
                RemoveEvent(this.role);
            }

            this.role = role;

            if (role == null)
            {
                
                roleGo.SetActive(false);
                return;
            }               
            else
            {                              
                AddEvent(this.role);
                UpdateView();
            }
            
        }

        void AddEvent(RoleBase role)
        {
            role.OnUpdateEquip += UpdateEquip;
        }

        void RemoveEvent(RoleBase role)
        {
            role.OnUpdateEquip -= UpdateEquip;
        }

        void UpdateEquip( )
        {
            if(null !=role.GetEquip(0))
            {
                wuqi.SetActive(true);
            }
            else
            {
                wuqi.SetActive(false);
            }

            if(null != role.GetEquip(1))
            {
                fangju.SetActive(true);
            }
            else
            {
                fangju.SetActive(false);
            }
        }

        public void UpdateView()
        {
            if(role!=null)
            {
                string cooperName = RoleBase.GetCooperName(role.GetRoleId());
                string proName = RoleBase.GetPreName(role.GetRoleId());

                nameText.text = $"<color={ConstConfig.levelColor[role.cost]}>{role.name}</color>\n【{cooperName}】\n【{proName}】" ;

                toneImage.sprite = AssetLoader.instance.tones[role.cost - 1];
                int level = role.GetLevel();
                kuang0Image.sprite = AssetLoader.instance.kuang0s[level - 1];
                kuang1Image.sprite = AssetLoader.instance.kuang1s[level - 1];
                UpdateEquip();

                roleGo.SetActive(true);
            }
            else
            {
                roleGo.SetActive(false);
            }
            
        }
    }


    //-----------------------------------------------------------
    PlayerModel playerModel;
    WndTips wndTips;

    RectTransform talentListRect;
    RectTransform selfRect;
    RectTransform fightList;
    RectTransform preList;
    RectTransform talentContent;
    //战斗与准备队列
    public RectTransform[] fightPositions;
    public RectTransform[] prePositions;
    //类对象
    public RoleItem[] fightRoleItems;
    public RoleItem[] preRoleItems;
    //go对象
    GameObject[] fightRoleGos;
    GameObject[] preRoleGos;

    GameObject talentBtn;
    GameObject dragRoleGo;
    GameObject talentItem;

    Text moneyText;
    Text fightCountText;
    Text preCountText;
    Text gameLevelText;
    Text playerHpText;
    Text levelUpMoneyText;
    Text equipNumber;

    GameObject sellGo;
    RectTransform sellRect;
    Text sellPriceText;

    GameObject helpInfoPlane;


    Dictionary<GameObject, RoleItem> goItems = new Dictionary<GameObject, RoleItem>();

    public override void Init()
    {
        base.Init();
        
        EventManager.RegistEvent(EventType.GameOver, (d) => { SetVisible(false); });
    }

    public override void OnFirstOpen()
    {
        base.OnFirstOpen();

        talentBtn = GetGameObject("talentBtn");

        gameLevelModel = ModelManager.Get("GameLevelModel") as GameLevelModel;

        equipNumber = GetText("belowUI/bag/number");
        fightCountText = GetText("fightCount");
        preCountText = GetText("preCount");
        moneyText = GetText("belowUI/money/value");
        gameLevelText = GetText("gameLevel");
        levelUpMoneyText = GetText("belowUI/levelUp/levelUpMoney");

        sellGo = GetGameObject("sell");
        sellRect = GetRectTransform("sell");
        sellGo.SetActive(false);
        sellPriceText = GetText("sell/price");
        playerHpText = GetText("playerHp");

        wndTips = ViewManager.Get<WndTips>("WndTips");
        playerModel = ModelManager.Get("PlayerModel") as PlayerModel;

        int preCount = PlayerModel.Max_Pre_Role_Count;
        int fightCount = FightManager.Max_Fight_List_Number;

        talentListRect = GetRectTransform("talentList");
        talentContent = GetRectTransform("talentList/content");
        selfRect = go.GetComponent<RectTransform>();

        talentItem = GetGameObject("talentItem");
        talentItem.SetActive(false);
        fightList = GetRectTransform("fightList/list");
        preList = GetRectTransform("prepareList/list");

        fightPositions = new RectTransform[fightCount];
        for(int i =0;i<9;i++)
        {
            fightPositions[i] = fightList.Find( i.ToString()).GetComponent<RectTransform>();
        }

        prePositions = new RectTransform[preCount];
        for (int i = 0; i < preCount; i++)
        {
            prePositions[i] = preList.Find( i.ToString()).GetComponent<RectTransform>();
        }

        fightRoleGos = new GameObject[fightCount];
        for(int i=0;i< fightCount; i++)
        {
            fightRoleGos[i] = fightPositions[i].Find("Role").gameObject;
        }

        preRoleGos = new GameObject[preCount];
        for(int i=0;i<preCount;i++)
        {
            preRoleGos[i] = prePositions[i].Find("Role").gameObject;
        }

        fightRoleItems = new RoleItem[fightCount];
        for (int i = 0; i < fightRoleItems.Length; i++)
        {
                
            fightRoleItems[i] = new RoleItem(fightRoleGos[i], EnumType.RoleListType.Fight, i);
            goItems.Add(fightRoleItems[i].roleGo, fightRoleItems[i]);
        }

        preRoleItems = new RoleItem[preCount];
        for(int i = 0;i < preCount; i++)
        {
            preRoleItems[i] = new RoleItem(preRoleGos[i], EnumType.RoleListType.Pre, i);
            goItems.Add(preRoleItems[i].roleGo, preRoleItems[i]);
        }

        foreach(var v in goItems.Keys)
        {
            var uiDrag = UIDrag.GetUIDrag(v);
            uiDrag.AddBeginDrag(OnBeginDrag);
            uiDrag.AddDrag(OnDrag);
            uiDrag.AddEndDrag(OnEndDrag);
        }
        orTalentListX = talentListRect.anchoredPosition.x;
        targetTalentListX = orTalentListX - 87;

        helpInfoPlane = GetGameObject("helpInfoPlane");

        AddUIButton("helpInfoBtn", (g) => { helpInfoPlane.SetActive(true); });
        AddUIButton("belowUI/shop", OnClickOpenShop);
        AddUIButton("belowUI/startFight", OnClickStartFight);
        AddUIButton("belowUI/levelUp", OnClickLevelUp);
        AddUIButton("belowUI/bag", OnClickOpenBag);
        AddUIButton("talentBtn", OnClickOpenTalent);
        AddUIButton("belowUI/return", OnClickReturn);
        AddUIButton("helpInfoPlane/helpContent/closeBtn", (g) => { helpInfoPlane.SetActive(false); });
    }


    public override void OnShow()
    {
        base.OnShow();

        AddEvent();

        UpdateView();

        AudioManager.instance.PlayBGM(ConstConfig.WndPrepareBg);
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

    void UpdateMoneyView(object ob = null )
    {
        moneyText.text = playerModel.GetMoney().ToString();
    }

    //--------------------------------------------------------------------------------
    Vector3 orPosition; //原始坐标

    void OnBeginDrag(GameObject go)
    {
        orPosition = go.transform.position;

    }


    void OnDrag(GameObject go)
    {
        //仅在ui的世界坐标和 鼠标的屏幕坐标一致时有用
        go.transform.position = Input.mousePosition;
        go.transform.parent.SetAsLastSibling();
    }
    void OnEndDrag(GameObject go)
    {
        AudioManager.instance.PlayEffect(ConstConfig.DropRole);
        //先判断原本在哪个队列
        var listType = goItems[go].listType;
        
        var mousePosition = Input.mousePosition;
        //RectTransformUtility.ScreenPointToWorldPointInRectangle(rect, Input.mousePosition, Camera.main, out v);

        if (listType == EnumType.RoleListType.Fight)
        {
            //如果停在fight里
            if (RectTransformUtility.RectangleContainsScreenPoint
                (fightList, mousePosition))
            {
                for (int i = 0; i < fightPositions.Length; i++)
                {
                    bool isHere = RectTransformUtility.RectangleContainsScreenPoint(fightPositions[i], Input.mousePosition);
                    if (isHere)
                    {
                        //我拖的这个东西的索引
                        int orIndex = goItems[go].index;

                        //int newIndex = i;
                        playerModel.ExChangeRolesBetFight(orIndex, i);
                        break;
                    }

                }
            }
            //如果停在准备列表
            else if (RectTransformUtility.RectangleContainsScreenPoint(preList, mousePosition))
            {
                for (int i = 0; i < prePositions.Length; i++)
                {
                    bool isHere = RectTransformUtility.RectangleContainsScreenPoint(prePositions[i], Input.mousePosition);
                    if (isHere)
                    {
                        int orIndex = goItems[go].index;
                        playerModel.ExChangeRolesBetPreAndFight(orIndex, i);


                        break;
                    }
                }
            }

        }
        //如果原本是在准备列表
        else if(listType == EnumType.RoleListType.Pre)
        {
            //如果停在fight里
            if (RectTransformUtility.RectangleContainsScreenPoint
                (fightList, mousePosition))
            {
                for (int i = 0; i < fightPositions.Length; i++)
                {
                    bool isHere = RectTransformUtility.RectangleContainsScreenPoint(fightPositions[i], Input.mousePosition);
                    if (isHere)
                    {
                        int orIndex = goItems[go].index;
                        playerModel.ExChangeRolesBetPreAndFight(i, orIndex);

                        break;
                    }

                }
            }
            //如果停在准备列表
            else if (RectTransformUtility.RectangleContainsScreenPoint(preList, mousePosition))
            {
                for (int i = 0; i < prePositions.Length; i++)
                {
                    bool isHere = RectTransformUtility.RectangleContainsScreenPoint(prePositions[i], Input.mousePosition);
                    if (isHere)
                    {
                        int orIndex = goItems[go].index;

                        playerModel.ExChangeRolesBetPre(i, orIndex);

                        break;
                    }
                }
            }

        }

        go.transform.position = orPosition;
    }

    //--------------------------------------------------------------------------------
    //go 点击角色的 go
    public void UpdateSellText(RoleBase role, GameObject go)
    {
        sellGo.SetActive(true);
        sellPriceText.text = (role.GetPrice() - RoleShopModel.zhekou).ToString();

        //更改sell button的贩卖事件
        UIUtil.SetUIOnClick(sellGo, (g)=> {

            //拿role在所在队列中的index

            int index = goItems[go].index;
            var type = goItems[go].listType;          
            playerModel.SellRole(index, type);

            sellGo.SetActive(false);
        });

        //开启监听 售卖选项 的关闭
        EventManager.RegistEvent(EventType.InputClick, CanCloseSellGo);


    }
    //判断是否关闭 售卖 button
    void CanCloseSellGo(object ob)
    {

        if (!RectTransformUtility.RectangleContainsScreenPoint(sellRect, Input.mousePosition))
        {
            sellGo.SetActive(false);
            EventManager.UnRegistEvent(EventType.InputClick, CanCloseSellGo);
        }
    }

    void OnClickOpenTalent(GameObject go)
    {
        ShowTalentList();
        talentBtn.SetActive(false);
        EventManager.RegistEvent(EventType.InputClick, CanCloseTalent);
    }
    //天赋list====================================================
    float orTalentListX;
    float targetTalentListX;
    void ShowTalentList()
    {
        //现在还在屏幕右边
       
        //移动过来，然后监听点击移出去，移出去之后监听移出
        talentListRect.DOAnchorPosX(targetTalentListX, 0.8f);

        int childCount = talentContent.childCount;
        int talentCount = playerModel.talents.Count;

        if(childCount<talentCount)
        {
            int chaZhi = talentCount - childCount;

            for(int i=0;i<chaZhi;i++)
            {
                var go = GameObject.Instantiate(talentItem, talentContent);
                UIUtil.SetUIOnClick(go, OnClickTalent);                
            }
        }

        for(int i=0;i< talentCount;i++)
        {
            var t = playerModel.talents[i];
            var go = talentContent.GetChild(i).gameObject;
            go.transform.Find("nameText").GetComponent<Text>().text = t.name;
            go.SetActive(true);
            UIListener.GetUIListener(go).param = t;
        }
        for(int i=talentCount;i<childCount;i++)
        {
            talentListRect.GetChild(i).gameObject.SetActive(false);
        }
    }

    void OnClickTalent(GameObject go)
    {
        var talent = UIListener.GetUIListener(go).param as MyTalent.Talent;
        wndTips.ShowInfo($"<size=35><color=red>{talent.desc}</color></size>");
    }
    //天赋list====================================================
    void CanCloseTalent(object ob = null)
    {
        if (!RectTransformUtility.RectangleContainsScreenPoint(talentListRect, Input.mousePosition))
        {
            talentListRect.DOAnchorPosX(orTalentListX, 0.8f);
            EventManager.UnRegistEvent(EventType.InputClick, CanCloseTalent);
            talentBtn.SetActive(true);
        }
    }

    void OnClickReturn(GameObject go)
    {
        EventManager.ExecuteEvent(EventType.GameOver);
       
        SaveAndLoad.SaveGame();
    }

    void OnClickOpenBag(GameObject go)
    {
        ViewManager.Get<WndEquip>("WndEquip").SetVisible(true);

    }

    void OnClickOpenShop(object ob = null )
    {
        ViewManager.Get<WndRoleShop>("WndRoleShop").SetVisible(true);
    }

    void OnClickStartFight(object ob = null)
    {
        SetVisible(false);

        SaveAndLoad.SaveGame();
        /*(ModelManager.Get("GameLevelModel") as GameLevelModel).SetAIRole();*/

        ViewManager.Get<WndFight>("WndFight").SetVisible(true);
    }

    void OnClickLevelUp(object ob = null)
    {
        playerModel.TryUpPeopleLevel();
    }
    //更新上阵数量显示
    void UpdateFightCount(object ob = null)
    {
        fightCountText.text = $"上阵：{playerModel.GetFightRoleCount().ToString()}/{playerModel.peopleLevel.ToString()}";
    }
    //更新准备角色数量
    void UpdatePreCount(object ob =null)
    {
        preCountText.text = $"准备：{playerModel.GetPreRoleCount().ToString()}/{ PlayerModel.Max_Pre_Role_Count }";
    }

    void UpdateView()
    {
        UpdateEquipNumber();
        UpdateFightView();
        UpdatePrepareView();
        UpdateFightCount();
        UpdatePreCount();
        UpdateMoneyView();
        UpdateLevelView();
        UpdatePlayerHpView();
        UpdateUpLevelMoneyView();
    }

    void UpdateUpLevelMoneyView(dynamic ob = null)
    {
        levelUpMoneyText.text = (4 * playerModel.peopleLevel).ToString();
    }

    void UpdateEquipNumber( dynamic d = null)
    {
        equipNumber.text = ModelManager.Get<EquipModel>("EquipModel").GetEquipNumber().ToString();

    }

    //更新战斗队列的显示
    void UpdateFightView()
    {
        var roles = playerModel.fightRoles;

        for(int i=0;i<roles.Length;i++)
        {
            fightRoleItems[i].SetRole(roles[i]);
        }

    }

    //更新准备队列的显示
    void UpdatePrepareView(dynamic ob = null )
    {
        var roles = playerModel.preRoles;

        for (int i = 0; i < roles.Length; i++)
        {
            preRoleItems[i].SetRole(roles[i]);
        }
    }

    void UpdateFightView(dynamic ob)
    {          
        fightRoleItems[ob.index].SetRole(playerModel.fightRoles[ob.index]);
    }

    void UpdatePrepareView(int index )
    {

        preRoleItems[ index].SetRole(playerModel.preRoles[ index]);
    }

    void UpdatePlayerHpView(object ob = null)
    {
        playerHpText.text =  playerModel.GetHp().ToString();
    }
    GameLevelModel gameLevelModel;

    void UpdateLevelView(dynamic ob = null)
    {
        gameLevelText.text = "Level  "+ gameLevelModel.GetLevel().ToString();
    }

    void AddEvent()
    {
        EventManager.RegistEvent(EventType.EquipUpdate, UpdateEquipNumber);
        EventManager.RegistEvent(EventType.LevelUp, UpdateUpLevelMoneyView);
        EventManager.RegistEvent(EventType.GameFial, OnGameOver);
        EventManager.RegistEvent(EventType.PlayerHpUpdate, UpdatePlayerHpView);
        EventManager.RegistEvent(EventType.PreRoleUpdate, UpdatePrepareView);
        EventManager.RegistEvent(EventType.PreRoleUpdate, UpdatePreCount);
        EventManager.RegistEvent(EventType.FightRoleUpdate, UpdateFightView);
        EventManager.RegistEvent(EventType.FightRoleUpdate, UpdateFightCount);
        EventManager.RegistEvent(EventType.MoneyUpdate, UpdateMoneyView);
        EventManager.RegistEvent(EventType.LevelUp, UpdateFightCount);
        EventManager.RegistEvent(EventType.FightStart, UpdateLevelView);
    }

    void RemoveEvent()
    {
        EventManager.UnRegistEvent(EventType.EquipUpdate, UpdateEquipNumber);
        EventManager.UnRegistEvent(EventType.LevelUp, UpdateUpLevelMoneyView);
        EventManager.UnRegistEvent(EventType.GameFial, OnGameOver);
        EventManager.UnRegistEvent(EventType.PlayerHpUpdate, UpdatePlayerHpView);
        EventManager.UnRegistEvent(EventType.PreRoleUpdate, UpdatePrepareView);
        EventManager.UnRegistEvent(EventType.PreRoleUpdate, UpdatePreCount);
        EventManager.UnRegistEvent(EventType.FightRoleUpdate, UpdateFightView);
        EventManager.UnRegistEvent(EventType.FightRoleUpdate, UpdateFightCount);
        EventManager.UnRegistEvent(EventType.MoneyUpdate, UpdateMoneyView);
        EventManager.UnRegistEvent(EventType.LevelUp, UpdateFightCount);
        EventManager.UnRegistEvent(EventType.FightStart, UpdateLevelView);
    }

    void OnGameOver(object ob)
    {
        SetVisible(false);
    }
}

