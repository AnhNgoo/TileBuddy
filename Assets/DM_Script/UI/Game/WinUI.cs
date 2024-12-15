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

public class WinUI : UIBase
{
    private int starValue;
    private int timeValue;
    private int level;
    private int starAddGoldValue = 0;
    private int timeAddGoldValue = 0;
    private int totalAddGoldValue = 0;
    private float timer = 0;
    public static void Create(int star , int time)
    {
        WinUI ui = new WinUI();
        ui.Init(star , time);
    }

    void Init(int star , int time)
    {
        this.CurrentUIType.UIForms_Type = UIFormsType.PopUp;
        this.CurrentUIType.UIForms_ShowMode = UIFormsShowMode.PopUp;
        starValue = star;
        timeValue = time;
        timer = 0;
        CreateAndAttachRoot("WinUI");
        Attach();
        Refresh();
    }

    void Attach()
    {
        AudioManager.Instance.PlaySingle("Sound/win");
        level = SaveModel.GetPlayer().level - 1;
        starAddGoldValue = Mathf.FloorToInt(starValue / 3);
        timeAddGoldValue = Mathf.FloorToInt(timeValue / 6);
        totalAddGoldValue = starAddGoldValue + timeAddGoldValue;

        SaveModel.AddGold(totalAddGoldValue , false);
        ViewUtils.AddButtonClick(root, "ReStartBtn", OnClickReStartBtn);
        ViewUtils.AddButtonClick(root, "AdBtn", OnClickAdBtn);
        ViewUtils.AddButtonClick(root, "EvaluateBtn", OnClickEvaluateBtn);
        ViewUtils.AddButtonClick(root, "ShareBtn", OnClickShareBtn);

        ViewUtils.SetText(root, "TitleText", "YOU WIN！");
        ViewUtils.SetText(root, "CheckPointText", API.GetText(WordIDs.WinUI_LevelText) +" "+ level);

        ViewUtils.SetText(root, "StarBg/StarValue", starValue.ToString());
        ViewUtils.SetText(root, "StarBg/GoldValue", "+" + starAddGoldValue);

        ViewUtils.SetText(root, "TimeBg/TimeValue", timeValue + "s");
        ViewUtils.SetText(root, "TimeBg/GoldValue", "+" + timeAddGoldValue);
        ViewUtils.SetText(root, "AdBtn/Text", "" + totalAddGoldValue);
        ViewUtils.SetText(root, "ReStartBtn/Text", API.GetText(WordIDs.WinUI_Next));

    
        Dictionary<string, object> param = new Dictionary<string, object>();
        param["name"] = "level" + level;
        param["ispassed"] = "true";
        param["getstars"] = starValue.ToString();
        param["lefttime"] = timeValue.ToString();
        param["getcoins"] = totalAddGoldValue.ToString();

        ShowLuckyRewards();
        //ShowEvaluate();
        
    }

    void OnUpdate() {
        timer += Time.deltaTime;
    }

    void Refresh()
    {

    }

    void ShowEvaluate()
    {
        if (level == Const.EVALUETE_LEVEL)
        {
            SDKInterface.Instance.Evaluate();
        }
    }

    void OnClickReStartBtn() {

        if(level%2 == 0)//Level chan
        {
            Gley.MobileAds.API.ShowInterstitial();
        }
        
        Close();
        GameUI.Create();

        Dictionary<string, object> param = new Dictionary<string, object>();
        param["action"] = "close";
        param["time"] = "" + (int)timer;
    }

    void OnClickAdBtn() {
       Gley.MobileAds.API.ShowRewardedVideo((s) =>
        {
            SaveModel.AddGold(totalAddGoldValue);
            MessageCenter.SendMessage(MyMessageType.GAME_UI, MyMessage.REFRESH_RES, totalAddGoldValue);
            ViewUtils.SetActive(root, "AdBtn", false);
        });

        Dictionary<string, object> param = new Dictionary<string, object>();
        param["action"] = "whatad";
        param["time"] = "" + (int)timer;
    
    }

    //TODO: Rate
    void OnClickEvaluateBtn()
    {
        
    }

  
    //TODO : Share
    void OnClickShareBtn() {
     
    }

    void ShowLuckyRewards()
    {
//        Debug.Log("ShowLuckyRewards");
        int luckyRewardsGameNum = Config.Instance.commonNode.GetInt("luckyRewardsGameNum");
        if (GameManager.Instance.gameNum >= luckyRewardsGameNum)
        {
            GameManager.Instance.gameNum = 0;
            SurpriseRewardUI.Create();
        }
    }
}