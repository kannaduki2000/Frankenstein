
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField]
    Transform Player;

    [SerializeField]
    float agroRange;

    [SerializeField]
    float moveSpeed;

    Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //distance to player
        float distToPlayer = Vector2.Distance(transform.position, Player.position);
        
        if(distToPlayer < agroRange)
        {
            //code to chase player
            ChasePlayer();
        }
        else
        {
            //stop chasing player
            StopChasingPlayer();
        }
    }
    void ChasePlayer()
    {
        if (transform.position.x < Player.position.x)
        {
            //enemy is to the left side of the player, so move right
            rb2d.velocity = new Vector2(moveSpeed, 0);
            transform.localScale = new Vector2(-2, 2);
        }
        else if (transform.position.x > Player.position.x)
        {
            //enemy is to the left side of the player, so move left

            rb2d.velocity = new Vector2(-moveSpeed, 0);
            transform.localScale = new Vector2(2, 2);
             
        }
        
    }
    void StopChasingPlayer()
    {
        rb2d.velocity = new Vector2(0, 0);
    }

    
}
