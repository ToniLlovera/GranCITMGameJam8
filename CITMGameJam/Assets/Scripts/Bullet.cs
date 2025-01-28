using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Target"))
        {
            print("Hit " + collision.gameObject.name + " !");
            Destroy(gameObject);    
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            print("Hit a Wall !");
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            print("Hit Ground !");
            Destroy(gameObject);
        }
    }
}
