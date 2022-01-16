using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Language
{
    ÇÑ±¹¾î,English,Count
}

public class LanguageObj : MonoBehaviour
{
    public GameObject[] Obj = new GameObject[(int)Language.Count];

    private Language nowLanguage = Language.Count;

    private void Update()
    {
        var data = DataManager.playData;
        if (data != null)
        {
            if(nowLanguage != data.language)
            {
                nowLanguage = data.language;
                foreach (GameObject obj in Obj)
                    obj.SetActive(false);
                Obj[(int)nowLanguage].SetActive(true);
            }
        }
    }
}
