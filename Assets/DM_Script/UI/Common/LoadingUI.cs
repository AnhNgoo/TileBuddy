﻿/*
 * Created on 2024
 *
 * Copyright (c) 2024 dotmobstudio
 * Support : dotmobstudio@gmail.com
 */
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using Gley.Localization;

class LoadingUI : MonoBehaviour
{
    const float TIPS_CHANGE_TIME = 2;
    static List<string> TIPS = new List<string>(){ "Tips：Match two more distant tile to get a higher score."
        , "Tips：Use shuffle button to rearrange tiles."
        , "Tips：In the Themes Pannel, you can close theimages you don’t like."};
    public Text progressLabel;
    public Transform loadingLabel;
    public Text tipsLabel;
    public Image progressImg;
    private float currentProgress = 0;
    private float targetProgress = 0;
    private float tipsTime = 0;
    void Awake()
    {
        StartLoad();
        ChangeTip();
        ViewUtils.SetText(loadingLabel, "", API.GetText(WordIDs.Loading_UI));
    }

    void Update()
    {
        tipsTime += Time.deltaTime;
        if (tipsTime >= TIPS_CHANGE_TIME)
        {
            tipsTime = 0;
            ChangeTip();
        }
    }

    public void StartLoad()
    {
        currentProgress = 0;
        targetProgress = 0;
        StartCoroutine("Loading");
    }

    public void ChangeTip()
    {
        int random = Random.Range(0, TIPS.Count);
        tipsLabel.text = TIPS[random];
    }

    IEnumerator Loading()
    {
        Debug.Log(currentProgress);
        while (currentProgress < 1f)
        {
            float r1 = Random.Range(0f, 0.05f);
            currentProgress += r1;
            float r2 = Random.Range(0f, 0.2f);
            OnProgress(currentProgress);
            yield return new WaitForSeconds(r2);
        }
    }

    void OnProgress(float progress)
    {
        progressImg.fillAmount = progress;
        int intProgress = Mathf.FloorToInt(progress * 100);
        if (intProgress > 100)
        {
            intProgress = 100;
        }
        progressLabel.text = intProgress + "%";
        if (progress >= 1)
        {
            SceneManager.LoadScene("Game");
        }
    }
}

