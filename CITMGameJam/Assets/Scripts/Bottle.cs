using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : MonoBehaviour
{
    public List<Rigidbody> allParts = new List<Rigidbody>();
    private Collider bottleCollider;

    private void Awake()
    {
        bottleCollider = GetComponent<Collider>();
    }

    public void Shatter()
    {
        if (bottleCollider != null)
        {
            bottleCollider.enabled = false;
        }

        foreach (Rigidbody part in allParts)
        {
            part.isKinematic = false;
        }
    }
}