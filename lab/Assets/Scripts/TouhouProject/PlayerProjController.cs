using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjController : MonoBehaviour
{
    private double lifespan = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lifespan -= Time.deltaTime;
        if (lifespan <= 0)
        {
            Destroy(gameObject);

        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);

        }

    }
}
