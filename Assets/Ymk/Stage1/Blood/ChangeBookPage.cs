using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBookPage : MonoBehaviour
{
    public Book book;
    public AutoFlip autoFlip;

    private bool clear = false;
    public ItemData manualBook;
    public GameObject removeBook;
    public GameObject removeBook2;
    public GameObject disObj;
    public LoadOpenClose loadObj;
    public List<Sprite> defPage_kor;
    public List<Sprite> newPage_kor;
    public List<Sprite> defPage_eng;
    public List<Sprite> newPage_eng;

    private bool[] stamp = new bool[5];

    public void BookPageSlot1()
    {
        if (autoFlip.isFlipping)
            return;
        if (book.currentPage == 2 && Inventory.GetSelectItem() == ItemData.Type.Toungue_Blood)
        {
            Inventory.CostSelectItem();
            stamp[2] = true;
            SavePage("스테이지1_책_혓바닥_페이지");
            SoundManager.PlaySE(SoundManager.SE.stamp);
        }
        else if (book.currentPage == 4 && Inventory.GetSelectItem() == ItemData.Type.Eye_Blood)
        {
            Inventory.CostSelectItem();
            stamp[4] = true;
            SavePage("스테이지1_책_눈_페이지");
            SoundManager.PlaySE(SoundManager.SE.stamp);
        }
        UpdataPage(nowLanguage);
    }

    public void BookPageSlot2()
    {
        if (autoFlip.isFlipping)
            return;
        if (book.currentPage == 2 && Inventory.GetSelectItem() == ItemData.Type.Ear_Blood)
        {
            Inventory.CostSelectItem();
            stamp[1] = true;

            SavePage("스테이지1_책_귀_페이지");
            SoundManager.PlaySE(SoundManager.SE.stamp);
        }
        else if (book.currentPage == 4 && Inventory.GetSelectItem() == ItemData.Type.Finger_Blood)
        {
            Inventory.CostSelectItem();
            stamp[3] = true;

            SavePage("스테이지1_책_손가락_페이지");
            SoundManager.PlaySE(SoundManager.SE.stamp);
        }
        UpdataPage(nowLanguage);
    }

    float time = 0;

    private void Start()
    {
        var data = DataManager.playData;


        if (data.ActObject.Contains("스테이지1_책_귀_페이지"))
        {
            stamp[1] = true;
        }
        if (data.ActObject.Contains("스테이지1_책_손가락_페이지"))
        {
            stamp[3] = true;
        }
        if (data.ActObject.Contains("스테이지1_책_혓바닥_페이지"))
        {
            stamp[2] = true;
        }
        if (data.ActObject.Contains("스테이지1_책_눈_페이지"))
        {
            stamp[4] = true;
        }
        if (DataManager.playData != null)
            UpdataPage(DataManager.playData.language);
    }

    public bool PuzzleClear()
    {
        for (int i = 1; i <= 4; i++)
            if (!stamp[i])
                return false;
        return true;
    }

    private Language nowLanguage = Language.Count;

    private void UpdataPage(Language language)
    {
        if (language == Language.한국어)
        {
            for (int i = 0; i <= 4; i++)
                book.bookPages[i] = stamp[i] ? newPage_kor[i] : defPage_kor[i];
            book.UpdateSprites();

        }
        else if (language == Language.English)
        {
            for (int i = 0; i <= 4; i++)
                book.bookPages[i] = stamp[i] ? newPage_eng[i] : defPage_eng[i];
            book.UpdateSprites();
        }
    }

    public void Update()
    {
        if (loadObj.act)
            clear = true;

        if (DataManager.playData != null)
        {
            Language language = DataManager.playData.language;
            if (nowLanguage != language)
            {
                nowLanguage = language;
                UpdataPage(nowLanguage);
            }
        }


        if (!clear)
        {
            if (PuzzleClear())
            {
                time += Time.deltaTime;
                disObj.SetActive(clear);
            }
            else
                disObj.SetActive(!autoFlip.isFlipping);
        }

        if (PuzzleClear() && !clear && time > 2f)
        {
            clear = true;
            Inventory.AddItem(manualBook);
            disObj.SetActive(true);
            removeBook.gameObject.SetActive(false);
            removeBook2.gameObject.SetActive(false);
            loadObj.GetObject();
        }
    }

    public void SavePage(string pageKey)
    {
        var data = DataManager.playData;
        if (!data.ActObject.Contains(pageKey))
            data.ActObject.Add(pageKey);
        DataManager.SaveData();
    }
}
