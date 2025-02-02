using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBobbing : MonoBehaviour
{
    public float bobbingAmount = 0.1f; // Cantidad de bobbing
    public float bobbingSpeed = 10f; // Velocidad del bobbing
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
            float newY = originalPosition.y + Mathf.Sin(Time.time * bobbingSpeed) * bobbingAmount;
            transform.localPosition = new Vector3(originalPosition.x, newY, originalPosition.z);
        }
        else
        {
            transform.localPosition = originalPosition; // Restablecer a la posición original si no se está moviendo
        }
    }
}