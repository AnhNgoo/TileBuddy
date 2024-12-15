/*
 * Created on 2024
 *
 * Copyright (c) 2024 dotmobstudio
 * Support : dotmobstudio@gmail.com
 */
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;


public class ResourcesMgr : MonoBehaviour
{
    
    private static ResourcesMgr _Instance;              
    private Hashtable ht = null;                       


    public static ResourcesMgr GetInstance()
    {
        if (_Instance == null)
        {
            _Instance = new GameObject("_ResourceMgr").AddComponent<ResourcesMgr>();
        }
        return _Instance;
    }

    void Awake()
    {
        ht = new Hashtable();
    }
 
    public T LoadResource<T>(string path, bool isCatch) where T : UnityEngine.Object
    {
        if (ht.Contains(path))
        {
            return ht[path] as T;
        }

        T TResource = Resources.Load<T>(path);
        if (TResource == null)
        {
            Debug.LogError(GetType() + "/GetInstance()/TResource 提取的资源找不到，请检查。 path=" + path);
        }
        else if (isCatch)
        {
            ht.Add(path, TResource);
        }

        return TResource;
    }


    public GameObject LoadAsset(string path, bool isCatch)
    {
        GameObject goObj = LoadResource<GameObject>(path, isCatch);
        GameObject goObjClone = GameObject.Instantiate<GameObject>(goObj);
        if (goObjClone == null)
        {
            Debug.LogError(GetType() + "/LoadAsset()/克隆资源不成功，请检查。 path=" + path);
        }
        //goObj = null;//??????????
        return goObjClone;
    }       
}//Class_end