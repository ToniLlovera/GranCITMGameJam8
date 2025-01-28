using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector2 moveInput;
    public float speed = 5f;
    private float originalSpeed;

    private Vector3 playerVelocity;
    private bool grounded;
    public float gravity = -9.8f;
    public float jumpForce = 1.5f;

    public Camera cam;
    private Vector2 lookPos;
    private float xRotation = 0f;
    public float xSens = 20f;
    public float ySens = 20f;

    private bool crouching = false;
    private bool sprinting = false;
    private float originalHeight;
    public float crouchHeight = 1f;
    public float crouchSpeed = 2.5f;

    private float targetHeight;
    public float heightTransitionSpeed = 5f;

    public float walkBobSpeed = 8f;
    public float walkBobAmount = 0.05f;
    public float sprintBobSpeed = 12f;
    public float sprintBobAmount = 0.1f;
    private float bobTimer = 0f;
    private Vector3 camInitialPosition;

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            jump();
        }
    }
    public void OnLook(InputAction.CallbackContext context)
    {
        lookPos = context.ReadValue<Vector2>();
    }
    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ToggleCrouch();
        }
    }
    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            sprinting = true;
            speed *= 1.5f;
        }
        else if (context.canceled)
        {
            sprinting = false;
            speed = originalSpeed;
        }
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        originalSpeed = speed;
        originalHeight = controller.height;
        targetHeight = originalHeight;
        camInitialPosition = cam.transform.localPosition;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        grounded = controller.isGrounded;
        movePlayer();
        playerLook();
        SmoothCrouchTransition();
        ApplyHeadBobbing();
    }

    public void movePlayer()
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = moveInput.x;
        moveDirection.z = moveInput.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);

        playerVelocity.y += gravity * Time.deltaTime;
        if (grounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void jump()
    {
        if (grounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpForce * -3f * gravity);
        }
    }

    public void playerLook()
    {
        xRotation -= (lookPos.y * Time.deltaTime) * ySens;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * (lookPos.x * Time.deltaTime) * xSens);
    }

    private void ToggleCrouch()
    {
        if (crouching)
        {
            targetHeight = originalHeight;
            speed = originalSpeed;
        }
        else
        {
            targetHeight = crouchHeight;
            speed = crouchSpeed;
        }
        crouching = !crouching;
    }

    private void SmoothCrouchTransition()
    {
        controller.height = Mathf.Lerp(controller.height, targetHeight, Time.deltaTime * heightTransitionSpeed);

        Vector3 center = controller.center;
        center.y = controller.height / 2f;
        controller.center = center;

        camInitialPosition.y = targetHeight == crouchHeight ? crouchHeight * 0.5f : originalHeight * 0.5f;
    }

    private void ApplyHeadBobbing()
    {
        if (grounded && moveInput.magnitude > 0f)
        {
            float bobSpeed = sprinting ? sprintBobSpeed : walkBobSpeed;
            float bobAmount = sprinting ? sprintBobAmount : walkBobAmount;

            bobTimer += Time.deltaTime * bobSpeed;

            float bobOffset = Mathf.Sin(bobTimer) * bobAmount;

            cam.transform.localPosition = new Vector3(
                camInitialPosition.x,
                camInitialPosition.y + bobOffset,
                camInitialPosition.z
            );
        }
        else
        {
            bobTimer = 0f;
            cam.transform.localPosition = Vector3.Lerp(cam.transform.localPosition, camInitialPosition, Time.deltaTime * heightTransitionSpeed);
        }
    }
}

