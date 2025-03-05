using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TMPro.Examples;

public class GameOverMenu : MonoBehaviour
{
    public TextMeshProUGUI gameOverScoreText;
    public TextMeshProUGUI gameOverHighScoreText;
    public GameObject gameOverMenuPanel;
    public IntVariable gameScore;

    private float score;
    private float highScore;
    private bool gameOver;
    private bool gameStart;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowMenu()
    {
        gameOverMenuPanel.SetActive(true);
        score = gameScore.Value;
        highScore = gameScore.previousHighestValue;
        gameOverHighScoreText.text = "HIGH SCORE: " + highScore.ToString();
        gameOverScoreText.text = "SCORE: " + score.ToString();
    }
}
