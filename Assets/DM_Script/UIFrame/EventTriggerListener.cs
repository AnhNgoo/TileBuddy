﻿/*
 * Created on 2024
 *
 * Copyright (c) 2024 dotmobstudio
 * Support : dotmobstudio@gmail.com
 */
using UnityEngine;
using UnityEngine.EventSystems;

public class EventTriggerListener :UnityEngine.EventSystems.EventTrigger 
{
    public delegate void VoidDelegate(GameObject go);
    public VoidDelegate onClick;
    public VoidDelegate onDown;
    public VoidDelegate onEnter;
    public VoidDelegate onExit;
    public VoidDelegate onUp;
    public VoidDelegate onSelect;
    public VoidDelegate onUpdateSelect;




    
    public static EventTriggerListener Get(GameObject go)
    {
        EventTriggerListener lister = go.GetComponent<EventTriggerListener>();
        if (lister==null)
        {
            lister = go.AddComponent<EventTriggerListener>();                
        }
        return lister;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if(onClick!=null)
        {
            onClick(gameObject);
        }
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (onDown != null)
        {
            onDown(gameObject);
        }
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (onEnter != null)
        {
            onEnter(gameObject);
        }
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        if (onExit != null)
        {
            onExit(gameObject);
        }
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        if (onUp != null)
        {
            onUp(gameObject);
        }
    }

    public override void OnSelect(BaseEventData eventBaseData)
    {
        if (onSelect != null)
        {
            onSelect(gameObject);
        }
    }

    public override void OnUpdateSelected(BaseEventData eventBaseData)
    {
        if (onUpdateSelect != null)
        {
            onUpdateSelect(gameObject);
        }
    }

}//Class_end
