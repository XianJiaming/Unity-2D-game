using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IState
{
    
    public StateControl stateControl;

    public IState(StateControl stateControl)
    {
        this.stateControl = stateControl;
    }

    public IState()
    {

    }

    public abstract void OnEnter();

    public abstract void Update();

    public abstract void OnLeave();

    public abstract int GetId();

}

