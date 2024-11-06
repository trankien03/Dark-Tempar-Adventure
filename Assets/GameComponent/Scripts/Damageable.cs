using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public UnityEvent<float, Vector2> damageableHit;

    public UnityEvent<bool> VelocityLocking;
    Animator animator;
    public bool isDead = false;

    [SerializeField]
    private float _maxHealth = 100.0f;
    public float maxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth = value;
        }
    }

    [SerializeField]
    private float _health = 100.0f;

    

    public float Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            if (_health <= 0)
            {
                IsAlive = false;
               
            }
        }
    }

    [SerializeField]
    private bool _isAlive = true;


    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }
        set
        {
            _isAlive = value;
            VelocityLocking?.Invoke(value);
            animator.SetBool(AnimationStrings.isAlive, value);
            Debug.Log("IsAlive set " + value);

        }
    }
    // the velocity should not be changed while this is true but needs to be respected by other component like the player controller
    [SerializeField]
    private bool _lockVelocity = false;
    public bool LockVelocity
    {
        get
        {
            return _lockVelocity = false;
        }
        set
        {

            _lockVelocity = value;
            animator.SetBool(AnimationStrings.lockVelocity, value);

        }
    }

    [SerializeField]
    private bool isInvincible = false;



    private float timeSinceHit;
    public float invincibilityTime = 0.1f;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isInvincible)
        {
            if (timeSinceHit > invincibilityTime)
            {
                isInvincible = false;
                timeSinceHit = 0;
            }
            timeSinceHit += Time.deltaTime;
        }
        //Hit(10, new Vector2(2,1));

    }
    public bool Hit(int damage, Vector2 knockback)
    {
        if (IsAlive && !isInvincible) 
        {
            Health -= damage;
            isInvincible = true;

            animator.SetTrigger(AnimationStrings.hitTrigger);
            LockVelocity = true;

            damageableHit?.Invoke(damage, knockback);
            CharacterEvents.characterDamaged.Invoke(gameObject, damage);

            return true;
        }
        return false;
    }
}
