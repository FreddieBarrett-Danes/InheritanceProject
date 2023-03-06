using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WraithAttack : MonoBehaviour
{
    private EnemyHandling eH;
    public GameObject bullet;
    public GameObject gun;

    private float canShootTime = 0.0f;
    public float shootDelay = 1.0f;
    // Start is called before the first frame update
    void Start()
    { 
        eH = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<EnemyHandling>();
    }

    // Update is called once per frame
    void Update()
    {
        //calls shoot functuion
        Shoot();
    }

    void Shoot()
    {
        //checks if the wraith is in range 
        if (eH.myWraith.inRange)
        {
            //shoots with a one second delay to each shot
            if(canShootTime <= Time.time)
            {
                Instantiate(bullet, gun.transform.position, Quaternion.identity);
                canShootTime = Time.time + shootDelay;
            }
        }
    }
}
