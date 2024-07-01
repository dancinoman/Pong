using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    //The PC is first and Com is the second
    public bool[] playerLost = new bool[] { false, false } ;
    //Pad collision, Wall collision and Ball lost
    public AudioClip[] soundEffect = new AudioClip[3];
    public string mode;
    public int numberHits;
    public GameObject ball;
    public GameObject player1TextGO;
    public GameObject player2TextGO;
    public GameObject timeDisplay;
    public GameObject gameOverOb;
    private TextMeshProUGUI player1Text;
    private TextMeshProUGUI player2Text;
    private TextMeshProUGUI timeText;
    private TextMeshProUGUI gameOverText;
    private int player1Score = 0;
    private int player2Score = 0;
    private AudioSource gAudio;
    private float timeFromStart;
    private bool gameOver = false;
    public bool player1Turn = true;
    private bool checkTimer = false;
    private float timerStart = 0;
    // Start is called before the first frame update
    void Start()
    {

        player1Text = player1TextGO.GetComponent<TextMeshProUGUI>();
        player2Text = player2TextGO.GetComponent<TextMeshProUGUI>();
        if(timeDisplay != null) { timeText = timeDisplay.GetComponent<TextMeshProUGUI>(); }
        if(gameOverOb != null) { gameOverText = gameOverOb.GetComponent<TextMeshProUGUI>(); }
        gAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            SceneManager.LoadScene(0);
        }

        //Game continues
        if (mode == "Normal" || mode == "VS")
        {
            if (playerLost[0])
            {
                Instantiate(ball, new Vector2(0, 0), Quaternion.identity);
                player2Score++;
                player2Text.text = player2Score.ToString();
                playerLost[0] = false;
            }

            if (playerLost[1])
            {
                Instantiate(ball, new Vector2(0, 0), Quaternion.identity);
                player1Score++;
                player1Text.text = player1Score.ToString();
                playerLost[1] = false;
            }
            
        }

        //Game over
        if(mode == "Survival")
        {
            
            if (!gameOver)
            {
                if (!checkTimer)
                {
                    timerStart = Time.realtimeSinceStartup;
                    checkTimer = true;
                }
                timeFromStart = Time.realtimeSinceStartup - timerStart;
                timeText.text = timeFromStart.ToString();
            }
            else if(gameOver && Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(2);
            }
            

            if (playerLost[0])
            {
                //Pop game over message
                gameOverText.enabled = true;
                playerLost[0] = false;
                gameOver = true;

                
            }
        }
       
    }

    public void PlayAudio(int index)
    {
        gAudio.clip = soundEffect[index];
        gAudio.Play();
    }

}
