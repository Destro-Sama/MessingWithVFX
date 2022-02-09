using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Detector detector;
    private bool collided;
    public GameObject impactVFX;
    public float destroyTime = 2f;

    private void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Bullet" && collision.gameObject.tag != "Player" && !collided)
        {
            collided = true;

            if (impactVFX != null)
            {
                ContactPoint contact = collision.contacts[0];
                Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
                Vector3 pos = contact.point;

                var hitVFX = Instantiate(impactVFX, pos, rot) as GameObject;

                Destroy(hitVFX, 2f);
            }

            if (detector != null)
                detector.DestroyAllLinks();
            
            Destroy(gameObject);
        }
    }
}
