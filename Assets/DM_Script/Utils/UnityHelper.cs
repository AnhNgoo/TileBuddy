#pragma warning disable 1591
/*
 * Created on 2024
 *
 * Copyright (c) 2024 dotmobstudio
 * Support : dotmobstudio@gmail.com
 */
using System;
using System.IO;
using System.Text;
using System.Reflection;
using LitJson;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class UnityHelper : UnitySingleton<UnityHelper>
{

    public static int GetRandom(int num1, int num2)
    {
        if (num1 < num2)
        {
            return UnityEngine.Random.Range(num1, num2 + 1);
        }
        else
        {
            return UnityEngine.Random.Range(num2, num1 + 1);
        }
    }

  
    public static void ClearMemory()
    {
        Resources.UnloadUnusedAssets();
        GC.Collect();
    }


    public static Transform FindTheChild(GameObject goParent, string childName)
    {
        Transform searchTrans = goParent.transform.Find(childName);
        if (searchTrans == null)
        {
            foreach (Transform trans in goParent.transform)
            {
                searchTrans = FindTheChild(trans.gameObject, childName);
                if (searchTrans != null)
                {
                    return searchTrans;
                }
            }
        }
        return searchTrans;
    }

 
    public static T GetTheChildComponent<T>(GameObject goParent, string childName) where T : Component
    {
        Transform searchTrans = FindTheChild(goParent, childName);
        if (searchTrans != null)
        {
            return searchTrans.gameObject.GetComponent<T>();
        }
        else
        {
            // Debug.LogWarning("Cant Find This Child: " + childName);
            return null;
        }
    }


    public static T AddTheChildComponent<T>(GameObject goParent, string childName) where T : Component
    {
        Transform searchTrans = FindTheChild(goParent, childName);
        if (searchTrans != null)
        {
            T[] theComponentsArr = searchTrans.GetComponents<T>();
            for (int i = 0; i < theComponentsArr.Length; i++)
            {
                if (theComponentsArr[i] != null)
                {
                    Destroy(theComponentsArr[i]);
                }
            }
            return searchTrans.gameObject.AddComponent<T>();
        }
        else
        {
            return null;
        }
    }


    public static void AddChildToParent(Transform parentTrs, Transform childTrs)
    {
        //childTrs.parent = parentTrs; //Original Method
        childTrs.SetParent(parentTrs, false);
        childTrs.localPosition = Vector3.zero;
        childTrs.localScale = Vector3.one;
        childTrs.localEulerAngles = Vector3.zero;
    }


    public static bool IsTouchIn(Transform responseArea)
    {
        GameObject clickTarget = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        if (clickTarget == null)
            return false;

        return responseArea != null ? clickTarget.transform.IsChildOf(responseArea) : false;
    }


    public static string GetScreenshotPath()
    {
        return Application.persistentDataPath + "/ScreenShot.png";
    }

    public static bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }


    public static string String2Unicode(string source)
    {
        byte[] bytes = Encoding.Unicode.GetBytes(source);
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < bytes.Length; i += 2)
        {
            stringBuilder.AppendFormat("\\u{0}{1}", bytes[i + 1].ToString("x").PadLeft(2, '0'), bytes[i].ToString("x").PadLeft(2, '0'));
        }
        return stringBuilder.ToString();
    }

    public static string GetNetWorkTypeName()
    {
        switch (Application.internetReachability)
        {
            case NetworkReachability.ReachableViaCarrierDataNetwork:
                return "Carrier network";
            case NetworkReachability.ReachableViaLocalAreaNetwork:
                return "wifi";
            default:
                return "Error getting network information";
        }
    }

    public static string GetDeviceName()
    {
        return SystemInfo.deviceName;
    }

    public static string GetDeviceSystem()
    {
        return SystemInfo.operatingSystem;
    }

    #region 
    public static bool IsAndroid()
    {
        return Application.platform == RuntimePlatform.Android;
    }

    public static bool IsIOS()
    {
        return Application.platform == RuntimePlatform.IPhonePlayer;
    }

    public static bool IsEditor()
    {
        return Application.isEditor;
    }

    public static string GetPlatformName()
    {
        if (IsAndroid())
        {
            return "Android";
        }
        else if (IsIOS())
        {
            return "iOS";
        }
        else if (IsEditor())
        {
            return "editor";
        }

        return "default";
    }

    public static string GetActivePlatformName()
    {
#if UNITY_ANDROID
        return "Android";
#elif UNITY_IPHONE
            return "iOS";
#else
            return "UnKnow";
#endif
    }

    public static bool IsActiveAndroid()
    {
#if UNITY_ANDROID
        return true;
#else
        return false;
#endif
    }

    public static bool IsActiveIos()
    {
#if UNITY_IPHONE
        return true;
#else
        return false;
#endif
    }


    #endregion

  
    public static bool VersionMatching(string theOperator, string theVersion)
    {
        string[] cVersionArr = Application.version.Split('.');
        string[] sVersionArr = theVersion.Split('.');

        switch (theOperator)
        {
            case ">":
            case ">=":
                for (int i = 0; i < cVersionArr.Length; i++)
                {
                    int cVersion;
                    int sVersion;
                    bool cValue = int.TryParse(cVersionArr[i], out cVersion);
                    bool sValue = int.TryParse(sVersionArr[i], out sVersion);
                    if (!cValue || !sValue)
                        return false;
                    if (cVersion > sVersion)
                    {
                        return true;
                    }
                    if (cVersion < sVersion)
                    {
                        return false;
                    }
                }
                if (theOperator == ">=")
                    return Application.version == theVersion;
                return false;
            case "<":
            case "<=":
                for (int i = 0; i < cVersionArr.Length; i++)
                {
                    int cVersion;
                    int sVersion;
                    bool cValue = int.TryParse(cVersionArr[i], out cVersion);
                    bool sValue = int.TryParse(sVersionArr[i], out sVersion);
                    if (!cValue || !sValue)
                        return false;
                    if (cVersion < sVersion)
                        return true;
                    if (cVersion > sVersion)
                    {
                        return false;
                    }
                }
                if (theOperator == "<=")
                    return Application.version == theVersion;
                return false;
            case "==":
            case "=":
                return Application.version == theVersion;
            default:
                return Application.version == theVersion;
        }
    }

    public static bool VersionMatching(string version)
    {
        char[] operators = new char[] { '>', '<', '=' };
        int index = version.LastIndexOfAny(operators);
        string theOperator = version.Substring(0, index + 1);
        string theVersion = version.Substring(index + 1, version.Length - index - 1);
        return VersionMatching(theOperator, theVersion);
    }

    public static float BatteryLevel()
    {
        if (IsEditor())
            return 1;
        return SystemInfo.batteryLevel;
    }

    public static string GetMD5HashFromFile(string fileName)
    {
        try
        {
            FileStream file = new FileStream(fileName, FileMode.Open);
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] retVal = md5.ComputeHash(file);
            file.Close();

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x2"));
            }
            return sb.ToString();
        }
        catch (Exception ex)
        {
            throw new Exception("GetMD5HashFromFile() fail,error:" + ex.Message);
        }
    }


    public static string GetPlatformPersistentDataPath()
    {
        return string.Format("{0}/{1}", Application.persistentDataPath, GetActivePlatformName());
    }

    public static void LogToTxt(string content, string textFileName = "MyDebug.txt")
    {
        if (!IsEditor())
            return;

        string tmpPath = Application.dataPath + "/../HiddenFolder/" + textFileName;


        FileStream fs = new FileStream(tmpPath, FileMode.Create, FileAccess.Write);
        fs.Flush();
        StreamWriter bw = new StreamWriter(fs);
        bw.Write(content);
        bw.Close();

        fs.Close();
    }


}//Class_end
