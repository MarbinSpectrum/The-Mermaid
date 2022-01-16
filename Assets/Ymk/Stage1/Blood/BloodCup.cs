using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodCup : MonoBehaviour
{
    private bool itemCheck()
    {
        for (int i = 0; i < transform.childCount; i++)
            if (transform.GetChild(i).gameObject.activeSelf)
                return false;
        return true;
    }

    public GameObject blood_ear;
    public LoadOpenClose earLoad;

    public GameObject blood_eye;
    public LoadOpenClose eyeLoad;

    public GameObject blood_tongue;
    public LoadOpenClose tongueLoad;

    public GameObject blood_finger;
    public LoadOpenClose fingerLoad;
    public void RunEvent()
    {
        if (!itemCheck())
            return;
        if(Inventory.GetSelectItem() == ItemData.Type.Ear)
        {
            Inventory.CostSelectItem();
            blood_ear.SetActive(true);
            earLoad.GetObject();
            SoundManager.PlaySE(SoundManager.SE.Blood_item);
        }
        else if (Inventory.GetSelectItem() == ItemData.Type.Eye)
        {
            Inventory.CostSelectItem();
            blood_eye.SetActive(true);
            eyeLoad.GetObject();
            SoundManager.PlaySE(SoundManager.SE.Blood_item);
        }
        else if (Inventory.GetSelectItem() == ItemData.Type.Toungue)
        {
            Inventory.CostSelectItem();
            blood_tongue.SetActive(true);
            tongueLoad.GetObject();
            SoundManager.PlaySE(SoundManager.SE.Blood_item);
        }
        else if (Inventory.GetSelectItem() == ItemData.Type.Finger)
        {
            Inventory.CostSelectItem();
            blood_finger.SetActive(true);
            fingerLoad.GetObject();
            SoundManager.PlaySE(SoundManager.SE.Blood_item);
        }
    }
}
