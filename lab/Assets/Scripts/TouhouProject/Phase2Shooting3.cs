using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase2Shooting3 : MonoBehaviour
{
    public GameObject proj_prefab;
    public KanakoController kanakoControl;
    public ProjectilePool projectilePoolScript;
    public GameObject autoBall2;

    private float angle = 180f;
    private bool swingLeft;
    private bool firedOnce = false;

    private Vector2 projectileMoveDirection;
    private GameObject proj;

    private GameObject gameControl;
    private GameControl gameControlScript;
    private Ball2Controller ball2Controller;

    // Start is called before the first frame update
    void Start()
    {
        gameControl = GameObject.Find("GameControl");
        gameControlScript = gameControl.GetComponent<GameControl>();
        ball2Controller = autoBall2.GetComponent<Ball2Controller>();

    }

    void Update()
    {
        if (ball2Controller.destinationReached)
        {
            if (kanakoControl.currentHealth < 200)
            {
                StopShoot();
                firedOnce = false;
            }
            else if (firedOnce == false && gameControlScript.gameStart)
            {
                StartShoot();
                firedOnce = true;
            }
        }

    }

    private void Shoot()
    {
        if (angle <= 135)
        {
            swingLeft = true;
        }
        else if (angle >= 225f)
        {
            swingLeft = false;
        }

        float projDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
        float projDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);
        Vector3 projMoveVector = new Vector3(projDirX, projDirY, 0f);
        Vector2 projDir = (projMoveVector - transform.position).normalized;
        SetProjectiles(projDir);

        if (swingLeft)
        {
            angle += 25f;
        }
        else
        {
            angle -= 25f;
        }




    }

    public void StartShoot()
    {
        InvokeRepeating("Shoot", 0f, 0.5f);
    }

    public void StopShoot()
    {
        CancelInvoke();
    }

    void SetProjectiles(Vector2 projDir)
    {
        // Get projectile instance
        proj = this.gameObject.GetComponent<ProjectilePool>().GetProjectile();
        // Set the projectile's position to the current object's position
        proj.transform.position = transform.position;
        proj.transform.rotation = transform.rotation;
        // Set the projectile's movement direction
        proj.GetComponent<Projectile2Controller>().SetMoveDirection(projDir);
    }
}
