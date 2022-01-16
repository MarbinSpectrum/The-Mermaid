using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class curtain : RequireEvent
{
    public OneTimeText oneTimeText;


    public override void RunEvent()
    {
        if (Inventory.GetSelectItem() == requireObj)
        {
            if (costObject)
                Inventory.CostSelectItem();
            Open();
            loadObj.ForEach(x => x.GetObject());
            if (runSE)
                runSE.RunSE();
            oneTimeText.Print();
        }
        else
        {
            if (FailRunSE)
                FailRunSE.RunSE();
        }
    }
}
