using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Data", menuName = "Scriptable Object/Item Data")]
public class ItemData : ScriptableObject
{

    public enum Type
    {
        None,
        Ear,
        Toungue,
        Eye,
        Finger,
        Ear_Blood,
        Toungue_Blood,
        Eye_Blood,
        Finger_Blood,
        Manual,
        Piece1,
        Piece2,
        Piece3,
        Piece4,
        Piece5,
        MirrorPiece,
        Matches,
        SmokedPortrait,
        UnfinishedSheetMusic,
        StrangeSheetMusic,
        FinishedSheetMusic,
        MusicBoxKey,
        Portrait,
        Diary,
        SafeKey,
        Approximately,
        TrueKey
    }

    public enum UseType
    {
        Consumable,
        Permanent
    }

    [SerializeField]
    private Type itemType;
    [SerializeField]
    private string itemName;
    [SerializeField]
    private Sprite itemSprite;
    [SerializeField]
    private string itemDesc;
    [SerializeField]
    private UseType itemUseType;

    public Type ItemType => itemType;
    public string ItemName => itemName;
    public Sprite ItemSprite => itemSprite;
    public string ItemDesc => itemDesc;
    public UseType ItemUseType => itemUseType;
}
