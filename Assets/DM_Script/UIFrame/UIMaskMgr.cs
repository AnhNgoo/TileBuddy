/*
 * Created on 2024
 *
 * Copyright (c) 2024 dotmobstudio
 * Support : dotmobstudio@gmail.com
 */
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class UIMaskMgr : MonoBehaviour
{

    private static UIMaskMgr _Instance;
  
    private GameObject _GoCanvasRoot = null;
    
    private Transform _CanTransformUIScripts = null;
   
    private GameObject _GoTopPlane;
 
    private GameObject _GoPopLayerMask;
  
    private GameObject _GoFixedLayerMask;
   
    private Camera _UICamear;
    
    private float _OriginalUICameraDepth;



    public static UIMaskMgr GetInstance()
    {
        if (_Instance == null)
        {
            _Instance = new GameObject("_UIMaskMgr").AddComponent<UIMaskMgr>();
        }
        return _Instance;
    }

    void Awake()
    {
       
        _GoCanvasRoot = GameObject.FindGameObjectWithTag(SysDefine.SYS_TAG_CANVAS);
        _CanTransformUIScripts = UnityHelper.FindTheChild(_GoCanvasRoot, SysDefine.SYS_CANVAS_UISCRIPTS_NODE_NAME);
        
        UnityHelper.AddChildToParent(_CanTransformUIScripts, this.gameObject.transform);

        
        _GoTopPlane = _GoCanvasRoot;
        _GoPopLayerMask = UnityHelper.FindTheChild(_GoCanvasRoot.gameObject, SysDefine.SYS_CANVAS_POPLAYERMASK_NODE_NAME).gameObject;
        _GoFixedLayerMask = UnityHelper.FindTheChild(_GoCanvasRoot.gameObject, SysDefine.SYS_CANVAS_FIXEDLAYERMASK_NODE_NAME).gameObject;

        
        _UICamear = GameObject.FindGameObjectWithTag(SysDefine.SYS_TAG_UICAMERA).GetComponent<Camera>();
        if (_UICamear != null)
        {
            _OriginalUICameraDepth = _UICamear.depth;
        }
        else
        {
            Debug.LogWarning(GetType() + "/Start()/_UICamera is Null ,please Check!");
        }
    }

   
    public void SetMaskWindow(GameObject goDisplayPlane, UIFormsLucencyType UILucencyType = UIFormsLucencyType.Lucency)
    {
       
        _GoTopPlane.transform.SetAsLastSibling();

        
        switch (UILucencyType)
        {
            case UIFormsLucencyType.Lucency:
                _GoPopLayerMask.SetActive(true);
                Color newColor1 = new Color(SysDefine.SYS_UIMASK_LUCENCY_COLOR_RGB, SysDefine.SYS_UIMASK_LUCENCY_COLOR_RGB, SysDefine.SYS_UIMASK_LUCENCY_COLOR_RGB, SysDefine.SYS_UIMASK_LUCENCY_COLOR_A);
                _GoPopLayerMask.GetComponent<Image>().color = newColor1;
                break;
            case UIFormsLucencyType.Translucence:
                _GoPopLayerMask.SetActive(true);
                Color newColor2 = new Color(SysDefine.SYS_UIMASK_TRANSLUCENCY_COLOR_RGB, SysDefine.SYS_UIMASK_TRANSLUCENCY_COLOR_RGB, SysDefine.SYS_UIMASK_TRANSLUCENCY_COLOR_RGB, SysDefine.SYS_UIMASK_TRANSLUCENCY_COLOR_A);
                _GoPopLayerMask.GetComponent<Image>().color = newColor2;
                break;
            case UIFormsLucencyType.Impenetrable:
                _GoPopLayerMask.SetActive(true);
                Color newColor3 = new Color(SysDefine.SYS_UIMASK_IMPENETRABLE_COLOR_RGB, SysDefine.SYS_UIMASK_IMPENETRABLE_COLOR_RGB, SysDefine.SYS_UIMASK_IMPENETRABLE_COLOR_RGB, SysDefine.SYS_UIMASK_IMPENETRABLE_COLOR_A);
                _GoPopLayerMask.GetComponent<Image>().color = newColor3;
                break;
            case UIFormsLucencyType.OpaqueBlack:
                _GoPopLayerMask.SetActive(true);
                Color newColor4 = new Color(SysDefine.SYS_UIMASK_OPAQUE_BLACK_COLOR_RGB, SysDefine.SYS_UIMASK_OPAQUE_BLACK_COLOR_RGB, SysDefine.SYS_UIMASK_OPAQUE_BLACK_COLOR_RGB, SysDefine.SYS_UIMASK_OPAQUE_BLACK_COLOR_A);
                _GoPopLayerMask.GetComponent<Image>().color = newColor4;
                break;
            case UIFormsLucencyType.Penetrate:
                _GoPopLayerMask.SetActive(false);
            
                break;
            default:
                _GoPopLayerMask.SetActive(true);   
                break;
        }
        
        _GoPopLayerMask.GetComponent<Canvas>().sortingOrder = goDisplayPlane.GetComponent<Canvas>().sortingOrder - 1;
        _GoPopLayerMask.transform.localPosition = new Vector3(_GoPopLayerMask.transform.localPosition.x, _GoPopLayerMask.transform.localPosition.y, goDisplayPlane.transform.localPosition.z + 100);
       
        goDisplayPlane.transform.SetAsLastSibling();

        
        if (_UICamear != null)
        {
            _UICamear.depth = _UICamear.depth + SysDefine.SYS_UICAMERA_DEPTH_INCREMENT;
        }
    }

    
    public void CancelMaskWindow()
    {
       
        // _GoTopPlane.transform.SetAsFirstSibling();
      
        if (_GoPopLayerMask.activeInHierarchy)
        {
            _GoPopLayerMask.SetActive(false);
        }
        
        _UICamear.depth = _OriginalUICameraDepth;
    }

    public void ShowFixedMask(GameObject goDisplayPlane, UIFormsLucencyType UILucencyType = UIFormsLucencyType.Impenetrable)
    {
        // _GoFixedLayerMask.gameObject.SetActive(true);

     
        switch (UILucencyType)
        {
            case UIFormsLucencyType.Lucency:
                _GoFixedLayerMask.SetActive(true);
                Color newColor1 = new Color(SysDefine.SYS_UIMASK_LUCENCY_COLOR_RGB, SysDefine.SYS_UIMASK_LUCENCY_COLOR_RGB, SysDefine.SYS_UIMASK_LUCENCY_COLOR_RGB, SysDefine.SYS_UIMASK_LUCENCY_COLOR_A);
                _GoFixedLayerMask.GetComponent<Image>().color = newColor1;
                break;
            case UIFormsLucencyType.Translucence:
                _GoFixedLayerMask.SetActive(true);
                Color newColor2 = new Color(SysDefine.SYS_UIMASK_TRANSLUCENCY_COLOR_RGB, SysDefine.SYS_UIMASK_TRANSLUCENCY_COLOR_RGB, SysDefine.SYS_UIMASK_TRANSLUCENCY_COLOR_RGB, SysDefine.SYS_UIMASK_TRANSLUCENCY_COLOR_A);
                _GoFixedLayerMask.GetComponent<Image>().color = newColor2;
                break;
            case UIFormsLucencyType.Impenetrable:
                _GoFixedLayerMask.SetActive(true);
                Color newColor3 = new Color(SysDefine.SYS_UIMASK_IMPENETRABLE_COLOR_RGB, SysDefine.SYS_UIMASK_IMPENETRABLE_COLOR_RGB, SysDefine.SYS_UIMASK_IMPENETRABLE_COLOR_RGB, SysDefine.SYS_UIMASK_IMPENETRABLE_COLOR_A);
                _GoFixedLayerMask.GetComponent<Image>().color = newColor3;
                break;
            case UIFormsLucencyType.OpaqueBlack:
                _GoFixedLayerMask.SetActive(true);
                Color newColor4 = new Color(SysDefine.SYS_UIMASK_OPAQUE_BLACK_COLOR_RGB, SysDefine.SYS_UIMASK_OPAQUE_BLACK_COLOR_RGB, SysDefine.SYS_UIMASK_OPAQUE_BLACK_COLOR_RGB, SysDefine.SYS_UIMASK_OPAQUE_BLACK_COLOR_A);
                _GoFixedLayerMask.GetComponent<Image>().color = newColor4;
                break;
            case UIFormsLucencyType.Penetrate:
                if (_GoFixedLayerMask.activeInHierarchy)
                {
                    _GoFixedLayerMask.SetActive(false);
                }
                break;
            default:
                break;
        }

       
        _GoFixedLayerMask.transform.SetAsLastSibling();
       
        goDisplayPlane.transform.SetAsLastSibling();
    }

    public void HideFixedMask()
    {
        _GoFixedLayerMask.gameObject.SetActive(false);
    }
}//Class_end
