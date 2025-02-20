using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnButton : MonoBehaviour
{
    private GameObject gameControl;
    private GameControl gameControlScript;

    // Start is called before the first frame update
    void Start()
    {
        gameControl = GameObject.Find("GameControl");
        gameControlScript = gameControl.GetComponent<GameControl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReturnToMario()
    {
        gameControlScript.currentScene = "MarioScene";
        gameControlScript.prevScene = "TouhouProject";
        gameControlScript.enteringScene = true;
        gameControlScript.musicPlayed = false;
        SceneManager.LoadScene(0);
    }
}
