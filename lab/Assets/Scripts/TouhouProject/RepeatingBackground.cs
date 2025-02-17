using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingBackground : MonoBehaviour
{
    private BoxCollider2D groundCollider;
    private float groundVerticalLength;
    private Rigidbody2D rb2d;
    private float scrollSpeed = -1f;

    // Start is called before the first frame update
    void Start()
    {
        groundCollider = GetComponent<BoxCollider2D>();
        groundVerticalLength = groundCollider.size.y;
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(0, scrollSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -groundVerticalLength)
        {
            RepositionBackground();
        }
    }

    private void RepositionBackground()
    {
        Vector2 groundOffset = new Vector2(0, groundVerticalLength * 2f);
        transform.position = (Vector2)transform.position + groundOffset;
    }
}
