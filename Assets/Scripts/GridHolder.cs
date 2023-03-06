using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GridHolder : MonoBehaviour
{
    [SerializeField]
    private int rows = 5;
    [SerializeField]
    private int columns = 8;
    [SerializeField]
    private float tileSize = 1;

    private float randomXPos;
    private float randomYPos;

    private bool hasFound = false;
    private GameObject XTile;
    public GameObject TreasureTile;
    public Player player;
    private Vector3 oldPos;
    public bool createdMap = false;
        
    // Start is called before the first frame update
    void Start()
    {
        // chooses a random space in the grid making sure it cant be on the edge using 1 and -1
        randomXPos = Random.Range(1, columns - 1);
        randomYPos = Random.Range(1, rows - 1);
        //spawns the grid
        GenerateGrid();

        XTile = GameObject.FindGameObjectWithTag("Treasure");
        //stores position of x tile to then give them to treasure tile when the player has dug
        oldPos.x = XTile.transform.position.x;
        oldPos.y = XTile.transform.position.y;
    }
    void Update()
    {
        //checks if player has dug up treasure to then swap out x tile with treasure tile
        if(player.hasDug)
        {
            Destroy(XTile);
            if (!hasFound)
            {
                Instantiate(TreasureTile, oldPos, Quaternion.identity);
                player.allEnemiesDead = true;
                hasFound = true;
            }
        }
    }

    private void GenerateGrid()
    {
        //loads all needed game objects into reference gameobjects for later use
        GameObject centerTile = Resources.Load<GameObject>("SandTile");
        GameObject crossTile = Resources.Load<GameObject>("Cross");

        GameObject topedgeTile = Resources.Load<GameObject>("TopEdgeTile");
        GameObject rightedgeTile = Resources.Load<GameObject>("RightEdgeTile");
        GameObject botedgeTile = Resources.Load<GameObject>("BotEdgeTile");
        GameObject leftedgeTile = Resources.Load<GameObject>("LeftEdgeTile");

        GameObject topleftcornerTile = Resources.Load<GameObject>("TopLeftCornerTile");
        GameObject toprightcornerTile = Resources.Load<GameObject>("TopRightCornerTile");
        GameObject botleftcornerTile = Resources.Load<GameObject>("BotLeftCornerTile");
        GameObject botrightcornerTile = Resources.Load<GameObject>("BotRightCornerTile");

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                //checking if it is the top left position
                if (i == 0 && j == 0)
                {
                    //instatiates loaded gameobjects by asssigning the reference object to another gameobject in the scene
                    GameObject topleftTile = Instantiate(topleftcornerTile, transform);
                    
                    //uses the nested for loop and the allowed gap or tilesize to position each tile
                    float posX = j * tileSize;
                    float posY = i * -tileSize;

                    topleftTile.transform.position = new Vector2(posX, posY);
                }
                //checking if it is the top right position
                else if (i == 0 && j == columns - 1)
                {
                    GameObject toprightTile = Instantiate(toprightcornerTile, transform);

                    float posX = j * tileSize;
                    float posY = i * -tileSize;

                    toprightTile.transform.position = new Vector2(posX, posY);
                }
                //checking if it is the bottom left position
                else if (i == rows - 1 && j == 0)
                {
                    GameObject botleftTile = Instantiate(botleftcornerTile, transform);

                    float posX = j * tileSize;
                    float posY = i * -tileSize;

                    botleftTile.transform.position = new Vector2(posX, posY);
                }
                //checking if it is the bottom right position
                else if (i == rows - 1 && j == columns - 1)
                {
                    GameObject botrightTile = Instantiate(botrightcornerTile, transform);

                    float posX = j * tileSize;
                    float posY = i * -tileSize;

                    botrightTile.transform.position = new Vector2(posX, posY);
                }
                //checks if tile is positioned along the top
                else if (i == 0)
                {
                    GameObject topETile = Instantiate(topedgeTile, transform);

                    float posX = j * tileSize;
                    float posY = i * -tileSize;

                    topETile.transform.position = new Vector2(posX, posY);

                }
                //checks if tile is positioned along the edge
                else if (j == 0)
                {
                    GameObject leftETile = Instantiate(leftedgeTile, transform);

                    float posX = j * tileSize;
                    float posY = i * -tileSize;

                    leftETile.transform.position = new Vector2(posX, posY);

                }
                //checks if tile is position along the bottom
                else if (i == rows - 1)
                {
                    GameObject botETile = Instantiate(botedgeTile, transform);

                    float posX = j * tileSize;
                    float posY = i * -tileSize;

                    botETile.transform.position = new Vector2(posX, posY);

                }
                //checks if tile is positioned along the right
                else if (j == columns - 1)
                {
                    GameObject rightETile = Instantiate(rightedgeTile, transform);

                    float posX = j * tileSize;
                    float posY = i * -tileSize;

                    rightETile.transform.position = new Vector2(posX, posY);

                }
                //checks if position in for loop matches random position chosen for tile
                else if(j == randomXPos && i == randomYPos)
                {
                    GameObject XTile = Instantiate(crossTile, transform);

                    float posX = randomXPos * tileSize;
                    float posY = randomYPos * -tileSize;

                    XTile.transform.position = new Vector2(posX, posY);
                }
                //everthing else is assumed to be sand tile
                else
                {
                    GameObject sandTile = Instantiate(centerTile, transform);

                    float posX = j * tileSize;
                    float posY = i * -tileSize;

                    sandTile.transform.position = new Vector2(posX, posY);
                    
                }
            }
        }

        //sets the position of the grid to be in the center of the screen 
        float gridW = columns * tileSize;
        float gridH = rows * tileSize;
        transform.position = new Vector2(-gridW / 2 + tileSize / 2, gridH / 2 - tileSize / 2);
        createdMap = true;

    }
}
