using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartButton : MonoBehaviour
{
    private GameObject mario;
    private GameObject gameControl;

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
        mario = GameObject.Find("Mario");
        gameControl = GameObject.Find("GameControl");
        // reset everything
        mario.GetComponent<PlayerMovement>().ResetGame();
        gameControl.GetComponent<GameControl>().gameOver = false;
    }
}
