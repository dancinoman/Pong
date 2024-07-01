using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// BallCredit is spreads ball every where for an art effect
public class BallCredits : MonoBehaviour
{
    private GameController gc;
    private Rigidbody2D rb2d;

    public float velocity;
    public float ampDir;
    private bool oneTimeForce = true;
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

    private void GiveBallVelocity()
    {
        float startDirection = 0;
        float incorrectDirection = 0f;
        startDirection = Random.Range(-ampDir, ampDir);

        rb2d.velocity = Vector2.zero;
        rb2d.AddForce(new Vector2(1, startDirection) * velocity, ForceMode2D.Impulse);
        oneTimeForce = false;
    }
}
