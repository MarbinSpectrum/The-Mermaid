using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Clothes : MonoBehaviour, IDragHandler,IEndDragHandler
{
    public int nowClothes;
    public static int nowDrag = -1;

    public void OnDrag(PointerEventData eventData)
    {
        if (WardrobePuzzle.instance.clear)
            return;

        float x = eventData.position.x;
        x = Mathf.Max(x, WardrobePuzzle.minX - WardrobePuzzle.instance.dis * 0.5f);
        x = Mathf.Min(x, WardrobePuzzle.maxX + WardrobePuzzle.instance.dis * 0.5f);

        nowDrag = nowClothes;
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
        transform.SetAsLastSibling();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        nowDrag = -1;
    }
}
