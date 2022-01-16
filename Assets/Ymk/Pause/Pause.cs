using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject obj;

    private void Update()
    {
        obj.SetActive(MoveCamera.nowRoom.Length == 7);
    }

    public void ActPause()
    {
        SoundManager.PlaySE(SoundManager.SE.UI_Select);
        UIManager.EnableSetting(true, UIManager.UI.Setting);
    }
}
