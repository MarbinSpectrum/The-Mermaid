using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PrintText : MonoBehaviour
{
    [SerializeField]
    private Animation animation;
    [SerializeField]
    //private TextMeshProUGUI text;
    private Text text;
    public static PrintText instance;
    private void Awake() => instance = this;

    public static void Print(string s)
    {
        instance.animation.Rewind();
        instance.animation.Play();
        instance.text.text = s;
    }
}
