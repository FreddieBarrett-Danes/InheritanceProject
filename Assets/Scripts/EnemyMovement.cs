using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    private EnemyHandling eH;
    private Transform target;

    public GameObject Wraith;
    public GameObject Skeleton;
    public GameObject Pirate;
    public GameObject[] weapons;

    public float flashTime = 0.1f;

    public float meleestoppingDistance;
    public float rangedstoppingDistance;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        eH = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<EnemyHandling>();
        weapons = GameObject.FindGameObjectsWithTag("Weapon");
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (eH.hasSpawned)
        {
            //checks if skeleton exists in the scene
            if (Skeleton != null)
            {
                //moves to the player inherited from parent enemy class
                eH.mySkele.MoveToPlayer(target, Skeleton, speed, meleestoppingDistance);
                //Checks if the players is to the left or the right
                CheckXPosition(Skeleton);
            }
            //checks if wraith  exists in the scene
            if (Wraith != null)
            {
                //moves to player function inherited from parent enemy class
                eH.myWraith.MoveToPlayer(target, Wraith, speed, rangedstoppingDistance);
                //Checks if the player is to the left or the right
                CheckXPosition(Wraith);
            }
            //checks if pirate exists in the scene
            if (Pirate != null)
            {
                //moves to player function inherited from parent enemy class
                eH.myPirate.MoveToPlayer(target, Pirate, speed, meleestoppingDistance);
                //Checks if the player is to the left or the right
                CheckXPosition(Pirate);
            }

            //cyccles through the array calling the function for each weapon
            foreach (GameObject weapon in weapons)
            {
                if (weapon != null)
                {
                    //checks if the player is above or below 
                    CheckYPosition(weapon);
                }
            }
        }
        //checks health of wraith
        eH.myWraith.CheckHealth(eH.myWraith.health);
        if (eH.myWraith.health <= 0)
        {
            //destroys wriath
            Destroy(Wraith);
        }
        //checks health of skele
        eH.mySkele.CheckHealth(eH.mySkele.health);
        if (eH.mySkele.health <= 0)
        {
            //destroys skele
            Destroy(Skeleton);
        }
        //checks health of pirate
        eH.myPirate.CheckHealth(eH.myPirate.health);
        if (eH.myPirate.health <= 0)
        {
            //destorys pirate
            Destroy(Pirate);
        }
    }
    //function for checking if the player is to the left or right
    private void CheckXPosition(GameObject gameObject)
    {
        if (gameObject.transform.position.x < target.position.x)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else if (gameObject.transform.position.x > target.position.x)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
    //function for checking if the player is above or below
    private void CheckYPosition(GameObject weapon)
    {
        float yDifference = target.position.y - weapon.transform.position.y;

        if (yDifference > 1)
        {
            //look up
            weapon.transform.localPosition = new Vector3(0.0f, 1.0f, 0.0f);
            weapon.transform.localRotation = Quaternion.Euler(0, 0, -90);
        }
        else if (yDifference < -1)
        {
            //look down
            weapon.transform.localPosition = new Vector3(0.0f, -1.0f, 0.0f);
            weapon.transform.localRotation = Quaternion.Euler(0, 0, 90);
        }
        else
        {
            weapon.transform.localPosition = new Vector3(-1.0f, 0.0f, 0.0f);
            weapon.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
}

    
