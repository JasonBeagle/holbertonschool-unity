using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    public float jumpForce = 5.0f;
    public float fallThreshold = -10.0f;
    public float turnSpeed = 5.0f; // Speed of turning
    private bool isJumping = false;

    private CharacterController characterController;
    private Vector3 startPosition;

    private float verticalVelocity = 0.0f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        startPosition = transform.position;
    }

    void Update()
    {
        if (transform.position.y < fallThreshold)
        {
            Vector3 newPosition = new Vector3(startPosition.x, startPosition.y + 10, startPosition.z);
            characterController.enabled = false;
            transform.position = newPosition;
            characterController.enabled = true;
            verticalVelocity = 0.0f;
        }

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 movement = (forward * moveVertical + right * moveHorizontal) * speed * Time.deltaTime;
        characterController.Move(movement);

        // Update character rotation to face direction of movement
        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, turnSpeed * Time.deltaTime);
        }

        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            verticalVelocity = Mathf.Sqrt(2.0f * jumpForce * Mathf.Abs(Physics.gravity.y));
            isJumping = true;
        }

        verticalVelocity += Physics.gravity.y * Time.deltaTime;

        Vector3 verticalMovement = Vector3.up * verticalVelocity * Time.deltaTime;
        characterController.Move(verticalMovement);

        if (characterController.isGrounded)
        {
            isJumping = false;
            verticalVelocity = 0.0f;
        }
    }
}