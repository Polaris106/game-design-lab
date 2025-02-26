using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase2Shooting1 : MonoBehaviour
{
    public KanakoController kanakoController;
    public ProjectilePool projectilePoolScript;

    [SerializeField]
    private float startAngle = 0f, endAngle = 360f;

    private Vector2 projectileMoveDirection;
    private bool oneTime = false;
    private float coolDown;
    private float shootDuration;
    private GameObject proj;
    private int projectilesAmount = 3;
    private GameObject gameControl;
    private GameControl gameControlScript;
    private float rotationSpeed = 90f;

    // Start is called before the first frame update
    void Start()
    {
        coolDown = 0.05f;
        shootDuration = 0f;
        gameControl = GameObject.Find("GameControl");
        gameControlScript = gameControl.GetComponent<GameControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (kanakoController.currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }

        // Rotate the GameObject around the Z-axis (for 2D)
        Vector3 worldRotation = new Vector3(0, 0, rotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + worldRotation);

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
            //kanakoAudio.PlayOneShot(kanakoShootAudio);
            Shoot();
            oneTime = true;
            coolDown = 0.05f;
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
        proj.GetComponent<Projectile3Controller>().SetMoveDirection(projDir);
    }


    void StopShoot()
    {
        CancelInvoke();
    }
}
