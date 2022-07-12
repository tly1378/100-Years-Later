using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public TMPro.TMP_Text text;

    private void Start()
    {
        instance = this;
    }

    public IEnumerator Lose()
    {
        while(text.color.a < 3)
        {
            Color color = text.color;
            color.a += Time.deltaTime;
            text.color = color;
            yield return null;
        }
        SceneManager.LoadScene(0);
    }
}
