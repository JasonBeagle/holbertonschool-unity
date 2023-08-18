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
    private float distanceToGround;
    private Animator animator;
    private Vector3 initialChildPosition;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        startPosition = transform.position;
        animator = GetComponentInChildren<Animator>();
        initialChildPosition = transform.GetChild(0).localPosition;
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
            animator.SetBool("IsJumping", true);
        }

        verticalVelocity += Physics.gravity.y * Time.deltaTime;

        Vector3 verticalMovement = Vector3.up * verticalVelocity * Time.deltaTime;
        characterController.Move(verticalMovement);

        if (characterController.isGrounded)
        {
            isJumping = false;
            verticalVelocity = 0.0f;
            animator.SetBool("IsJumping", false);
            transform.GetChild(0).localPosition = initialChildPosition;
        }
        
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        bool isRunning = horizontalInput != 0f || verticalInput != 0f;
        animator.SetBool("IsRunning", isRunning);
    
        bool isFalling = !isJumping && !characterController.isGrounded && verticalVelocity < 0;
        animator.SetBool("IsFalling", isFalling);

        bool isGrounded = IsPlayerGrounded();
        animator.SetBool("IsGrounded", isGrounded);
        Debug.Log("Is Grounded: " + isGrounded);
    
        if (characterController.isGrounded && Vector3.Distance(transform.position, startPosition) < 0.1f)
        {
            animator.SetBool("HasLanded", true); // or animator.SetBool("HasLanded", true);
        }
    }

    bool IsPlayerGrounded()
    {
        float extraHeightText = 0.1f;
        bool hitGround = Physics.Raycast(characterController.bounds.center, Vector3.down, characterController.bounds.extents.y + extraHeightText);
        return hitGround;
    }
}
