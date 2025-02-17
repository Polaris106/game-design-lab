using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouhouCoinMovement : MonoBehaviour
{

    private float speed = 3f;

    private float lifespan = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the downward movement
        Vector3 movement = Vector2.down * speed * Time.deltaTime;

        // Apply the movement to the object's position
        transform.position += movement;

        lifespan -= Time.deltaTime;
        if (lifespan <= 0)
        {
            Destroy(gameObject);

        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Destroy(gameObject);

        }
    }
}
