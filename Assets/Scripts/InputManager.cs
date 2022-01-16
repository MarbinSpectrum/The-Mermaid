using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class InputManager : Singleton<InputManager>, IPointerDownHandler, IPointerUpHandler
{

    private static GameObject donDestroyObj;
    private static GameObject selectObj;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.touchCount == 1)
            {
                Vector2 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                Ray2D ray = new Ray2D(wp, Vector2.zero);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

                if (hit)
                    selectObj = hit.collider.gameObject;
                else
                    selectObj = null;
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Ray2D ray = new Ray2D(wp, Vector2.zero);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

                if (hit)
                    selectObj = hit.collider.gameObject;
                else
                    selectObj = null;
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.touchCount == 1)
            {
                Vector2 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                Ray2D ray = new Ray2D(wp, Vector2.zero);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

                if (hit && selectObj == hit.collider.gameObject)
                    if (selectObj != null)
                    {
                        EventObject eventObject = selectObj.GetComponent<EventObject>();
                        if (eventObject != null)
                            eventObject.RunEvent();
                    }

                selectObj = null;
            }
        }
        else
        {
            if (Input.GetMouseButtonUp(0))
            {
                Vector2 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Ray2D ray = new Ray2D(wp, Vector2.zero);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

                if (hit && selectObj == hit.collider.gameObject)
                    if (selectObj != null)
                    {
                        EventObject eventObject = selectObj.GetComponent<EventObject>();
                        if (eventObject != null)
                            eventObject.RunEvent();
                    }

                selectObj = null;
            }
        }
    }
}
