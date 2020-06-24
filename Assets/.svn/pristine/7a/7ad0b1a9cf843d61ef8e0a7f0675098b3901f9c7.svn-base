using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateControl
{
    public IState currentState;
    Dictionary<int, IState> stateDic = new Dictionary<int, IState>();


    public StateControl(params IState[] states)
    {
        for(int i=0;i<states.Length;i++)
        {
            RegistState(states[i]);
        }
        
    }

    void RegistState(IState state)
    {
        if (stateDic.ContainsValue(state))
        {
            Debug.LogError("状态机中已经存有该状态！");
            return;
        }
        else
        {
            stateDic.Add((int)state.GetId(), state);
        }
    }


    public void Update()
    {
        currentState.Update();
    }

    public void SetState(int nextStateId)
    {
        if(currentState == null)
        {
            Debug.LogError("当前状态机状态为空");
            return;
        }

        currentState.OnLeave();
        currentState = stateDic[nextStateId];
        currentState.OnEnter();
    }

    public void SetFirstState(IState state)
    {
        currentState = state;
        currentState.OnEnter();
    }

    public IState GetState(int id)
    {
        if(!stateDic.ContainsKey(id))
        {
            Debug.LogError("状态机中不包含此状态");
            return null;
        }
        return stateDic[id];
    }

}


