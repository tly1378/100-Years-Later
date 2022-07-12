using Com.StellmanGreene.CSVReader;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

[System.Serializable]
public struct Dialogue
{
    public string title;
    [Multiline(5)]
    public string dialogue;
    public bool onLeftSide;
    public Sprite portrait;
}

[CreateAssetMenu(fileName = "DialogueData", menuName = "ScriptableObjects/DialogueData", order = 1)]
public class DialogueData : ScriptableObject
{
    public Dialogue[] dialogues;
    public string filename;
    public List<Dialogue> GetDialogue(string filename)
    {
        List<List<string>> table = CSVReader.Read("Assets/ScriptableObjects/Dialogue/" + filename, System.Text.Encoding.UTF8);
        List<Dialogue> dialogues = new List<Dialogue>();
        foreach (List<string> line in table)
        {
            if (line.Count != 4)
            {
                Debug.Log(string.Join(" ", line.ToArray()));
                continue;
            }
            Dialogue dialogue = new Dialogue();
            
            dialogue.title = line[0];
            dialogue.dialogue = line[1];
            dialogue.portrait = Resources.Load<Sprite>("Portrait/" + line[2]);
            Debug.Log(Resources.Load<Sprite>("Portrait/" + line[2]));
            dialogue.onLeftSide = line[3]=="是";

            dialogues.Add(dialogue);
        }
        return dialogues;
    }
}
