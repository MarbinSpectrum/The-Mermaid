using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleEvent : RequireEvent
{
    public ItemData itemData;

    public override void RunEvent()
    {
        if (Inventory.GetSelectItem() == requireObj)
        {
            if (costObject)
                Inventory.CostSelectItem();
            Inventory.AddItem(itemData);
            SoundManager.PlaySE(SoundManager.SE.Get_Item);
            if (runSE)
                runSE.RunSE();
        }
    }
}
