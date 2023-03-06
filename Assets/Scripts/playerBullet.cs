using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBullet : MonoBehaviour
{
    public float movespeed = 5.0f;
    public float destroyTimer = 3.0f;

    private EnemyHandling eH;



    // Start is called before the first frame update
    void Start()
    {
        eH = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<EnemyHandling>();
        //destroys bullet after 3 seconds
        Destroy(gameObject, destroyTimer);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //if the collision was the player destroy the bullet
            Destroy(gameObject);
        }
        
        else if (collision.transform.gameObject.name == "Wraith(Clone)")
        {
            //if collision was named Wriath clone acess wriath health and damage it
            eH.myWraith.health--;
            eH.myWraith.isHit = true;
            Destroy(gameObject);
        }
        else if (collision.transform.gameObject.name == "Skele(Clone)")
        {
            //if collision was named Skele clone acess Skele health and damage it
            eH.mySkele.health--;
            eH.mySkele.isHit = true;
            Destroy(gameObject);
        }
        else if (collision.transform.gameObject.name == "Pirate(Clone)")
        {
            //if collision was named Pirate clone acess Pirate health and damage it
            eH.myPirate.health--;
            eH.myPirate.isHit = true;
            Destroy(gameObject);
        }

    }
}
