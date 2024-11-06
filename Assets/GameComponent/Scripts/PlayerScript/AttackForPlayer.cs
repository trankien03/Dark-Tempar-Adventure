using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackForPlayer : MonoBehaviour
{
    public int attackDamage = 20;
    public  Vector2 Knockback = new Vector2 (2,1);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable != null) 
        {
            Vector2 delivererdKnockback = transform.parent.rotation.y == 0f ? Knockback : new Vector2(-Knockback.x, Knockback.y);
            bool gothit = damageable.Hit(attackDamage, delivererdKnockback);
            if (damageable.Health <= 0 && !damageable.isDead)
            {
                StageProgress.enemyDeathCount++;
                damageable.isDead = true;
            }
            if (gothit) Debug.Log(collision.name + "hiting" + attackDamage);
        }
    }
}
