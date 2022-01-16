using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBoxEvent : RequireEvent
{

    public ItemData.Type failObj;

    public override void RunEvent()
    {
        if (Inventory.GetSelectItem() == requireObj)
        {
            if (costObject)
                Inventory.CostSelectItem();
            Open();
            loadObj.ForEach(x => x.GetObject());
            SoundManager.PlaySE(SoundManager.SE.Success_Music_Box);
        }
        else if (Inventory.GetSelectItem() == failObj)
        {
            if (costObject)
                Inventory.CostSelectItem();

            if (DataManager.playData != null)
            {
                SoundManager.PlaySE(SoundManager.SE.Fail_Music_Box);
                if (DataManager.playData.language == Language.�ѱ���)
                    PrintText.Print("�̰� �ƴ� ���ϴ�...");
                else if (DataManager.playData.language == Language.English)
                    PrintText.Print("I don't think this is right..");

            }
        }
    }
  
}
