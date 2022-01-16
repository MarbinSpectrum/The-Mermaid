using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadOpenClose : OpenCloseObject
{
    public string Key;
    public bool act = false;
    public virtual void Update()
    {
        var data = DataManager.playData;
        if (!act && data.ActObject.Contains(Key))
        {
            act = true;
            Open();
        }
    }

    public void GetObject()
    {
        var data = DataManager.playData;
        if (!data.ActObject.Contains(Key))
            data.ActObject.Add(Key);
        DataManager.SaveData();
    }
}
