using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneTimeText : LoadOpenClose
{
    public string text;
    public string text_eng;

    public override void Update()
    {
        base.Update();
        if(act)
            Destroy(this);
    }

    public void Print()
    {
        if(DataManager.playData != null)
        {
            if(DataManager.playData.language == Language.ÇÑ±¹¾î)
                PrintText.Print(text);
            else if (DataManager.playData.language == Language.English)
                PrintText.Print(text_eng);

        }
        GetObject();
        Destroy(this);
    }
}
