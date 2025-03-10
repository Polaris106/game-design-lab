using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TMPro.Examples;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class FlappyBirdMovement : MonoBehaviour
{
    public float upSpeed = 1.5f;
    public Transform moveableObjects;
    public AudioSource flappyBirdAudio;
    public AudioClip teleportingAudio;
    public AudioClip coinAudio;
    public IntVariable gameScore;
    public UnityEvent incrementScore;

    [System.NonSerialized]
    public bool isAlive = true;

    private Rigidbody2D flappyBirdBody;
    private GameObject gameControl;
    private bool moveForward = false;
    private bool isTeleporting = false;
    private float teleportDelay = 1.3f;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 30;
        flappyBirdBody = GetComponent<Rigidbody2D>();
        gameControl = GameObject.Find("GameControl");
        //scoreText = TextMeshProUGUI.Find("ScoreText");
    }

    // Update is called once per frame
    void Update()
    {
        if (moveForward)
        {
            transform.position += Vector3.right * 3f * Time.deltaTime;
        }

    }

    private void FixedUpdate()
    {
        if (isAlive && !isTeleporting)
        {
            if (Input.GetKeyDown("space"))
            {
                flappyBirdAudio.PlayOneShot(flappyBirdAudio.clip);
                flappyBirdBody.velocity = Vector2.up * upSpeed;
            }
        }

        else if (isAlive && isTeleporting)
        {
            teleportDelay -= Time.deltaTime;
            if (teleportDelay < 0)
            {
                teleportDelay = 1.3f;
                gameControl.GetComponent<GameControl>().currentScene = "MarioScene";
                gameControl.GetComponent<GameControl>().prevScene = "FlappyBird";
                gameControl.GetComponent<GameControl>().enteringScene = true;
                isTeleporting = false;
                gameControl.GetComponent<GameControl>().canFly = true;
                Debug.Log("fly enabled");
                SceneManager.LoadScene(0);

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Pipe") || col.gameObject.CompareTag("Ground"))
        {
            gameControl.GetComponent<GameControl>().gameOver = true;
        }

        if (col.gameObject.name == "EndMoveTrigger")
        {
            foreach (Transform child in moveableObjects)
            {
                GameObject eachChild = child.gameObject;
                eachChild.GetComponent<PipeMovement>().moveSpeed = 0f;
                Debug.Log("movespeed: " + eachChild.GetComponent<PipeMovement>().moveSpeed);
            }
            moveForward = true;
            
        }

        if (col.gameObject.CompareTag("FlappyBirdCoin"))
        {
            incrementScore.Invoke();
            flappyBirdAudio.PlayOneShot(coinAudio);
            Destroy(col.gameObject);
        }

        if (col.gameObject.CompareTag("PipeTeleport"))
        {
            flappyBirdAudio.PlayOneShot(teleportingAudio);
            isTeleporting = true;
            flappyBirdBody.velocity = Vector2.zero;
            flappyBirdBody.gravityScale = 0;
            flappyBirdBody.velocity = new Vector2(3f, 0);
        }

    }

    public void ResetGame()
    {
        SceneManager.LoadScene(0);
        gameControl.GetComponent<GameControl>().currentScene = "MarioScene";
        gameControl.GetComponent<GameControl>().prevScene = null;
        // reset score
        gameScore.Value = 0;
    }
}
