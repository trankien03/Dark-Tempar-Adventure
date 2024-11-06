using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Flying_followPlayer : MonoBehaviour
{

    public float Speed = 3.0f;
    private Transform playerPosition;
    public float lineOfSize;
    public float shootingRange;
    public GameObject bullet;
    public GameObject bulletParent;
    public float fireRate = 1f;
    private float timer = 0f;
    private float nextFireTime;
    private Animator animator;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

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


    // Update is called once per frame
    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(playerPosition.position, transform.position);
        if (distanceFromPlayer < lineOfSize && distanceFromPlayer > shootingRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, playerPosition.position, Speed * Time.deltaTime);
        }
        //else if (distanceFromPlayer <= shootingRange && nextFireTime < Time.time)
        //{
         //   generateBullet();
            
        //}
        HasTarget = shootingRange >= distanceFromPlayer;
        timer += Time.deltaTime;
        if (timer < fireRate && shootingRange >= distanceFromPlayer)
        {
            HasTarget = false;
        }
        else if (timer >= fireRate && shootingRange >= distanceFromPlayer)
        {
            timer = 0;
        }

        if (rb.position.x - playerPosition.position.x > 0)
        {
            transform.localScale = new Vector3(3, 3, 3);

        }
        else if (rb.position.x - playerPosition.position.x < 0)
        {
            transform.localScale = new Vector3(-3, 3, 3);
        }

        
    }

    private void generateBullet()
    {
        GameObject projectile = Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
        Vector3 oriScale = projectile.transform.localScale;
        nextFireTime = Time.time + fireRate;
        projectile.transform.localScale = new Vector3(
            oriScale.x * transform.localScale.x > 0 ? 2 : -2,
            oriScale.y,
            oriScale.z
            );
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,lineOfSize);
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
}
