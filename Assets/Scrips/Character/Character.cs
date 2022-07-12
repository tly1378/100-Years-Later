using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[SelectionBase]
public class Character : MonoBehaviour
{
    public static Dictionary<int, List<Character>> characters = new Dictionary<int, List<Character>>();
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
    protected Animator animator;
    new protected SpriteRenderer renderer;

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

    public List<List<Character>> Enemies
    {
        get
        {
            List<List<Character>> enemies = new List<List<Character>>();
            foreach (var item in characters)
            {
                if (item.Key != faction)
                    enemies.Add(item.Value);
            }
            return enemies;
        }
    }

    public Character NearestEnemy
    {
        get
        {
            Character nearest = null;
            float minDistance = float.MaxValue;
            int i = 0;
            foreach (List<Character> characters in Enemies)
            {
                foreach(Character character in characters)
                {
                    if(nearest == null)
                    {
                        nearest = character;
                        minDistance = (character.transform.position - transform.position).sqrMagnitude;
                    }
                    else
                    {
                        float distance = (character.transform.position - transform.position).sqrMagnitude;
                        if (distance < minDistance)
                        {
                            nearest = character;
                            minDistance = distance;
                        }
                    }
                    i++;
                }
            }
            return nearest;
        }
    }

    public virtual int Level { get => level; set => level = value; }

    public virtual bool IsEnemy(Character character)
    {
        if (character.faction != faction) return true;
        return false;
    }

    protected virtual void Start()
    {

        if (!characters.ContainsKey(faction))
        {
            characters.Add(faction, new List<Character>());
        }
        characters[faction].Add(this);

        body = GetComponent<Collider2D>();
        renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        if (weapon == null)
            weapon = GetComponentInChildren<Weapon>();
    }

    protected virtual void Update()
    {
        if (Direction.x > 0)
        {
            renderer.flipX = true;
        }
        else if (Direction.x < 0)
        {
            renderer.flipX = false;
        }

        if (Direction.x != 0 || Direction.y != 0)
            animator.SetBool("run", true);
        else
            animator.SetBool("run", false);

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
    protected virtual void Die()
    {
        if (isDead) return;

        characters[faction].Remove(this);
        isDead = true;
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

    protected virtual void SetKnockback(float knockbackTime, Vector2 direction)
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
