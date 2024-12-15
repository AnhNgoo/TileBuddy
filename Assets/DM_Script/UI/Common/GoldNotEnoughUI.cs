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
using System;
using UnityEngine.UI;
using Gley.EasyIAP;

public class GoldNotEnoughUI : UIBase
{
    private Text goldText;
    public static GoldNotEnoughUI Create()
    {
        GoldNotEnoughUI goldNotEnoughUI = new GoldNotEnoughUI();
        goldNotEnoughUI.Init();
        return goldNotEnoughUI;
    }

    private int gold;
    public void Init()
    {
        this.CurrentUIType.UIForms_Type = UIFormsType.PopUp;
        this.CurrentUIType.UIForms_ShowMode = UIFormsShowMode.PopUp;
        //this.CreateAndAttachRoot("GoldNotEnoughUI");
        //Attach();
        //GetListPrice();
        Redisplay();
    }

    public override void Redisplay()
    {
        CreateAndAttachRoot("GoldNotEnoughUI");
        Attach();
        Refresh();
    }

    public void Attach()
    {
        ViewUtils.AddButtonClick(this.root, "CloseBtn", Close);
        ViewUtils.AddButtonClick(this.root, "AdBtn", this.OnClickADBtn);
        ViewUtils.AddButtonClick(this.root, "IAP_Product/IAP_1/Buy", OnClickIAP_1);
        ViewUtils.AddButtonClick(this.root, "IAP_Product/IAP_2/Buy", OnClickIAP_2);
        ViewUtils.AddButtonClick(this.root, "IAP_Product/IAP_3/Buy", OnClickIAP_3);
        ViewUtils.AddButtonClick(this.root, "IAP_Product/IAP_4/Buy", OnClickIAP_4);
        ViewUtils.AddButtonClick(this.root, "IAP_Product/IAP_5/Buy", OnClickIAP_5);
        ViewUtils.AddButtonClick(this.root, "IAP_Product/Restore", OnClickRestore);

        gold = Config.Instance.commonNode.GetInt("adGold");
        ViewUtils.SetText(root, "AdBtn/Text", "+" + gold);
        ViewUtils.SetText(root, "Top/Image/TitleText", Gley.Localization.API.GetText(WordIDs.GoldNotEnoughUI_Title));
        ViewUtils.SetText(root, "HintText", Gley.Localization.API.GetText(WordIDs.GoldNotEnoughUI_Hint));

        goldText = root.FindAChild<Text>("Gold/Text");


#if GLEY_IAP_IOS
		ViewUtils.SetActive(root, "IAP_Product/Restore", true);
#else
        ViewUtils.SetActive(root, "IAP_Product/Restore", false);
#endif

        GetListPrice();
    }

    private void OnClickRestore()
    {
        FBstatistics.Instance.Restore();
    }

    private void GetListPrice()
    {
        //Debug.Log("Chay vao day ne");

        ViewUtils.SetText(root, "IAP_Product/IAP_1/Buy/Text", Gley.EasyIAP.API.GetLocalizedPriceString(ShopProductNames.Coins1));
        ViewUtils.SetText(root, "IAP_Product/IAP_2/Buy/Text", Gley.EasyIAP.API.GetLocalizedPriceString(ShopProductNames.Coins2));
        ViewUtils.SetText(root, "IAP_Product/IAP_3/Buy/Text", Gley.EasyIAP.API.GetLocalizedPriceString(ShopProductNames.Coins3));
        ViewUtils.SetText(root, "IAP_Product/IAP_4/Buy/Text", Gley.EasyIAP.API.GetLocalizedPriceString(ShopProductNames.Coins4));
        ViewUtils.SetText(root, "IAP_Product/IAP_5/Buy/Text", Gley.EasyIAP.API.GetLocalizedPriceString(ShopProductNames.Coins5));
    }


        void Refresh()
    {
        goldText.text = SaveModel.player.gold.ToString();

      //  Debug.Log("COIN TEXT IN MAIN :" + goldText.text.ToString());
    }

    private void OnClickIAP_2()
    {
        Debug.Log("ON CLICK IAP_2");
        FBstatistics.Instance.MakeBuyProduct(2);
        Close();
    }

    private void OnClickIAP_1()
    {
        Debug.Log("ON CLICK IAP_1");
        FBstatistics.Instance.MakeBuyProduct(1);
        Close();
    }

    private void OnClickIAP_3()
    {
        Debug.Log("ON CLICK IAP_3");
        FBstatistics.Instance.MakeBuyProduct(3);
        Close();
    }

    private void OnClickIAP_4()
    {
        Debug.Log("ON CLICK IAP_4");
        FBstatistics.Instance.MakeBuyProduct(4);
        Close();
    }

    private void OnClickIAP_5()
    {
        Debug.Log("ON CLICK IAP_5");
        FBstatistics.Instance.MakeBuyProduct(5);
        Close();
    }

    //public override void Close()
    //{
    //    Debug.Log("CLOSE AAAA");
    //    //Destroy();
    //}

    private void OnClickADBtn()
    {
        Gley.MobileAds.API.ShowRewardedVideo((s) =>
        {
            SaveModel.AddGold(gold);
            MessageCenter.SendMessage(MyMessageType.GAME_UI, MyMessage.REFRESH_RES, gold);
            Close();
        });
    }
}
