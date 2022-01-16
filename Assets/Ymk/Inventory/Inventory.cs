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
            //�̹� ��ġ�Ǿ� �ִ� 6���� ���� �Ҵ�
            if(i < START_BOX)
                slotObjList.Add(content.GetChild(i).GetComponent<ItemSlot>());
            //������ �����ϴٸ� ������ �߰�
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

        //������ ��������ۺ��� ������ �߰�
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

    /// <summary>�κ��丮�� ��κ���</summary>
    public static void ItemClear()
    {
        itemList.Clear();
        if(instance != null)
            instance.UpdateItemList();
    }

    /// <summary>�κ��丮�� �������� �߰�</summary>
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

    /// <summary>Ư�� �ε��� ������ ����</summary>
    public static void RemoveItem(int idx)
    {
        itemList.RemoveAt(idx);
        if (instance != null)
            instance.UpdateItemList();
    }

    /// <summary>Ư�� ������ ����</summary>
    public static void RemoveItem(ItemData.Type item)
    {
        for (int i = 0; i < itemList.Count; i++)
            if (itemList[i].ItemType == item)
            {
                RemoveItem(i);
                return;
            }
    }

    /// <summary>Ư�� �ε��� �������� Ȯ��</summary>
    public static ItemData.Type GetItem(int idx)
    {
        return itemList[idx].ItemType;
    }

    /// <summary>�κ��丮 ������ ����</summary>
    public static void ItemSelect(int idx)
    {
        if (instance == null)
            return;

        if(idx < itemList.Count)
            selectSlot = idx;
        SoundManager.PlaySE(SoundManager.SE.item_Click);
        instance.UpdateItemList();
    }

    /// <summary>�������� ������ Ȯ�� </summary>
    public static ItemData.Type GetSelectItem()
    {
        if (selectSlot == -1)
            return ItemData.Type.None;

        return itemList[selectSlot].ItemType;
    }

    /// <summary>�������� ������ �Ҹ� </summary>
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

    /// <summary>�κ��丮 ������ ���� ���</summary>
    public static void ItemDeselect()
    {
        selectSlot = -1;
        if (instance != null)
            instance.UpdateItemList();
    }

    /// <summary>�����۸���Ʈ �ޱ�</summary>
    public static List<ItemData> GetItemList()
    {
        return itemList;
    }

    /// <summary>�����۸���Ʈ �ֱ�</summary>
    public static void SetItemList(List<ItemData> itemDatas)
    {
        ItemClear();
        foreach (ItemData item in itemDatas)
            itemList.Add(item);
    }

    /// <summary>�ش� �������� �̺�Ʈ�� ����(�κ��丮 â���� �ش� �������� Ŭ���ϴ� ��Ȳ)</summary>
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

    /// <summary>�����ۿ� �̺�Ʈ ��� </summary>
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
