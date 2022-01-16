using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockImg : LockNumber
{
    public List<Sprite> sprites;
    public Image lockImg;

    public override void UpdateData(ref int n)
    {
        int size = sprites.Count;
        n = (n + size) % size;
        lockImg.sprite = sprites[n];
    }
}
