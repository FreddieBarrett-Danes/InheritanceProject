using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandling : MonoBehaviour
{
    //creates refernce to child classes
    public Wraith myWraith = new Wraith();
    public Pirate myPirate = new Pirate();
    public Skeleton mySkele = new Skeleton();

    public bool hasSpawned = false;
   
    public GameObject Wraith;
    public GameObject Skeleton;
    public GameObject Pirate;
    public GridHolder island;

    private GameObject[] landTiles;
    private GameObject[] edgeTiles;

    private Player player;

    public GameObject skeleSpawnPoint;
    public GameObject pirateSpawnPoint;
    public GameObject wraithSpawnPoint;

    private int pirateIndex;
    private int skeleIndex;
    private int wraithIndex;

    // Start is called before the first frame update
    void Start()
    {
        //references player scripts
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //sets gameobjects for classes to use
        myWraith.self = Wraith;
        mySkele.self = Skeleton;
        myPirate.self = Pirate;
        //checks if island has been created yet to compile land and edge tiles into arrays
        if(island.createdMap)
        {
            landTiles = GameObject.FindGameObjectsWithTag("Land");
            edgeTiles = GameObject.FindGameObjectsWithTag("Edge");
        }
        
        //creates a random number to use in the array for spawn location on edge of island
        pirateIndex = Random.Range(0, edgeTiles.Length);
        pirateSpawnPoint = edgeTiles[pirateIndex];

        //gives wriath and skele a random spawn point in center of island
        skeleIndex = Random.Range(0, landTiles.Length);
        skeleSpawnPoint = landTiles[skeleIndex];

        wraithIndex = Random.Range(0, landTiles.Length);
        wraithSpawnPoint = landTiles[wraithIndex];
        //checks if they got the same spawn point and if they have to rolls again.
        if(wraithIndex == skeleIndex)
        {
            wraithIndex = Random.Range(0, landTiles.Length);
            wraithSpawnPoint = landTiles[wraithIndex];
        }
    }

    // Update is called once per frame
    void Update()
    {
        //checks if island exists if the enemies have already spawned and if the player has dug up the treasure
        if (island.createdMap && !hasSpawned && player.hasDug)
        {
            //sets transform each time they spawn as not to create a offset from last time they restartesd the scene
            SetTransform(myWraith.self);
            SetTransform(mySkele.self);
            SetTransform(myPirate.self);
            //spawns the objects in the scene
            Instantiate(myWraith.self, landTiles[wraithIndex].transform);
            Instantiate(mySkele.self, landTiles[skeleIndex].transform);
            Instantiate(myPirate.self, edgeTiles[pirateIndex].transform);
            
            hasSpawned = true;
        }
    }
    //sets gameobject to have a transform of 0
    private void SetTransform(GameObject gameObject)
    {
        gameObject.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
    }
}
