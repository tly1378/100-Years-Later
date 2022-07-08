using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public float walkingSpeed;
    public override Vector2 Direction
    {
        get
        {
            Vector3 vector3 = Player.player.transform.position - transform.position;
            if (vector3.sqrMagnitude < 0.5f)
                return Vector3.zero;
            else
                return vector3.normalized;
        }
    }
    public override float Speed => walkingSpeed;

    public override bool IsEnemy(Character character)
    {
        if (character.faction != faction) return true;
        return false;
    }
    protected override bool Die()
    {
        if (base.Die())
        {
            EnemyGenerator.instance.enemyRemaining--;
            return true;
        }
        else
        {
            return false;
        }
    }
}
