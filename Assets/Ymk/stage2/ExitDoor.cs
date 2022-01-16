using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : RequireEvent
{
    public override void RunEvent()
    {
        if (Inventory.GetSelectItem() == requireObj)
        {
            if (costObject)
                Inventory.CostSelectItem();
            SoundManager.PlaySE(SoundManager.SE.open);
            SoundManager.PlaySE(SoundManager.SE.Door_open);
            SceneManager.LoadScene(SceneManager.Scene.Ending);
        }
        else
        {
            SoundManager.PlaySE(SoundManager.SE.Lock);
        }
    }

    
}
