using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LockPanel : Singleton<LockPanel>
{
    public enum Version
    {
        Alphabet,
        Number
    }

    [SerializeField]
    private GameObject alphabet;
    [SerializeField]
    private GameObject number;
    [SerializeField]
    private LockKey[] keys;
    [SerializeField]
    private AudioSource audioSource;

    private Version version;
    private string answer;
    private Action onSuccess = null;

    private void Start()
    {
        Refresh();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Refresh()
    {
        for(int i = 0; i < keys.Length; i++)
        {
            keys[i].Init(i < 4 ? Version.Alphabet : Version.Number);
        }
    }

    public void Set(Version _version, string _answer, Action _OnSuccess, Sprite[] _hint = null)
    {
        if (_answer.Length != 4)
        {
            Debug.LogError("비밀번호 자리수를 확인해주세용");
            return;
        }

        version = _version;
        answer = _answer;
        onSuccess = _OnSuccess;

        int start = version == Version.Alphabet ? 0 : 4;

        gameObject.SetActive(true);

        if (_hint != null)
        {
            for (int i = 0; i < _hint.Length; i++)
            {
                keys[start + i].SetHint(_hint[i]);
            }
        }

        alphabet.SetActive(version == Version.Alphabet);
        number.SetActive(version == Version.Number);

    }

    public void SetOnClickEvent(Action _onClick)
    {
        GameObject go = version == Version.Alphabet ? alphabet : number;
        Button button = go.GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() =>
        {
            _onClick?.Invoke();
        });
    }

    public void CheckPassword()
    {
        int start = version == Version.Alphabet ? 0 : 4;
        for (int i = 0; i < 4; i++)
        {
            if (keys[start + i].key[0] != answer[i]) 
                return;
        }

        Debug.Log("자물쇠 해제");
        onSuccess?.Invoke();
        audioSource.Play();
    }
}
