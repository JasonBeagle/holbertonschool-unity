using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player; // The player GameObject
    public float sensitivity = 100.0f; // Mouse sensitivity
    public Vector3 offset; // Offset from the player

    private float xRotation = 0.0f; // To keep track of vertical rotation

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Hide and lock the cursor
        transform.LookAt(player.transform.position); // Make the camera look at the player
    }

    void LateUpdate()
    {
        // Camera follows the player
        transform.position = player.transform.position + offset;

        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        // Invert and accumulate vertical rotation
        xRotation -= mouseY;
        // Clamp the vertical rotation between -90 and 90 degrees
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Apply rotation
        transform.localRotation = Quaternion.Euler(xRotation + 30, 0f, 0f);

        // Rotate the player horizontally
        player.transform.Rotate(Vector3.up * mouseX);
    }
}
