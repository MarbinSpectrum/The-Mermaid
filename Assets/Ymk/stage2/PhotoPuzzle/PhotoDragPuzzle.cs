using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoDragPuzzle : MonoBehaviour
{
    public static PhotoDragPuzzle instance;
    public static float minX = 0;
    public static float maxX = 0;

    [System.NonSerialized]
    public bool clear = false;
    public GameObject center;
    public List<PhotoDrag> photoObj;
    public float dis;
    public float small_dis;
    public GameObject smallcenter;
    public List<GameObject> small_photoObj;
    public LoadOpenClose loadObj;

    private void Awake() => instance = this;

    public void Update()
    {
        SortAns();
        ClothesSort();
        ClothesPuzzle();
    }

    public void SortAns()
    {
        if (loadObj.act && !clear)
        {
            clear = true;
            {
                float newDis = (((float)Screen.height / (float)1440) * (float)2560 / (float)2560) * dis;
                Vector2 v = center.transform.position;
                v.x -= (newDis * (photoObj.Count - 1)) * 0.5f;
                for (int j = 0; j < photoObj.Count; j++)
                    for (int i = 0; i < photoObj.Count; i++)
                        if (photoObj[i].nowClothes == j)
                        {
                            photoObj[i].transform.position = v;
                            v.x += newDis;
                        }
            }
            photoObj.Sort((x, y) => x.transform.position.x.CompareTo(y.transform.position.x));
            {
                float small_newDis = (((float)Screen.height / (float)1440) * (float)2560 / (float)2560) * small_dis;
                Vector2 v = smallcenter.transform.position;
                v.x -= (small_newDis * (photoObj.Count - 1)) * 0.5f;
                for (int i = 0; i < photoObj.Count; i++)
                {
                    small_photoObj[photoObj[i].nowClothes].transform.position = v;
                    v.x += small_newDis;
                }
            }
        }
    }

    private void ClothesSort()
    {
        if (clear)
            return;
        float newDis = (((float)Screen.height / (float)1440) * (float)2560 / (float)2560) * dis;
        photoObj.Sort((x, y) => x.transform.position.x.CompareTo(y.transform.position.x));
        {
            Vector2 v = center.transform.position;
            v.x -= (newDis * (photoObj.Count - 1)) * 0.5f;
            minX = v.x;
            for (int i = 0; i < photoObj.Count; i++)
            {
                if (photoObj[i].nowClothes != PhotoDrag.nowDrag)
                    photoObj[i].transform.position = v;
                maxX = v.x;
                v.x += newDis;
            }
        }

        float small_newDis = (((float)Screen.height / (float)1440) * (float)2560 / (float)2560) * small_dis;
        {
            Vector2 v = smallcenter.transform.position;
            v.x -= (small_newDis * (photoObj.Count - 1)) * 0.5f;
            for (int i = 0; i < photoObj.Count; i++)
            {
                small_photoObj[photoObj[i].nowClothes].transform.position = v;
                v.x += small_newDis;
            }

        }

    }

    private bool ClearCheck()
    {
        for (int i = 0; i < photoObj.Count; i++)
            if (photoObj[i].nowClothes != i)
                return false;
        return true;
    }
    private void ClothesPuzzle()
    {
        if (clear)
            return;
        if (ClearCheck())
        {
            clear = true;
            loadObj.GetObject();
        }
    }

}
