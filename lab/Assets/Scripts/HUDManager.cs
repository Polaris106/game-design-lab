using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : Singleton<HUDManager>
{
    public GameObject skillsPanel;
    public GameObject flyIcon;

    public GameObject gameControl;
    private string currentScene;
    private bool canFly;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentScene = gameControl.GetComponent<GameControl>().currentScene;
        canFly = gameControl.GetComponent<GameControl>().canFly;
        if (currentScene == "MarioScene")
        {
            
            skillsPanel.SetActive(true);
            if (canFly)
            {
                flyIcon.SetActive(true);
            }
            else
            {
                flyIcon.SetActive(false);
            }
        }
        else if (currentScene == "TouhouProject")
        {
            gameObject.SetActive(false);
        }
        else
        {
            skillsPanel.SetActive(false);
        }

    }
}
