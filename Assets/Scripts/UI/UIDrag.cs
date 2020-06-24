﻿using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{

    Action<GameObject> onBeginDrag;
    Action<GameObject> onDrag;
    Action<GameObject> onEndDrag;
    Action<GameObject> onDrop;

    public PointerEventData EventData
    {
        get;
        private set;
    }

    public static UIDrag GetUIDrag(GameObject go)
    {
        var uiDrag = go.GetComponent<UIDrag>();
        if (uiDrag == null)
            uiDrag = go.AddComponent<UIDrag>();

        return uiDrag;
    }

    float dragTime;

    UIListener listener;

    public void OnBeginDrag(PointerEventData eventData)
    {
        listener = GetComponent<UIListener>();

        this.EventData = eventData;

        dragTime = 0;

        onBeginDrag?.Invoke(gameObject);

        
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.EventData = eventData;

        dragTime += Time.deltaTime;
        if( dragTime>0.1f && listener && listener.enabled  )
        {
            listener.enabled = false;
        }


        onDrag?.Invoke(gameObject);
    }

    public void OnDrop(PointerEventData eventData)
    {
        this.EventData = eventData;
        
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.EventData = eventData;
        onEndDrag?.Invoke(gameObject);

        listener.enabled = true;
    }

    public void AddBeginDrag(Action<GameObject> callBack)
    {

        onBeginDrag = callBack;

    }
    public void AddDrag(Action<GameObject> callBack)
    {

        onDrag = callBack;
    }
    public void AddEndDrag(Action<GameObject> callBack)
    {

        onEndDrag = callBack;
    }
    public void AddDrop(Action<GameObject> callBack)
    {

        onDrop = callBack;
    }
}

