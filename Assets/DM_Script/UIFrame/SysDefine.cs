/*
 * Created on 2024
 *
 * Copyright (c) 2024 dotmobstudio
 * Support : dotmobstudio@gmail.com
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region 

public enum UIFormsType
{
    Normal,             
    Fixed,             
    Middle,            
    PopUp,              
    //Notice,             
    NoviceGuide,        
    Front,             
    Waiting,           
}


public enum UIFormsShowMode
{
    Normal,             
    ReverseChange,     
    HideOther,         
    PopUp,              
    Mixed,
    NoviceGuide,        
    Simple,            

}


public enum UIFormsLucencyType
{
    Lucency,            
    Translucence,       
    Impenetrable,       
    Penetrate,          
    OpaqueBlack         
}
#endregion


public static class SysDefine
{
    #region as
  
    public const string SYS_PATH_CANVAS = "Prefabs/UIBase/UICanvas";
    public const string SYS_PATH_UICAMERA = "Prefabs/UIBase/UICamera";
    
    public const string SYS_PATH_UIFormConfigJson = "Config/Index/PrefabPathJson";
    public const string SYS_PATH_SpriteConfigJson = "Config/Index/SpriteIndex";
    public const string SYS_PATH_ConfigJson = "Config/Index/ConfigIndex";

   
    public const string SYS_TAG_CANVAS = "_TagCanvas";
    public const string SYS_TAG_UICAMERA = "_TagUICamera";
    public const string SYS_TAG_GAMECONTROLLER = "_TagGameController";
    public const string SYS_TAG_NOREFLECTIONTILE = "_TagNoReflectionTile";
    public const string SYS_TAG_CLONETILE = "_TagCloneTile";
    public const string SYS_TAG_GUIDEZKEEPUI = "_TagGuideZKeepUI";
   
    public const string SYS_CANVAS_NORMAL_NODE_NAME = "Normal";
    public const string SYS_CANVAS_FIXED_NODE_NAME = "Fixed";
    public const string SYS_CANVAS_POPUP_NODE_NAME = "PopUp";
    public const string SYS_CANVAS_WAITING_NODE_NAME = "Waiting";
    public const string SYS_CANVAS_FRONT_NODE_NAME = "Front";
    public const string SYS_CANVAS_MIDDLE_NODE_NAME = "Middle";
    public const string SYS_CANVAS_NOVICE_NODE_NAME = "Novice";
    public const string SYS_CANVAS_BLOCK_NODE_NAME = "BlockInput";
    public const string SYS_CANVAS_UISCRIPTS_NODE_NAME = "_UIScripts";
    public const string SYS_CANVAS_POPLAYERMASK_NODE_NAME = "PopLayerMask";
    public const string SYS_CANVAS_FIXEDLAYERMASK_NODE_NAME = "FixedLayerMask";
   
    public const float SYS_UIMASK_LUCENCY_COLOR_RGB = 255F / 255F;
    public const float SYS_UIMASK_LUCENCY_COLOR_A = 0F / 255F;
    
    public const float SYS_UIMASK_TRANSLUCENCY_COLOR_RGB = 0F / 255F;
    public const float SYS_UIMASK_TRANSLUCENCY_COLOR_A = 180F / 255F;
   
    public const float SYS_UIMASK_IMPENETRABLE_COLOR_RGB = 0F;
    public const float SYS_UIMASK_IMPENETRABLE_COLOR_A = 200F / 255F;
    
    public const float SYS_UIMASK_OPAQUE_BLACK_COLOR_RGB = 0F;
    public const float SYS_UIMASK_OPAQUE_BLACK_COLOR_A = 1F;

  
    public const int SYS_UICAMERA_DEPTH_INCREMENT = 100;
    #endregion

    #region bs
    
    public static string GetLogPath()
    {
        string logPath = null;

     
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            logPath = Application.streamingAssetsPath + "/LogConfigInfo.xml";
        }
        //Win
        else
        {
            logPath = "file://" + Application.streamingAssetsPath + "/LogConfigInfo.xml";
        }

        return logPath;
    }
 
    public static string GetLogRootNodeName()
    {
        string strReturnXMLRootNodeName = null;

        strReturnXMLRootNodeName = "SystemConfigInfo";
        return strReturnXMLRootNodeName;
    }


    public static string GetUIFormsConfigFilePath()
    {
        string logPath = null;

        //Android,Iphone
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            logPath = Application.streamingAssetsPath + "/UIFormsConfigInfo.xml";
        }
        //Win
        else
        {
            logPath = "file://" + Application.streamingAssetsPath + "/UIFormsConfigInfo.xml";
        }

        return logPath;
    }
   
    public static string GetUIFormsConfigFileRootNodeName()
    {
        string strReturnXMLRootNodeName = null;

        strReturnXMLRootNodeName = "UIFormsConfigInfo";
        return strReturnXMLRootNodeName;
    }


    #endregion

    #region 

    #endregion

    #region 

    #endregion
}//Class_end


public class UIType
{

   
    public bool IsClearAllOtherView = false;
   
    public bool IsClearReverseChange = false;

    public bool IsClearPopUp = false;
   
    public bool IsNeedPush = true;
   
    public bool NoHidePopUp = false;
    public bool IsGuide = false;
 
    public bool CanPopMessage = false;
    
    public bool IsOverAllPop = true;

    
    public bool IsRefreshCurrentNormalViewWhenIsLastPopClosed = false;
 
    public bool NeedBind = true;
 
    public bool IsLobbyView = false;
    
    public bool IsForcedPop = false;
   
    public bool PopUpNeedAnime = true;

    public bool IsNeedHideWhenGuiding = true;
    public UIFormsType UIForms_Type = UIFormsType.Normal;
  
    public UIFormsShowMode UIForms_ShowMode = UIFormsShowMode.Normal;
   
    public UIFormsLucencyType UIForms_LucencyType = UIFormsLucencyType.Translucence;
   
    public bool IsHideNormalView = false;

    public string LinkUIName = "";
}
