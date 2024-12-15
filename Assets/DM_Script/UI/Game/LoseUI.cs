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

public class LoseUI : UIBase
{
    private int addGoldValue = 20;
    public static void Create()
    {
        LoseUI ui = new LoseUI();
        ui.Init();
    }

    void Init()
    {
        this.CurrentUIType.UIForms_Type = UIFormsType.PopUp;
        this.CurrentUIType.UIForms_ShowMode = UIFormsShowMode.PopUp;
        Redisplay();
    }

    public override void Redisplay()
    {
        CreateAndAttachRoot("LoseUI");
        Attach();
        Refresh();
    }

    void Attach()
    {
        ViewUtils.AddButtonClick(root, "ReStartBtn", OnClickReStartBtn);
        ViewUtils.AddButtonClick(root, "AdBtn", OnClickAdBtn);
        ViewUtils.AddButtonClick(root, "EvaluateBtn", OnClickEvaluateBtn);
        ViewUtils.AddButtonClick(root, "NoAdBtn", OnClickNoAdBtn);
        ViewUtils.AddButtonClick(root, "ShareBtn", OnClickShareBtn);

        
        ViewUtils.SetText(root, "AdBtn/Text", addGoldValue.ToString());
        ViewUtils.SetText(root, "ReStartBtn/Text", API.GetText(WordIDs.LoseUI_Restart));
        ViewUtils.SetText(root, "TitleText", "YOU LOSE!");
        ViewUtils.SetText(root, "CheckPointText", API.GetText(WordIDs.LoseUI_LevelText) +" "+ SaveModel.player.level);
        ViewUtils.SetText(root, "TimeOutText", API.GetText(WordIDs.LoseUI_TimeOutText));

        Dictionary<string,object> param = new Dictionary<string,object>();
        param["name"] = "level" + SaveModel.player.level;
        param["ispassed"] = "false";
        param["getstars"] = "";
        param["lefttime"] = "";
        param["getcoins"] = "";

//        FBstatistics.LogEvent("gamelevel", param);
    }

    void Refresh()
    {

    }

    void OnClickReStartBtn() 
    {
        Close();
        GameUI.Create();
      
    }

    void OnClickAdBtn() 
    {
        Gley.MobileAds.API.ShowRewardedVideo((s) =>
        {
            SaveModel.AddGold(addGoldValue);
            MessageCenter.SendMessage(MyMessageType.GAME_UI, MyMessage.REFRESH_RES, addGoldValue);
            ViewUtils.SetActive(root, "AdBtn", false);
        });
      
    }

    void OnClickEvaluateBtn()
    {
        SDKInterface.Instance.Evaluate();
    }

    void OnClickNoAdBtn()
    {
       // FBstatistics.LogEvent("clickremovead");
    }

    void OnClickShareBtn()
    {
        SDKInterface.Instance.Share();
    }
}