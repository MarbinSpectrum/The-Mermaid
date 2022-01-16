using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockLoade : MonoBehaviour
{
    public WatchDrag sh;
    public WatchDrag sm;
    public WatchDrag m;
    public WatchDrag h;
    public WatchPuzzle puzzle;
    public GameObject hint;
    public void OnEnable()
    {
        var data = DataManager.playData;
        if (data != null)
        {
            sh.SetAngle(data.watchState.x);
            sm.SetAngle(data.watchState.y);

            h.SetAngle(data.watchState.x);
            m.SetAngle(data.watchState.y);

            puzzle.flag = data.watchState.z;

            if(hint)
                hint.SetActive(puzzle.flag != 2);
        }
    }
}
