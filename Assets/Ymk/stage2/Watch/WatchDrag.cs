using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WatchDrag : MonoBehaviour, IDragHandler
{

    RectTransform rectTransform;

    public int now = 12;
    [Range(0,60)]
    public int size = 12;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    float GetAngle(Vector2 start, Vector2 end)
    {
        Vector2 v2 = end - start;
        return Mathf.Atan2(v2.y, v2.x) * Mathf.Rad2Deg + 90;
    }

    public void SetAngle(int v)
    {
        int a = 360 / size;
        if (rectTransform == null)
            rectTransform = GetComponent<RectTransform>();
        rectTransform.eulerAngles = new Vector3(0, 0, 360 - v * a);
        now = v;
    }

    public void OnDrag(PointerEventData eventData)
    {
        float angle = GetAngle(eventData.position, transform.position);
        int a = 360 / size;

        angle = angle < 0 ? angle + 360 : angle;
        angle = (int)angle;
        angle /= a;
        angle = (int)angle;
        angle *= a;
        now = (360 - (int)angle)/a;
        rectTransform.eulerAngles = new Vector3(0, 0, angle);
    }
}
