using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent (typeof (Rigidbody2D), typeof (TouchingDirectionForPlayer), typeof (DamageableForPlayer))]
public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 200.0f;
    public float runSpeed = 400.0f;
    public float jumpImpluse = 250.0f;
    public float airWalkSpeed = 150.0f;

    DamageableForPlayer damageable;

    

    public float currentMoveSpeed {
        get
        {
            if (CanMove && !touchingDrirection.IsOnWall)
            {
                if (IsRunning) return runSpeed;
                else return walkSpeed;
                  
            }
            else return 0;
        }

        set 
        { 

        }
    }

    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);

        }
    }

    public bool IsAlive
    {
        get
        {
            return animator.GetBool(AnimationStrings.isAlive);
        }
       
    }


    Vector2 moveInput;
    TouchingDirectionForPlayer touchingDrirection;

    [SerializeField]
    private bool _isMoving = false;

    public bool IsMoving { get 
        {
            return _isMoving;
        }

        private set {
            _isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value);
        }
    }

    [SerializeField]
    private bool _IsRunning = false;

    public bool IsRunning { 
        get {
            return _IsRunning;
        }
        private set
        {
            _IsRunning = value;
            animator.SetBool(AnimationStrings.isRunning, value);
        }
    }

    public bool _isFacingRight = true;
    public bool IsFacingRight { get {
            return _isFacingRight;
        }
        private set { 
            if (_isFacingRight != value)
            {
                float rotation = 0f;
                if (!value) rotation = 180f;
                transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, rotation, transform.rotation.z));               
            }
            _isFacingRight = value;
        } 
    }


    Animator animator;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDrirection = GetComponent<TouchingDirectionForPlayer>();
        damageable =  GetComponent<DamageableForPlayer>();
        
    }


    private void FixedUpdate()  
    {
        if (!damageable.LockVelocity) 
            rb.velocity = new Vector2(moveInput.x * currentMoveSpeed , rb.velocity.y);

        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        if (IsAlive)
        {
            IsMoving = moveInput != Vector2.zero;
            setFacingDirection(moveInput);
        }
        else
        {
            IsMoving = false;
        }
        
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started) 
        {
            IsRunning = !IsRunning;
        }
        
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        //Check if alive
        if (context.started && touchingDrirection.IsGrounded && CanMove)
        {
            animator.SetTrigger(AnimationStrings.jump);
            rb.velocity = new Vector2(rb.velocity.x, jumpImpluse);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started && touchingDrirection.IsGrounded)
        {
            animator.SetTrigger(AnimationStrings.attack);
        }

        if (context.started && !touchingDrirection.IsGrounded)
        {
            animator.SetTrigger(AnimationStrings.attack);
        }
    }

    public void OnAttackCombo2(InputAction.CallbackContext context)
    {
        if (context.started && touchingDrirection.IsGrounded)
        {
            animator.SetTrigger(AnimationStrings.attackComboB);
        }
    }

    private void setFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x >0 && !IsFacingRight) 
        {
            IsFacingRight = true;
        }
        else if (moveInput.x < 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
    }

    public void OnHit(float damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);

    }
    
}
