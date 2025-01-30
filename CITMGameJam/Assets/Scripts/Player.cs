using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int HP = 100;

    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;

        if (HP < 0)
        {
            print("Player Dead");

            //GameOver
            //Re Spawn Player
            //Dying Animation

        }
        else
        {
            print("PlayerHit");
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("RobotHit"))
        {
            TakeDamage(other.gameObject.GetComponent<RobotHit>().damage);
        }
    }
}
