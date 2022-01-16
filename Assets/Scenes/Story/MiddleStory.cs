using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class MiddleStory : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector timeline;

    private void Awake()
    {
        timeline.stopped += GoInGame;
        UIManager.EnableSetting(false, UIManager.UI.InGame);
        UIManager.EnableSetting(false, UIManager.UI.Setting);
    }

    private void Start()
    {
        SoundManager.PlayBGM(SoundManager.BGM.Middle_BGM);
    }

    private void GoInGame(PlayableDirector aDirector)
    {
        if (DataManager.playData != null)
        {
            DataManager.playData.nowStage = 1;
            DataManager.playData.nowRoom = "Room2-1";
            DataManager.SaveData();
        }
        SceneManager.LoadScene(SceneManager.Scene.InGame);
    }

    double[] textTIme = { 62 / 60, 360 / 60, 540 / 60, 840 / 60, 1080 / 60, 1350 / 60, 1600 / 60, 1830 / 60, 2100 / 60 };

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
