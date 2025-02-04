using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public GameObject superMarioLogo;
    public GameObject startButton;
    public GameObject gameOverMenu;
    public GameObject restartButton;
    public GameObject scoreText;

    [System.NonSerialized]
    public bool gameOver = false;
    public bool gameStart = false;


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStart)
        {
            Time.timeScale = 1.0f;
            startButton.SetActive(false);
            superMarioLogo.SetActive(false);
        }
        if (gameOver && gameStart)
        {
            Time.timeScale = 0.0f;
            gameOverMenu.SetActive(true);
            restartButton.SetActive(false);
            scoreText.SetActive(false);
        }
        else if (!gameOver && gameStart)
        {
            Time.timeScale = 1.0f;
            gameOverMenu.SetActive(false);
            restartButton.SetActive(true);
            scoreText.SetActive(true);
        }
    }
}
