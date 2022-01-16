using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunSE_Obj : MonoBehaviour
{
    public SoundManager.SE se;

    public void RunSE()
    {
        SoundManager.PlaySE(se);    
    }
}
