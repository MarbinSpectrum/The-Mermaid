using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    public GameObject hasLoadData;
    private void Start()
    {
        DataManager.LoadData();

        hasLoadData.SetActive(DataManager.playData.played);

        UIManager.EnableSetting(false, UIManager.UI.InGame);
        UIManager.EnableSetting(false, UIManager.UI.Setting);

        if (!DataManager.playData.GameClear)
            SoundManager.PlayBGM(SoundManager.BGM.Main_BGM);

    }

    public GameObject question;

    public void NewGameBtn()
    {
        SoundManager.PlaySE(SoundManager.SE.UI_Select);
        if (DataManager.playData.played)
            NewGameQuestion(true);
        else
            NewGame();
    }

    public void NewGameQuestion(bool state)
    {
        SoundManager.PlaySE(SoundManager.SE.UI_Select);
        question.SetActive(state);
    }

    public void NewGame()
    {
        SoundManager.PlaySE(SoundManager.SE.UI_Select);

        float se = DataManager.playData.seValue;
        bool seM = DataManager.playData.muteSE;

        float bgm = DataManager.playData.bgmValue;
        bool bgmM = DataManager.playData.muteBGM;

        Language language = DataManager.playData.language;

        DataManager.playData = null;
        DataManager.playData = new PlayData();

        DataManager.playData.seValue = se;
        DataManager.playData.muteSE = seM;

        DataManager.playData.bgmValue = bgm;
        DataManager.playData.muteBGM = bgmM;
        DataManager.playData.language = language;

        DataManager.SaveData();

        SceneManager.LoadScene(SceneManager.Scene.Prologue);

    }

    public void LoadGame()
    {

        SoundManager.PlaySE(SoundManager.SE.UI_Select);
        SceneManager.LoadScene(SceneManager.Scene.InGame);

    }

    public void OpenSetting()
    {
        SoundManager.PlaySE(SoundManager.SE.UI_Select);
        UIManager.EnableSetting(true, UIManager.UI.Setting);

    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}
