/*
 * Created on 2024
 *
 * Copyright (c) 2024 dotmobstudio
 * Support : dotmobstudio@gmail.com
 */
using UnityEngine;
using LitJson;
using System.Collections.Generic;
using System;

public class IndexManager: Singleton<IndexManager>
{
   
    private JsonData _prefabIndexes;

    private JsonData _spriteIndexes;

    public IndexManager()
    {
        var txt = iResourceManager.Load<TextAsset>(SysDefine.SYS_PATH_SpriteConfigJson);
        _spriteIndexes = JsonMapper.ToObject(txt.text);
        txt = iResourceManager.Load<TextAsset>(SysDefine.SYS_PATH_UIFormConfigJson);
        _prefabIndexes = JsonMapper.ToObject(txt.text);
    }

    public string getSpritePath(string name, bool require = true)
    {
        if(name.Contains("/") || name.Contains("\\"))
        {
            return name;
        }
        return _spriteIndexes.GetString(name, require);
    }

    public string getPrefabPath(string name)
    {
        if (name.Contains("/") || name.Contains("\\"))
        {
            return name;
        }   
        return _prefabIndexes.GetString(name);
    }
}
