using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float movespeed = 5.0f;
    public float destroyTimer = 3.0f;

    private Rigidbody2D rb;
    private Transform target;
    private Vector2 moveDirection;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        moveDirection = (target.position - transform.position).normalized * movespeed;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);

        //destroys game object after 3 seconds of being created, clean up code
        Destroy(gameObject, destroyTimer);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //checks if collider was the player
         if(collision.gameObject.tag == "Player")
         {
            //damages the player and says it has been hit
            Player player = collision.gameObject.GetComponent<Player>();
            player.health--;
            player.isHit = true;
            Destroy(gameObject);
         }
        else if(collision.gameObject.tag != "Enemy")
        {
            //if collision was the enemy destroy itself 
            Destroy(gameObject);
        }

    }
}
