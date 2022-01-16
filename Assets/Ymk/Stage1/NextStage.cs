using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStage : MonoBehaviour
{
    public int next;

    public void RunEvent()
    {
        if(next == 2)
        {
            next = 0;
            SceneManager.LoadScene(SceneManager.Scene.Story);
        }
        else if (next == 3)
        {
            next = 0;
            SceneManager.LoadScene(SceneManager.Scene.Ending);
        }
    }
}
