using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed =  10f;
    public float gravity= -9.81f *2;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;

    bool isGrounded;
    bool isMoving;

    private Vector3 lastPosition = new Vector3(0f, 0f, 0f);
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Grounded check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        // Resseting the default velocity
        if (isGrounded && velocity.y < 0f)
        {
            velocity.y = -2f;
        }

        //Getting the inputs
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Creating the moving vector
        Vector3 move = transform.right * x + transform.forward * z;

        //Actually moving the player
        controller.Move(move * speed * Time.deltaTime);

        // Check if player can jump
        if(Input.GetButtonDown("Jump")&& isGrounded)
        {   
            // Actually Jumping
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Falling Down
        velocity.y += gravity * Time.deltaTime;

        // Executing The Jump
        controller.Move(velocity *  Time.deltaTime);

        if(lastPosition != gameObject.transform.position && isGrounded ==true)
        {
            isMoving = true;
            //for later use
        }
        else
        {
            isMoving = false;
            //for later use
        }
        lastPosition = gameObject.transform.position;
    }
}
