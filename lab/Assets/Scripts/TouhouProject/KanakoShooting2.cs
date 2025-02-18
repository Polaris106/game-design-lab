using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KanakoShooting2 : MonoBehaviour
{
    public GameObject proj_prefab;
    public KanakoController kanakoControl;
    public Animator kanakoAnimator;

    private float angle = 90f;
    private bool swingLeft;
    private bool firedOnce;

    private Vector2 projectileMoveDirection;
    private GameObject proj;

    // Start is called before the first frame update
    void Start()
    {
        firedOnce = true;
        kanakoAnimator.SetTrigger("skillActive");
        StartShoot();
    }

    void Update()
    {

        if (kanakoControl.currentHealth < 200)
        {
            StopShoot();
            firedOnce = false;
        }
        else if (firedOnce == false)
        {
            StartShoot();
            firedOnce = true;
        }
    }

    private void Shoot()
    {
        float projDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
        float projDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);
        Vector3 projMoveVector = new Vector3(projDirX, projDirY, 0f);
        Vector2 projDir = (projMoveVector - transform.position).normalized;

        SetProjectiles(projDir);

        angle += Random.Range(15f, 20f);
    }

    public void StartShoot()
    {
        InvokeRepeating("Shoot", 0f, 0.03f);
    }

    public void StopShoot()
    {
        CancelInvoke();
    }

    void SetProjectiles(Vector2 projDir)
    {
        // Get projectile instance
        proj = Instantiate(proj_prefab);
        // Set the projectile's position to the current object's position
        proj.transform.position = transform.position;
        proj.transform.rotation = transform.rotation;
        // Set the projectile's movement direction
        proj.GetComponent<Projectile2Controller>().SetMoveDirection(projDir);
    }
}
