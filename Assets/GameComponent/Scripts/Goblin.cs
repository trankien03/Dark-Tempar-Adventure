using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//[RequireComponent (typeof(Rigidbody2D), typeof(TouchingDrirection))]
public class Knight : MonoBehaviour
{
    public float walkSpeed = 3f;
    public DetectionZone attackZone;
    public float walkStopRate = 0.6f;

    private Damageable damageable;

    Rigidbody2D rb;
    Animator animator;
    /*  
    public enum WalkableDirection { Left, Right }
    private Vector2 walkDirectionVector = Vector2.right;
    
    private WalkableDirection _walkDirection;
    private WalkableDirection WalkDirection
    {
        get 
        { return _walkDirection; }
        set { 
            if(_walkDirection != value && !animator.GetBool(AnimationStrings.lockVelocity))
            {
                //direction flipped
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
                if (value == WalkableDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                }
                else if (value == WalkableDirection.Left)
                {
                    walkDirectionVector = Vector2.left;
                }

            }
            
            
            _walkDirection = value; }
    }
    */
    public bool _hasTarget = false;
    public bool HasTarget { get { return _hasTarget; } 
        private set { 
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
    
    private void Awake()
    {
       rb = GetComponent<Rigidbody2D>();
       animator = GetComponent<Animator>();
       damageable = GetComponent<Damageable>();
    }

    private void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;
    }
    
    private void FixedUpdate()
    {

        if (damageable.LockVelocity == false)
        {
            if (!CanMove)
                rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkStopRate), rb.velocity.y);
        }
        if (HasTarget)
        {
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkStopRate), rb.velocity.y);
        }
    }
    /*
    private void FlipDirection()
    {
        if (WalkDirection == WalkableDirection.Right)
        {
            
            WalkDirection = WalkableDirection.Left;
        }
        else if (WalkDirection == WalkableDirection.Left)
        {
            
            WalkDirection = WalkableDirection.Right;
        }
        else
        {
            Debug.LogError("Current walkable direction is not set to legal values of right or left");
        }
    }

    */

    public void OnHit(float damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);

    }

}
