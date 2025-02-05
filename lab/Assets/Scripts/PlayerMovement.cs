using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10;
    public float maxSpeed = 20;
    public float upSpeed = 10;
    public TextMeshProUGUI scoreText;
    public GameObject enemies;
    public JumpOverGoomba jumpOverGoomba;
    
    public float deathImpulse = 15;
    public Transform gameCamera;

    // for audio
    public AudioSource marioAudio;
    public AudioClip marioDeath;
    public AudioClip teleportingAudio;

    // state
    [System.NonSerialized]
    public bool alive = true;

    // for animation
    public Animator marioAnimator;

    private bool onGroundState = true;
    private Rigidbody2D marioBody;
    private SpriteRenderer marioSprite;
    private bool faceRightState = true;
    private GameObject gameControl;
    private bool isTeleporting = false;
    private float teleportDelay = 1.3f;


    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
        gameControl = GameObject.Find("GameControl");
        // update animator state
        marioAnimator.SetBool("onGround", onGroundState);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("a") && faceRightState)
        {
            faceRightState = false;
            marioSprite.flipX = true;
            if (marioBody.velocity.x > 0.1f)
            {
                marioAnimator.SetTrigger("onSkid");
            }
                
        }

        if (Input.GetKeyDown("d") && !faceRightState )
        {
            faceRightState = true;
            marioSprite.flipX = false;
            if (marioBody.velocity.x < -0.1f)
            {
                marioAnimator.SetTrigger("onSkid");
            }
        }

        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Pipe") || col.gameObject.CompareTag("PipeTeleport") && !onGroundState)
        {
            onGroundState = true;
            // update animator state
            marioAnimator.SetBool("onGround", onGroundState);
        }

    }

    private void FixedUpdate()
    {

        if (alive && !isTeleporting)
        {
            float moveHorizontal = Input.GetAxisRaw("Horizontal");

            if (Mathf.Abs(moveHorizontal) > 0)
            {
                Vector2 movement = new Vector2(moveHorizontal, 0);
                if (marioBody.velocity.magnitude < maxSpeed)
                {
                    marioBody.AddForce(movement * speed);
                }
            }

            // stop mario's movement
            if (Input.GetKeyUp("a") || Input.GetKeyUp("d"))
            {
                marioBody.velocity = Vector2.zero;
            }

            if (Input.GetKeyDown("space") && onGroundState)
            {
                marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
                onGroundState = false;
                // update animator state
                marioAnimator.SetBool("onGround", onGroundState);
            }
        }

        else if (alive && isTeleporting)
        {
            teleportDelay -= Time.deltaTime;
            if (teleportDelay < 0)
            {
                teleportDelay = 1.3f;
                gameControl.GetComponent<GameControl>().currentScene = "FlappyBird";
                gameControl.GetComponent<GameControl>().prevScene = "MarioScene";
                gameControl.GetComponent<GameControl>().enteringScene = true;
                isTeleporting = false;
                SceneManager.LoadScene(1);

            }
        }



    }

    void GameOverScene()
    {
        // set gameover scene
        gameControl.GetComponent<GameControl>().gameOver = true; 
    }

    void PlayDeathImpulse()
    {
        marioBody.AddForce(Vector2.up * deathImpulse, ForceMode2D.Impulse);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && alive)
        {
            
            // play death animation
            marioAnimator.Play("Mario-Die");
            marioAudio.PlayOneShot(marioDeath);
            alive = false;
        }

        if (other.gameObject.CompareTag("PipeTeleport"))
        {
            marioAudio.PlayOneShot(teleportingAudio);
            isTeleporting = true;
            other.GetComponent<BoxCollider2D>().enabled = false;
            marioBody.velocity = Vector2.zero;
            marioBody.velocity = new Vector2(0, -0.05f);

            
        }
    }

    void PlayJumpSound()
    {
        // play jump sound
        marioAudio.PlayOneShot(marioAudio.clip);
    }

    public void ResetGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        // reset position
        marioBody.transform.position = new Vector3(-1.5f, -2.5f, 0.0f);
        // reset sprite direction
        faceRightState = true;
        marioSprite.flipX = false;
        // reset score
        scoreText.text = "Score: 0";
        // reset Goomba
        foreach (Transform eachChild in enemies.transform)
        {
            eachChild.transform.position = eachChild.GetComponent<GoombaMovement>().startPosition;
        }
        // reset score
        gameControl.GetComponent<GameControl>().score = 0;

        // reset camera position
        gameCamera.position = new Vector3(3.45f, 2.03f, -10f);

        // reset animation
        marioAnimator.SetTrigger("gameRestart");
        alive = true;
    }
}
