using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeMovement : MonoBehaviour
{
    public float moveSpeed = 3f;

    private bool startMoving = false;
    private GameObject gameControl;
    private GameObject player;
    private bool isAlive;
    private bool gameStart;

    // Start is called before the first frame update
    void Start()
    {
        gameControl = GameObject.Find("GameControl");
        player = GameObject.FindWithTag("Player");
        isAlive = player.GetComponent<FlappyBirdMovement>().isAlive;
    }

    // Update is called once per frame
    void Update()
    {
        gameStart = gameControl.GetComponent<GameControl>().gameStart;

        if (isAlive && !startMoving)
        {
            if (Input.GetKeyDown("space"))
            {
                startMoving = true;
                
            }
        }
        if (startMoving)
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        }

    }

}
