using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class RestartButton : MonoBehaviour
{
    private GameObject player;
    private GameObject gameControl;
    private string playerName = "";
    public IntVariable gameScore;
    public UnityEvent gameRestart;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartGame()
    {
        player = GameObject.FindWithTag("Player");
        playerName = player.name;
        gameControl = GameObject.Find("GameControl");
        gameScore.Value = 0;
        gameRestart.Invoke();
        // reset everything
        switch (playerName)
        {
            case "Mario":
                player.GetComponent<PlayerMovement>().ResetGame();
                break;
            case "FlappyBird":
                player.GetComponent<FlappyBirdMovement>().ResetGame();
                break;
            case "TouhouPlayer":
                player.GetComponent<TouhouPlayerMovement>().ResetGame();
                break;
        }
        
        gameControl.GetComponent<GameControl>().gameOver = false;
    }
}
