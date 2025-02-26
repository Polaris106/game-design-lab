using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class TouhouPlayerShooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject projPrefab;
    public float projForce = 20f;

    private GameObject kanako;
    private KanakoController kanakoController;
    private float fireRate = 0.1f;
    private float canFire;
    private GameObject gameControl;
    private GameControl gameControlScript;

    // Start is called before the first frame update
    void Start()
    {
        gameControl = GameObject.Find("GameControl");
        gameControlScript = gameControl.GetComponent<GameControl>();
        kanako = GameObject.Find("Kanako");
        kanakoController = kanako.GetComponent<KanakoController>();
    }

    // Update is called once per frame
    void Update()
    {
        canFire += Time.deltaTime;
        if (EnemyIsAlive() && gameControlScript.gameStart)
        {
            if (kanakoController.currentHealth > 0)
            {
                if (canFire > fireRate)
                {
                    Shoot();
                    canFire = 0;

                }
            }

        }
    }

    private bool EnemyIsAlive()
    {
        if (GameObject.FindGameObjectWithTag("Enemy") == null)
        {
            Debug.Log("Enemy is not alive");
            return false;
        }
        else
        {
            Debug.Log("enemy is alive");
            return true;
        }

    }

    private void Shoot()
    {

        GameObject projectile = Instantiate(projPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * projForce, ForceMode2D.Impulse);
    }
}
