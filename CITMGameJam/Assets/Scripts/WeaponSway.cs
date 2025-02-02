using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    public float swayAmount = 0.1f; // Cantidad de sway
    public float swaySpeed = 5f; // Velocidad del sway
    private Vector3 originalPosition;

    private PlayerMovement playerMovement;

    void Start()
    {
        originalPosition = transform.localPosition;
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    void Update()
    {
        if (playerMovement.isMoving)
        {
            float moveX = Input.GetAxis("Horizontal") * swayAmount;
            float moveY = Input.GetAxis("Vertical") * swayAmount;

            Vector3 swayPosition = new Vector3(moveX, 0, moveY) * swaySpeed;
            transform.localPosition = originalPosition + swayPosition;
        }
        else
        {
            transform.localPosition = originalPosition; // Restablecer a la posición original si no se está moviendo
        }
    }
}