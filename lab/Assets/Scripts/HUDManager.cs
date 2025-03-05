using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public GameObject skillsPanel;
    public GameObject flyIcon;
    public GameObject shootIcon;
    public IntVariable gameScore;

    public GameObject gameControl;
    private string currentScene;
    private bool canFly;
    private bool canShoot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentScene = gameControl.GetComponent<GameControl>().currentScene;
        canFly = gameControl.GetComponent<GameControl>().canFly;
        canShoot = gameControl.GetComponent<GameControl>().canShoot;
        if (currentScene == "MarioScene" || currentScene == "Mario2")
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
            if (canShoot)
            {
                shootIcon.SetActive(true);
            }
            else
            {
                shootIcon.SetActive(false);
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
