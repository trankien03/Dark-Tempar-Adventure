using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 10;
    public Vector2 moveSpeed = new Vector2(3f,0);
    Rigidbody2D rb;
    public float timer = 5f;

    private Animator animator;

    public Vector2 Knockback = new Vector2(0, 0);
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //if you want the projectile to be effected by gravity by default, make it dynamic mode rigidbody
        rb.velocity = new Vector2(moveSpeed.x * transform.localScale.x, moveSpeed.y);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageableForPlayer damageableforf = collision.GetComponent<DamageableForPlayer>();
        if (damageableforf != null)
        {
           
            Vector2 delivererdKnockback = transform.localScale.x > 0 ? Knockback : new Vector2(-Knockback.x, Knockback.y);
            bool gothit = damageableforf.Hit(damage, delivererdKnockback);
            if (gothit)
            {
                Debug.Log(collision.name + "hiting" + damage);
                
                Destroy(gameObject);
            }
        }
        
    }
    public bool Hit()
    {

        animator.SetTrigger(AnimationStrings.hitTrigger);
       
        return true;
    }
}
