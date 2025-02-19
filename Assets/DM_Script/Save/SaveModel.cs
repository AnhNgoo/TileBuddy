﻿/*
 * Created on 2024
 *
 * Copyright (c) 2024 dotmobstudio
 * Support : dotmobstudio@gmail.com
 */
using LitJson;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
public class SaveModel
{
    public static Save player;
    static readonly string saveKey = "playerPaintWorld";
    static readonly string saveFileName = saveKey + ".json";
    static readonly string saveFileDir = Application.persistentDataPath + "/Save";
    static readonly string saveFilePath = saveFileDir + "/" + saveFileName;
    public static readonly int version = 1;

    public static Save GetPlayer() {
        if (SaveModel.player != null)
        {
            return SaveModel.player;
        }
        CreateSave();
        return SaveModel.player;
    }

    public static void AddGold(int value , bool showAnime = true)
    {
        SaveModel.player.gold += value;
        if (SaveModel.player.gold <= 0)
        {
            SaveModel.player.gold = 0;
        }
        if (showAnime)
        {
            GetGoldUI.Create(value);
        }
        ForceStorageSave();
    }

    public static void UseGold(int value) 
    {
        SaveModel.player.gold -= value;
        if (SaveModel.player.gold <= 0)
        {
            
          //  SaveModel.player.gold = 0;
        }
        ForceStorageSave();
    }

    public static bool CheckGold(int value, bool needTip = true)
    {
        if (SaveModel.player.gold >= value)
        {
            return true;
        }
        if (needTip)
        {
            GoldNotEnoughUI.Create();
            //Debug.Log("Not Enough Coins");
        }
        return false;
    }

    public static bool SoundSwith
    {
        get { return SaveModel.player.soundSwitch;}

        set { 
            SaveModel.player.soundSwitch = value;
            SaveModel.ForceStorageSave();
        }
    }

    public static bool MusicSwith
    {
         get { return SaveModel.player.musicSwitch;}

        set { 
            SaveModel.player.musicSwitch = value;
            SaveModel.ForceStorageSave();
        }
    }

    public static bool NotifySwitch
    {
         get { return SaveModel.player.notifySwitch;}

        set { SaveModel.player.notifySwitch = value; }
    }
    

    public static void LevelUp() {
        SaveModel.player.level += 1;
        ForceStorageSave();
    }

    static void CheckSave()
    {
        int[] defaultGallery = JsonMapper.ToObject<int[]>(Config.Instance.commonNode.GetNode("defaultGallery").ToJson());
        int minGalleryCount = Config.Instance.commonNode.GetInt("minGalleryCount");
        foreach (int id in defaultGallery)
        {
            if (SaveModel.player.galleryIds.IndexOf(id) < 0)
            {
                SaveModel.player.galleryIds.Add(id);
            }
            if (SaveModel.player.currentGalleryIds.Count < minGalleryCount && SaveModel.player.currentGalleryIds.IndexOf(id) < 0)
            {
                SaveModel.player.currentGalleryIds.Add(id);
            }
        }
        GalleryModel.alreadyGalleryData = Config.Instance.GetGalleryDataByIds(SaveModel.player.galleryIds);
        GalleryModel.currentGalleryData = Config.Instance.GetGalleryDataByIds(SaveModel.player.currentGalleryIds);
        ForceStorageSave();
    }

    public static void CreateSave()
    {
        Save player = new Save();
        player.name = ViewUtils.GenerateUUID();
        SaveModel.player = player;
        CheckSave();
    }
    /// <summary>
    /// 
    /// </summary>
    public static void DeSerialize() {
        string user = LocalSave.LoadUser();
        //user = "";
//        Debug.Log(user);
        if (string.IsNullOrEmpty(user)) {
            SaveModel.player = GetPlayer();
            ForceStorageSave();
        }
        else {
            //StreamReader sr = new StreamReader(saveFilePath);
            //string saveJsonStr = sr.ReadToEnd();
            //sr.Close();
            SaveModel.player = JsonMapper.ToObject<Save>(user);
            CheckSave();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public static void ForceStorageSave()
    {
        //string saveJsonStr = JsonMapper.ToJson(SaveModel.player);
        //StreamWriter sw = new StreamWriter(saveFilePath);
        //sw.Write(saveJsonStr);
        //sw.Close();
        LocalSave.SaveUser(JsonMapper.ToJson(SaveModel.player));
    }

    public static void ResetItemList(List<List<Item>> itemList)
    {
        player.currentLevel.itemTypeList.Clear();
        if (itemList != null)
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                List<Item> items = itemList[i];
                for (int j = 0; j < items.Count; j++)
                {
                    Item item = items[j];
                    player.currentLevel.itemTypeList.Add(item.hasItem ? item.itemType : -1);
                    if (item.isBomb)
                    {
                        player.currentLevel.bobmPos = player.currentLevel.itemTypeList.Count - 1;
                        player.currentLevel.bobmTime = Mathf.FloorToInt(item.nowSec);
                    }
                }
            }
        }
        ForceStorageSave();
    }
    public static void ClearCurrentLevel()
    {
        player.currentLevel = new CurrentLevel();

        ForceStorageSave();
    }

    public static bool IsGalleryUnlock(int galleryId)
    {
        int index = player.galleryIds.FindIndex((int id) =>
        {
            return id == galleryId;
        });
        return index >= 0;
    }

    public static void RemoveAD()
    {
        SaveModel.player.removeAD = true;
        SaveModel.ForceStorageSave();
    }
}
