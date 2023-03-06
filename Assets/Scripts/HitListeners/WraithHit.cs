using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WraithHit : MonoBehaviour
{
    private SpriteRenderer sR;
    private EnemyHandling eH;
    private Color ogColor;
    private float flashTime = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        sR = gameObject.GetComponent<SpriteRenderer>();
        eH = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<EnemyHandling>();
        ogColor = sR.color;
    }

    // Update is called once per frame
    void Update()
    {
        FlashOnHit();
    }
    private void FlashOnHit()
    {
        //checks if the variable is true
        if (eH.myWraith.isHit)
        {
            //turns sprite renderer to red and then flashes back using the invoke method to call reset color
            sR.color = Color.red;
            Invoke("ResetColor", flashTime);
        }
    }
    private void ResetColor()
    {
        sR.color = ogColor;
        eH.myWraith.isHit = false;
    }
}
