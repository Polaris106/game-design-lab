using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class StartButton : MonoBehaviour
{
    //public UnityEvent gameStart;


    public void StartGame()
    {
        // start the game 
        //gameControl.GetComponent<GameControl>().gameStart = true;
        //gameStart.Invoke();
        SceneManager.LoadScene(1);
        Debug.Log("starting game");
    }
}
