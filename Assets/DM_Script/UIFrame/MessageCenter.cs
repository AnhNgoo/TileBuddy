/*
 * Created on 2024
 *
 * Copyright (c) 2024 dotmobstudio
 * Support : dotmobstudio@gmail.com
 */
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;

public class MessageCenter
{
    
    public delegate void DelMessageDelivery(KeyValuesUpdate kv);

    
    public static Dictionary<int, DelMessageDelivery> dicMessages = new Dictionary<int, DelMessageDelivery>();


    public static void AddMsgListener(int messageType, DelMessageDelivery handler)
    {
        if (!dicMessages.ContainsKey(messageType))
        {
            dicMessages.Add(messageType, null);
        }
        dicMessages[messageType] += handler;
    }


    public static void RemoveMsgListener(int messageType, DelMessageDelivery handler)
    {
        if (dicMessages.ContainsKey(messageType))
        {
            dicMessages[messageType] -= handler;
        }
    }

    public static void RemoveMsgListener(int messageType)
    {

        if (dicMessages.ContainsKey(messageType))
        {
            dicMessages[messageType] = null;
            dicMessages.Remove(messageType);
        }
    }


  
    public static void RemoveAllMsgListener()
    {
        dicMessages.Clear();
    }


    public static void SendMessage(int messageType, KeyValuesUpdate kv)
    {
        DelMessageDelivery del;
        if (dicMessages.TryGetValue(messageType, out del))
        {
            if (del != null)
            {
                del(kv);
            }
        }
    }

    public static void SendMessage(int messageType, int msgName, object msgContent = null)
    {
        KeyValuesUpdate kvs = new KeyValuesUpdate(msgName, msgContent);
        MessageCenter.SendMessage(messageType, kvs);
    }
}//Class_end


public class KeyValuesUpdate
{
   
    private int _Key;
   
    private object _Values;

    
    public int Key
    {
        get { return _Key; }
    }
    public object Values
    {
        get { return _Values; }
    }

    public KeyValuesUpdate(int key, object Values)
    {
        _Key = key;
        _Values = Values;
    }
}//Class_end
