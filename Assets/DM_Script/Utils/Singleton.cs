/*
 * Created on 2024
 *
 * Copyright (c) 2024 dotmobstudio
 * Support : dotmobstudio@gmail.com
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AllSceneSingletonSet
{
    public static List<GameObject> _ddolObjects = new List<GameObject>();

    public static void DestroyAll()
    {
        for (int i = _ddolObjects.Count - 1; i >= 0; i--)
            GameObject.Destroy(_ddolObjects[i]);

        _ddolObjects.Clear();
    }
}

public abstract class Singleton<T> where T : new()
{
    private static T _instance;
    static object _lock = new object();

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                        _instance = new T();
                }
            }
            return _instance;
        }
    }

    public static void Destory()
    {
        _instance = default(T);
    }
}

public class UnitySingleton<T> : MonoBehaviour
    where T : Component
{
    protected static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(T)) as T;
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    // obj.hideFlags = HideFlags.DontSave;
                    obj.hideFlags = HideFlags.HideAndDontSave;
                    _instance = (T)obj.AddComponent(typeof(T));
                }
            }
            return _instance;
        }
    }
    public virtual void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        AllSceneSingletonSet._ddolObjects.Add(this.gameObject);

        if (_instance == null)
            _instance = this as T;
        else
            Destroy(gameObject);
    }

    protected virtual void OnDestroy()
    {
        _instance = null;
    }
}

public class UnitySceneSingleton<T> : MonoBehaviour
    where T : Component
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(T)) as T;
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    //obj.hideFlags = HideFlags.HideAndDontSave;
                    _instance = (T)obj.AddComponent(typeof(T));
                    obj.name = _instance.GetType().ToString();

                }
            }
            return _instance;
        }
    }

    public virtual void Awake()
    {
        if (_instance == null)
            _instance = this as T;
        else
            Destroy(gameObject);
    }

    public virtual void OnDestroy()
    {
        _instance = null;
    }
}
