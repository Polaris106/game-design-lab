using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GoombaMovement : MonoBehaviour
{
    public Vector3 startPosition;
    public bool goombaIsAlive = true;
    public Animator goombaAnimator;
    public AudioSource goombaAudio;
    public AudioClip goombaDeathAudio;
    public UnityEvent damagePlayer;

    private float originalX;
    private float maxOffset = 5.0f;
    private float enemyPatroltime = 2.0f;
    private int moveRight = -1;
    private Vector2 velocity;
    private Rigidbody2D enemyBody;
    private bool collidedWithPipe = false;
    private float health = 30f;


    // Start is called before the first frame update
    void Start()
    {
        startPosition = this.gameObject.transform.position;
        enemyBody = GetComponent<Rigidbody2D>();
        //get starting position
        originalX = enemyBody.position.x;
        ComputeVelocity();
    }

    void ComputeVelocity()
    {
        velocity = new Vector2((moveRight) * maxOffset / enemyPatroltime, 0);
    }

    void Movegoomba()
    {
        enemyBody.MovePosition(enemyBody.position + velocity * Time.fixedDeltaTime);
    }


    // Update is called once per frame
    void Update()
    {
        if (goombaIsAlive)
        {
            if (health <= 0)
            {
                goombaIsAlive = false;
            }

            if ((Mathf.Abs(enemyBody.position.x - originalX) < maxOffset) && !collidedWithPipe)
            {// move goomba
                Movegoomba();
            }
            else
            {
                // change direction
                moveRight *= -1;
                ComputeVelocity();
                Movegoomba();
                collidedWithPipe = false;
            }

        }
        else
        {
            goombaAnimator.Play("Goomba-die");
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<PlayerMovement>().onGroundState)
            {
                damagePlayer.Invoke();
            }

        }
        if (other.gameObject.CompareTag("Pipe"))
        {
            //Debug.Log("collided with pipe");
            collidedWithPipe = true;
        }
        if (other.gameObject.CompareTag("Projectile"))
        {
            health -= 1;
        }
    }

    public void playDeathAudio()
    {
        goombaAudio.PlayOneShot(goombaDeathAudio);
    }

    public void Die()
    {
        Destroy(gameObject);
    }


    public void GameRestart()
    {
        transform.localPosition = startPosition;
        originalX = transform.position.x;
        moveRight = -1;
        ComputeVelocity();
    }

}
