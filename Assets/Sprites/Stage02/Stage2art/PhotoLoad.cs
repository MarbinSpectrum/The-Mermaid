using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoLoad : MonoBehaviour
{
    public PhotoDragPuzzle PhotoDragPuzzle;

    public void OnEnable()
    {
        var data = DataManager.playData;
        if (data != null)
        {
            if(data.clearPhotoPuzzle)
            {
                PhotoDragPuzzle.SortAns();
            }
        }
    }
}
