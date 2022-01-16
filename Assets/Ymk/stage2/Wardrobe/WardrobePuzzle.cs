using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardrobePuzzle : MonoBehaviour
{
    public static WardrobePuzzle instance;
    public static float minX = 0;
    public static float maxX = 0;

    [System.NonSerialized]
    public bool clear = false;
    public GameObject center;
    public GameObject messageObj;
    public List<Clothes> clothesObj;
    public float dis;
    public LoadOpenClose loadObj;


    private void Awake() => instance = this;

    public void Update()
    {
        if(loadObj.act && !clear)
        {
            clear = true;
            float newDis = (((float)Screen.height / (float)1440) * (float)2560 / (float)2560) * dis;
            Vector2 v = center.transform.position;
            v.x -= (newDis * (clothesObj.Count - 1)) * 0.5f;
            for (int j = 0; j < clothesObj.Count; j++)
                for (int i = 0; i < clothesObj.Count; i++)
                    if (clothesObj[i].nowClothes == j)
                    {
                        clothesObj[i].transform.position = v;
                        v.x += newDis;
                    }
            messageObj.SetActive(true);
        }

        ClothesSort();
        ClothesPuzzle();
    }

    private List<int> flagArr = new List<int>();
    void ChangeArr()
    {

        List<int> temp = new List<int>();
        for (int i = 0; i < clothesObj.Count; i++)
            temp.Add(clothesObj[i].nowClothes);


        if (flagArr.Count > 0)
        {
            for(int i = 0; i < temp.Count; i++)
                if(flagArr[i] != temp[i])
                {
                    SoundManager.PlaySE(SoundManager.SE.Clothesincloset);
                    break;
                }
        }

        flagArr = temp;

    }

    private void ClothesSort()
    {
        if (clear)
            return;

        float newDis = (((float)Screen.height / (float)1440) * (float)2560 / (float)2560) * dis;

        clothesObj.Sort((x, y) => x.transform.position.x.CompareTo(y.transform.position.x));
        ChangeArr();

        {
            Vector2 v = center.transform.position;
            v.x -= (newDis * (clothesObj.Count - 1)) * 0.5f;
            minX = v.x;
            for (int i = 0; i < clothesObj.Count; i++)
            {
                if (clothesObj[i].nowClothes != Clothes.nowDrag)
                    clothesObj[i].transform.position = v;
                maxX = v.x;
                v.x += newDis;
            }
        }
    }
    private bool ClearCheck()
    {
        for (int i = 0; i < clothesObj.Count; i++)
            if (clothesObj[i].nowClothes != i)
                return false;
        return true;
    }
    private void ClothesPuzzle()
    {
        if (clear)
            return;
        if(ClearCheck())
        {
            Solved();
            clear = true;
        }
    }

    private void Solved()
    {
        loadObj.GetObject();
        messageObj.SetActive(true);
        messageObj.GetComponent<Animation>().Play();
    }
}
