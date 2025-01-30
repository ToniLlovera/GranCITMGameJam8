using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Robot : MonoBehaviour
{
    [SerializeField] private int HP = 100;
    private Animator animator;


    private NavMeshAgent navAgent;
    private void Start()
    {
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
    }

    public void TakeDamage (int damageAmount)
    {
        HP -= damageAmount;

        if(HP <0)
        {
            animator.SetTrigger("DIE");
        }
        else
        {
            animator.SetTrigger("DAMAGE");
        }
    }

    private void Update()
    {
        if (navAgent.velocity.magnitude > 0.1)
        {
            animator.SetBool("isWalking", true);
        }
        else
        { 
            animator.SetBool("isWalking", false);
        }
    }
}
