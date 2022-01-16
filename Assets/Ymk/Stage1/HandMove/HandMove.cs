using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HandMove : MonoBehaviour, IBeginDragHandler,IDragHandler,IDropHandler,IPointerUpHandler,IPointerDownHandler
{
    public RectTransform table;
    public static HandMove nowFinger;
    [System.NonSerialized]
    public int nowPos = -1;
    [System.NonSerialized]
    public Vector2 start;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (FingerPuzzle.instance.clear)
            return;
        start = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (FingerPuzzle.instance.clear)
            return;
        nowFinger = this;

        float xv = Screen.width / 2560f;
        float maxX = table.transform.position.x + table.sizeDelta.x* xv / 2f;
        float minX = table.transform.position.x - table.sizeDelta.x * xv / 2f;

        float yv = Screen.height / 1440f;
        float maxY = table.transform.position.y + table.sizeDelta.y * yv / 2f;
        float minY = table.transform.position.y - table.sizeDelta.y * yv / 2f;

        transform.position = eventData.position;
        transform.position = new Vector3(
            Mathf.Max(minX,Mathf.Min(maxX, transform.position.x)),
              Mathf.Max(minY, Mathf.Min(maxY, transform.position.y)), 
            transform.position.z);
        GetComponent<Image>().raycastTarget = false;
    }

    public void OnDrop(PointerEventData eventData)
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        nowFinger = null;

        GetComponent<Image>().raycastTarget = true;
    }
}
