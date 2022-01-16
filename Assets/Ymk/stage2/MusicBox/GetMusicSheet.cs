using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMusicSheet : MonoBehaviour
{
    public ItemData itemData;

    public void RunEvent()
    {
        bool flag = true;
        List<ItemData> temp = Inventory.GetItemList();
        foreach(ItemData item in temp)
            if (item.ItemType == ItemData.Type.FinishedSheetMusic ||
             item.ItemType == ItemData.Type.StrangeSheetMusic ||
             item.ItemType == ItemData.Type.UnfinishedSheetMusic)
                flag = false;

        if(flag)
            Inventory.AddItem(itemData);
    }
}
