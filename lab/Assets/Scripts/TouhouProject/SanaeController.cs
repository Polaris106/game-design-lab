using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanaeController : MonoBehaviour
{
    public Transform sanaePosition;
    public KanakoController kanakoController;
    public KanakoHealthBar healthBar;
    public GameObject autoBall2;
    public bool inPosition = false;
    public IntVariable gameScore;

    private float moveSpeed = 3f;
    private GameObject gameControl;
    private GameControl gameControlScript;


    // Start is called before the first frame update
    void Start()
    {
        gameControl = GameObject.Find("GameControl");
        gameControlScript = gameControl.GetComponent<GameControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (kanakoController.currentHealth <= 0)
        {
            if (!kanakoController.secondPhase)
            {
                float step = moveSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, sanaePosition.position, step);
                if (transform.position == sanaePosition.position)
                {
                    inPosition = true;
                    this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
                }
            }
            else
            {
                Die();
            }

        }

    }

    public void TakeDamage(int damage)
    {
        kanakoController.currentHealth -= damage;
        healthBar.TakeDamage(damage);

    }

    public void ScorePoints(int points)
    {
        gameScore.ApplyChange(1);

    }

    void Die()
    {
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Projectile" && !kanakoController.isInvincible)
        {
            ScorePoints(1);
            TakeDamage(1);
        }
    }
}
