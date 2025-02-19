﻿/*
 * Created on 2024
 *
 * Copyright (c) 2024 dotmobstudio
 * Support : dotmobstudio@gmail.com
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Gley.Localization;

public class PauseUI : UIBase
{
    private UnityAction<string> callBack;
    public static void Create(UnityAction<string> cb)
    {
        PauseUI ui = new PauseUI();
        ui.Init(cb);
        
    }

    void Init(UnityAction<string> cb)
    {
        this.CurrentUIType.UIForms_Type = UIFormsType.PopUp;
        this.CurrentUIType.UIForms_ShowMode = UIFormsShowMode.PopUp;
        callBack = cb;
        Redisplay();
    }

    public override void Redisplay()
    {
        CreateAndAttachRoot("PauseUI");
        Attach();
        Refresh();
    }

    void Attach()
    {
        ViewUtils.SetText(root, "TitleText", API.GetText(WordIDs.PauseUI_TitleText));
        ViewUtils.SetText(root, "Sound/Text", API.GetText(WordIDs.PauseUI_SoundText));
        ViewUtils.SetText(root, "Music/Text", API.GetText(WordIDs.PauseUI_MusicText));
        ViewUtils.SetText(root, "ContinueBtn/Text", API.GetText(WordIDs.PauseUI_Continue));
        ViewUtils.SetText(root, "HomeBtn/Text", API.GetText(WordIDs.PauseUI_Home));
        ViewUtils.SetText(root, "ReStarBtn/Text", API.GetText(WordIDs.PauseUI_Restart));
        ViewUtils.AddButtonClick(root, "ContinueBtn", OnClickContinueBtn);
        ViewUtils.AddButtonClick(root, "HomeBtn", OnClickHomeBtn);
        ViewUtils.AddButtonClick(root, "ReStarBtn", OnClickReStartBtn);
        ViewUtils.AddButtonClick(root, "Sound/SwitchBtn", OnClickSoundBtn);
        ViewUtils.AddButtonClick(root, "Music/SwitchBtn", OnClickMusicBtn);
    }

    void Refresh()
    {
        RefreshMusicSwitch();
        RefreshSoundSwitch();
    }

    void OnClickContinueBtn() 
    {
        Close();
        callBack(GameModel.BACK_GAME_CONTIUE);
    }

    void OnClickHomeBtn()
    {
        Close();
        callBack(GameModel.BACK_GAME_CLOSE);
    }

    void OnClickReStartBtn()
    {
        Close();
        callBack(GameModel.BACK_GAME_RESTART);
    }

    void OnClickSoundBtn()
    {
        SaveModel.SoundSwith = !SaveModel.SoundSwith;
        RefreshSoundSwitch();
    }
    void OnClickMusicBtn()
    {
        SaveModel.MusicSwith = !SaveModel.MusicSwith;
        AudioManager.Instance.SetMusicOn(SaveModel.MusicSwith);
        RefreshMusicSwitch();
    }

    void RefreshMusicSwitch()
    {
        ViewUtils.SetActive(root, "Music/SwitchBtn/OnImage", SaveModel.MusicSwith);
        ViewUtils.SetActive(root, "Music/SwitchBtn/OffImage", !SaveModel.MusicSwith);
    }
    void RefreshSoundSwitch()
    {
        ViewUtils.SetActive(root, "Sound/SwitchBtn/OnImage", SaveModel.SoundSwith);
        ViewUtils.SetActive(root, "Sound/SwitchBtn/OffImage", !SaveModel.SoundSwith);
    }
}