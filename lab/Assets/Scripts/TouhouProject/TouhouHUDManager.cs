using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TouhouHUDManager : MonoBehaviour
{
    public GameObject speechBox;
    public GameObject kanakoHealthBar;
    public GameObject restartButton;
    public GameObject reimuPortrait;
    public GameObject kanakoPortrait;
    public GameObject marisaPortrait;
    public GameObject sanaePortrait;
    public GameObject stageCompletePanel;
    public GameObject kanakoBackground;
    public KanakoController kanakoController;
    public SanaeController sanaeController;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI speechText;
    public TextMeshProUGUI speakerName;
    public TextMeshProUGUI stageCompleteScoreText;
    public int lineNumber = 0;

    private GameObject gameControl;
    private GameControl gameControlScript;
    private float countdown = 1;

    // Start is called before the first frame update
    void Start()
    {
        gameControl = GameObject.Find("GameControl");
        gameControlScript = gameControl.GetComponent<GameControl>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + gameControlScript.score.ToString();
        if (gameControlScript.gameOver)
        {
            restartButton.SetActive(false);
        }
        if (kanakoController.currentHealth <= 0)
        {
            if (kanakoController.secondPhase)
            {
                kanakoHealthBar.SetActive(false);
                kanakoBackground.SetActive(false);
                countdown -= Time.deltaTime;

                if (countdown <= 0)
                {
                    stageCompletePanel.SetActive(true);
                    stageCompleteScoreText.text = "Score: " + gameControlScript.score.ToString();
                }
            }
            else
            {
                kanakoHealthBar.SetActive(false);
                if (sanaeController.inPosition)
                {
                    speechBox.SetActive(true);
                }
            }

        }
    }

    public void StartGame()
    {
        gameControlScript.gameStart = true;
        kanakoHealthBar.SetActive(true);
        speechBox.SetActive(false);
    }

    public void StartPhaseTwo()
    {
        kanakoController.secondPhase = true;
        kanakoHealthBar.SetActive(true);
        kanakoHealthBar.GetComponent<KanakoHealthBar>().currentHealth = kanakoController.secondPhaseHealth;
        kanakoController.currentHealth = kanakoController.secondPhaseHealth;
        kanakoController.isInvincible = false;
        kanakoBackground.SetActive(true);
        speechBox.SetActive(false);
    }

    public void NextLine()
    {
        switch (lineNumber)
        {
            case 0:
                reimuPortrait.SetActive(false);
                kanakoPortrait.SetActive(true);
                speakerName.text = "Kanako";
                speechText.text = "A mere miko dares to challenge me!? I shall unleash the wrath of a god upon you!!";
                lineNumber++;
                break;
            case 1:
                kanakoPortrait.SetActive(false);
                marisaPortrait.SetActive(true);
                speakerName.text = "Marisa";
                speechText.text = "Heh, you are not the first god we have exterminated, nor will you be the last. Let's go, Reimu!";
                lineNumber++;
                break;
            case 2:
                marisaPortrait.SetActive(false);
                kanakoPortrait.SetActive(true);
                speakerName.text = "Kanako";
                speechText.text = "Tch, is this the end for me?";
                StartGame();
                lineNumber++;
                break;
            case 3:
                kanakoPortrait.SetActive(false);
                sanaePortrait.SetActive(true);
                speakerName.text = "Sanae";
                speechText.text = "Kanako-sama!! Damn you Hakurei miko, you will pay for this!";
                lineNumber++;
                break;
            case 4:
                sanaePortrait.SetActive(false);
                kanakoPortrait.SetActive(true);
                speakerName.text = "Kanako";
                speechText.text = "Join me, Sanae. Together, they shall witness the full might of the Moriya Shrine";
                lineNumber++;
                break;
            case 5:
                StartPhaseTwo();
                break;
            default:
                break;
        }

    }

}
