using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSShooter : MonoBehaviour
{
    public Camera cam;
    public List<GameObject> projectiles;
    public GameObject projectile;
    public Transform firePoint;
    public float fireRate = 4f;
    public float projectileSpeed = 30f;
    public float arcRange = 1;

    private Vector3 destination;
    private float timeToFire;

    public bool active;

    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= timeToFire && active)
        {
            timeToFire = Time.time + 1 / fireRate;
            ShootProjectile();
        }
    }

    private void ShootProjectile()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
            destination = hit.point;
        else
            destination = ray.GetPoint(1000);

        InstantiateProjectile();
    }

    private void InstantiateProjectile()
    {
        var ProjectileObj = Instantiate(projectile, firePoint.position, Quaternion.identity) as GameObject;
        ProjectileObj.GetComponent<Rigidbody>().velocity = (destination - firePoint.position).normalized * projectileSpeed;

        iTween.PunchPosition(ProjectileObj, new Vector3(Random.Range(-arcRange, arcRange), Random.Range(-arcRange, arcRange), 0), Random.Range(0.5f, 2f));
    }
}
