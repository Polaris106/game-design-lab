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
    public GameObject stageCompletePanel;
    public KanakoController kanakoController; 
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI speechText;
    public TextMeshProUGUI speakerName;
    public TextMeshProUGUI stageCompleteScoreText;


    private GameObject gameControl;
    private GameControl gameControlScript;
    private bool reimuTurnToSpeak = true;
    private bool kanakoTurnToSpeak = false;
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
            kanakoHealthBar.SetActive(false);
            countdown -= Time.deltaTime;
 
            if (countdown <= 0)
            {
                stageCompletePanel.SetActive(true);
                stageCompleteScoreText.text = "Score: " + gameControlScript.score.ToString();
            }


        }
    }

    public void StartGame()
    {
        gameControlScript.gameStart = true;
        kanakoHealthBar.SetActive(true);
        speechBox.SetActive(false);
    }

    public void NextLine()
    {
        reimuPortrait.SetActive(false);
        kanakoPortrait.SetActive(true);
        speakerName.text = "Kanako";
        speechText.text = "A mere miko dares to challenge me!? I shall unleash the wrath of a god upon you!!";
    }

    public void SpeechControl()
    {
        if (reimuTurnToSpeak)
        {
            NextLine();
            reimuTurnToSpeak = false;
            kanakoTurnToSpeak = true;
        }
        else if (kanakoTurnToSpeak)
        {
            {
                StartGame();
            }
        }
    }
}
