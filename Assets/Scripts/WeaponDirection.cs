using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDirection : MonoBehaviour
{
    private GameObject[] weapons;
    private EnemyHandling eH;
    private Transform targetPos;
    private Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        eH = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<EnemyHandling>();
        targetPos = GameObject.FindGameObjectWithTag("Player").transform;
        if(eH.hasSpawned)
        {
            weapons = GameObject.FindGameObjectsWithTag("Weapon");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //goes through array pointing each weapon at the player 
        foreach (GameObject weapon in weapons)
        {
            //checks if that weapon exists in the scene
            if (weapon != null)
            {
                direction = targetPos.position - weapon.transform.position;
                weapon.transform.right = -direction;
            }
        }

    }   
}
