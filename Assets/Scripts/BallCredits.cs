using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCredits : MonoBehaviour
{
    bool oneTimeForce = true;
    private Rigidbody2D rb2d;
    public float velocity;
    private GameController gc;
    public float ampDir;
    private bool firstShot = true;




    // Start is called before the first frame update
    void Start()
    {
       rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (oneTimeForce)
        {
            GiveBallVelocity();
        }

    }

    private void Update()
    {
      
    }

    private void GiveBallVelocity()
    {
        float startDirection = 0;
        float incorrectDirection = 0f;

        if (firstShot)
        {

         /*   while (startDirection < incorrectDirection && startDirection > -incorrectDirection)
            {
                startDirection = Random.Range(-ampDir, ampDir);
            }
            firstShot = false;*/
        }
        startDirection = Random.Range(-ampDir, ampDir);

        rb2d.velocity = Vector2.zero;
        rb2d.AddForce(new Vector2(1, startDirection) * velocity, ForceMode2D.Impulse);
        oneTimeForce = false;
    }
}
