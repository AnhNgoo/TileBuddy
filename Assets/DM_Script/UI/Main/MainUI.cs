/*
 * Created on 2024
 *
 * Copyright (c) 2024 dotmobstudio
 * Support : dotmobstudio@gmail.com
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Gley.Localization;
using System;

public class MainUI : UIBase
{
    private Text goldText;
    public static void Create()
    {
        MainUI ui = new MainUI();
        ui.Init();
    }

    void Init()
    {
        Redisplay();

     

        if (PlayerPrefs.GetInt("isRemoveAds", 0) == 1)
        {
            ViewUtils.SetActive(root, "NoadBtn", false);
        }
        else
        {
            ViewUtils.SetActive(root, "NoadBtn", true);
        }
    }


    public override void Redisplay()
    {
        CreateAndAttachRoot("MainUI");
        Attach();
        Refresh();
    }


  

    void Attach()
    {
        root.FindAChild<Button>("SetBtn");
        ViewUtils.AddButtonClick(root, "SetBtn", OnClickSetting);
        ViewUtils.AddButtonClick(root, "ClearBtn", OnClickClear);
        ViewUtils.AddButtonClick(root, "PlayBtn", OnClickPlay);
        ViewUtils.AddButtonClick(root, "GalleryBtn", OnClickGallery);
        ViewUtils.AddButtonClick(root, "NoadBtn", OnClickNoAdBtn);
        ViewUtils.AddButtonClick(root, "HelpBtn", OnClickHelp);
        ViewUtils.AddButtonClick(root, "ShopBtn", OnClickShop);
        ViewUtils.SetText(root, "GalleryBtn/Text", API.GetText(WordIDs.MainUI_GalleryBtn_text));
        ViewUtils.SetText(root, "NoadBtn/Text", API.GetText(WordIDs.MainUI_NoadBtn_text));
        ViewUtils.SetText(root, "HelpBtn/Text", API.GetText(WordIDs.MainUI_HelpBtn_text));
        ViewUtils.SetText(root, "PlayBtn/Text", API.GetText(WordIDs.MainUI_PlayBtn_id));
        ViewUtils.SetText(root, "ShopBtn/Text", API.GetText(WordIDs.MainUI_Shopbtn));
        ViewUtils.SetActive(root, "ClearBtn", false);
      
#if UNITY_EDITOR
        ViewUtils.SetActive(root, "ClearBtn", true);
#endif
        goldText = root.FindAChild<Text>("TopArea/Gold/Text");
    }

    private void OnClickShop()
    {
        GoldNotEnoughUI.Create();
    }

    void Refresh()
    {
        goldText.text = SaveModel.player.gold.ToString();

//        Debug.Log("COIN TEXT IN MAIN :" + goldText.text.ToString());
    }

    void OnClickSetting() {
        SettingUI.Create();
    }

    void OnClickPlay() {
        if (SaveModel.GetPlayer().guide)
        {
            GameUI.Create();
        }
        else
        {
            NoviceGuideUI.Create();
        }
    }
    void OnClickGallery()
    {
        GalleryUI.Create();

        
    }
    void OnClickNoAdBtn()
    {
        
        FBstatistics.Instance.OnClickRemoveAds();
        
         //ViewUtils.SetActive(root, "NoadBtn", false);
        
        
    }
    void OnClickHelp()
    {
      //  FBstatistics.LogEvent("clickhelp");
        IntroductionUI.Create();
    }
    void OnClickClear()
    {
        SaveModel.CreateSave();
    }
}
