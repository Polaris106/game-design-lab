using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile1Controller : MonoBehaviour
{
    private int damage = 1;
    private float move_speed;

    private Vector2 moveDirection;
    private float lifespan = 6f;

    // Start is called before the first frame update
    void Start()
    {

        move_speed = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        lifespan -= Time.deltaTime;
        if (lifespan <= 0)
        {
            Destroy(gameObject);

        }
        transform.Translate(moveDirection * move_speed * Time.deltaTime);

    }

    public void SetMoveDirection(Vector2 dir)
    {
        moveDirection = dir;
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ReimuMovement rm = other.GetComponent<ReimuMovement>();
            if (rm != null)
            {
                rm.TakeDamage(damage);
            }
            Destroy();

        }
    }
}
