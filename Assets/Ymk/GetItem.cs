using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItem : MonoBehaviour
{
    public ItemData type;
    public void RunEvent()
    {
        Inventory.AddItem(type,true);

        gameObject.SetActive(false);
    }
}
