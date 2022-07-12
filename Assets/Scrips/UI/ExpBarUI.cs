using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpBarUI : MonoBehaviour
{
    public static ExpBarUI instance;
    public RectTransform exp_bar;
    public float target;
    public float speed;
    public int levelUp;
    float max;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        max = exp_bar.sizeDelta.x;
        exp_bar.sizeDelta = new Vector2(0, exp_bar.sizeDelta.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (levelUp > 0)
        {
            exp_bar.sizeDelta = new Vector2(exp_bar.sizeDelta.x + Time.deltaTime * speed, exp_bar.sizeDelta.y);
            if (exp_bar.sizeDelta.x >= max)
            {
                exp_bar.sizeDelta = new Vector2(0, exp_bar.sizeDelta.y);
                levelUp--;
            }
        }
        else if(exp_bar.sizeDelta.x < target/Player.player.maxExp * max)
        {
            exp_bar.sizeDelta = new Vector2(exp_bar.sizeDelta.x + Time.deltaTime* speed, exp_bar.sizeDelta.y);
        }
    }
}
