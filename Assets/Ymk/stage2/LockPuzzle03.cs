using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPuzzle03 : OpenCloseObject
{
    [SerializeField]
    private List<LockNumber> lockNumbers = new List<LockNumber>();
    [SerializeField]
    private string answer;
    [SerializeField]
    private MoveCamera moveRoom;

    private bool success = false;
    public GameObject disObj;

    public LoadOpenClose loadObj;

    private void Start()
    {
        foreach (LockNumber lockObj in lockNumbers)
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
            loadObj.GetObject();
            StartCoroutine(RunEvent());
        }
    }

    IEnumerator RunEvent()
    {
        SoundManager.PlaySE(SoundManager.SE.open);
        foreach (LockNumber lockObj in lockNumbers)
            lockObj.enabled = false;
        disObj.SetActive(false);
        yield return new WaitForSeconds(1f);
        SoundManager.PlaySE(SoundManager.SE.Drawer_Open );
        moveRoom.MoveRoom(false);
        Open();
        disObj.SetActive(true);
    }
}
