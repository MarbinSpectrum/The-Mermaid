using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchPuzzle : LoadOpenClose
{
    public WatchDrag m;
    public WatchDrag h;
    public GameObject sm;
    public GameObject sh;

    private bool solved = false;

    [Range(1, 12)]
    public int ansH = 6;
    [Range(1, 60)]
    public int ansM = 21;

    public int flag = 0;

    int ph = -1;
    int pm = -1;

    public override void Update()
    {
        base.Update();
        if(act)
            solved = true;

        if (solved)
        {
            h.enabled = false;
            m.enabled = false;

            return;
        }

        sm.transform.eulerAngles = m.transform.eulerAngles;
        sh.transform.eulerAngles = h.transform.eulerAngles;

        if(ph != h.now || pm != m.now)
        {
            if(ph != -1)
            {
                SoundManager.PlaySE(SoundManager.SE.Clock);
            }
            ph = h.now;
            pm = m.now;
            DataManager.playData.watchState.y = pm;
            DataManager.playData.watchState.x = ph;
            DataManager.playData.watchState.z = flag;
            DataManager.SaveData();
        }

        if (h.now == 2 && m.now == 9 && flag == 0)
        {
            flag++;
            SoundManager.PlaySE(SoundManager.SE.clockEvent);
        }
        else if (h.now == ansH && m.now == ansM && flag == 1)
        {
            flag++;
            GetObject();
            m.enabled = false;
            h.enabled = false;
            solved = true;
            SoundManager.PlaySE(SoundManager.SE.clockEvent);
            SoundManager.PlaySE(SoundManager.SE.Drop_Ladder);
        }
    }
}
