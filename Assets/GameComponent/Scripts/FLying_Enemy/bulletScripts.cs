using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class bulletScripts : MonoBehaviour
{
    public GameObject target;
    public float speed = 2.0f;
    Rigidbody2D rb;
    public int damage = 10;
    public float timer = 5f;
    public Vector2 Knockback = new Vector2(0, 0);
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        Vector2 moveDir = (target.transform.position - transform.position).normalized * speed;
        rb.velocity = new Vector2(moveDir.x, moveDir.y);
        Destroy(this.gameObject,10);
    }

    // Update is called once per frame
    void Update()
    {
        
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

}
