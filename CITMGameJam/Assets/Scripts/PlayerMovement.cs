using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 10f;
    public float sprintMultiplier = 1.5f;
    public float gravity = -9.81f * 2;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private Vector3 velocity;
    private bool isGrounded;
    private bool isMoving = false;
    private Vector3 lastPosition = Vector3.zero;

    // Bobbing Variables
    public float bobbingSpeed = 6f;
    public float bobbingAmount = 0.05f;
    public float sprintBobbingAmount = 0.1f; // Increased bobbing when sprinting
    private float bobbingTimer = 0f;
    private Vector3 originalCameraPosition;
    public Transform playerCamera;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        originalCameraPosition = playerCamera.localPosition;
    }

    void Update()
    {
        // Grounded check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0f)
        {
            velocity.y = -2f;
        }

        // Getting inputs
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        bool isSprinting = Input.GetKey(KeyCode.LeftControl);
        float moveSpeed = isSprinting ? speed * sprintMultiplier : speed;

        // Movement
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * moveSpeed * Time.deltaTime);

        // Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Check movement for bobbing
        isMoving = lastPosition != transform.position && isGrounded;
        lastPosition = transform.position;

        // Apply head bobbing with sprinting effect
        if (isMoving)
        {
            bobbingTimer += Time.deltaTime * bobbingSpeed;
            float bobbingOffset = Mathf.Sin(bobbingTimer) * (isSprinting ? sprintBobbingAmount : bobbingAmount);
            playerCamera.localPosition = originalCameraPosition + new Vector3(0, bobbingOffset, 0);
        }
        else
        {
            bobbingTimer = 0f;
            playerCamera.localPosition = Vector3.Lerp(playerCamera.localPosition, originalCameraPosition, Time.deltaTime * bobbingSpeed);
        }
    }
}
