using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.AI;

public class Robot : MonoBehaviour
{
    [SerializeField] private int HP = 100;
    private Animator animator;


    private NavMeshAgent navAgent;

    public bool isDead;
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
            isDead = true;

            // Dead Sound
            SoundManager.Instance.robotChannel.PlayOneShot(SoundManager.Instance.robotDeath);
        }
        else
        {
            animator.SetTrigger("DAMAGE");
            SoundManager.Instance.robotChannel.PlayOneShot(SoundManager.Instance.robotHurt);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 2.5f); //Attacking // Stop Attacking

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 18f); //Detection (start chasing)

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 21f); //Stop Chasing
    }
}
