using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoFrame : OpenCloseObject
{
    public List<GameObject> checkObj;
    bool solved = false;
    private bool Check()
    {
        foreach (GameObject obj in checkObj)
            if (obj.activeSelf)
                return false;
        return true;
    }

    private void Update()
    {
        if (solved)
            return;
        if(Check())
        {
            solved = true;
            RunEvent();
        }
    }
    public virtual void RunEvent()
    {
        Open();
    }
}
