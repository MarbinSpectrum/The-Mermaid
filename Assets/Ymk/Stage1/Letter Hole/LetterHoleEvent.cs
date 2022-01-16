using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class LetterHoleEvent : MonoBehaviour
{
    public GameObject mainGameObj;
    public GameObject event01;
    public GameObject event02;
    public GameObject paper;
    public bool eventEnd = false;
    public LoadOpenClose loadObj;

    public void RunEvent()
    {
        if (eventEnd)
            return;
        SoundManager.PlaySE(SoundManager.SE.Letter_Hole);
        if(Inventory.GetSelectItem() != ItemData.Type.Manual)
        {
            mainGameObj.SetActive(false);
            event01.SetActive(true);
            event01.GetComponent<PlayableDirector>().Play();
            event01.GetComponent<PlayableDirector>().stopped += ActThis;
            event01.GetComponent<PlayableDirector>().stopped += UnActThisEvent01;
        }
        else
        {
            Inventory.CostSelectItem();
            mainGameObj.SetActive(false);
            event02.SetActive(true);
            event02.GetComponent<PlayableDirector>().Play();
            event02.GetComponent<PlayableDirector>().stopped += ActThis;
            event02.GetComponent<PlayableDirector>().stopped += UnActThisEvent02;
        }
    }

    public void OnEnable()
    {
        event01.GetComponent<PlayableDirector>().stopped -= ActThis;
        event01.GetComponent<PlayableDirector>().stopped -= UnActThisEvent01;
        event02.GetComponent<PlayableDirector>().stopped -= ActThis;
        event02.GetComponent<PlayableDirector>().stopped -= UnActThisEvent02;
    }

    public void UnActThisEvent01(PlayableDirector director)
    {
        event01.SetActive(false);
    }

    public void UnActThisEvent02(PlayableDirector director)
    {
        event02.SetActive(false);
        paper.SetActive(true);
        eventEnd = true;
        loadObj.GetObject();
    }

    public void ActThis(PlayableDirector director)=> mainGameObj.SetActive(true);
 
}
