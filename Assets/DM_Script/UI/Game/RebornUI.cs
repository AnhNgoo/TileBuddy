/*
 * Created on 2024
 *
 * Copyright (c) 2024 dotmobstudio
 * Support : dotmobstudio@gmail.com
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Gley.Localization;

public class RebornUI : UIBase
{
    private UnityAction<string> callBack;
    private TextTimer textTimer;
    private Image heart;
    private bool startTiming;
    private int totalTime = 5;
    public static void Create(UnityAction<string> cb)
    {
        RebornUI ui = new RebornUI();
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
        CreateAndAttachRoot("RebornUI");
        Attach();
        Refresh();
    }

    void Attach()
    {
        ViewUtils.SetText(root, "top/TitleText", API.GetText(WordIDs.RebornUI_Title));
        ViewUtils.SetText(root, "HintText", API.GetText(WordIDs.RebornUI_Hint));//Time is up!
        ViewUtils.SetText(root, "CloseBtn/Text", API.GetText(WordIDs.RebortUI_Close));//No, Thank you!
        ViewUtils.SetText(root, "AdRebornBtn/Text", API.GetText(WordIDs.RebortUI_Ad));//Continue
        ViewUtils.AddButtonClick(root, "AdRebornBtn", OnClickAdBtn);
        ViewUtils.AddButtonClick(root, "CloseBtn", OnClickCloseBtn);
        heart = root.FindAChild<Image>("Heart/Image");
        textTimer = root.AddComponent<TextTimer>();
        textTimer.startTimingBySeconds(totalTime);
        textTimer.setCallback(OnClickCloseBtn);
        textTimer.setUpdataCallback(RefteshHeart);
        startTiming = true;
    }

    void RefteshHeart() {
        heart.fillAmount = textTimer.getTime()/10000 / totalTime;
    }

    void Refresh()
    {

    }

    void OnClickAdBtn() {
        Gley.MobileAds.API.ShowRewardedVideo((s) =>
        {
            Close();
            callBack(GameModel.BACK_GAME_ADDTIME);
        });
        
//        FBstatistics.LogEvent("watchad");
    }

    void OnClickCloseBtn() {
        Close();
        callBack(GameModel.BACK_GAME_FAIL);
    }
}