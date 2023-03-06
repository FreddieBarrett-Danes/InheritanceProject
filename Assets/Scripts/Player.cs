using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    public float movespeed = 5.0f;
    
    private bool isFacingRight = false;

    private EnemyHandling eH;

    public int health = 100;
    public bool canDig = false;
    public bool hasDug = false;
    public bool allEnemiesDead = false;
    public bool isHit = false;
    public Rigidbody2D rb;

    private GameObject[] enemies;

    public GameObject gun;
    public GameObject gunTip;
    public GameObject bullet;

    private Vector2 lookDirection;
    private float lookAngle;
    private Vector2 movement;

    private SpriteRenderer sR;
    private Color ogColor;
    private float flashTime = 0.1f;
    void Start()
    {
        eH = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<EnemyHandling>();
        sR = gameObject.GetComponent<SpriteRenderer>();
        ogColor = sR.color;
    }
    // Update is called once per frame
    void Update()
    {
        //checking how many enemies in scene
        CheckEnemies();
        //setting direction of gun
        ShootDirection();

        CheckHealth();

        FlashOnHit();
        //movement code 
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        //checking which way the player is moving
        if (movement.x < 0)
        {
            //im pretty sure these are backwards as in this condtion the player is moving left
            isFacingRight = true;
        }
        else if (movement.x > 0)
        {
            isFacingRight = false;
        }

        if(Input.GetMouseButtonDown(0))
        {
            //call shoot function
            Shoot();
        }


        //checking is trying to dig treasure
        if (canDig && Input.GetKeyDown(KeyCode.E))
        {
            hasDug = true;
        }

        //allows player to restart the scene
        if(hasDug && allEnemiesDead && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("BootyGrab");
        }
        //flips sprite according to movement
        SpriteFlip();
    }
    //handles movement to avoid any frame related physic issues
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * movespeed * Time.fixedDeltaTime);
    }
    //finds the mouse cursor direction and stores it
    private void ShootDirection()
    {
        lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - gun.transform.position;
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        gun.transform.rotation = Quaternion.Euler(0.0f, 0.0f, lookAngle - 180.0f);
    }
    //Shoots a bullet towards the direction of the mouse cursor
    private void Shoot()
    {
        float canShootTime = 0.0f;
        float shootDelay = 1.0f;

        if(canShootTime <= Time.time)
        {
            GameObject firedBullet = Instantiate(bullet, gunTip.transform.position, gunTip.transform.rotation);
            firedBullet.GetComponent<Rigidbody2D>().velocity = -gunTip.transform.right * 5.0f;
            canShootTime = Time.time + shootDelay;
        }
    }
    //checks movement and flips sprite accordingly
    private void SpriteFlip()
    {
        if(isFacingRight)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        if (!isFacingRight)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }
    //checks if any enemies in the scene to furfill a win condition
    private void CheckEnemies()
    {
        if (eH.hasSpawned)
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            
            if (enemies.Length == 0)
            {
                allEnemiesDead = true;
            }
            else if (enemies.Length != 0)
            {
                allEnemiesDead = false;
            }
        }
    }
    //checks if the player has died if they have restarts the scene
    private void CheckHealth()
    {
        if(health <= 0)
        {
            SceneManager.LoadScene("BootyGrab");
        }
    }
    //flashes the player to red on hit then resets color using Invoke
    private void FlashOnHit()
    {
        if(isHit)
        {
            sR.color = Color.red;
            Invoke("ResetColor", flashTime);
        }
    }
    //resets renderers color
    private void ResetColor()
    {
        sR.color = ogColor;
        isHit = false;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //checks if the player is stood on the xTile
        if (collision.gameObject.tag == "Treasure")
        {
            canDig = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //checks if the player is stood on the xTile
        if (collision.gameObject.tag == "Treasure")
        {
            canDig = false;
        }
    }

}
