using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicSheetEvent : MonoBehaviour
{
    public GameObject musicSheet;
    public GameObject starPoint;
    public GameObject overImg;

    private void Start()
    {
        Inventory.SetItemEvent(ItemData.Type.UnfinishedSheetMusic, ShowMusicSheet);
    }

    private void ShowMusicSheet() 
    {
        if(musicSheet.activeSelf)
        {
            musicSheet.SetActive(false);
        }
        else
        {
            musicSheet.SetActive(true);
            ResetSheet();
        }
    }

    public void ResetSheet()
    {
        for (int i = 0; i < 30; i++)
            Check[i] = false;
        ansCount = 0;
        spr.ForEach(x => x.color = new Color(1, 1, 1, 1));
    }

    public List<int> ans;
    public List<Image> spr;
    private bool[] Check = new bool[30];
    private int ansCount;

    public ItemData finishedMusicSheet;
    public ItemData strangeMusicSheet;

    private void Update()
    {
        overImg.SetActive(starPoint.activeSelf);
    }

    bool SolvePuzzle()
    {
        foreach (int a in ans)
            if (!Check[a])
                return false;
        return true;
    }

    public void SetCheck(int n)
    {
        if (Inventory.GetSelectItem() != ItemData.Type.MirrorPiece)
            return;
        if (Check[n])
            return;
        Check[n] = true;
        spr[n].color = new Color(1, 1, 1, 0);
        ansCount++;
        if(ansCount == 5)
        {
            Inventory.RemoveItem(ItemData.Type.UnfinishedSheetMusic);
            if (SolvePuzzle())
                Inventory.AddItem(finishedMusicSheet);
            else
                Inventory.AddItem(strangeMusicSheet);

            musicSheet.SetActive(false);

        }

    }
}
