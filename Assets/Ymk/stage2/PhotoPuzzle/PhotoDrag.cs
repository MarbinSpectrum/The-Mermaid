using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PhotoDrag : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public int nowClothes;
    public static int nowDrag = -1;

    public void OnDrag(PointerEventData eventData)
    {
        if (PhotoDragPuzzle.instance.clear)
            return;

        float x = eventData.position.x;
        x = Mathf.Max(x, PhotoDragPuzzle.minX - PhotoDragPuzzle.instance.dis * 0.5f);
        x = Mathf.Min(x, PhotoDragPuzzle.maxX + PhotoDragPuzzle.instance.dis * 0.5f);

        nowDrag = nowClothes;
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
        transform.SetAsLastSibling();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        nowDrag = -1;
    }
}
