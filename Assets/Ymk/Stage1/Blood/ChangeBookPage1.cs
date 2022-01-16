using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBookPage1 : MonoBehaviour
{
    public Book book;
    public AutoFlip autoFlip;



    public List<Sprite> defPage_kor;
    public List<Sprite> defPage_eng;






    private Language nowLanguage = Language.Count;

    private void UpdataPage(Language language)
    {
        if (language == Language.ÇÑ±¹¾î)
        {
            for (int i = 0; i < defPage_kor.Count; i++)
                book.bookPages[i] = defPage_kor[i];
            book.UpdateSprites();

        }
        else if (language == Language.English)
        {
            for (int i = 0; i < defPage_eng.Count; i++)
                book.bookPages[i] = defPage_eng[i];
            book.UpdateSprites();
        }
    }

    public void Update()
    {


        if (DataManager.playData != null)
        {
            Language language = DataManager.playData.language;
            if (nowLanguage != language)
            {
                nowLanguage = language;
                UpdataPage(nowLanguage);
            }
        }



    }

}
