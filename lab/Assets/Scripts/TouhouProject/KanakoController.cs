using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KanakoController : MonoBehaviour
{
    
    public GameObject kanako;
    public float currentHealth;
    public GameObject autoBall;
    public GameObject ShootingPoint1;
    public GameObject ShootingPoint2;
    public Animator kanakoAnimator;
    public bool isInvincible = false;
    public bool secondPhase = false;
    public KanakoHealthBar healthBar;
    public float secondPhaseHealth = 400;

    private float maxHealth = 400;
    private GameObject gameControl;
    private GameControl gameControlScript;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        
        gameControl = GameObject.Find("GameControl");
        gameControlScript = gameControl.GetComponent<GameControl>();
    }

    // Update is called once per frame
    void Update()
    {

        if (gameControlScript.gameStart)
        {
            kanakoAnimator.SetTrigger("skillActive");
        }
        if (!secondPhase)
        {
            if (currentHealth < 200)
            {
                isInvincible = true;
                ShootingPoint1.SetActive(true);
                ShootingPoint2.SetActive(true);
            }
            if (currentHealth <= 0)
            {
                ShootingPoint1.SetActive(false);
                ShootingPoint2.SetActive(false);
            }
        }
        else
        {
            if (currentHealth <= 0)
            {
                Die();
            }

        }
    }

    public void SetAutoBallActive()
    {
        autoBall.SetActive(true);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.TakeDamage(damage);
    }

    public void ScorePoints(int points)
    {
        gameControlScript.score += points;
       
    }

    void Die()
    {
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Projectile" && !isInvincible)
        {
            ScorePoints(1);
            TakeDamage(1);
        }
    }
}
