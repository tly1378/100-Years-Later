using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public static EnemyGenerator instance;

    public TMPro.TMP_Text text;
    public RectTransform bar;

    public GameObject enemyPrefab;
    public int nextCount;
    public float growthRate;
    public float coolingTime;
    public int maxCount;
    public float spreadDistance;
    public float minimumDistance;

    float timer;
    int totalNumberOfEnemy;
    [HideInInspector]
    public int enemyRemaining;

    private void Start()
    {
        instance = this;
    }

    void Update()
    {
        SetBar(Time.time, 100);
        text.text = $"{totalNumberOfEnemy - enemyRemaining}/{totalNumberOfEnemy}";

        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            timer = coolingTime;
            for(int i = 0; i< nextCount; i++)
            {
                if (enemyRemaining > maxCount)
                    return;
                GameObject enemy = Instantiate(enemyPrefab);
                Vector3 random = Random.insideUnitCircle * spreadDistance;
                Vector3 direction = random.normalized * (minimumDistance + spreadDistance);
                enemy.transform.position = Player.player.transform.position + random + direction;
                enemyRemaining++;
                totalNumberOfEnemy++;
            }
            nextCount = Mathf.CeilToInt(growthRate * nextCount);
        }
    }

    void SetBar(float width, float height)
    {
        Vector2 vector2 = new Vector2(width, height);
        bar.sizeDelta = vector2;
    }
}
