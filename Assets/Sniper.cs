using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Character
{
    Aimer aimer;
    protected override void Start()
    {
        base.Start();
        aimer = weapon.GetComponent<Aimer>();
        aimer.target = transform;
    }
}
