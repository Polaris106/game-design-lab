using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KanakoShooting1 : MonoBehaviour
{

    public GameObject projectile1_prefab;
    public KanakoController kanakoControl;

    private int projectilesAmount;

    private float startAngle = 0f, endAngle = 360f;
    private Vector2 projectileMoveDirection;
    private bool oneTime = false;
    private float coolDown;
    private float shootDuration;
    private GameObject projectile1;

    // Start is called before the first frame update
    void Start()
    {
        coolDown = 2f;
        shootDuration = 0f;
    }

    void Update()
    {
        shootDuration -= Time.deltaTime;
        if (shootDuration <= 0)
        {
            coolDown -= Time.deltaTime;
            StopShoot();
            oneTime = false;
            if (coolDown <= 0)
            {
                shootDuration = 0.5f;
            }
        }
        else if (!oneTime && shootDuration > 0)
        {
            StartShoot();
            oneTime = true;
            coolDown = 0.8f;
        }

        if (kanakoControl.currentHealth < 200)
        {
            Debug.Log("Stop Shooting");
            StopShoot();
        }
    }

    private void Shoot()
    {
        // Angle between each projectile is determined by their amount
        projectilesAmount = Random.Range(20, 25);
        float angleStep = (endAngle - startAngle) / projectilesAmount;
        float angle = startAngle;

        for (int i = 0; i <= projectilesAmount; i++)
        {
            float projDirX = Mathf.Sin((angle * Mathf.PI) / 180f);
            float projDirY = Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector2 projDir = new Vector2(projDirX, projDirY).normalized;

            SetProjectiles(projDir);

            angle += angleStep;
        }
    }

    public void StartShoot()
    {
        Shoot();
    }

    public void StopShoot()
    {
        CancelInvoke();
    }

    void SetProjectiles(Vector2 projDir)
    {
        // Get projectile instance
        projectile1 = Instantiate(projectile1_prefab);
        // Set the projectile's position to the current object's position
        projectile1.transform.position = transform.position;
        projectile1.transform.rotation = transform.rotation;
        // Set the projectile's movement direction
        projectile1.GetComponent<Projectile1Controller>().SetMoveDirection(projDir);
    }

}
