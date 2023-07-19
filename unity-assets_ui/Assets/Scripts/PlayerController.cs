using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f; // Speed of the player
    public float jumpForce = 5.0f; // Jump force
    public float fallThreshold = -10.0f; // Threshold for considering the player falling
    private bool isJumping = false; // Track if the player is currently jumping

    private CharacterController characterController; // The CharacterController component
    private Vector3 startPosition; // The start position of the player

    private float verticalVelocity = 0.0f; // Vertical velocity for the jump

    void Start()
    {
        // Get the CharacterController component
        characterController = GetComponent<CharacterController>();

        // Store the start position
        startPosition = transform.position;
    }

    void Update()
    {
        // Check if the player has fallen off the platform
        if (transform.position.y < fallThreshold)
        {
            // Move the player to the start position
            Vector3 newPosition = new Vector3(startPosition.x, startPosition.y + 10, startPosition.z);
            characterController.enabled = false; // Disable the CharacterController temporarily
            transform.position = newPosition;
            characterController.enabled = true; // Enable the CharacterController again
            // Reset the vertical velocity
            verticalVelocity = 0.0f;
        }

        // Get input from WASD keys
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Create a movement vector from the input, relative to the camera's orientation
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        // Ignore vertical (Y) component of the camera's forward and right vectors
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 movement = (forward * moveVertical + right * moveHorizontal) * speed * Time.deltaTime;

        // Apply the movement
        characterController.Move(movement);

        // If the space key is pressed and the player is not currently jumping
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            // Calculate the required jump velocity based on the desired jump height and gravity
            verticalVelocity = Mathf.Sqrt(2.0f * jumpForce * Mathf.Abs(Physics.gravity.y));
            isJumping = true;
        }

        // Apply gravity
        verticalVelocity += Physics.gravity.y * Time.deltaTime;

        // Apply vertical movement
        Vector3 verticalMovement = Vector3.up * verticalVelocity * Time.deltaTime;
        characterController.Move(verticalMovement);

        // Detect if the character is on the ground
        if (characterController.isGrounded)
        {
            isJumping = false;
            verticalVelocity = 0.0f;
        }
    }
}
