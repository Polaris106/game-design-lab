using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public KanakoController kanakoController;
    public Transform destination;
    public GameObject beam1;
    public GameObject beam2;

    // Rotation speed in degrees per second
    private float rotationSpeed = 25f;
    private float moveSpeed = 3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        // Rotate the GameObject around the Z-axis (for 2D)
        Vector3 worldRotation = new Vector3(0, 0, rotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + worldRotation);

        if (kanakoController.currentHealth < 200)
        {
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, destination.position, step);
            if (transform.position == destination.position)
            {
                beam1.SetActive(true);
                beam2.SetActive(true);
            }

        }
    }
}
