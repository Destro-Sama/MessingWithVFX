using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Detector detector;
    private bool collided;
    public GameObject impactVFX;
    public float destroyTime = 2f;
    public float ImpactTime = 2f;
    public bool groundImpact;

    public LayerMask ground;

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
                Quaternion rot;
                ContactPoint contact = collision.contacts[0];
                Vector3 pos = contact.point;
                GameObject hitVFX;
                if (!groundImpact)
                {
                    rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
                    hitVFX = Instantiate(impactVFX, pos, rot) as GameObject;
                }
                else
                {
                    RaycastHit hit;
                    Vector3 distance = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
                    if (Physics.Raycast(distance, transform.TransformDirection(-Vector3.up), out hit, 4f, ground))
                    {
                        pos = new Vector3(transform.position.x, hit.point.y, transform.position.z);
                    }
                    else
                    {
                        pos = new Vector3(transform.position.x, 0, transform.position.z);
                    }
                    rot = Quaternion.FromToRotation(Vector3.up, Vector3.up);
                    hitVFX = Instantiate(impactVFX, pos, rot) as GameObject;
                }

                Destroy(hitVFX, ImpactTime);
            }

            if (detector != null)
                detector.DestroyAllLinks();
            
            Destroy(gameObject);
        }
    }
}
