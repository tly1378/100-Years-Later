using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Fist
{
    float timer;
    Vector2 velocity;
    List<GameObject> bullets;
    int penetration;

    protected override void Start() { }

    public void Init(int harm, Vector2 velocity, float time, List<GameObject> bullets, Character master, int penetration = 1)
    {
        timer = time;
        this.harm = harm;
        this.velocity = velocity;
        this.bullets = bullets;
        this.master = master;
        this.penetration = penetration;
    }

    private void FixedUpdate()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
            transform.Translate(velocity * Time.deltaTime);
        }
        else
        {
            bullets.Remove(gameObject);
            Destroy(gameObject);
        }
    }

    protected override void Attack(Character character)
    {
        base.Attack(character);
        penetration--;
        if (penetration <= 0)
        {
            bullets.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}
