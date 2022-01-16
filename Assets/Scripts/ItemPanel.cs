using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPanel : Singleton<ItemPanel>
{
    [SerializeField]
    private Image image;
    [SerializeField]
    private GameObject textGroup;
    [SerializeField]
    private Text text;

    public void Show(Sprite sprite)
    {
        image.gameObject.SetActive(true);
        textGroup.SetActive(false);
        image.sprite = sprite;
        gameObject.SetActive(true);
    }

    public void Show()
    {
        image.gameObject.SetActive(false);
        textGroup.SetActive(true);
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
