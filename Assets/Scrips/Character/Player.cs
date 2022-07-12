using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public RectTransform hp_panel;
    public GameObject hpSample;
    public Joystick direction_joystick;
    public Joystick fire_joystick;

    public static Player player;

    //角色能力
    public float walkingSpeed;
    public float runningSpeed;
    public float rollingSpeed;

    public float rollingTime;
    private float rollingTimer;

    public bool isGhostable;
    public override float Exp 
    { 
        get => exp;
        set
        {
            if (value >= maxExp)
            {
                Level++;
                Exp = value - maxExp;
            }
            else if (value != exp)
            {
                exp = value;
                ExpBarUI.instance.target = value;
            }
        }
    }
    public override int Level 
    { 
        get => level;
        set
        {
            print("level up");
            ExpBarUI.instance.levelUp += value - level;
            level = value;
        }
    }

    public override int Hp 
    { 
        get => base.Hp;
        set
        {
            if (value <= 0)
            {
                value = 0;
                Die();
            }
            for(int i=0;i< hp - value; i++)
            {
                Destroy(hp_panel.GetChild(0).gameObject);
            }
            hp = value;
        }
    }


    public override Vector2 Direction
    {
        get
        {
#if UNITY_ANDROID
            return direction_joystick.joystick.normalized;
#else
            Vector2 direction = Vector2.zero;
            if (direction.x == 0 && direction.y == 0)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    direction += Vector2.up;
                }
                if (Input.GetKey(KeyCode.A))
                {
                    direction += Vector2.left;
                }
                if (Input.GetKey(KeyCode.S))
                {
                    direction += Vector2.down;
                }
                if (Input.GetKey(KeyCode.D))
                {
                    direction += Vector2.right;
                }
            }
            return direction.normalized;
#endif
        }
    }
    public override float Speed
    {
        get
        {
#if UNITY_ANDROID
            if (direction_joystick.joystick.sqrMagnitude > 1000)
                return runningSpeed;
            else
                return walkingSpeed;
#else
            if (Input.GetKey(KeyCode.LeftShift))
            {
                return runningSpeed;
            }
            else
            {
                return walkingSpeed;
            }
#endif
        }
    }

    public override bool IsAttack 
    {
        get
        {
#if UNITY_ANDROID
            return fire_joystick.joystick.sqrMagnitude > 0;
#else
            return Input.GetMouseButton(0);
#endif
        }

    }
    public override bool IsReload
    {
        get
        {
#if UNITY_ANDROID
            // （待完善）按键逻辑
#else
            return Input.GetKeyDown(KeyCode.R);
#endif

        }

    }

    protected override void Start()
    {
        base.Start();
        player = this;
        faction = 1;
        UpdateHP(Hp);
    }

    private void UpdateHP(int hp)
    {
        foreach(RectTransform rectTransform in hp_panel)
        {
            Destroy(rectTransform);
        }
        for(int i = 0; i < hp; i++)
        {
            Instantiate(hpSample, hp_panel);
        }
    }

    public float invincibleTime;
    float invincibleTimer;
    public override float BeAttacked(float knockbackTime, Vector2 direction, int harm, Character attacker) 
    {
        if (invincibleTimer > 0) 
            return 0;
        lastAttecker = attacker;
        invincibleTimer = invincibleTime;

        if (isGhostable)
            body.enabled = false;

        Hp -= harm;
        SetKnockback(knockbackTime, direction);
        float reward;
        if (Hp <= 0)
        {
            reward = Exp;
        }
        else
            reward = 0f;

        return reward;
    }

    protected override void Update()
    {
        base.Update();
        if (invincibleTimer > 0)
        {
            invincibleTimer -= Time.deltaTime;
        }
        else
        {
            body.enabled = true;
        }
    }

    protected override void Die()
    {
        if (isDead) return;
        base.Die();
        enabled = false;
        // isWalkable = false;
        body.enabled = false;
        Destroy(weapon.gameObject);
        StartCoroutine(GameManager.instance.Lose());
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        //翻滚
        if (isWalkable)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(Rolling(Direction, rollingSpeed));
            }
        }
    }

    IEnumerator Rolling(Vector2 direction, float speed)
    {
        isWalkable = false;
        rollingTimer = rollingTime;
        while (rollingTimer > 0)
        {
            rollingTimer -= Time.deltaTime;
            transform.Translate(speed * Time.deltaTime * direction);
            yield return null;
        }
        isWalkable = true;
    }
}
