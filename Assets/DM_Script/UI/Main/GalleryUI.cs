﻿/*
 * Created on 2024
 *
 * Copyright (c) 2024 dotmobstudio
 * Support : dotmobstudio@gmail.com
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Gley.Localization ;
using UnityEngine.UI;
 class GalleryUI: UIBase{
     private Text goldText;
     
     public static void Create()
     {
         GalleryUI ui = new GalleryUI();
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
         CreateAndAttachRoot("GalleryUI");
         Attach();
         Refresh();
     }
     
     void Attach()
     {
         ViewUtils.AddButtonClick(root, "CloseBtn", CloseThemes);
         ViewUtils.SetText(root, "HintText", API.GetText(WordIDs.GalleryUI_Hint));
        ViewUtils.SetText(root, "TitleBg/TitleText", API.GetText(WordIDs.GalleryUI_Title));
        goldText = root.FindAChild<Text>("Gold/Text");
         InitItems();
     }



    private void CloseThemes()
    {
        //Destroy();
        base.Destroy();
        Debug.Log("CLOSE THEMES");
    }

     void InitItems(){
         Transform content = root.FindAChild("Content");
         for (int i = 0; i < GalleryModel.galleryData.Length; i++)
         {
             GalleryData gd = GalleryModel.galleryData[i];
             GameObject item = ViewUtils.CreatePrefabAndSetParent(content, "GalleryItem");
             InitSingleItem(item.transform, gd);
         }
     }

     void InitSingleItem(Transform item, GalleryData gd)
     {
        string imageName = gd.imgName;
        string name = gd.name;

        ViewUtils.SetText(item.transform, "Title", name);
        for (int j = 1; j <= 6; j++)
        {
            ViewUtils.SetImage(item.transform, "ItemGroup/Item" + j + "/Bg/Image", "img_" + imageName + "_" + j);
            ViewUtils.SetActive(item.transform, "ItemGroup/Item" + j + "/Mask", !GalleryModel.HaveThisGallery(gd.id));
        }

        bool payUnlock = gd.unlockType == Const.UNLOCK_TYPE_PAY;
        ViewUtils.SetActive(item.transform, "Choose", GalleryModel.GalleryUsed(gd.id));
        ViewUtils.SetActive(item.transform, "Unlock",GalleryModel.HaveThisGallery(gd.id) && !GalleryModel.GalleryUsed(gd.id));
        ViewUtils.SetActive(item.transform, "ButtonGroup/UnlockBtn", GalleryModel.HaveThisGallery(gd.id));
        ViewUtils.SetActive(item.transform, "ButtonGroup/AdBtn", !GalleryModel.HaveThisGallery(gd.id) && !payUnlock);
        ViewUtils.SetActive(item.transform, "ButtonGroup/PayBtn", !GalleryModel.HaveThisGallery(gd.id) && payUnlock);
        ViewUtils.SetText(item.transform, "PayBtn/Value", gd.gold.ToString());
        if (!GalleryModel.HaveThisGallery(gd.id) && payUnlock)
        {
            if (SaveModel.player.gold < gd.gold)
            {
                item.transform.FindAChild<Text>("PayBtn/Value").color = Color.red;
            }
            else
            {
                item.transform.FindAChild<Text>("PayBtn/Value").color = Color.white;
            }
        }
             

        ViewUtils.AddButtonClick(item.transform, "AdBtn", delegate ()
        {
            OnClickAdBtn(item, gd);
        });
        ViewUtils.AddButtonClick(item.transform, "PayBtn", delegate()
        {
            OnClickPay(item, gd);
        });
        ViewUtils.AddButtonClick(item.transform, "", delegate()
        {
            OnClickItem(item, gd);
        });
             
     }
     
     void Refresh()
     {
         goldText.text = SaveModel.player.gold.ToString();
     }

     void OnClickItem(Transform item, GalleryData gd)
     {
         Debug.Log(GalleryModel.GalleryUsed(gd.id));
         if (GalleryModel.GalleryUsed(gd.id))
         {
             bool unload = GalleryModel.UnloadGallery(gd.id);
             if (!unload)
             {
                 HintUI.Create("At least 1 atlases need to be selected");
             }
         }
         else
         {
             Dictionary<string, object> param = new Dictionary<string, object>();
             param["name"] = gd.name;
             GalleryModel.UseGallery(gd.id);
         }
         InitSingleItem(item, gd);
     }

     void OnClickAdBtn(Transform item, GalleryData gd)
     {
         Gley.MobileAds.API.ShowRewardedVideo((s) =>
         {
             GalleryModel.UnlockGallery(gd.id);
             InitSingleItem(item, gd);
         });
     }

     void OnClickPay(Transform item, GalleryData gd)
     {
         if (!SaveModel.CheckGold(gd.gold))
         {
             return;
         }
         SaveModel.UseGold(gd.gold);
         GalleryModel.UnlockGallery(gd.id);
         InitSingleItem(item, gd);
     }
}

