using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ResumeButton : MonoBehaviour
{

    public UnityEvent gameResume;
    public GameObject pauseMenu;

    public void ButtonClick()
    {
        gameResume.Invoke();
        Time.timeScale = 1.0f;
        pauseMenu.SetActive(false);
    }

    public void gamePaused()
    {
        pauseMenu.SetActive(true);
    }
}
