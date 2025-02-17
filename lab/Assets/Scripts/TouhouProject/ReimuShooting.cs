using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class ReimuShooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject projPrefab;
    public float projForce = 20f;

    private float fireRate = 0.15f;
    private float canFire;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        canFire += Time.deltaTime;
        if (EnemyIsAlive())
        {
            if (canFire > fireRate)
            {
                Shoot();
                canFire = 0;

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
