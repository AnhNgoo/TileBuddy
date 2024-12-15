/*
 * Created on 2024
 *
 * Copyright (c) 2024 dotmobstudio
 * Support : dotmobstudio@gmail.com
 */
public class UINoticeBase: UIBase
{
    public string uiPopType;   
    public bool needPopNext = true;
    override public void Destroy()
    {
        base.Destroy();
        // PopUpManager.Instance.DoPopAction(uiPopType);
        if (needPopNext)
            PopUpManager.Instance.DoPopAction(uiPopType);
    }
}
