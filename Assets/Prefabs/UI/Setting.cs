using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Setting : MonoBehaviour
{
    public Slider bgm;
    public Slider se;
    public GameObject inGameUI;

    private void Start()
    {
        DataManager.LoadData();
        bgm.value = SoundManager.GetSoundValue(SoundManager.SoundType.BGM);
        se.value = SoundManager.GetSoundValue(SoundManager.SoundType.SE);
        ChangeLaguage((int)DataManager.playData.language,false);
    }

    public void ChangeBGM() => SoundManager.SetSoundValue(bgm.value, SoundManager.SoundType.BGM);
    public void ChangeSE() => SoundManager.SetSoundValue(se.value, SoundManager.SoundType.SE);

    public void ChangeBGMmute(bool state) => SoundManager.MuteSound(state, SoundManager.SoundType.BGM);
    public void ChangeSEmute(bool state) => SoundManager.MuteSound(state, SoundManager.SoundType.SE);

    public void CloseUI()
    {
        SoundManager.PlaySE(SoundManager.SE.UI_Select);
        UIManager.EnableSetting(false, UIManager.UI.Setting);
    }

    public void GoTitle()
    {
        SoundManager.PlaySE(SoundManager.SE.UI_Select);
        SceneManager.LoadScene(SceneManager.Scene.Main);
        CloseUI();
    }

    public void QuitGame()
    {
        SoundManager.PlaySE(SoundManager.SE.UI_Select);

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }

    private void OnEnable()
    {
        inGameUI.SetActive(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "InGame");
    }

    public List<GameObject> selectLaguageObj = new List<GameObject>();
    public void ChangeLaguage(int n,bool sound)
    {
        if(sound)
            SoundManager.PlaySE(SoundManager.SE.UI_Select);

        selectLaguageObj.ForEach(x => x.gameObject.SetActive(false));
        selectLaguageObj[n].SetActive(true);
        DataManager.playData.language = (Language)(n);

        DataManager.SaveData();
    }

    public void ChangeLaguage(int n)
    {
        ChangeLaguage(n, false);
    }

    public void ChangeLaguageUI(int n)
    {
        ChangeLaguage(n, true);
    }
}
