using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Character
{
    Aimer aimer;
    Gun gun;

    protected override void Start()
    {
        base.Start();
        aimer = weapon.GetComponent<Aimer>();
        gun = (Gun)weapon;
    }

    protected override void Update()
    {
        base.Update();

        Character target = NearestEnemy;
        if (target != null)
        {
            if((target.transform.position - transform.position).magnitude < 10)
            {
                aimer.target = target.transform;
            }
        }
    }

    protected override void FixedUpdate()
    {

    }

    public override bool IsAttack => aimer.target != null;
    public override bool IsReload => gun.bulletRemaining == 0;

    protected override void Die()
    {
        base.Die();
        lastAttecker.Exp += Exp;
        Destroy(gameObject);
    }
}
