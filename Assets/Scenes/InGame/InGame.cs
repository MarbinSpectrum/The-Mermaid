using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGame : MonoBehaviour
{
    

    private void Start()
    {
        UIManager.EnableSetting(true, UIManager.UI.InGame);
        UIManager.EnableSetting(false, UIManager.UI.Setting);

        DataManager.LoadData();
        var data = DataManager.playData;

        data.played = true;

        //��������
        Transform nowStage = transform.GetChild(data.nowStage);

        //����ġ
        nowStage.gameObject.SetActive(true);
        MoveCamera moveCamera = new MoveCamera();
        moveCamera.target = nowStage.Find(data.nowRoom);
        moveCamera.target.gameObject.SetActive(true);
        moveCamera.LoadRoom(false);

        //�κ��丮
        Inventory.LoadInventory(data);

        DataManager.SaveData();






        SoundManager.StopBGM();
    }


}
