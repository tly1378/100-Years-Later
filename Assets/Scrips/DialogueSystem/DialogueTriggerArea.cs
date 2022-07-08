using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerArea : MonoBehaviour
{
    public DialogueData dialogueData;
    public bool isDisposable;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (dialogueData == null) return;
        if (collision.CompareTag(nameof(Player)))
        {
            DialogueManager.instance.Load(dialogueData);
            DialogueManager.instance.Next();
            if (isDisposable) dialogueData = null;
            Time.timeScale = 0;
        }
    }
}
