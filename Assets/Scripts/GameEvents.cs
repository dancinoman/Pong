using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// GameEvents manages many events in the game
public class GameEvents : MonoBehaviour
{
    public GameObject menu;
    public GameObject credits;
    public GameObject loadingScreen;
    public GameObject tray;
    public GameObject ball;

    private Vector2 startPositionTray;

    public float traySpeed;
    private bool stopCredit = false;
    private int intervalsOfBalls = 1;
    private int increment = 1;
    private int loadInt;

    // Start is called before the first frame update
    void Awake()
    {
        StopCoroutine(LoadNextLevel());
        credits.SetActive(false);
        loadingScreen.SetActive(false);
        startPositionTray = tray.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (credits.activeSelf == true)
        {
            MoveTray();
        }

        if(credits.activeSelf == true && Input.GetMouseButton(0)&& !stopCredit)
        {
            GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball Parent");
            tray.transform.position = startPositionTray;
            credits.SetActive(false);
            menu.SetActive(true);
            int i = 0;
            for (i = 0; i < balls.Length; i++)
            {
                Destroy(balls[i]);
            }
            stopCredit = true;
            intervalsOfBalls = increment = 1;
        }

    }

    public void SceneButton(int scene)
    {
        if(scene != 10)
        {
            loadInt = scene;
            StartCoroutine(LoadNextLevel());
        }
        else if(scene == 10)
        {
            menu.SetActive(false);
            credits.SetActive(true);
            stopCredit = false;
        }
    }

    private void MoveTray()
    {
        increment++;
        float forceVertical = 2 * Time.deltaTime * traySpeed;
        float movement = tray.transform.position.y + forceVertical;


        GameObject[] amountOfBall = GameObject.FindGameObjectsWithTag("Ball");
        tray.transform.position = new Vector2(0, movement);
        //Diminish with time balls
        if(amountOfBall.Length % 100 == 0) { intervalsOfBalls++; Debug.Log("Give intervals " + intervalsOfBalls); }

        if(increment % intervalsOfBalls == 0) { Instantiate(ball, new Vector2(-15, 0), Quaternion.identity); }


    }

    IEnumerator LoadNextLevel()
    {
        menu.SetActive(false);
        loadingScreen.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        AsyncOperation ll = SceneManager.LoadSceneAsync(loadInt);
        yield return null;
    }
}
