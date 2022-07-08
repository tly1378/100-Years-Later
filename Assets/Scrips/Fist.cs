using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fist : Weapon
{
    public int harm;
    public float knockbackTime;
    public float knockbackSpeed;

    private void OnTriggerStay2D(Collider2D collision)
    {
        Character character = collision.GetComponent<Character>();
        if (character == null)
            return;
        if (coolingTimer <= 0)
        {
            if (master.IsEnemy(character))
            {
                coolingTimer = coolingTime;
                Attack(character);
            }
        }
    }

    protected override void Attack(Character character) => character.BeAttacked(knockbackTime, (character.transform.position - transform.position) * knockbackSpeed, harm, master);

    protected override void Reload(Character character)
    {
        throw new System.NotImplementedException();
    }
}
