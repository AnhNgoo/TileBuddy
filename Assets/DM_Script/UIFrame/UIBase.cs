/*
 * Created on 2024
 *
 * Copyright (c) 2024 dotmobstudio
 * Support : dotmobstudio@gmail.com
 */

using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;
using System;

public class UIBase
{
    public Transform root;
    protected bool shouldHide;  

    public string uiName;       

    private UIType _currentUIType = new UIType();
    public UIType CurrentUIType {
        get { return _currentUIType; }
        set {
            _currentUIType = value;
        }
    }
        
   
    public List<UIBase> BindedPopUpUIForm = new List<UIBase>();
    
    public List<UIBase> LinkedPopUpUIForm = new List<UIBase>();

    private Action _redisplayAction;
    public void SetRedisplayAction(Action action)
    {
        _redisplayAction = action;
    }

    private Action _destroyAction;
    public void SetDestroyAction(Action action)
    {
        _destroyAction = action;
    }

    private Action _onDestroyAction;
    public void SetOnDestroyAction(Action action)
    {
        _onDestroyAction = action;
    }

    public bool IsMannullyRefresh;

    // timer register
    public List<string> timerList = new List<string>();

    public virtual void Show()
    {
        if (root == null)
            return;
                    
        root.gameObject.SetActive(true);
        if (CurrentUIType.UIForms_Type == UIFormsType.PopUp)
        {
            UIMaskMgr.GetInstance().SetMaskWindow(root.gameObject, CurrentUIType.UIForms_LucencyType);
        }
        if (CurrentUIType.UIForms_Type == UIFormsType.Fixed)
        {
            UIMaskMgr.GetInstance().ShowFixedMask(root.gameObject, CurrentUIType.UIForms_LucencyType);
        }
    }

    public virtual void Update()
    {

    }

    public virtual void Hide()
    {
        if (root != null)
        {
            root.gameObject.SetActive(false);
                
            if (CurrentUIType.UIForms_Type == UIFormsType.Fixed)
            {
                UIMaskMgr.GetInstance().HideFixedMask();
            }
            else if (CurrentUIType.UIForms_Type == UIFormsType.PopUp)
            {
                UIMaskMgr.GetInstance().CancelMaskWindow();
            }

        }
    }

    public virtual void Redisplay()
    {
        if (_redisplayAction != null)
            _redisplayAction();

        foreach (var item in LinkedPopUpUIForm)
        {
            item.Show();
        }

        if (CurrentUIType.UIForms_Type == UIFormsType.PopUp)
        {
            root.gameObject.SetActive(true);
            //
            UIMaskMgr.GetInstance().SetMaskWindow(root.gameObject, CurrentUIType.UIForms_LucencyType);
        }

        if (CurrentUIType.UIForms_Type == UIFormsType.Fixed)
        {
            UIMaskMgr.GetInstance().ShowFixedMask(root.gameObject, CurrentUIType.UIForms_LucencyType);
        }
    }


    public void CloseAndClearLinkedUI()
    {
        foreach (var item in LinkedPopUpUIForm)
        {
            item.Close();
        }

        LinkedPopUpUIForm.Clear();
    }


    public bool CreateAndAttachRoot(string rootName)
    {
        uiName = rootName;
        return UIManager.GetInstance().ShowUIForm(this);
    }

   
    public void AddToUIStack(string rootName)
    {
        uiName = rootName;
        UIManager.GetInstance().AddNormalUIToStack(this);
    }

    public virtual void OnDestroyRoot()
    {
        if (_onDestroyAction != null)
            _onDestroyAction();
    }

    public virtual void Destroy()
    {
        if (_destroyAction != null)
            _destroyAction();
        if (root != null)
        {
            GameObject.Destroy(root.gameObject);
        }

        switch (CurrentUIType.UIForms_Type)
        {
            case UIFormsType.Normal:
                UIManager.GetInstance().CloseBindedUiForms(this);
                break;
            case UIFormsType.PopUp:
                UIMaskMgr.GetInstance().CancelMaskWindow();
                break;
            case UIFormsType.Fixed:
                UIMaskMgr.GetInstance().HideFixedMask();
                break;

        }
    }


