using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProgress : MonoBehaviour
{
    public TMPro.TMP_Text text;
    public RectTransform bar;
    public int faction;

    private int Count
    {
        get
        {
            if (Character.characters.ContainsKey(faction))
            {
                return Character.characters[faction].Count;
            }
            else
            {
                return 0;
            }
        }
    }

    private void Update()
    {
        SetBar(Time.time, 100);
        text.text = $"{EnemyGenerator.total - Count}/{EnemyGenerator.total}";
    }

    void SetBar(float width, float height)
    {
        Vector2 vector2 = new Vector2(width, height);
        bar.sizeDelta = vector2;
    }
}
