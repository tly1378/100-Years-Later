using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [HideInInspector]
    public Character master;

    public float coolingTime;

    protected float coolingTimer;

    protected virtual void Start()
    {
        master = GetComponentInParent<Character>();
    }

    private void Update()
    {
        if (coolingTimer > 0)
        {
            coolingTimer -= Time.deltaTime;
        }
    }

    public void TryAttack(Character target = null)
    {
        if (coolingTimer <= 0)
        {
            coolingTimer = coolingTime;
            Attack(target);
        }
    }

    public void TryReload(Character character = null)
    {
        Reload(character);
    }

    protected abstract void Attack(Character character);
    protected abstract void Reload(Character character);
}
