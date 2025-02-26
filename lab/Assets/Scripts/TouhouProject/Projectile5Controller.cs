using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile5Controller : MonoBehaviour
{
    private KanakoController kanakoController;
    private int damage = 1;
    private float moveSpeed;

    private Vector2 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        kanakoController = GameObject.Find("Kanako").GetComponent<KanakoController>();
        moveSpeed = 7f;
    }

    // Update is called once per frame
    void Update()
    {
        if (kanakoController.currentHealth <= 0 && kanakoController.secondPhase)
        {
            Destroy();
        }
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

    }

    public void SetMoveDirection(Vector2 dir)
    {
        moveDirection = dir;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TouhouPlayerMovement rm = other.GetComponent<TouhouPlayerMovement>();
            if (rm != null)
            {
                rm.TakeDamage(damage);
            }
            turnInvisible();

        }
    }

    private void OnEnable()
    {
        Invoke("Destroy", 3f);
    }

    private void Destroy()
    {
        undoInvisible();
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }


    void turnInvisible()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        this.GetComponent<BoxCollider2D>().enabled = false;
    }

    void undoInvisible()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        this.GetComponent<BoxCollider2D>().enabled = true;
    }
}
