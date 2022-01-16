using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FingerPuzzle : MonoBehaviour
{
    public static FingerPuzzle instance;

    private void Awake() => instance = this;

    public List<HandMove> fingerObj = new List<HandMove>();
    [HideInInspector]
    public HandMove enterFinger;
    [HideInInspector]
    public bool clear = false;

    public List<string> ans;
    public LoadOpenClose loadObj;


    bool SuccessCheck()
    {
        if (fingerObj.Count == 3)
        {
            for (int i = 0; i < 3; i++)
                if (fingerObj[i] == null)
                    return false;

            for (int i = 0; i < 3; i++)
                if (!fingerObj[i].name.Contains(ans[i]) )
                    return false;

            return true;

        }
        return false;
    }

    public List<BoxCollider2D> castObj;

    bool InBox(BoxCollider2D boxCollider2D)
    {
        if (boxCollider2D.transform.position.x - boxCollider2D.size.x / 2f <= Input.mousePosition.x &&
            Input.mousePosition.x <= boxCollider2D.transform.position.x + boxCollider2D.size.x / 2f)
            if (boxCollider2D.transform.position.y - boxCollider2D.size.y / 2f <= Input.mousePosition.y &&
    Input.mousePosition.y <= boxCollider2D.transform.position.y + boxCollider2D.size.y / 2f)
                return true;
        return false;
    }

    public Animation animation;

    private int nowBox = -1;

    private void Update()
    {
        if (loadObj.act)
            clear = true;
        if (clear)
            return;

        int boxCheck = -1;
        for (int i = 0; i < 3; i++)
            if (InBox(castObj[i]))
            {
                boxCheck = i;
                break;
            }

        nowBox = boxCheck;

        if (HandMove.nowFinger && enterFinger == null)
        {
            enterFinger = HandMove.nowFinger;
            enterFinger.nowPos = nowBox;
        }
        else if (enterFinger != null && enterFinger.nowPos != nowBox)
        {
            for (int i = 0; i < 3; i++)
                if (fingerObj[i] && fingerObj[i].transform.name == enterFinger.transform.name)
                {
                    fingerObj[i] = null;
                    break;
                }
        }

        if (Input.GetMouseButtonUp(0))
            if (enterFinger)
            {
                if(nowBox >= 0)
                {
                    if (fingerObj[nowBox] != null)
                    {
                        if(enterFinger.nowPos == -1)
                        {
                            fingerObj[nowBox].transform.position = enterFinger.start;
                            fingerObj[nowBox] = enterFinger;
                            enterFinger.nowPos = nowBox;
                        }
                        else
                        {
                            fingerObj[enterFinger.nowPos] = fingerObj[nowBox];
                            fingerObj[nowBox].nowPos = enterFinger.nowPos;

                        }
                    }

                    enterFinger.nowPos = nowBox;
                    fingerObj[nowBox] = enterFinger;

                    for (int i = 0; i < 3; i++)
                        if (fingerObj[i])
                            fingerObj[i].transform.position = castObj[i].transform.position;
                }

                enterFinger = null;
            }

        if(!clear && SuccessCheck())
        {
            clear = true;
            StartCoroutine(RunEvent());
        }
    }




    IEnumerator RunEvent()
    {
        animation.Play();
        yield return new WaitForSeconds(1f);
        loadObj.GetObject();


    }
}
