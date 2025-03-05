using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PauseButton : MonoBehaviour
{

    public UnityEvent gamePause;
    public GameObject pauseButton;

    public void ButtonClick()
    {
        gamePause.Invoke();
        pauseButton.SetActive(false);
    }

    public void gameResumed()
    {
        pauseButton.SetActive(true);
    }
}
