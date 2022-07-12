using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    public AudioSource source;

    public Transform[] muzzles;
    public GameObject bullet;

    public float bulletSpeed;
    public float bulletLife;
    public bool isRelative;
    public int harm;
    public int count;
    public float dispersion;
    public int penetration;
    public int bulletCapacity;
    public int bulletRemaining;

    [HideInInspector]
    public List<GameObject> bullets;


    protected override void Attack(Character character)
    {
        if (bulletRemaining <= 0 || Time.timeScale==0) 
            return;

        source.Play();
        foreach (Transform muzzle in muzzles)
        {
            Vector2[] directions = Dispersion(count, dispersion, muzzle.up);
            foreach(Vector2 direction in directions)
            {
                bulletRemaining--;
                GameObject bullet = Instantiate(this.bullet);
                bullet.transform.position = muzzle.position;
                bullets.Add(bullet);
                Vector2 velocity = GetVelocity(direction);
                bullet.GetComponent<Bullet>().Init(harm, velocity, bulletLife, bullets, master, penetration);
            }
        }
    }

    protected override void Reload(Character _)
    {
        bulletRemaining = bulletCapacity;
    }

    private Vector3 GetVelocity(Vector3 direction)
    {
        Vector2 velocity;
        if (isRelative)
        {
            Character character = GetComponentInParent<Character>();
            velocity = (Vector2)(direction * bulletSpeed) + (character.Direction * character.Speed);
        }
        else
        {
            velocity = direction * bulletSpeed;
        }
        return velocity;
    }

    public Vector2[] Dispersion(int count, float angle, Vector2 direction)
    {
        Vector2[] directions = new Vector2[count];
        Vector3 leftMost = Quaternion.AngleAxis(angle / 2, Vector3.back) * direction;
        directions[0] = leftMost;
        float eachAngle = angle / (count - 1);
        for (int i = 1; i < count; i++)
        {
            directions[i] = Quaternion.AngleAxis(eachAngle, Vector3.forward) * directions[i-1];
        }
        return directions;
    }

}
