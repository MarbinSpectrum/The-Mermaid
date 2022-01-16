using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Warning : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector timeline;

    private void Awake()
    {
        timeline.stopped += GoMain;
        UIManager.EnableSetting(false, UIManager.UI.InGame);
        UIManager.EnableSetting(false, UIManager.UI.Setting);
    }



    private void GoMain(PlayableDirector aDirector)
    {
        SceneManager.LoadScene(SceneManager.Scene.Main);
    }
}
