using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy 
{
    public int health;
    public int damage;
    
    public bool isHit;

    public bool canShoot;
    public string name;
    public GameObject self;

    
    public bool inRange;

    //enemy constructor
    public Enemy()
    {
        name = "noname";

        damage = 1;

        isHit = false;

        canShoot = false;
        inRange = false;


        self = null;
    }
    //virtual function for moving to the player
    public virtual void MoveToPlayer(Transform target, GameObject self, float speed, float stoppingDistance)
    {
        if (Vector2.Distance(self.transform.position, target.position) > stoppingDistance)
        {
            self.transform.position = Vector2.MoveTowards(self.transform.position, target.position, speed * Time.deltaTime);
            inRange = false;
        }
        else
        {
            inRange = true;
           
        }
    }
    //virtual function for checking health of enemy
    public virtual int CheckHealth(int health)
    {
        if(health <= 0)
        {
            return health;

        }
        else
        {
            return health;
        }
        
    }
}
