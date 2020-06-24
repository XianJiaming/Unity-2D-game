using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WndGameStart:ViewBase
{

    GameObject tipsGo;

    public override void Init() {
        base.Init();
        

        EventManager.RegistEvent(EventType.GameOver, (d) => { SetVisible(true); });

    }

    public override void OnFirstOpen()
    {
        tipsGo = GetGameObject("tips");

        AddUIButton("btnList/startGame", OnClickStartGame);
        AddUIButton("btnList/exitGame", OnClickExitGame);
        AddUIButton("btnList/loadGame", OnClickLoadGame);

        AddUIButton("tips/tip/sure", OnClickSure);
        AddUIButton("tips/tip/cancel", OnClickCancel);
    }

    void OnClickSure(GameObject go)
    {
        NewGameStart();
        tipsGo.SetActive(false);
    }

    void OnClickCancel(GameObject go)
    {
        tipsGo.SetActive(false);
    }

    public override void OnShow() {
        AudioManager.instance.PlayBGM(ConstConfig.WndGameStartBg);
        
    }

    void OnButtonClick(GameObject go)
    {
        go.transform.DOScale(0.7f, 0.3f).OnComplete(() => { go.transform.DOScale(1, 0.3f); });
       
    }

    public override void OnHide() { }

    public override void OnDispose() { }

    public void OnClickStartGame(GameObject go)
    {
        if( !SaveAndLoad.hasSaveFile)
        NewGameStart();
        else
        {
            //显示提示
            tipsGo.SetActive(true);
        }

        OnButtonClick(go);
    }
    void OnClickLoadGame(GameObject go)
    {
        if(SaveAndLoad.hasSaveFile)
        LoadGame();
        else
        {
            WndTips.ShowTips("您暂时没有存档，请点击开始游戏");
        }
        OnButtonClick(go);
    }
    void NewGameStart()
    {
        SaveAndLoad.Clear();

        SetVisible(false);

       

        EventManager.ExecuteEvent(EventType.NewGameStart);
        ViewManager.Get("WndPrepare").SetVisible(true);
        ViewManager.Get("WndCooper").SetVisible(true);

       

        ViewManager.Get("WndTalent").SetVisible(true);

        
    }

    void LoadGame()
    {

        SetVisible(false);
        EventManager.ExecuteEvent(EventType.NewGameStart);

        SaveAndLoad.Load();

        ModelManager.Get<GameLevelModel>("GameLevelModel").SetAIRole();

        ViewManager.Get("WndPrepare").SetVisible(true);
        ViewManager.Get("WndCooper").SetVisible(true);

        
    }

    void OnClickExitGame(GameObject go)
    {
        Application.Quit();
    }

}
