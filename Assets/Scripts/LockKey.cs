using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockKey : MonoBehaviour
{
    [SerializeField]
    private Image hintImage;
    [SerializeField]
    private Text keyText;
    [SerializeField]
    private AudioSource clickSource;

    private LockPanel.Version version;
    public string key => keyText.text;

    public void Init(LockPanel.Version _version)
    {
        version = _version;
        Refresh();
    }

    public void SetHint(Sprite sprite)
    {
        hintImage.enabled = true;
        hintImage.sprite = sprite;
    }

    public void Refresh()
    {
        keyText.text = version == LockPanel.Version.Alphabet ? "A" : "0";
        hintImage.enabled = version == LockPanel.Version.Alphabet; ;
    }

    public void OnUpButton()
    {
        clickSource.Play();

        if (version == LockPanel.Version.Alphabet)
        {
            int code = 'A' + (keyText.text[0] + 1 - 'A') % 26;
            keyText.text = ((char)code).ToString();
        }
        else
        {
            int code = (int.Parse(keyText.text) + 1) % 10;
            keyText.text = code.ToString();
        }
        
        LockPanel.instance.CheckPassword();
    }

    public void OnDownButton()
    {
        clickSource.Play();

        if(version == LockPanel.Version.Alphabet)
        {
            int code = 'A' + (keyText.text[0] - 1 - 'A') % 26;
            if (code < 'A') code = 'Z';
            keyText.text = ((char)code).ToString();
        }
        else
        {
            int code = int.Parse(keyText.text) - 1;
            if (code < 0) code = 9;
            keyText.text = code.ToString();
        }

        LockPanel.instance.CheckPassword();
    }
}
