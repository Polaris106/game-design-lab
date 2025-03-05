using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform player; // Mario's Transform
    public Transform endLimitX; // GameObject that indicates end of map
    public Transform endLimitY; // GameObject that indicates end of map
    private float offsetX; // initial x-offset between camera and Mario
    private float offsetY; // initial y-offset between camera and Mario
    private float startX; // smallest x-coordinate of the Camera
    private float startY; // smallest y-coordinate of the Camera
    private float endX; // largest x-coordinate of the camera
    private float endY; // largest y-coordinate of the camera
    private float viewportHalfWidthX;
    private float viewportHalfHeightY;
    private Transform startPosition;

    void Start()
    {
        // get coordinate of the bottomleft of the viewport
        // z doesn't matter since the camera is orthographic
        player = GameObject.Find("Mario").transform;
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)); // the z-component is the distance of the resulting plane from the camera 
        viewportHalfWidthX = Mathf.Abs(bottomLeft.x - this.transform.position.x);
        viewportHalfHeightY = Mathf.Abs(bottomLeft.y - this.transform.position.y);
        offsetX = this.transform.position.x - player.position.x;
        offsetY = this.transform.position.y - player.position.y;
        startX = this.transform.position.x;
        startY = this.transform.position.y;
        endX = endLimitX.transform.position.x - viewportHalfWidthX;
        endY = endLimitY.transform.position.y;
        this.transform.position = new Vector3(player.position.x + offsetX, this.transform.position.y, -10);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        startPosition = this.transform;
    }

    void Update()
    {
        float desiredX = player.position.x + offsetX;
        float desiredY = player.position.y + offsetY;

        // check if desiredX is within startX and endX
        if (desiredX > startX && desiredX < endX)
        {
            this.transform.position = new Vector3(desiredX, this.transform.position.y, this.transform.position.z);
        }

        // Check if player's y position exceeds half the camera's height and desiredY is within bounds
        //if (player.position.y > startY && desiredY < endY)
        //{
        //    this.transform.position = new Vector3(this.transform.position.x, player.position.y, this.transform.position.z);
        //}

    }


    public void GameRestart()
    {
        // reset camera position
        transform.position = startPosition.position;
        Debug.Log("position reset for camera");
    }
}
