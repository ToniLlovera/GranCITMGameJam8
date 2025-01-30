using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public int bulletDamage;

    private void OnCollisionEnter(Collision objectWeHit)
    {
        if(objectWeHit.gameObject.CompareTag("Target"))
        {
            print("Hit " + objectWeHit.gameObject.name + " !");

            CreateBulletImpactEffect(objectWeHit);

            Destroy(gameObject);    
        }

        if (objectWeHit.gameObject.CompareTag("Wall"))
        {
            print("Hit a Wall !");
            CreateBulletImpactEffect(objectWeHit);
            Destroy(gameObject);
        }
        if (objectWeHit.gameObject.CompareTag("Ground"))
        {
            print("Hit Ground !");
            CreateBulletImpactEffect(objectWeHit);
            Destroy(gameObject);
        }
        if (objectWeHit.gameObject.CompareTag("Bottle"))
        {
            print("Hit Bottle !");
            objectWeHit.gameObject.GetComponent<Bottle>().Shatter();
        }
        if (objectWeHit.gameObject.CompareTag("Robot"))
        {
            print("Hit Robot !");
            objectWeHit.gameObject.GetComponent<Robot>().TakeDamage(bulletDamage);
            Destroy(gameObject);
        }
    }

    void CreateBulletImpactEffect(Collision objectWeHit)
    {
        ContactPoint contact = objectWeHit.contacts[0];

        GameObject hole = Instantiate(
            GlobalReferences.Instance.bulletImpactEffectPrefab,
            contact.point,
            Quaternion.LookRotation(contact.normal)

            );

        hole.transform.SetParent(objectWeHit.gameObject.transform);
    }

}
