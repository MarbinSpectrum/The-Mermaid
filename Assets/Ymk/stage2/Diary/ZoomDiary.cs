using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomDiary : MonoBehaviour
{
    public GameObject diary;

    private bool runEvant = false;
    public AutoFlip autoFlip;
    public Book book;
    public GameObject donClick;
    private bool eventOn = false;
    public OneTimeText oneTimeText;

    private void Start()
    {
        Inventory.SetItemEvent(ItemData.Type.Diary, ShowDiary);
    }

    public void ShowDiary()
    {
        if (autoFlip.isFlipping)
            return;

        if (diary.activeSelf)
        {
            diary.SetActive(false);
            if (eventOn)
            {
                eventOn = false;
                if(oneTimeText)
                oneTimeText.Print();
            }
        }
        else
        {
            donClick.SetActive(false);
            diary.SetActive(true);
        }
    }

    private void Update()
    {
        if (!runEvant)
        {
            if (book.currentPage == 6)
            {
                runEvant = true;
                eventOn = true;
            }
        }
    }


}
