using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaWeakPoint : MonoBehaviour
{
    public GameObject goomba;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<PlayerMovement>().onGroundState == false)
            {
                BoxCollider2D boxCollider = goomba.GetComponent<BoxCollider2D>();
                boxCollider.enabled = false;
                goomba.GetComponent<GoombaMovement>().goombaIsAlive = false;
                this.gameObject.SetActive(false);
            }

        }
    }

}
