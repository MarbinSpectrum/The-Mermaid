using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : Singleton<Inventory>
{
    private static List<ItemData> itemList = new List<ItemData>();
    private List<ItemSlot> slotObjList = new List<ItemSlot>();
    private static Dictionary<ItemData.Type, Action> itemEvent = new Dictionary<ItemData.Type, Action>();

    private const int START_BOX = 8;
    public static int selectSlot = -1;

    public Transform itemSlotRoot;
    public GameObject itemSlotObj;

    ////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////

    private bool InitFlag = false;

    private void Start()
    {
        Init();

    }

    ////////////////////////////////////////////////////////////////////////////////////////////

    private void Init()
    {
        if (InitFlag)
            return;
        InitFlag = true;

        selectSlot = -1;

        Transform content = itemSlotRoot;
        for (int i = 0; i < Mathf.Max(itemList.Count, START_BOX); i++)
        {
            //이미 배치되어 있는 6개의 슬롯 할당
            if(i < START_BOX)
                slotObjList.Add(content.GetChild(i).GetComponent<ItemSlot>());
            //슬롯이 부족하다면 슬롯을 추가
            else
            {
                GameObject temp = Instantiate(itemSlotObj);
                slotObjList.Add(temp.GetComponent<ItemSlot>());
                temp.transform.SetParent(content);
                temp.SetActive(true);
            }
        }

        UpdateItemList();
    }

    private void UpdateItemList()
    {
        Init();

        Transform content = itemSlotRoot;

        //슬롯이 현재아이템보다 적으면 추가
        while(slotObjList.Count < itemList.Count)
        {
            GameObject temp = Instantiate(itemSlotObj);
            slotObjList.Add(temp.GetComponent<ItemSlot>());
            temp.transform.SetParent(content);
            temp.SetActive(true);
        }

        for (int i = 0; i < slotObjList.Count; i++)
        {
            if (itemList.Count <= i)
            {
                slotObjList[i].gameObject.SetActive(i < START_BOX);
                slotObjList[i].item.enabled = false;
            }
            else
            {
                slotObjList[i].gameObject.SetActive(true);
                slotObjList[i].item.enabled = true;
                slotObjList[i].item.sprite = itemList[i].ItemSprite;
                slotObjList[i].nameText.text = itemList[i].ItemName;
                slotObjList[i].itemText.text = itemList[i].ItemDesc;
            }
            slotObjList[i].selectImg.enabled = (selectSlot == i);
            slotObjList[i].num = i;
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>인벤토리에 모두비우기</summary>
    public static void ItemClear()
    {
        itemList.Clear();
        if(instance != null)
            instance.UpdateItemList();
    }

    /// <summary>인벤토리에 아이템을 추가</summary>
    public static void AddItem(ItemData item, bool playSFX = true)
    {
        itemList.Insert(0, item);
        if (playSFX)
        {
            SoundManager.PlaySE(SoundManager.SE.Get_Item);
        }
        if (instance != null)
            instance.UpdateItemList();

        SaveInventory();
    }

    /// <summary>특정 인덱스 아이템 제거</summary>
    public static void RemoveItem(int idx)
    {
        itemList.RemoveAt(idx);
        if (instance != null)
            instance.UpdateItemList();
    }

    /// <summary>특정 아이템 제거</summary>
    public static void RemoveItem(ItemData.Type item)
    {
        for (int i = 0; i < itemList.Count; i++)
            if (itemList[i].ItemType == item)
            {
                RemoveItem(i);
                return;
            }
    }

    /// <summary>특정 인덱스 아이템을 확인</summary>
    public static ItemData.Type GetItem(int idx)
    {
        return itemList[idx].ItemType;
    }

    /// <summary>인벤토리 아이템 선택</summary>
    public static void ItemSelect(int idx)
    {
        if (instance == null)
            return;

        if(idx < itemList.Count)
            selectSlot = idx;
        SoundManager.PlaySE(SoundManager.SE.item_Click);
        instance.UpdateItemList();
    }

    /// <summary>선택중인 아이템 확인 </summary>
    public static ItemData.Type GetSelectItem()
    {
        if (selectSlot == -1)
            return ItemData.Type.None;

        return itemList[selectSlot].ItemType;
    }

    /// <summary>선택중인 아이템 소모 </summary>
    public static void CostSelectItem()
    {
        if (selectSlot == -1)
            return;

        RemoveItem(selectSlot);
        selectSlot = -1;
        if (instance != null)
            instance.UpdateItemList();

        SaveInventory();
    }

    /// <summary>인벤토리 아이템 선택 취소</summary>
    public static void ItemDeselect()
    {
        selectSlot = -1;
        if (instance != null)
            instance.UpdateItemList();
    }

    /// <summary>아이템리스트 받기</summary>
    public static List<ItemData> GetItemList()
    {
        return itemList;
    }

    /// <summary>아이템리스트 넣기</summary>
    public static void SetItemList(List<ItemData> itemDatas)
    {
        ItemClear();
        foreach (ItemData item in itemDatas)
            itemList.Add(item);
    }

    /// <summary>해당 아이템의 이벤트를 실행(인벤토리 창에서 해당 아이템을 클릭하는 상황)</summary>
    public static void RunItemEvent(int idx)
    {
        if (idx < itemList.Count)
            RunItemEvent(itemList[idx].ItemType);
    }
    public static void RunItemEvent(ItemData.Type item)
    {
        if (itemEvent.ContainsKey(item))
            itemEvent[item].Invoke();
    }

    /// <summary>아이템에 이벤트 등록 </summary>
    public static void SetItemEvent(ItemData.Type item, Action action) => itemEvent[item] = action;

    public static void LoadInventory(PlayData playData)
    {
        ItemClear();
        List<ItemData.Type> types = new List<ItemData.Type>();
        foreach (ItemData.Type data in playData.items)
            itemList.Add(DataManager.GetItem(data));

        if (instance != null)
        {
            instance.UpdateItemList();
        }
    }

    private static void SaveInventory()
    {
        List<ItemData.Type> types = new List<ItemData.Type>();
        foreach (ItemData data in itemList)
            types.Add(data.ItemType);
        DataManager.playData.items = types;
        DataManager.SaveData();
    }
}
