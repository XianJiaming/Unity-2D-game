using System;
using System.Collections.Generic;
using UnityEngine;


public class OnEffectStop:MonoBehaviour
{
    string effectName;

    public void SetName(string name)
    {
        effectName = name;
    }

    public void OnParticleSystemStopped()
    {

        ObjectPool.SaveGo(effectName, gameObject);
    }
}
