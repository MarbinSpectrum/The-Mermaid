using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeActiveObject : MonoBehaviour
{
    public string Key;

    private void Update()
    {
        var data = DataManager.playData;
        if(data.ActObject.Contains(Key))
            gameObject.SetActive(false);
    }

    public void GetObject()
    {
        var data = DataManager.playData;
        if (!data.ActObject.Contains(Key))
            data.ActObject.Add(Key);
        DataManager.SaveData();
    }
}
