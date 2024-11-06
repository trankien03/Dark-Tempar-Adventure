using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss : MonoBehaviour
{
    public Transform player;
    public bool isFlipped = false;
    

    private Damageable damageable;
    private float bossScale;
    private void Awake()
    {
        damageable = GetComponent<Damageable>();
    }

    private void Update()
    {
        if (damageable.Health < damageable.maxHealth / 2)
        {
            if (transform.localScale.x <= 3.5)
            {
                transform.localScale = new Vector3(transform.localScale.x + Time.fixedDeltaTime,
                    transform.localScale.y + Time.fixedDeltaTime, transform.localScale.z);
            }
            
        }
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1;
        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0, 180, 0);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0, 180, 0);
            isFlipped = true;
        }
    }

}
