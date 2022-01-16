using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : DontDestroy<UIManager>
{

    public enum UI
    {
        InGame, Setting
    }

    [SerializeField]
    private GameObject inGame;

    [SerializeField]
    private GameObject setting;

    public static void EnableSetting(bool state, UI ui)
    {
        if (ui == UI.InGame)
            instance.inGame.SetActive(state);
        else if (ui == UI.Setting)
            instance.setting.SetActive(state);
    }
}
