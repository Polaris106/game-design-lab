using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TouhouPlayerMovement : MonoBehaviour
{

    public float maxHp = 10f;
    public float currentHp;
    public TextMeshProUGUI healthText;
    public Transform startingPosition;
    public GameObject Boundary;
    public GameObject SpeechBox;
    public GameObject reimu;
    public GameObject marisa;
    public GameObject reimuPortrait;
    public GameObject marisaPortrait;
    public TextMeshProUGUI characterName;

    [System.NonSerialized]
    public bool isAlive = true;

    // for animation
    public Animator reimuAnimator;
    public Animator marisaAnimator;

    private Rigidbody2D rigidBody;
    private GameObject gameControl;
    private GameControl gameControlScript;
    private bool facingRight = false;
    private bool facingLeft = false;
    private float currentSpeed;
    private float moveSpeed = 4f;
    private float speedX = 2f;
    private float speedY = 2f;
    private bool atStartingPos = false;
    private bool isReimu = true;

    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxHp;
        gameControl = GameObject.Find("GameControl");
        gameControlScript = gameControl.GetComponent<GameControl>();
        rigidBody = gameObject.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (reimuAnimator != null)
        {
            Debug.Log("no reimu animator");
        }
        if (marisaAnimator != null)
        {
            Debug.Log("no marisa animator");
        }
        if (!atStartingPos)
        {
            float step = 2 * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, startingPosition.position, step);
            if (transform.position == startingPosition.position)
            {
                atStartingPos = true;
                SpeechBox.SetActive(true);
                Boundary.SetActive(true);
            }
        }

        if (Input.GetKeyDown("a") && !facingLeft)
        {
            Debug.Log("a is pressed");
            facingLeft = true;
            if (!isReimu)
            {
                reimuAnimator.SetBool("faceLeft", true);
            }
            else
            {
                marisaAnimator.SetBool("faceLeft", true);
            }

        }
        if (Input.GetKeyDown("d") && !facingRight)
        {
            facingRight = true;

            if (!isReimu)
            {
                reimuAnimator.SetBool("faceRight", true);
            }
            else
            {
                marisaAnimator.SetBool("faceRight", true);
            }
        }
        if (Input.GetKeyUp("a"))
        {
            facingLeft = false;

            if (!isReimu)
            {
                reimuAnimator.SetBool("faceLeft", false);
            }
            else
            {
                marisaAnimator.SetBool("faceLeft", false);
            }
        }
        if (Input.GetKeyUp("d"))
        {
            facingRight = false;
            if (!isReimu)
            {
                reimuAnimator.SetBool("faceRight", false);
            }
            else
            {
                marisaAnimator.SetBool("faceRight", false);
            }
        }

        if (Input.GetMouseButtonDown(1) && isAlive && gameControlScript.gameStart)
        {
            if (isReimu)
            {
                isReimu = false;
                reimu.SetActive(false);
                marisa.SetActive(true);
                marisaPortrait.SetActive(true);
                reimuPortrait.SetActive(false);
                characterName.text = "Marisa Kirisame";
            }
            else
            {
                isReimu = true;
                marisa.SetActive(false);
                reimu.SetActive(true);
                marisaPortrait.SetActive(false);
                reimuPortrait.SetActive(true);
                characterName.text = "Reimu Hakurei";
            }
        }

        if (!isReimu)
        {
            reimuAnimator.SetFloat("xSpeed", Mathf.Abs(rigidBody.velocity.x));
        }
        else
        {
            marisaAnimator.SetFloat("xSpeed", Mathf.Abs(rigidBody.velocity.x));
        }

        healthText.text = "PLAYER HP: " + currentHp;

    }

    private void FixedUpdate()
    {
        if (isAlive)
        {
            speedX = Input.GetAxisRaw("Horizontal") * moveSpeed;
            speedY = Input.GetAxisRaw("Vertical") * moveSpeed;
            rigidBody.velocity = new Vector2(speedX, speedY);


        }
    }

    public void TakeDamage(int damage)
    {
        currentHp -= damage;
        if (currentHp <= 0)
        {
            gameControlScript.gameOver = true;
        }
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(0);
        gameControlScript.currentScene = "MarioScene";
        gameControlScript.prevScene = null;
        // reset score
        gameControlScript.score = 0;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            TakeDamage(1);
        }

    }
}
