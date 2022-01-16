using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BlinkHint : MonoBehaviour
{
    public List<Animation> animations;

    public void OnEnable()
    {
        for (int i = 0; i < animations.Count; i++)
            animations[i].GetComponent<Image>().color = new Color(1, 1, 1, 0);
        for (int i = 0; i < animations.Count; i++)
            animations[i].Rewind();
        for (int i = 0; i < animations.Count; i++)
            animations[i].GetComponent<CanvasGroup>().alpha = 0;
    }

    public void RunBlink()
    {
        //for (int i = 0; i < animations.Count; i++)
        //    if (animations[i] && animations[i].isPlaying)
        //        return;

        SoundManager.PlaySE(SoundManager.SE.UI_Select);

        for (int i = 0; i < animations.Count; i++)
            if(animations[i])
            animations[i].Play();
    }

}
