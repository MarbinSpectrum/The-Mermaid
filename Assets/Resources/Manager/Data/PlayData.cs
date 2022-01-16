using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayData
{
    public bool played = false;


    public int nowStage = 0;
    public string nowRoom = "Room1-1";
    public List<ItemData.Type> items = new List<ItemData.Type>();
    public List<string> ActObject = new List<string>();

    public Vector3Int watchState;
    public bool clearPhotoPuzzle;
    public bool GameClear = false;
    public bool muteSE = false;
    public bool muteBGM = false;
    public float seValue = 0.5f;
    public float bgmValue = 0.5f;

    public Language language = Language.ÇÑ±¹¾î;
}
