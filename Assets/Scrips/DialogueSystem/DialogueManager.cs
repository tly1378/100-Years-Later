using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    public TMPro.TMP_Text title;
    public TMPro.TMP_Text text;

    public Image left;
    public Image right;

    public DialogueData dialogueData;

    [HideInInspector]
    public int pointer;

    private void Start()
    {
        instance = this;
        gameObject.SetActive(false);
    }

    public void Next()
    {
        Load(pointer++);
    }

    public void Load(int index)
    {
        if (index >= dialogueData.dialogues.Length)
        {
            gameObject.SetActive(false);
            Time.timeScale = 1;
            return;
        }
        Dialogue dialogue = dialogueData.dialogues[index];
        title.text = dialogue.title;
        text.text = dialogue.dialogue;
        if (dialogue.onLeftSide)
        {
            left.sprite = dialogue.portrait;
            left.enabled = true;
            right.enabled = false;
        }
        else
        {
            right.sprite = dialogue.portrait;
            title.text = "<align=\"right\">" + title.text;
            left.enabled = false;
            right.enabled = true;
        }
    }

    public void Load(DialogueData dialogueData)
    {
        this.dialogueData = dialogueData;
        pointer = 0;
        gameObject.SetActive(true);
    }
}
