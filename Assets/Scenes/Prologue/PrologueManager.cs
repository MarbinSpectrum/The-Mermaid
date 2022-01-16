using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PrologueManager : MonoBehaviour
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
        SoundManager.PlayBGM(SoundManager.BGM.Prologue_BGM);
    }

    private void GoInGame(PlayableDirector aDirector)
    {
        SceneManager.LoadScene(SceneManager.Scene.InGame);
    }

    double[] textTIme = { 62 / 60, 302 / 60, 542 / 60, 783 / 60, 1056 / 60, 1300 / 60, 1575 / 60, 1810 / 60, 2200 / 60 };

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
