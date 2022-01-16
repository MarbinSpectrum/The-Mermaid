using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class EventObject : MonoBehaviour
{
    public bool isUsed;

    public virtual void Load()
    {

    }

    public abstract void RunEvent();
}
