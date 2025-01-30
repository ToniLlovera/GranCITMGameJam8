using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player; // Referencia al transform del jugador
    public float detectionRange = 10f; // Rango de detecci�n
    public float speed = 3f; // Velocidad de movimiento del enemigo
    public float rotationSpeed = 5f; // Velocidad de rotaci�n del enemigo
    public float jumpForce = 1f; // Fuerza del salto
    public float jumpDetectionDistance = 1f; // Distancia para detectar obst�culos antes de saltar

    private Animator animator; // Referencia al Animator del enemigo
    private Rigidbody rb; // Referencia al Rigidbody del enemigo

    void Start()
    {
        // Obtener el componente Animator y Rigidbody del enemigo
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Verifica la distancia al jugador
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Si el jugador est� dentro del rango de detecci�n
        if (distanceToPlayer < detectionRange)
        {
            // Mueve el enemigo hacia el jugador
            Vector3 direction = (player.position - transform.position).normalized;
            direction.y = 0; // Aseg�rate de que el enemigo no se mueva hacia arriba o abajo

            // Rotar el enemigo hacia el jugador
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);

            // Detectar si hay un obst�culo delante
            if (IsObstacleInFront())
            {
                Jump();
            }
            else
            {
                // Mueve el enemigo
                transform.position += direction * speed * Time.deltaTime;
            }

            // Cambia el estado de la animaci�n
            animator.SetBool("isChasing", true);
        }
        else
        {
            // Detiene la animaci�n de persecuci�n
            animator.SetBool("isChasing", false);
        }
    }

    private bool IsObstacleInFront()
    {
        // Realiza un raycast hacia adelante para detectar obst�culos
        RaycastHit hit;
        // Ajusta la direcci�n y la distancia del raycast
        if (Physics.Raycast(transform.position, transform.forward, out hit, jumpDetectionDistance))
        {
            // Verifica si el objeto detectado es un obst�culo
            return hit.collider != null && hit.collider.CompareTag("Obstacle");
        }
        return false;
    }

    private void Jump()
    {
        // Aplica una fuerza de salto al Rigidbody
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        animator.SetTrigger("Jump"); // Cambia a la animaci�n de salto si la tienes
    }
}