﻿/*
 * Created on 2024
 *
 * Copyright (c) 2024 dotmobstudio
 * Support : dotmobstudio@gmail.com
 */
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class LineItem : MonoBehaviour
{
    protected GameObject lineU;
    protected GameObject lineD;
    protected GameObject lineL;
    protected GameObject lineR;
    protected Item item;

    void Awake()
    {

    }
    public void initLine(int index, List<Point> pointList, int height, Item item)
    {

        lineU = gameObject.transform.Find("LineU").gameObject;
        lineD = gameObject.transform.Find("LineD").gameObject;
        lineL = gameObject.transform.Find("LineL").gameObject;
        lineR = gameObject.transform.Find("LineR").gameObject;
        this.item = item;

        hideAllLine();
        Vector2 size = lineU.transform.GetComponent<RectTransform>().sizeDelta;
        float width = height / 2 + 10f;
        lineU.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(width, size.y);
        lineD.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(width, size.y);
        lineL.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(width, size.y);
        lineR.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(width, size.y);
        if (index != 0)
        {
            checkShowLine(pointList[index], pointList[index - 1]);
        }

        if (index != pointList.Count - 1)
        {
            checkShowLine(pointList[index], pointList[index + 1]);
        }
    }

    protected void hideAllLine()
    {
        lineU.SetActive(false);
        lineD.SetActive(false);
        lineL.SetActive(false);
        lineR.SetActive(false);
    }

    private void checkShowLine(Point now, Point next)
    {
        if (now.x > next.x)
        {
            gameObject.transform.Find("LineU").gameObject.SetActive(true);
        }
        if (now.x < next.x)
        {
            gameObject.transform.Find("LineD").gameObject.SetActive(true);
        }
        if (now.y > next.y)
        {
            gameObject.transform.Find("LineL").gameObject.SetActive(true);
        }
        if (now.y < next.y)
        {
            gameObject.transform.Find("LineR").gameObject.SetActive(true);
        }
    }

    public void DestroyThis()
    {
        Destroy(this.gameObject);
    }
}

