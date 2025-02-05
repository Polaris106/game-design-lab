using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControlFlappyBird : MonoBehaviour
{
    public bool gameStart = false;
    public bool gameOver = false;
    public GameObject gameOverMenuPanel;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            gameOverMenuPanel.SetActive(true);
            Time.timeScale = 0.0f;
        }
        else
        {
            gameOverMenuPanel.SetActive(false);
            Time.timeScale = 1.0f;
        }
    }
}
