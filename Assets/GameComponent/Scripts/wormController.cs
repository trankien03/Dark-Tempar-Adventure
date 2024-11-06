using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wormController : MonoBehaviour
{
    public float moveFowardSpd;

    Rigidbody2D rb;
    Animator animator;
    public float walkStopRate = 0.6f;
    private Damageable damageable;

    public DetectionZone fireDetectionZone;

    public bool _hasTarget = false;
    public bool HasTarget
    {
        get { return _hasTarget; }
        private set
        {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }

    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
        

    }

    // Update is called once per frame
    void Update()
    {
        HasTarget = fireDetectionZone.detectedColliders.Count > 0;    
    }

    private void FixedUpdate()
    {
        if (damageable.LockVelocity == false)
        {
            if (!CanMove)
                rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkStopRate), rb.velocity.y);
        }
    }

    public void OnHit(float damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);

    }
}
