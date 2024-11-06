using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int attackDamage = 20;
    public Vector2 Knockback = new Vector2(2, 1);

    private void OnTriggerEnter2D(Collider2D collision)
    {

        DamageableForPlayer damageableforf = collision.GetComponent<DamageableForPlayer>();

        if (damageableforf != null)
        {
            Vector2 delivererdKnockback = transform.parent.localScale.x > 0 ? Knockback : new Vector2(-Knockback.x, Knockback.y);
            bool gothit = damageableforf.Hit(attackDamage, delivererdKnockback);
            if (gothit) Debug.Log(collision.name + "hiting" + attackDamage);
        }
    }
}
