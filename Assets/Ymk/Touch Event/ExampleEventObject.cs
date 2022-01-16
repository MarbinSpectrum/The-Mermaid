using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleEventObject : EventObject
{
    public override void RunEvent()
    {
        Destroy(gameObject);
    }
}
