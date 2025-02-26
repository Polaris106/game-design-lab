using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class MarioShoot : MonoBehaviour
{
    public Transform firePoint;
    public GameObject projPrefab;
    public float projForce = 10f;

    private float fireRate = 0.1f;
    private float nextFireTime = 0f;
    private GameObject gameControl;
    private GameControl gameControlScript;

    // Start is called before the first frame update
    void Start()
    {
        gameControl = GameObject.Find("GameControl");
        gameControlScript = gameControl.GetComponent<GameControl>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        nextFireTime -= Time.deltaTime;
        if (Input.GetMouseButton(0) && nextFireTime <= 0)
        {
            Shoot();
            nextFireTime = fireRate;
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
        // Get the mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Ensure the z-axis is zero since we're in 2D

        // Calculate the direction from the fire point to the mouse position
        Vector2 direction = (mousePosition - firePoint.position).normalized;

        // Instantiate the projectile
        GameObject projectile = Instantiate(projPrefab, firePoint.position, Quaternion.identity);

        // Rotate the projectile to face the target direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.Euler(0, 0, angle);

        // Apply force to the projectile in the calculated direction
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.AddForce(direction * projForce, ForceMode2D.Impulse);
    }
}
