using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using TMPro;
using TMPro.Examples;

public class GameControl : Singleton<GameControl>
{
    public GameObject superMarioLogo;
    public GameObject startButton;
    public GameObject gameOverMenuPanel;
    public GameObject restartButton;
    public GameObject scoreTextObject;
    public TextMeshProUGUI scoreTextGUI;

    [System.NonSerialized]
    public bool gameOver = false;
    public bool gameStart = false;
    public bool alrCalled = false;
    public int score = 0;   // variables under System.NonSerialized will not appear in inspector

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
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStart && !alrCalled)
        {
            Time.timeScale = 1.0f;
            startButton.SetActive(false);
            superMarioLogo.SetActive(false);
            alrCalled = true;
        }
        if (gameOver && gameStart)
        {
            Time.timeScale = 0.0f;
            //gameOverMenuPanel.SetActive(true);
            restartButton.SetActive(false);
            scoreTextObject.SetActive(false);
        }
        else if (!gameOver && gameStart)
        {
            Time.timeScale = 1.0f;
            //gameOverMenuPanel.SetActive(false);
            restartButton.SetActive(true);
            scoreTextObject.SetActive(true);
            scoreTextGUI.text = "Score: " + score.ToString();
        }
    }

    public void addScore()
    {
        score++;
    }
}
