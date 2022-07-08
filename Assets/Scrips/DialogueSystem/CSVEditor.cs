using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DialogueData))]
[CanEditMultipleObjects]
public class CSVEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DialogueData dialogueData = (DialogueData)target;
        if (GUILayout.Button("读取文件"))
        {
            dialogueData.dialogues = dialogueData.GetDialogue(dialogueData.filename).ToArray();
        }
    }
}
