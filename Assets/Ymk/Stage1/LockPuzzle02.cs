using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPuzzle02 : MoveCamera
{
    [SerializeField]
    private List<LockNumber> lockNumbers = new List<LockNumber>();
    [SerializeField]
    private string answer;
    [SerializeField]
    private Transform moveRoom;
    [SerializeField]
    private Transform lockObj;
    [SerializeField]
    private Transform actObj;
    private bool success = false;
    public GameObject disObj;
    public LoadOpenClose loadObj;

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
        SoundManager.PlaySE(SoundManager.SE.open);
        disObj.SetActive(false);
        foreach (LockNumber lockObj in lockNumbers)
            lockObj.enabled = false;
          yield return new WaitForSeconds(1f);
        SoundManager.PlaySE(SoundManager.SE.open_box);
        MoveRoom(false);
        lockObj.gameObject.SetActive(false);
        actObj.gameObject.SetActive(true);
        loadObj.GetObject();

    }
}
