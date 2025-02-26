using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball2Controller : MonoBehaviour
{
    public KanakoController kanakoController;
    public Transform destination;
    public GameObject beam1;
    public GameObject beam2;
    public GameObject beam3;
    public GameObject beam4;
    public bool destinationReached = false;

    // Rotation speed in degrees per second
    private float rotationSpeed = 20f;
    private float moveSpeed = 3f;
    private BeamController beam1Controller;

    // Start is called before the first frame update
    void Start()
    {
       beam1Controller = beam1.GetComponent<BeamController>();
    }

    void Update()
    {
        // Rotate the GameObject around the Z-axis (for 2D)
        if (beam1Controller.canRotate)
        {
            Vector3 worldRotation = new Vector3(0, 0, rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + worldRotation);
        }


        if (kanakoController.secondPhase)
        {
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, destination.position, step);
            if (transform.position == destination.position)
            {
                destinationReached = true;
                beam1.SetActive(true);
                beam2.SetActive(true);
                beam3.SetActive(true);
                beam4.SetActive(true);
            }

        }
        if (kanakoController.currentHealth <= 0 && kanakoController.secondPhase)
        {
            gameObject.SetActive(false);
        }
    }
}
