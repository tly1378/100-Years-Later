using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public float walkingSpeed;
    float coolingTime;
    float timer;
    Character target;
    public override Vector2 Direction
    {
        get
        {
            if (timer <= 0 || target == null)
            {
                timer = coolingTime;
                target = NearestEnemy;
                return default;
            }

            Vector3 vector3 = target.transform.position - transform.position;
            if (vector3.sqrMagnitude < 0.5f)
                return default;
            else
                return vector3.normalized;
        }
    }
    public override float Speed => walkingSpeed;

    protected override void Start()
    {
        base.Start();
        coolingTime = 1f + 2f * Random.value;
    }

    protected override void Update()
    {
        base.Update();
        timer -= Time.deltaTime;
    }

    protected override void Die()
    {
        base.Die();
        lastAttecker.Exp += Exp;
        Destroy(gameObject);
    }
}
