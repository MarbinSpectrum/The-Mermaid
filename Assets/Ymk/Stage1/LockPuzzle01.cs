using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPuzzle01 : MoveCamera
{
    [SerializeField]
    private List<LockNumber> lockNumbers = new List<LockNumber>();
    [SerializeField]
    private string answer;
    [SerializeField]
    private Transform lockObj;

    public GameObject actObj;
    public GameObject disObj;

    public LoadOpenClose loadObj;

    private bool success = false;

    private void Start()
    {
        foreach(LockNumber lockObj in lockNumbers)
            lockObj.lockNum = 0;
    }

    private void Update()
    {
        if (success)
            return;
        string res = "";
        foreach (LockNumber lockObj in lockNumbers)
            res += lockObj.text.text;
        if (res == answer)
        {
            success = true;
            StartCoroutine(RunEvent());
        }
    }



    IEnumerator RunEvent()
    {
        foreach (LockNumber lockObj in lockNumbers)
            lockObj.enabled = false;
        disObj.SetActive(false);
        SoundManager.PlaySE(SoundManager.SE.open);
        if (DataManager.playData != null)
        {
            if (DataManager.playData.language == Language.한국어)
                PrintText.Print("노아...?");
            else if (DataManager.playData.language == Language.English)
                PrintText.Print("NOAH...?");
        }
        yield return new WaitForSeconds(3f);
        SoundManager.PlaySE(SoundManager.SE.Door_open);
        MoveRoom(false);

        lockObj.gameObject.SetActive(false);
        loadObj.GetObject();
        actObj.SetActive(true);

    }


}
