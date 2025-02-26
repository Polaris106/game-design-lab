using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Projectile3Controller : MonoBehaviour
{

    private GameObject leftBoundary;
    private GameObject rightBoundary;
    private GameObject topBoundary;
    private GameObject bottomBoundary;
    private Transform rotationPoint;
    private KanakoController kanakoController;
    private int damage = 1;
    private float moveSpeed;
    private float timeToChangeMotion;
    private float timeToChangeMotionAgain;
    private float moveDistance = 2f;
    private float distanceMoved = 0f;
    private Vector3 initialPosition;
    private bool motionChanged = false;
    private bool isRotating = false;
    private int launchProbability;
    private float rotationSpeed = 45;
    private Vector2 moveDirection;
    private Vector3 rotatingAxis;
    private bool hasTeleported = false;
    private Vector3 movement;

    // Start is called before the first frame update
    void Start()
    {
        kanakoController = GameObject.Find("Kanako").GetComponent<KanakoController>();
        moveSpeed = 3f;
        timeToChangeMotion = 0.7f;
        timeToChangeMotionAgain = 2f;
        launchProbability = Random.Range(0, 2);
        rotatingAxis = new Vector3(0, 0, 1);
        rotationPoint = GameObject.Find("KanakoBackground").transform;
        initialPosition = transform.position;

        leftBoundary = GameObject.Find("LeftBoundary");
        rightBoundary = GameObject.Find("RightBoundary");
        topBoundary = GameObject.Find("TopBoundary");
        bottomBoundary = GameObject.Find("BottomBoundary");
    }

    // Update is called once per frame
    void Update()
    {
        if (kanakoController.currentHealth <= 0)
        {
            Destroy();

        }
    }

    private void FixedUpdate()
    {

        if (!motionChanged)
        {
            distanceMoved += movement.magnitude;

            if (distanceMoved >= moveDistance)
            {
                moveSpeed = 0f;
                motionChanged = true;
                isRotating = true;
            }

        }
        if (isRotating)
        {
            Debug.Log("rotating now");
            transform.RotateAround(rotationPoint.position, rotatingAxis, rotationSpeed * Time.deltaTime);
            timeToChangeMotionAgain -= Time.deltaTime;
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
        if (timeToChangeMotionAgain <= 0)
        {
            if (launchProbability == 1)
            {
                isRotating = false;
                moveSpeed = 2f;
            }
            else
            {
                turnInvisible();
            }

        }
        //transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        movement = moveDirection * moveSpeed * Time.deltaTime;
        transform.Translate(movement);
        WrapAroundBoundaries();
    }

    void WrapAroundBoundaries()
    {
        // Get boundary positions
        float leftLimit = leftBoundary.transform.position.x;
        float rightLimit = rightBoundary.transform.position.x;
        float topLimit = topBoundary.transform.position.y;
        float bottomLimit = bottomBoundary.transform.position.y;

        Vector3 position = transform.position;

        // Check if the projectile exceeds any boundaries
        if (!hasTeleported)
        {
            if (position.x < leftLimit) // Exceeded left boundary
            {
                position.x = rightLimit;
                hasTeleported = true;
            }
            else if (position.x > rightLimit) // Exceeded right boundary
            {
                position.x = leftLimit;
                hasTeleported = true;
            }

            if (position.y > topLimit) // Exceeded top boundary
            {
                position.y = bottomLimit;
                hasTeleported = true;
            }
            else if (position.y < bottomLimit) // Exceeded bottom boundary
            {
                position.y = topLimit;
                hasTeleported = true;
            }
        }


        // Update the projectile position
        transform.position = position;
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
        //if (other.CompareTag("Projectile"))
        //{
        //    Destroy();
        //}

    }

    private void OnEnable()
    {
        Invoke("Destroy", 13f);
    }

    private void Destroy()
    {
        undoInvisible();
        motionChanged = false;
        isRotating = false;
        timeToChangeMotionAgain = 2f;
        distanceMoved = 0f;
        moveSpeed = 2f;
        hasTeleported = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
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
