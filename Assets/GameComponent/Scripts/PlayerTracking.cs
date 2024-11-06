using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerTracking : MonoBehaviour
{
    public float maxSpeed = 5f;

    private Transform player;
    private Rigidbody2D rb;

    public wormController wormContrl;
    //public DetectionZone detectionZone;
    //private PlayerController playerController;
    Animator animator;
   

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //wormContrl = GetComponent<wormController>();
        //knightContrl = GetComponent<Knight>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            GameObject go = GameObject.FindWithTag("Player");
            player = go.transform;
        }
        if (rb.position.x - player.position.x > 0)
        {
            transform.localScale = new Vector3(-2, 2, 1);
            //move if player out of the range attack
            if (!wormContrl.HasTarget && !wormContrl.CanMove)
            {
                Vector2 pos = transform.position;
                Vector2 velocity = new Vector2(maxSpeed * Time.deltaTime, 0);
                pos -= velocity;
                transform.position = pos;
            }


        }
        else if (rb.position.x - player.position.x < 0)
        {
            transform.localScale = new Vector3(2, 2, 1);

            if (!wormContrl.HasTarget && !wormContrl.CanMove)
            {
                Vector2 pos = transform.position;
                Vector2 velocity = new Vector2(maxSpeed * Time.deltaTime, 0);
                pos += velocity;
                transform.position = pos;
            }
        }

    }

}
