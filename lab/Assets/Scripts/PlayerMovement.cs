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
    public bool onGroundState = true;

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


    private Rigidbody2D marioBody;
    private SpriteRenderer marioSprite;
    private bool faceRightState = true;
    private GameObject gameControl;
    private bool isTeleporting = false;
    private float teleportDelay = 1.3f;
    private int collisionLayerMask = (1 << 6) | (1 << 7) | (1 << 8);
    private float flySpeed = 15;
    private bool moving = false;
    private bool jumpedState = false;


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
        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));
    }

    void FlipMarioSprite(int value)
    {
        if (value == -1 && faceRightState)
        {
            faceRightState = false;
            marioSprite.flipX = true;
            if (marioBody.velocity.x > 0.05f)
                marioAnimator.SetTrigger("onSkid");

        }

        else if (value == 1 && !faceRightState)
        {
            faceRightState = true;
            marioSprite.flipX = false;
            if (marioBody.velocity.x < -0.05f)
                marioAnimator.SetTrigger("onSkid");
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Pipe") || col.gameObject.CompareTag("PipeTeleport") && !onGroundState)
        {
            onGroundState = true;
            jumpedState = false;
            maxSpeed = 20;
            // update animator state
            marioAnimator.SetBool("onGround", onGroundState);
        }


        // enable this the code below if you prefer to use layer mask for ground detection instead of tags, also disable the code above
        // that use CompareTag

        //if (((collisionLayerMask & (1 << col.transform.gameObject.layer)) > 0) & !onGroundState)
        //{
        //    onGroundState = true;
        //    // update animator state
        //    marioAnimator.SetBool("onGround", onGroundState);
        //}

    }

    private void FixedUpdate()
    {

        if (alive && !isTeleporting)
        {
            if (moving)
            {
                Move(faceRightState == true ? 1 : -1);
            }

            if (Input.GetMouseButton(1))
            {
                if (gameControl.GetComponent<GameControl>().canFly)
                {
                    maxSpeed = 10;
                    jumpedState = false;

                    Vector2 movement = new Vector2(0, 5);

                    if (marioBody.velocity.y < 30)
                    {
                        marioBody.AddForce(movement * 10);
                    }

                    onGroundState = false;
                    // update animator state
                    marioAnimator.SetBool("isJumping", jumpedState);
                    marioAnimator.SetTrigger("isFlying");
                    marioAnimator.SetBool("onGround", onGroundState);
                }
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

    void Move(int value)
    {
        Vector2 movement = new Vector2(value, 0);
        // check if it doesn't go beyond maxSpeed
        if (marioBody.velocity.magnitude < maxSpeed)
        {
            marioBody.AddForce(movement * speed);
        }

    }

    public void MoveCheck(int value)
    {
        if (value == 0)
        {
            moving = false;
        }
        else
        {
            FlipMarioSprite(value);
            moving = true;
            Move(value);
        }
    }

    public void Jump()
    {
        if (alive && onGroundState)
        {
            // jump
            marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            onGroundState = false;
            jumpedState = true;
            // update animator state
            marioAnimator.SetBool("isJumping", jumpedState);
            marioAnimator.SetBool("onGround", onGroundState);
        }
    }

    public void JumpHold()
    {
        if (alive && jumpedState)
        {
            // jump higher
            marioBody.AddForce(Vector2.up * upSpeed * 30, ForceMode2D.Force);
            jumpedState = false;

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
        if (other.gameObject.CompareTag("Enemy") && alive && onGroundState)
        {
            // play death animation
            marioAnimator.Play("Mario-Die");
            gameControl.GetComponent<GameControl>().gameAudio.mute = true;
            marioAudio.PlayOneShot(marioDeath);
            alive = false;
        }

        if (other.gameObject.CompareTag("GoombaWeakpoint") && alive && !onGroundState)
        {
            gameControl.GetComponent<GameControl>().addScore();
            scoreText.text = "Score: " + gameControl.GetComponent<GameControl>().score;
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        gameControl.GetComponent<GameControl>().gameAudio.mute = false;
        gameControl.GetComponent<GameControl>().gameAudio.Play();
        //// reset position
        //marioBody.transform.position = new Vector3(-1.5f, -2.5f, 0.0f);
        //// reset sprite direction
        //faceRightState = true;
        //marioSprite.flipX = false;
        //// reset score
        //scoreText.text = "Score: 0";
        //// reset Goomba
        //foreach (Transform eachChild in enemies.transform)
        //{
        //    eachChild.transform.position = eachChild.GetComponent<GoombaMovement>().startPosition;
        //}
        //// reset score
        //gameControl.GetComponent<GameControl>().score = 0;

        //// reset camera position
        //gameCamera.position = new Vector3(3.45f, 2.03f, -10f);

        //// reset animation
        //marioAnimator.SetTrigger("gameRestart");
        //alive = true;
    }

    public void GameRestart()
    {
        // reset position
        marioBody.transform.position = new Vector3(-5.33f, -4.69f, 0.0f);
        // reset sprite direction
        faceRightState = true;
        marioSprite.flipX = false;

        // reset animation
        marioAnimator.SetTrigger("gameRestart");
        alive = true;

        // reset camera position
        gameCamera.position = new Vector3(0, 0, -10);
    }

}
