using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TMPro.Examples;

public class GameOverMenu : MonoBehaviour
{
    public TextMeshProUGUI gameOverScoreText;
    public GameObject gameControl;
    public GameObject gameOverMenuPanel;

    private float score;
    private bool gameOver;
    private bool gameStart;

    // Start is called before the first frame update
    void Start()
    {
        
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
                score = gameControl.GetComponent<GameControl>().score;
                gameOverScoreText.text = "Score: " + score.ToString();
            }
            else if (!gameOver)
            {
                gameOverMenuPanel.SetActive(false);
            }
        }

    }
}
