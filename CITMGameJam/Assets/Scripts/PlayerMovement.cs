using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 5f;
    public float sprintSpeed = 8f;
    public float crouchSpeed = 2f;
    public float slideSpeed = 10f; // Velocidad de deslizamiento
    public float gravity = -9.81f * 2;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private Vector3 velocity;
    private bool isGrounded;
    private bool isMoving = false;
    private Vector3 lastPosition = new Vector3(0f, 0f, 0f);

    private bool isCrouching = false;
    private bool isSprinting = false;
    private float standingHeight = 2f;
    private float crouchingHeight = 1f;
    private float proneHeight = -0.5f; // Altura al tumbarse
    private bool isSliding = false; // Estado de deslizamiento

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Comprobación de si está en el suelo
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0f)
        {
            velocity.y = -2f;
        }

        // Obtener los inputs de movimiento
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        // **Correr con Left Control**
        if (Input.GetKey(KeyCode.LeftControl) && !isCrouching)
        {
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }

        // **Tumbarse con Left Shift**
        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded)
        {
            if (isSprinting)
            {
                // Deslizar
                isSliding = true;
                StartCoroutine(Slide(move));
            }
            else
            {
                // Tumbarse
                isCrouching = true; // Cambiar a estado agachado
            }
        }

        // Aplicar velocidad: Correr > Normal > Agachado
        float currentSpeed = isSliding ? 0 : (isSprinting ? sprintSpeed : (isCrouching ? crouchSpeed : speed));
        controller.Move(move * currentSpeed * Time.deltaTime);

        // Saltar (Solo si no está agachado)
        if (Input.GetButtonDown("Jump") && isGrounded && !isCrouching)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Aplicar gravedad
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Comprobar si el jugador está en movimiento
        isMoving = lastPosition != transform.position && isGrounded;
        lastPosition = transform.position;

        // **Agacharse con C**
        if (Input.GetKeyDown(KeyCode.C))
        {
            isCrouching = !isCrouching; // Alternar estado de agachado
        }

        // Aplicar cambio de altura suavemente
        float targetHeight = isCrouching ? (Input.GetKey(KeyCode.LeftShift) ? proneHeight : crouchingHeight) : standingHeight;
        controller.height = Mathf.Lerp(controller.height, targetHeight, Time.deltaTime * 10f);
    }

    private IEnumerator Slide(Vector3 move)
    {
        float slideDuration = 0.5f; // Duración del deslizamiento
        float slideTime = 0f;

        while (slideTime < slideDuration)
        {
            controller.Move(move * slideSpeed * Time.deltaTime);
            slideTime += Time.deltaTime;
            yield return null;
        }

        isSliding = false; // Termina el deslizamiento
    }
}