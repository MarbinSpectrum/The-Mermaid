using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SetScreenSize : MonoBehaviour
{
    public RectTransform transform;

    // Update is called once per frame
    void Update()
    {
        if(transform)
            transform.sizeDelta = new Vector2(Screen.width, Screen.height);
    }
}
