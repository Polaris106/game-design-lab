using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ReimuMovement : MonoBehaviour
{

    public float maxHp = 10f;
    public float currentHp;
    public TextMeshProUGUI healthText;
    public Transform startingPosition;
    public GameObject Boundary;
    public GameObject SpeechBox;

    [System.NonSerialized]
    public bool isAlive = true;

    // for animation
    public Animator reimuAnimator;

    private Rigidbody2D reimuBody;
    private GameObject gameControl;
    private bool facingRight = false;
    private bool facingLeft = false;
    private float currentSpeed;
    private float moveSpeed = 4f;
    private float speedX = 2f;
    private float speedY = 2f;
    private bool atStartingPos = false;

    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxHp;
        gameControl = GameObject.Find("GameControl");
        reimuBody = gameObject.GetComponent<Rigidbody2D>();


    }

    // Update is called once per frame
    void Update()
    {
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
            facingLeft = true;
            reimuAnimator.SetBool("faceLeft", true);
        }
        if (Input.GetKeyDown("d") && !facingRight)
        {
            facingRight = true;
            reimuAnimator.SetBool("faceRight", true);
        }
        if (Input.GetKeyUp("a"))
        {
            facingLeft = false;
            reimuAnimator.SetBool("faceLeft", false);
        }
        if (Input.GetKeyUp("d"))
        {
            facingRight = false;
            reimuAnimator.SetBool("faceRight", false);
        }

        reimuAnimator.SetFloat("xSpeed", Mathf.Abs(reimuBody.velocity.x));
        healthText.text = "PLAYER HP: " + currentHp;

    }

    private void FixedUpdate()
    {
        if (isAlive)
        {
            speedX = Input.GetAxisRaw("Horizontal") * moveSpeed;
            speedY = Input.GetAxisRaw("Vertical") * moveSpeed;
            reimuBody.velocity = new Vector2(speedX, speedY);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHp -= damage;
        if (currentHp <= 0)
        {
            gameControl.GetComponent<GameControl>().gameOver = true;
        }
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(0);
        gameControl.GetComponent<GameControl>().currentScene = "MarioScene";
        gameControl.GetComponent<GameControl>().prevScene = null;
        // reset score
        gameControl.GetComponent<GameControl>().score = 0;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            TakeDamage(1);
        }

    }
}
