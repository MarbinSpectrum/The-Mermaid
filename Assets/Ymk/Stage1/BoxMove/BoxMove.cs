using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BoxMove : MonoBehaviour
{
    bool state = false;
    bool flag = false;
    public Animation animation;
    public Button paper;
    public LoadOpenClose loadObj;

    public void RunEvent() => state = true;

    public void Update()
    {
        if(state && !flag)
        {
            state = false;
            flag = true;
            animation.Play();
            paper.enabled = true;
            StartCoroutine(SaveRun());
        }
    }

    IEnumerator SaveRun()
    {
        SoundManager.PlaySE(SoundManager.SE.box);
        yield return new WaitForSeconds(0.5f);
        loadObj.GetObject();


    }
}
