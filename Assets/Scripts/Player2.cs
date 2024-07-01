using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player 2 is typically the second player of the game which is a bot or a player
public class Player2 : MonoBehaviour
{
    public GameObject gameController;
    private GameObject ball;
    private GameObject player1;
    private GameController gc;

    private Player1 pl1;
    public float speed = 1;
    public float player2Speed = 1;
    public bool invincibleStart = false;

    // Start is called before the first frame update
    void Start()
    {
        player1 = GameObject.FindGameObjectWithTag("Player1");
        ball = GameObject.FindGameObjectWithTag("Ball");
        gc = gameController.GetComponent<GameController>();
        pl1 = player1.GetComponent<Player1>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Make computer follow the ball by a bot
        if((!gc.playerLost[0] || !gc.playerLost[1]) && ball != null)
        {

            if(gc.mode != "VS")
            {
                if (gc.mode == "Normal" && !invincibleStart)
                {
                    Vector2 pos = Vector2.MoveTowards(transform.position, new Vector2(0, ball.transform.position.y), speed * Time.deltaTime);
                    GetComponent<Rigidbody2D>().MovePosition(pos);
                }
                else if (gc.mode == "Survival" || invincibleStart)
                {
                    transform.position = new Vector2(transform.position.x, ball.transform.position.y);
                }
            }
        }

        // If mode is versus give control to second player
        if(gc.mode == "VS")
        {
            float verticalMove = 0;

            if (Input.GetKey("w"))
            {
                verticalMove = player2Speed;
            }
            else if (Input.GetKey("s"))
            {
                verticalMove = -player2Speed;
            }

            float forceMovement = verticalMove * Time.deltaTime * speed;
            float rawMovement = transform.position.y + forceMovement;
            float clampedMovement = Mathf.Clamp(rawMovement, pl1.yMin, pl1.yMax);
            transform.position = new Vector2(transform.position.x, clampedMovement);
        }

        if (ball == null)
        {
            ball = GameObject.FindGameObjectWithTag("Ball");
        }
    }
}
