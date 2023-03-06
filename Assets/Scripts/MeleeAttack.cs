using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    private bool canAttack;

    private Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask playerLayer;

    private float canAttackTime = 0.0f;
    public float attackDelay = 2.0f;

    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        attackPoint = this.gameObject.transform.GetChild(0);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        //calls attack function
        Attack();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //checks if the player has walked into the attack circle
        if(collision.gameObject.tag == "Player")
        {
            //enables attack collider to be creates
            canAttack = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //checks if the player has walked out of the attack circle
        if (collision.gameObject.tag == "Player")
        {
            canAttack = false;
        }
    }
    void Attack()
    {
        if(canAttack)
        {
            if (canAttackTime <= Time.time)
            {
                //creates a 2D circle collider at set attack point at a range of 0.5 units on the player layer
                Collider2D hitplayer = Physics2D.OverlapCircle(attackPoint.transform.position, attackRange, playerLayer);

                //checks if the collider interacted with another collider in the player layer
                if (hitplayer != null)
                {
                    //removes health from the player
                    player.health--;
                    player.isHit = true;
                }
                //resets attack time
                canAttackTime = Time.time + attackDelay;
            }
        }
    }
    
}
