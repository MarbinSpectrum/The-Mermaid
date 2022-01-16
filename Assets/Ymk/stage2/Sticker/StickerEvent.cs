using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickerEvent : RequireEvent
{
    public void OnEnable()
    {
        Close();
    }

    public override void RunEvent()
    {
        bool flag = false;
        List<ItemData> temp = Inventory.GetItemList();
        foreach (ItemData item in temp)
            if (item.ItemType == ItemData.Type.UnfinishedSheetMusic)
                flag = true;

        if (flag)
            Open();
    }
}
