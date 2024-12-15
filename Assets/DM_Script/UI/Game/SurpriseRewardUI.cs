/*
 * Created on 2024
 *
 * Copyright (c) 2024 dotmobstudio
 * Support : dotmobstudio@gmail.com
 */
using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gley.Localization;

public class LuckyReward
{
    public int gold = 0;
    public int probability = 0;
}

public class SurpriseRewardUI : UIBase
{
    public static SurpriseRewardUI Create()
    {
        SurpriseRewardUI surpriseRewardUI = new SurpriseRewardUI();
        surpriseRewardUI.Init();
        return surpriseRewardUI;
    }

    private LuckyReward[] luckyRewards;
    public void Init()
    {
        this.CurrentUIType.UIForms_Type = UIFormsType.PopUp;
        this.CurrentUIType.UIForms_ShowMode = UIFormsShowMode.PopUp;
        this.CreateAndAttachRoot("SurpriseRewardUI");
        Attach();
    }

    public void Attach()
    {
        ViewUtils.SetText(root, "Top/TitleText", API.GetText(WordIDs.LuckyReward_Title));
        ViewUtils.SetText(root, "HintText", API.GetText(WordIDs.LuckyReward_Hint));
        luckyRewards = JsonMapper.ToObject<LuckyReward[]>(Config.Instance.commonNode["rewardsProbability"].ToJson());
        for (int i = 1; i <= 3; i++)
        {
            ViewUtils.AddButtonClick(this.root, "Gift" + i, this.onClickBtnGift);
        }
    }

    public void onClickBtnGift()
    {
        int probability = 0;
        foreach (LuckyReward item in luckyRewards)
        {
            probability += item.probability;
        }
        int random = Random.Range(1, probability + 1);
        foreach (LuckyReward item in luckyRewards)
        {
            random -= item.probability;
            Debug.Log(random);
            if (random <= 0)
            {
                SaveModel.AddGold(item.gold);
                MessageCenter.SendMessage(MyMessageType.GAME_UI, MyMessage.REFRESH_RES, item.gold);
                Close();
                return;
            }
        }
    }
}
