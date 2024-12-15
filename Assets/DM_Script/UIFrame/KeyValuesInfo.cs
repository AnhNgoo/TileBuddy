using System;
using System.Collections.Generic;

[Serializable]
public class KeyValuesInfo
{
 
    public List<KeyValuesNode> ConfigInfo = new List<KeyValuesNode>();
}

[Serializable]
public class KeyValuesNode
{
   
    public string Key = null;
    
    public string Value = null;

    public KeyValuesNode(string Key, string Value)
    {
        this.Key = Key;
        this.Value = Value;
    }
}

