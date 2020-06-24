using System;
using System.Collections.Generic;
using UnityEngine;


public  class AssetLoader:MonoBehaviour
{
    public Sprite[] tones;
    public Sprite[] kuang0s;
    public Sprite[] kuang1s;
    public Sprite[] coopers; //种族图片
    public Sprite[] pros; //职业图片


    public AudioClip[] audioClips;
 
    public static AssetLoader instance;
    private void Awake()
    {
        instance = this;

        foreach (var ad in audioClips)
        {
            audioClipMap.Add(ad.name, ad);

        }

        foreach(var s in coopers)
        {
            cooperAndPros.Add(s.name, s);

        }
        foreach (var s in pros)
        {
            cooperAndPros.Add(s.name, s);

        }

    }

    Dictionary<string, AudioClip> audioClipMap = new Dictionary<string, AudioClip>();

    Dictionary<string, Sprite> cooperAndPros = new Dictionary<string, Sprite>();

    public Sprite GetSprite(string name)
    {
        return cooperAndPros[name];
    }



    public AudioClip GetAudioClip(string name)
    {
        if(audioClipMap.ContainsKey(name))
        {
            return audioClipMap[name];
        }
        else
        {
            Debug.LogError("没有该音乐----"+name);
            return null;
        }
    }

    private void Start()
    {
        加钱.onClick.AddListener(() =>
        {
            ModelManager.Get<PlayerModel>("PlayerModel").AddMoney(10);
        });

        加血.onClick.AddListener(() =>
        {
            ModelManager.Get<PlayerModel>("PlayerModel").SetHp(10);
        });
    }

    internal void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }
    internal void OnDisable()
    {
        Application.logMessageReceived -= null;
    }
    public UnityEngine.UI.Text logText;
    private string m_logs;
    /// <summary>
    ///
    /// </summary>
    /// <param name="logString">错误信息</param>
    /// <param name="stackTrace">跟踪堆栈</param>
    /// <param name="type">错误类型</param>
    void HandleLog(string logString, string stackTrace, LogType type)
    {
        //m_logs = $"\n bug信息\n{ logString }\n{stackTrace}\n";
        if(Log)
        logText.text = "游戏进程遇到异常,建议重启游戏";
    }



    public bool Log = true;

    public UnityEngine.UI.Button 加钱;
    public UnityEngine.UI.Button 加血;

}
