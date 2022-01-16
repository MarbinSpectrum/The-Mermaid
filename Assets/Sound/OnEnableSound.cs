using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnableSound : RunSE_Obj
{


    public void OnEnable()
    {
        RunSE();
        Destroy(this.gameObject);
    }
}
