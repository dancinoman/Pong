using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    bool oneTimeLost = false;
    private Rigidbody2D rb2d;
    public float velocity;
    private GameObject gameController;
    private GameObject player2;
    private Player2 pl2;
    private GameController gc;
    public float ampDir;
    private float fasterNFaster = 0;
    private bool switchDir;
    private bool firstShot = true;
    private Vector3 oldPosition;
    private Vector2 distance;
    private Vector2 padPos;
    private GameObject player1;
    private bool recordOneTime = false;
    
    


    // Start is called before the first frame update
    void Start()
    {
        player2 = GameObject.FindGameObjectWithTag("Player2");
        player1 = GameObject.FindGameObjectWithTag("Player1");
        gameController = GameObject.FindGameObjectWithTag("Game Controller");
        gc = gameController.GetComponent<GameController>();
        pl2 = player2.GetComponent<Player2>();
        rb2d = GetComponent<Rigidbody2D>();
        GiveBallVelocity(velocity);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(transform.position.x > 7.4f && !oneTimeLost) {MatchScored(0);}

        if(transform.position.x < -7.4f && !oneTimeLost) {MatchScored(1);}
       
    }

    private void Update()
    {
        distance = transform.position - player1.transform.position;
        if(distance.x > -0.4f && !recordOneTime)
        {
            oldPosition = player1.transform.position;
            recordOneTime = true;
        }
    }
    private void MatchScored(int index)
    {
        gc.PlayAudio(2);
        gc.playerLost[index] = true;
        oneTimeLost = true;
        Destroy(GameObject.FindGameObjectWithTag("Ball Parent"));
    }

    private void GiveBallVelocity(float ballVelocity) 
    {
        float startDirection = transform.position.y;
        float incorrectDirection = 0.39f;
        
        if (!firstShot)
        {
            rb2d.AddForce(new Vector2(-1, startDirection) * ballVelocity, ForceMode2D.Impulse);
        }
        else if (firstShot)
        {
            while (startDirection < incorrectDirection && startDirection > -incorrectDirection)
            {
                startDirection = Random.Range(-ampDir, ampDir);
            }

            if (gc.mode != "Survival" && !gc.player1Turn)
            {
                
                rb2d.AddForce(new Vector2(-1, startDirection) * ballVelocity, ForceMode2D.Impulse);
                gc.player1Turn = true;
                pl2.invincibleStart = true;
            }
            else if (gc.player1Turn || gc.mode == "Survival")
            {
                rb2d.AddForce(new Vector2(1, startDirection) * (ballVelocity), ForceMode2D.Impulse);
                gc.player1Turn = false;
            }

            firstShot = false;
        }
        
    }
   

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2")
        {
            gc.PlayAudio(0);
        }

        if(collision.gameObject.tag == "Boundary")
        {
            gc.PlayAudio(1);
        }

        if(collision.gameObject.tag == "Player1" || (collision.gameObject.tag == "Player2" && gc.mode == "VS"))
        {
            //Make sure Computer is not anymore invicible at start
            if (pl2.invincibleStart) { pl2.invincibleStart = !pl2.invincibleStart; }
            GameObject thisObject = collision.gameObject;
            gc.numberHits++;
            //Get player1 actual speed and apply to ball velocity
            padPos = thisObject.transform.position - oldPosition;
            recordOneTime = false;
            //Dont Change Velocity in survival mode
            if (gc.mode != "Survival") { GiveBallVelocity(padPos.y); }
            //But Get if faster and faster
            if(gc.mode == "Survival")
            {
                fasterNFaster += 0.04f;
                GiveBallVelocity(fasterNFaster);
            }
            
            
        }

    }
}
