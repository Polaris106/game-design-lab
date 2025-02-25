using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile4Controller : MonoBehaviour
{

    public float moveSpeed;
    public float speed;
    public Animator projAnimator;

    private Vector2 moveDirection;
    private float timeToChangeMotion;
    private bool motionChanged = false;
    private bool onlyOnce;
    private Transform player;
    private Vector2 target;
    private Vector2 direction;
    private KanakoController kanakoController;
    private GameObject gameControl;
    private GameControl gameControlScript;
    private int damage = 1;


    // Start is called before the first frame update
    void Start()
    {
        kanakoController = GameObject.Find("Kanako").GetComponent<KanakoController>();
        timeToChangeMotion = 1.5f;
        speed = 1f;
        onlyOnce = true;
        moveSpeed = 2f;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        gameControl = GameObject.Find("GameControl");
        gameControlScript = gameControl.GetComponent<GameControl>();
    }

    // Update is called once per frame
    void Update()
    {
        timeToChangeMotion -= Time.deltaTime;
        if (timeToChangeMotion <= 0 && motionChanged == false)
        {
            projAnimator.SetTrigger("isActive");
            moveSpeed = 0f;
            motionChanged = true;
            onlyOnce = false;

        }

        if (onlyOnce == false)
        {
            target = new Vector2(player.position.x, player.position.y);
            direction = target - (Vector2)transform.position;
            onlyOnce = true;
        }

        transform.position = (Vector2)transform.position + direction * speed * Time.deltaTime;


        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        if (gameControlScript.gameOver == true || kanakoController.currentHealth <= 200)
        {
            Destroy();
        }

    }

    public void SetMoveDirection(Vector2 dir)
    {
        moveDirection = dir;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TouhouPlayerMovement rm = other.GetComponent<TouhouPlayerMovement>();
            if (rm != null)
            {
                rm.TakeDamage(damage);
            }
            turnInvisible();

        }
    }
    private void OnEnable()
    {
        Invoke("Destroy", 10f);
    }

    private void Destroy()
    {

        undoInvisible();
        motionChanged = false;
        onlyOnce = true;
        timeToChangeMotion = 1.5f;
        moveSpeed = 2f;
        direction = Vector2.zero;
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }


    void turnInvisible()
    {

        GetComponent<SpriteRenderer>().enabled = false;
        this.GetComponent<BoxCollider2D>().enabled = false;
    }

    void undoInvisible()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        this.GetComponent<BoxCollider2D>().enabled = true;
    }

}
