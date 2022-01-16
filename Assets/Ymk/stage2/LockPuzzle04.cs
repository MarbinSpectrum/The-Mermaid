using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPuzzle04 : OpenCloseObject
{
    [SerializeField]
    private List<LockNumber> lockNumbers = new List<LockNumber>();
    [SerializeField]
    private string answer;

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
            res += lockObj.lockNum.ToString();

        if (res == answer)
        {
            success = true;
            StartCoroutine(RunEvent());
        }
    }

    IEnumerator RunEvent()
    {
        SoundManager.PlaySE(SoundManager.SE.safe);
        foreach (LockNumber lockObj in lockNumbers)
            lockObj.enabled = false;
        disObj.SetActive(false);
        yield return new WaitForSeconds(1f);
        loadObj.GetObject();
        Open();
        disObj.SetActive(true);
    }
}
