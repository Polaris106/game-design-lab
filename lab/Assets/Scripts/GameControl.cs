using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using TMPro;
using TMPro.Examples;
using UnityEngine.SceneManagement;

public class GameControl : Singleton<GameControl>
{
    public GameObject superMarioLogo;
    public GameObject startButton;
    public GameObject gameOverMenuPanel;
    public GameObject restartButton;
    public GameObject scoreTextObject;
    
    public TextMeshProUGUI scoreTextGUI;
    public string prevScene;
    public string currentScene;

    [System.NonSerialized]
    public bool gameOver = false;
    public bool gameStart = false;
    public bool alrCalled = false;
    public bool enteringScene = true;
    public int score = 0;   // variables under System.NonSerialized will not appear in inspector

    private GameObject mario;

    override public void Awake()
    {
        base.Awake();
        Debug.Log("awake called");
        

    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0.0f;
        startButton.SetActive(true);
        superMarioLogo.SetActive(true);
        currentScene = "MarioScene";
        
    }

    // Update is called once per frame
    void Update()
    {
        if (mario == null)
        {
            mario = GameObject.Find("Mario");
        }
        if (enteringScene && SceneManager.GetActiveScene().name == currentScene)
        {
            Debug.Log("position changed");
            switch (prevScene)
            {
                case "FlappyBird":
                    mario.transform.position = new Vector3(24.41f, 9.05f, 0.0f);
                    break;
                default:
                    mario.transform.position = new Vector3(-1.5f, -2.5f, 0.0f);
                    break;
            }
            enteringScene = false;
        }

        if (gameStart)
        {
            if (!alrCalled)
            {
                Time.timeScale = 1.0f;
                startButton.SetActive(false);
                superMarioLogo.SetActive(false);
                alrCalled = true;
            }
            if (gameOver)
            {
                Time.timeScale = 0.0f;
                //gameOverMenuPanel.SetActive(true);
                restartButton.SetActive(false);
                scoreTextObject.SetActive(false);
            }
            else if (!gameOver)
            {
                Time.timeScale = 1.0f;
                //gameOverMenuPanel.SetActive(false);
                restartButton.SetActive(true);
                scoreTextObject.SetActive(true);
                scoreTextGUI.text = "SCORE: " + score.ToString();
            }
        }



    }

    public void addScore()
    {
        score++;
    }
}