    public T FindAChild<T>(string name) where T : Component
    {
        return FindAChild<T>(root.gameObject, name);
    }

   
    public T FindAChild<T>(GameObject parent, string name) where T : Component
    {
        Transform childTrans = FindAChild(parent, name);
        if (childTrans == null)
        {
            return null;
        }
        T child = childTrans.GetComponent<T>();
        if (child == null)
        {
            //Debug.LogWarningFormat(": {0}<{1}>", name, typeof(T));
            return null;
        }
        return child;
    }

    public Component FindAChildByType(string name, string componentType)
    {
        Transform child = FindAChild(root.gameObject, name);
        if (child != null)
        {
            return child.GetComponent(componentType);
        }
        return null;
    }

    
    public Transform FindAChild(string name)
    {
        return FindAChild(root.gameObject, name);
    }

  
    public Transform FindAChild(GameObject parent, string name)
    {
        Transform child = UnityHelper.FindTheChild(parent, name);
        if (child == null)
        {
          
            return null;
        }
        return child;
    }


    public Button AddBtnListener(string name, UnityEngine.Events.UnityAction call, bool clearFirst = true)
    {
        return AddBtnListener(root.gameObject, name, call, clearFirst);
    }

    public Button AddBtnListener(GameObject parent, string name, UnityEngine.Events.UnityAction call, bool clearFirst = true)
    {
        Button btn = FindAChild<Button>(parent, name);
        if (btn == null)
            return null;

        if (clearFirst)
        {
            btn.onClick.RemoveAllListeners();
        }

        btn.onClick.AddListener(call);
        return btn;
    }

    public Image SetImage(GameObject parent, string name, Sprite sprite)
    {
        Image image = FindAChild<Image>(parent, name);
        if (image == null)
            return null;

        image.sprite = sprite;
        return image;
    }

    public Image SetImage(string name, Sprite sprite)
    {
        return SetImage(root.gameObject, name, sprite);
    }

    public Text SetText(GameObject parent, string name, string content)
    {
        Text text = FindAChild<Text>(parent, name);
        if (text == null)
            return null;

        text.text = content;
        return text;
    }

    public bool IsShowing()
    {
        return root != null && root.gameObject.activeSelf;
    }

    public Text SetText(string name, string content)
    {
        return SetText(root.gameObject, name, content);
    }

    public void SetRootActive(bool value)
    {
        root.gameObject.SetActive(value);
    }

    public GameObject SetActive(GameObject parent, string name, bool active)
    {
        Transform child = FindAChild(parent, name);
        if (child == null)
            return null;

        child.gameObject.SetActive(active);
        return child.gameObject;
    }

    public GameObject SetActive(string name, bool active)
    {
        return SetActive(root.gameObject, name, active);
    }

    public void RefreshCurrentView()
    {
        Redisplay();
    }

    public virtual void FadOutClose(Action closeAction)
    {
        UIManager.GetInstance().FadOutClose(closeAction);
    }

    public virtual void Close()
    {
//        Debug.Log("chay vao day");
        if (!UIManager.GetInstance().CloseOrReturnUIForms(uiName))
        {
//            Debug.Log("UI Manager can not close:" + uiName);
            Destroy();
        }
    }

    public void BindUIForm(UIBase uiBase)
    {
        BindedPopUpUIForm.Add(uiBase);
    }

    public void UnbindUIForm(UIBase uiBase)
    {
        if (BindedPopUpUIForm.Contains(uiBase))
            BindedPopUpUIForm.Remove(uiBase);
    }

    public void LinkUIForm(UIBase uiBase)
    {
        LinkedPopUpUIForm.Add(uiBase);
    }

    public void BackAView()
    {
        //Action closeAction = delegate ()
        //{
            UIManager.GetInstance().BackAView();
        //};
        //FadOutClose(closeAction);
    }

    public string GetTypeName()
    {
        return this.GetType().Name;
    }

    public void SendMsg(int msgType, int msgName, object msgContent)
    {
        KeyValuesUpdate kvs = new KeyValuesUpdate(msgName, msgContent);
        MessageCenter.SendMessage(msgType, kvs);
    }

    public void SendMsg(int msgType, int msgName)
    {
        SendMsg(msgType, msgName, null);
    }

    public void RemoveMsgListener(int msgType, MessageCenter.DelMessageDelivery handler)
    {
        MessageCenter.RemoveMsgListener(msgType, handler);
    }

    public void ReceiveMsg(int messageType, MessageCenter.DelMessageDelivery handler)
    {
        MessageCenter.AddMsgListener(messageType, handler);
    }

}
