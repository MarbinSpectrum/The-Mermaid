using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Ending : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector timeline;

    private void Awake()
    {
        timeline.stopped += GoMain;
        UIManager.EnableSetting(false, UIManager.UI.InGame);
        UIManager.EnableSetting(false, UIManager.UI.Setting);
    }

    private void Start()
    {
        SoundManager.PlayBGM(SoundManager.BGM.Ending_BGM);
    }

    private void GoMain(PlayableDirector aDirector)
    {
        DataManager.LoadData();
        if (DataManager.playData != null)
        {
            DataManager.playData.GameClear = true;
            DataManager.playData.played = false;
            DataManager.SaveData();
        }

        SceneManager.LoadScene(SceneManager.Scene.Main);
    }

    double[] textTIme = { 80 / 60, 240 / 60, 450 / 60, 600 / 60, 900 / 60, 1140 / 60, 1410 / 60, 1650 / 60, 1830 / 60
    , 2070 / 60
    , 2310 / 60
    , 2600 / 60
    , 2880 / 60
    , 3100 / 60
        , 3100 / 60
        , 3280 / 60
        , 3480 / 60
        , 3660 / 60
            , 3900 / 60
            , 4140 / 60
            , 4400 / 60
           , 4600 / 60
           , 4850 / 60
           , 5070 / 60};

    public void SkipText()
    {
        for(int i = 0; i < textTIme.Length; i++)
            if (timeline.time < textTIme[i])
            {
                timeline.time = textTIme[i];
                return;
            }
    }
}
