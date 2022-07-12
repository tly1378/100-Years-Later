using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public static int total;
    public Transform center;

    public GameObject enemyPrefab;
    public int faction;
    public float spreadDistance;
    public float minimumDistance;

    public int nextCount;
    public float growthRate;
    public float coolingTime;
    public int maxCount;

    float timer;

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer = coolingTime;
            for(int i = 0; i< nextCount; i++)
            {
                if (Count > maxCount)
                    return;

                GameObject enemy = Instantiate(enemyPrefab);

                Vector3 random = Random.insideUnitCircle * spreadDistance;
                Vector3 direction = random.normalized * (minimumDistance + spreadDistance);
                enemy.transform.position = center.position + random + direction;

                enemy.GetComponent<Character>().faction = faction;

                total++;
            }
            nextCount = Mathf.CeilToInt(growthRate * nextCount);
        }
    }

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
}
