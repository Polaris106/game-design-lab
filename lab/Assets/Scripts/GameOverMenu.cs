using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TMPro.Examples;

public class GameOverMenu : MonoBehaviour
{
    public TextMeshProUGUI gameOverScoreText;
    public TextMeshProUGUI gameOverHighScoreText;
    public GameObject gameControl;
    public GameObject gameOverMenuPanel;
    public IntVariable gameScore;

    private float score;
    private float highScore;
    private bool gameOver;
    private bool gameStart;

    // Start is called before the first frame update
    void Start()
    {
        gameControl = GameObject.Find("GameControl");
    }

    // Update is called once per frame
    void Update()
    {

        gameOver = gameControl.GetComponent<GameControl>().gameOver;
        gameStart = gameControl.GetComponent<GameControl>().gameStart;


        if (gameStart)
        {
            if (gameOver)
            {
                gameOverMenuPanel.SetActive(true);
                score = gameScore.Value;
                highScore = gameScore.previousHighestValue;
                gameOverHighScoreText.text = "HIGH SCORE: " + highScore.ToString();
                gameOverScoreText.text = "SCORE: " + score.ToString();

            }
            else if (!gameOver)
            {
                gameOverMenuPanel.SetActive(false);
            }
        }

    }
}
