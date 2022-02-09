using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSlashShooter : MonoBehaviour
{
    public Camera cam;
    public GameObject projectile;
    public Transform firePoint;
    public float fireRate = 4f;

    private Vector3 destination;
    private float timeToFire;
    private GroundSlash groundSlashScript;

    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= timeToFire)
        {
            timeToFire = Time.time + 1 / fireRate;
            ShootProjectile();
        }
    }

    private void ShootProjectile()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        destination = ray.GetPoint(1000);

        InstantiateProjectile();
    }

    private void InstantiateProjectile()
    {
        var ProjectileObj = Instantiate(projectile, new Vector3(firePoint.position.x, firePoint.position.y, firePoint.position.z-1 ), Quaternion.identity);

        groundSlashScript = ProjectileObj.GetComponent<GroundSlash>();
        RotateToDestination(ProjectileObj, destination, true);
        ProjectileObj.GetComponent<Rigidbody>().velocity = transform.forward * groundSlashScript.speed;
    }

    private void RotateToDestination(GameObject obj, Vector3 destination, bool onlyY)
    {
        var direction = destination - obj.transform.position;
        var rotation = Quaternion.LookRotation(direction);

        if (onlyY)
        {
            rotation.x = 0;
            rotation.z = 0;
        }

        obj.transform.localRotation = Quaternion.Lerp(obj.transform.rotation, rotation, 1);
    }
}
