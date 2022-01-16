using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseObject : MonoBehaviour
{
    public List<GameObject> OpenObject;
    public List<GameObject> CloseObject;

    public void Open()
    {
        CloseObject.ForEach(x => x.SetActive(false));
        OpenObject.ForEach(x => x.SetActive(true));
    }

    public void Close()
    {
        CloseObject.ForEach(x => x.SetActive(true));
        OpenObject.ForEach(x => x.SetActive(false));
    }
}
