using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    public BoolVariable marioFaceRight;
    public TextMeshProUGUI scoreText;
    public GameObject enemies;
    public JumpOverGoomba jumpOverGoomba; 
    public bool onGroundState = true;
    public GameConstants gameConstants;
    public IntVariable gameScore;
    public UnityEvent gameOver;

    public Transform gameCamera;
    public GameObject firePoint1;
    public GameObject firePoint2;
    public GameObject firePoint3;

    // for audio
    public AudioSource marioAudio;
    public AudioClip marioDeath;
    public AudioClip teleportingAudio;
    public AudioClip slamGround;
    public AudioClip marioTransformAudio;

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
    private string telePipeName;
    private float speed;
    private float maxSpeed;
    private float upSpeed;
    private float deathImpulse;

    // Start is called before the first frame update
    void Start()
    {
        // Set constants
        speed = gameConstants.speed;
        maxSpeed = gameConstants.maxSpeed;
        deathImpulse = gameConstants.deathImpulse;
        upSpeed = gameConstants.upSpeed;

        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
        gameControl = GameObject.Find("GameControl");
        // update animator state
        marioAnimator.SetBool("onGround", onGroundState);
        if (gameControl.GetComponent<GameControl>().canShoot)
        {
            firePoint1.SetActive(true);
            firePoint2.SetActive(true);
            firePoint3.SetActive(true);
        }
    }


    // Update is called once per frame
    void Update()
    {
        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));

    }

    void FlipMarioSprite(int value)
    {
        Debug.Log("FlipMarioSprite: " + value);
        if (value == -1 && faceRightState)
        {
            updateMarioShouldFaceRight(false);
            faceRightState = false;
            marioSprite.flipX = true;
            if (marioBody.velocity.x > 0.05f)
                marioAnimator.SetTrigger("onSkid");

        }

        else if (value == 1 && !faceRightState)
        {
            updateMarioShouldFaceRight(true);
            faceRightState = true;
            marioSprite.flipX = false;
            if (marioBody.velocity.x < -0.05f)
                marioAnimator.SetTrigger("onSkid");
        }
    }

    private void updateMarioShouldFaceRight(bool value)
    {
        faceRightState = value;
        marioFaceRight.SetValue(faceRightState);
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
                if (telePipeName == "Pipe-Teleport-FlappyBird")
                {
                    gameControl.GetComponent<GameControl>().currentScene = "FlappyBird";
                    gameControl.GetComponent<GameControl>().prevScene = "MarioScene";
                    gameControl.GetComponent<GameControl>().enteringScene = true;
                    isTeleporting = false;
                    gameControl.GetComponent<GameControl>().musicPlayed = false;
                    SceneManager.LoadScene(2);
                }
                else if (telePipeName == "Pipe-Teleport-Touhou")
                {
                    gameControl.GetComponent<GameControl>().currentScene = "TouhouProject";
                    gameControl.GetComponent<GameControl>().prevScene = "MarioScene";
                    gameControl.GetComponent<GameControl>().enteringScene = true;
                    isTeleporting = false;
                    gameControl.GetComponent<GameControl>().musicPlayed = false;
                    SceneManager.LoadScene(3);
                }


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

    public void Slam()
    {
        if (alive && !onGroundState)
        {
            marioBody.AddForce(Vector2.down * 35, ForceMode2D.Impulse);
            jumpedState = false;
            marioAudio.PlayOneShot(slamGround);
        }
    }




    void GameOver()
    {

        alive = false;
        // set gameover scene
        InvokeGameOver();
    }

    public void InvokeGameOver()
    {
        gameOver.Invoke();
    }

    void PlayDeathImpulse()
    {
        gameControl.GetComponent<GameControl>().gameAudio.mute = true;
        marioAudio.PlayOneShot(marioDeath);
        marioBody.AddForce(Vector2.up * deathImpulse, ForceMode2D.Impulse);
    }

    public void DamageMario()
    {
        // GameOverAnimationStart(); // last time Mario dies right away

        // pass this to StateController to see if Mario should start game over
        // since both state StateController and MarioStateController are on the same gameobject, it's ok to cross-refer between scripts
        GetComponent<MarioStateController>().SetPowerup(PowerupType.Damage);

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.gameObject.CompareTag("Enemy") && alive && onGroundState)
        //{
        //    // play death animation
        //    marioAnimator.Play("Mario-Die");
        //    gameControl.GetComponent<GameControl>().gameAudio.mute = true;
        //    marioAudio.PlayOneShot(marioDeath);
        //    alive = false;

        //}

        if (other.gameObject.CompareTag("GoombaWeakpoint") && alive && !onGroundState)
        {
            //gameScore.ApplyChange(1);
            //scoreText.text = "Score: " + gameScore.Value;
        }

        if (other.gameObject.CompareTag("PipeTeleport"))
        {
            telePipeName = other.gameObject.name;
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

    void PlayTransformSound()
    {
        marioAudio.PlayOneShot(marioTransformAudio);
    }

    public void SetFirePointsActive()
    {
        firePoint1.SetActive(true);
        firePoint2.SetActive(true);
        firePoint3.SetActive(true);
    }

    public void muteBackgroundMusic()
    {
        gameControl.GetComponent<GameControl>().gameAudio.mute = true;
    }

    public void resumeBackgroundMusic()
    {
        gameControl.GetComponent<GameControl>().gameAudio.mute = false;
    }

    public void ResetGame()
    {
        SceneManager.LoadScene("MarioScene");
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

    //public void GameRestart()
    //{
    //    // reset position
    //    marioBody.transform.position = new Vector3(-5.33f, -4.69f, 0.0f);
    //    // reset sprite direction
    //    faceRightState = true;
    //    marioSprite.flipX = false;

    //    // reset animation
    //    marioAnimator.SetTrigger("gameRestart");
    //    alive = true;

    //    // reset camera position
    //    gameCamera.position = new Vector3(0, 0, -10);
    //}

}
