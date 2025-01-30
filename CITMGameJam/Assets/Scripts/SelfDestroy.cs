using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestrioy : MonoBehaviour
{
    public float timeForDestruction;


    void Start()
    {
       StartCoroutine(DestroySelf(timeForDestruction)); 
    }

    private IEnumerator DestroySelf(float timeForDestruction)
    {
        yield return new WaitForSeconds(timeForDestruction);

        Destroy(gameObject);
    }
}
