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

public class IntroductionUI : UIBase
{
    public static void Create()
    {
        IntroductionUI ui = new IntroductionUI();
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
        CreateAndAttachRoot("IntroductionUI");
        Attach();
        Refresh();
    }

    void Attach()
    {
        ViewUtils.AddButtonClick(root, "CloseBtn", Close);

        ViewUtils.SetText(root, "Top/TitleText", API.GetText(WordIDs.IntroductionUI_Title));
        ViewUtils.SetText(root, "Image1/PropHint1", API.GetText(WordIDs.IntroductionUI_PropHint1));
        ViewUtils.SetText(root, "Image2/PropHint2", API.GetText(WordIDs.IntroductionUI_PropHint2));
        ViewUtils.SetText(root, "Image3/PropHint3", API.GetText(WordIDs.IntroductionUI_PropHint3));
    }

    void Refresh()
    {

    }
}