using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ItemSlot : MonoBehaviour
{
    public Image item;
    public Image selectImg;

    public GameObject itemDesc;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI itemText;

    [System.NonSerialized]
    public int num;

    public void SelectItem() => Inventory.ItemSelect(num);

    public void EnableExplain(bool state) => itemDesc.gameObject.SetActive(item.enabled && state);

    public void RunItemEvent() => Inventory.RunItemEvent(num);

}
