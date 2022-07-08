using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[SelectionBase]
public class Character : MonoBehaviour
{
    [SerializeField]
    protected int hp;
    [SerializeField]
    protected float exp;

    public float maxExp;
    public int level;

    protected Collider2D body;
    [HideInInspector]
    public Weapon weapon;
    protected bool isWalkable = true;
    protected Character lastAttecker;

    public int faction;

    public virtual bool IsAttack { get => false; }
    public virtual bool IsReload { get => false; }
    
    public virtual float Speed { get => 0; }
    public virtual Vector2 Direction { get => Vector2.zero; }
    public virtual float Exp { get => exp; set => exp = value; }
    public virtual int Hp 
    { 
        get => hp;
        set
        {
            if (value <= 0)
            {
                value = 0;
                Die();
            }
            hp = value;
        }
    }

    public virtual int Level { get => level; set => level = value; }

    public virtual bool IsEnemy(Character character) { return true; }

    protected virtual void Start()
    {
        body = GetComponent<Collider2D>();
        if (weapon == null)
            weapon = GetComponentInChildren<Weapon>();
    }

    protected virtual void Update()
    {
        if (IsAttack)
        {
            weapon.TryAttack();
        }

        if (IsReload)
        {
            weapon.TryReload();
        }
    }

    protected bool isDead = false;
    protected virtual bool Die()
    {
        if (isDead)
            return false;
        isDead = true;
        lastAttecker.Exp += Exp;
        Destroy(gameObject);
        return true;
    }

    float knockbackTimer;
    Vector2 knockbackDirection;
    public virtual float BeAttacked(float knockbackTime, Vector2 direction, int harm, Character attacker) 
    {
        lastAttecker = attacker;
        Hp -= harm;
        SetKnockback(knockbackTime, direction);
        if (Hp <= 0)
        {
            return Exp;
        }
        else
            return 0f;
    }

    protected void SetKnockback(float knockbackTime, Vector2 direction)
    {
        knockbackTimer = knockbackTime;
        knockbackDirection = direction;
    }

    protected virtual void FixedUpdate()
    {
        //击退
        if (knockbackTimer > 0)
        {
            knockbackTimer -= Time.deltaTime;
            transform.Translate(knockbackDirection * Time.deltaTime);
        }
        //正常
        else
        {
            if (isWalkable)
                transform.Translate(Speed * Time.deltaTime * Direction);
        }
    }
}
