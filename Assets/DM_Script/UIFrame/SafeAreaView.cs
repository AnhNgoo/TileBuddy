/*
 * Created on 2024
 *
 * Copyright (c) 2024 dotmobstudio
 * Support : dotmobstudio@gmail.com
 */
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;

public class SafeAreaView : MonoBehaviour
{

    public RectTransform[] areas;
    public RectTransform[] maskAreas;

    
    public float matchWidth = 1080f;    
    public float matchHeight = 2160f;   

    public float minRatio = 9f / 18f;   
    public float maxRatio = 9f / 16f;  
    public float maxShowWidthRatio = 2.2f;   

    public float scale = 1;   

    private int lastWidth = 0;
    private int lastHeight = 0;

    private void Awake()
    {
        lastWidth = Screen.width;
        lastHeight = Screen.height;
        ResetSafeArea();
    }

    private void Update()
    {
        if (lastWidth != Screen.width || lastHeight != Screen.height)
        {
            Debug.Log("Screen resolution changes to: " + Screen.width + "x" + Screen.height);
            ResetSafeArea();
        }
        lastWidth = Screen.width;
        lastHeight = Screen.height;
    }

    private Rect GetSafeArea()
    {
        float x, y, w, h;
        x = 0;
        y = 0;
        w = Screen.width;
        h = Screen.height;
        return new Rect(x, y, w, h);
    }

    private void ResetSafeArea()
    {
        if (areas == null || areas.Length == 0)
        {
            return;
        }
        //Debug.LogFormat("MinRatio : {0}, maxRatio: {1}", minRatio, maxRatio);

     
        float screenRatio = (float)Screen.width / Screen.height;

      
        Rect safeArea = GetSafeArea();
#if UNITY_EDITOR
        // iPhoneX
        if (Screen.width == 1624 && Screen.height == 750)
        {
            safeArea.xMin = 88;
            safeArea.xMax = Screen.width - 68;
        }
#endif
        
        //safeArea.xMin = 100f;
        //safeArea.xMax = Screen.width - 200;

        
        scale = safeArea.width / Screen.width;   

        
        float height = matchWidth / screenRatio;
       
        height /= scale;

        float minHeight = matchWidth / maxRatio;    
        float maxHeight = matchWidth / minRatio;    
                                                    // Debug.LogFormat("MinHeight: {0}, MaxHeight: {1}, CurHeight: {2}", minHeight, maxHeight, height);
        if (height < minHeight)
        {
        
            scale *= height / minHeight;
            height = minHeight;
        }
        else if (height > maxHeight)
        {
           
            height = maxHeight;
        }

        
        float left = safeArea.xMin;
        float right = Screen.width - safeArea.xMax;
        Vector2 center = new Vector2((left - right) / Screen.width * matchWidth * 0.5f, 0);

        //Debug.LogFormat("Final wdith: {0}, height: {1}, scale: {2}, ratio: {3}", matchWidth, height, scale, matchWidth / height);
        Vector2 areaSize = new Vector2(matchWidth, height);
        Vector3 areaScale = new Vector3(scale, scale, 1);

        foreach (RectTransform area in areas)
        {
            area.anchorMin = new Vector2(0.5f, 0.5f);
            area.anchorMax = new Vector2(0.5f, 0.5f);

            area.sizeDelta = areaSize;
            area.localScale = areaScale;
            area.anchoredPosition = center;
        }

        StartCoroutine(ResetMaskArea(center, areaSize, areaScale));
    }

    private IEnumerator ResetMaskArea(Vector2 areaPos, Vector2 areaSize, Vector3 areaScale)
    {
        if (maskAreas == null || maskAreas.Length == 0)
        {
            yield break;
        }
        yield return new WaitForEndOfFrame();

        areaSize.x *= areaScale.x;
        areaSize.y *= areaScale.y;

        foreach (RectTransform maskArea in maskAreas)
        {
            Vector2 maskSize = maskArea.rect.size;

#if !UNITY_IOS
            float left = (maskSize.x - areaSize.y * maxShowWidthRatio + areaPos.x) * 0.5f;
            float right = (maskSize.x - areaSize.y * maxShowWidthRatio - areaPos.x) * 0.5f;
            RectTransform leftMask = maskArea.Find("Left") as RectTransform;
            RectTransform rightMask = maskArea.Find("Right") as RectTransform;
            SetWidth(leftMask, left);
            SetWidth(rightMask, right);
#endif

            float top = (maskSize.y - areaSize.y + areaPos.y) * 0.5f;
            float bottom = (maskSize.y - areaSize.y - areaPos.y) * 0.5f;

            RectTransform topMask = maskArea.Find("Top") as RectTransform;
            RectTransform bottomMask = maskArea.Find("Bottom") as RectTransform;

            SetHeight(topMask, top);
            SetHeight(bottomMask, bottom);
        }
    }

    private void SetWidth(RectTransform transform, float width)
    {
        if (width < 0)
            width = 0;
        Vector2 size = transform.sizeDelta;
        size.x = width;
        transform.sizeDelta = size;
    }

    private void SetHeight(RectTransform transform, float height)
    {
        Vector2 size = transform.sizeDelta;
        size.y = height;
        transform.sizeDelta = size;
    }

}
