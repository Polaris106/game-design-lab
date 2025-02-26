using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase2Shooting2 : MonoBehaviour
{
    public KanakoController kanakoController;
    public ProjectilePool projectilePoolScript;
    public GameObject autoBall2;
    public AudioSource shootAudio;
    public AudioClip shootSound;

    private float startAngle = 120f, endAngle = -120f;
    private Vector2 projectileMoveDirection;
    private bool oneTime = false;
    private float coolDown;
    private float shootDuration;
    private GameObject proj;
    private int projectilesAmount = 9;
    private GameObject gameControl;
    private GameControl gameControlScript;
    private Ball2Controller ball2Controller;

    // Start is called before the first frame update
    void Start()
    {
        coolDown = 0.05f;
        shootDuration = 0f;
        gameControl = GameObject.Find("GameControl");
        gameControlScript = gameControl.GetComponent<GameControl>();
        ball2Controller = autoBall2.GetComponent<Ball2Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        if (kanakoController.currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }

        if (ball2Controller.destinationReached)
        {

            shootDuration -= Time.deltaTime;
            if (shootDuration <= 0)
            {
                coolDown -= Time.deltaTime;
                StopShoot();
                oneTime = false;
                if (coolDown <= 0)
                {
                    shootDuration = 0.05f;
                }
            }
            else if (shootDuration > 0 && oneTime == false)
            {
                shootAudio.PlayOneShot(shootSound);
                Shoot();
                oneTime = true;
                coolDown = 0.2f;
            }
        }

    }

    private void Shoot()
    {
        float angleStep = (endAngle - startAngle) / projectilesAmount;
        float angle = startAngle;
        for (int i = 0; i < projectilesAmount + 1; i++)
        {
            float projDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float projDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector3 projMoveVector = new Vector3(projDirX, projDirY, 0f);
            Vector2 projDir = (projMoveVector - transform.position).normalized;

            SetProjectiles(projDir);

            angle += angleStep;
        }

    }

    void SetProjectiles(Vector2 projDir)
    {
        // Get projectile instance
        proj = this.gameObject.GetComponent<ProjectilePool>().GetProjectile();
        // Set the projectile's position to the current object's position
        proj.transform.position = transform.position;
        proj.transform.rotation = transform.rotation;
        // Set the projectile's movement direction
        proj.GetComponent<Projectile5Controller>().SetMoveDirection(projDir);
    }


    void StopShoot()
    {
        CancelInvoke();
    }
}
