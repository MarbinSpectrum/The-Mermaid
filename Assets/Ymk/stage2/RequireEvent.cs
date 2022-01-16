using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequireEvent : OpenCloseObject
{
    public ItemData.Type requireObj;
    public bool costObject = true;
    public List<LoadOpenClose> loadObj = new List<LoadOpenClose>();
    public RunSE_Obj runSE;
    public RunSE_Obj FailRunSE;
    public virtual void RunEvent()
    {
        if(Inventory.GetSelectItem() == requireObj)
        {
            if(costObject)
                Inventory.CostSelectItem();
            Open();
            loadObj.ForEach(x => x.GetObject());
            if (runSE)
                runSE.RunSE();
        }
        else
        {
            if (FailRunSE)
                FailRunSE.RunSE();
        }
    }
}
