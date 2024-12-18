using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1_walk : StateMachineBehaviour
{
    public float speed = 1f;
    Transform player;
    public float attackRange = 3f;
    private Damageable damageable;


    Rigidbody2D rb;
    boss boss_;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        boss_ = animator.GetComponent<boss>();
        damageable = animator.GetComponent<Damageable>();
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss_.LookAtPlayer();

        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        if (damageable.Health < damageable.maxHealth / 2)
        {
            speed = 2.5f;
            attackRange = 5f;
        }

        if (Vector2.Distance(player.position, rb.position) <= attackRange)
        {
            //Attack
            animator.SetTrigger("hasTarget");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("hasTarget");
    }


}
