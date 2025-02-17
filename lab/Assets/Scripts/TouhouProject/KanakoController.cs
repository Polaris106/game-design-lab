using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KanakoController : MonoBehaviour
{
    
    public GameObject kanako;
    public float maxHealth = 200;

    private float currentHealth;
    private KanakoHealthBar healthBar;
    private float healthLost;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar = GameObject.Find("KanakoHealthBar").GetComponent<KanakoHealthBar>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthLost += damage;
        healthBar.TakeDamage(damage);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Projectile")
        {
            TakeDamage(1);
        }
    }
}
