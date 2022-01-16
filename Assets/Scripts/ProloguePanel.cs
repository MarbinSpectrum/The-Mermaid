using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProloguePanel : Singleton<ProloguePanel>
{
    [SerializeField]
    private Text monologue;
    [SerializeField]
    private GameObject illustration;
    [SerializeField]
    private Image image;
    [SerializeField]
    private Text desc;
    [SerializeField] 
    private AudioSource typingSoure;

    [SerializeField]
    private PrologueScript prologue;
    [SerializeField]
    private PrologueScript middle0;
    [SerializeField]
    private PrologueScript middle1;
    [SerializeField]
    private PrologueScript ending;

    public enum Type
    {
        Begin,
        Middle0,
        Middle1,
        Ending
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public Coroutine ShowPrologue(Type type)
    {
        PrologueScript script = null;
        if (type == Type.Begin)
            script = prologue;
        else if (type == Type.Middle0)
            script = middle0;
        else if (type == Type.Middle1)
            script = middle1;
        else if (type == Type.Ending)
            script = ending;

        return StartCoroutine(ShowPrologue(script));
    }

    IEnumerator ShowPrologue(PrologueScript script)
    {
        monologue.text = desc.text = "";

        string[] prologueStrings = script.strs;
        Sprite[] sprites = script.sprites;

        Text prologueText = null;

        for (int i = 0; i < prologueStrings.Length; i++)
        {
            bool isMonologue = sprites[i] == null;
            monologue.gameObject.SetActive(isMonologue);
            illustration.SetActive(!isMonologue);
            if(!isMonologue) image.sprite = sprites[i];
            prologueText = isMonologue ? monologue : desc;
            prologueText.color = prologueStrings[i][0] == '¡°' ? Color.red : Color.white;

            int length = prologueStrings[i].Length;
            for (int j = 0; j < length; j++)
            {
                prologueText.text += prologueStrings[i][j];
                typingSoure.Play();
                yield return new WaitForSecondsRealtime(0.13f);
            }

            yield return new WaitForSeconds(0.5f);

            prologueText.text = "";
        }
    }
}

[System.Serializable]
public class PrologueScript
{
    public string[] strs;
    public Sprite[] sprites;
}