/*
 * Created on 2024
 *
 * Copyright (c) 2024 dotmobstudio
 * Support : dotmobstudio@gmail.com
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Gley.MobileAds;
public class GameManager : UnitySingleton<GameManager>
{
    bool init = false;
    public int gameNum = 0;
    public bool showInterstitial = false;
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        API.Initialize(OnInitialized);
    }

    public void Init()
    {
        if (init)
        {
            return;
        }
        init = true;
        Config.Instance.Init();
        SaveModel.DeSerialize();
        UIManager.GetInstance().ShowLobbyView();

        AudioManager.Instance.PlayBGM("Sound/BGM");
    }


    private void OnInitialized()
    {
        API.ShowBanner(BannerPosition.Bottom, BannerType.Adaptive);

        if (!API.GDPRConsentWasSet())
        {
            API.ShowBuiltInConsentPopup(PopupCloseds);
        }
    }

    private void PopupCloseds()
    {

    }


    void OnApplicationPause(bool pauseStatus)
    {
        //if (!pauseStatus)
        //{
        //    API.ShowAppOpen();
        //}
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject currentObject = EventSystem.current.currentSelectedGameObject;
            if (currentObject != null && currentObject.GetComponent<Button>() != null)
            {
                if (currentObject.GetComponent<Button>() != null)
                {
                    AudioManager.Instance.PlaySingle("Sound/click");
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
