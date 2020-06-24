using System;
using System.Collections.Generic;
using UnityEngine;
public class AudioManager:MonoBehaviour
{
    public const string BGMpath = "Music/BGM/";
    public const string effectPath = "Music/Effect/";

    public AudioSource BgAudioSource;

    public List<AudioSource> audioSouceList = new List<AudioSource>();

    public static AudioManager instance;

    private void Awake()
    {
        instance = this;
        BgAudioSource = gameObject.AddComponent<AudioSource>();
        BgAudioSource.volume = 0.3f;
        
    }

    private void Start()
    {
        EventManager.RegistEvent(EventType.LevelUp, (d) => { PlayEffect(ConstConfig.PlayerUpMus); });
    }

    public void PlayBGM(string name)
    {

        BgAudioSource.clip = AssetLoader.instance.GetAudioClip(name);//ResourceManager.LoadAsset<AudioClip>($"{BGMpath}{name}", name);
        
        BgAudioSource.Play();
    }

    public void PlayEffect(string name)
    {
       
        AudioSource myAs = null; 
        foreach(var aus in audioSouceList)
        {
            if(!aus.isPlaying)
            {
                myAs = aus;
                break;
            }
        }
        if(myAs==null)
        {
            myAs = gameObject.AddComponent<AudioSource>();
            audioSouceList.Add(myAs);
        }
        myAs.clip = AssetLoader.instance.GetAudioClip(name);// 
        myAs.Play();
    }
    
}

    

