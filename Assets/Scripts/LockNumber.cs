using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LockNumber : MonoBehaviour
{
    public TextMeshProUGUI text;
    public bool isDigit = false;
    private int LockNum;
    public int lockNum
    {
        get
        {
            return LockNum;
        }
        set
        {
            UpdateData(ref value);
            LockNum = value;
        }
    }

    public virtual void UpdateData(ref int n)
    {
        if (isDigit)
        {
            n = (n + 10) % 10;
            text.text = ((char)(n + '0')).ToString();
        }
        else
        {
            n = (n + 26) % 26;
            text.text = ((char)(n + 'A')).ToString();
        }
    }

    public void AddLockNum(int add)
    { 
        if(this.enabled)
            lockNum += add;
        SoundManager.PlaySE(SoundManager.SE.Dial);
    }
}
