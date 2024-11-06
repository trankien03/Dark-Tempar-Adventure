using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormDummyScrpit : MonoBehaviour
{

    // Start is called before the first frame update
    Damageable damageable;     
    Rigidbody2D rb;
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        damageable = GetComponent<Damageable>();
    }
    

    



    public void OnHit(float damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }
}
