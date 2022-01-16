using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : DontDestroy<TouchManager>
{
    public static GameObject selectObj;

    ////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////

    private void Update()
    {
        RunTouch();
    }

    ////////////////////////////////////////////////////////////////////////////////////////////

    private void RunTouch()
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
        else if (Input.GetMouseButtonUp(0))
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
