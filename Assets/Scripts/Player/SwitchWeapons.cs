using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWeapons : MonoBehaviour
{
    public GroundSlashShooter groundSlashScript;
    public FPSShooter projectileScript;
    //Fire Tornado

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            groundSlashScript.active = true;
            projectileScript.active = false;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            groundSlashScript.active = false;
            projectileScript.active = true;
            projectileScript.projectile = projectileScript.projectiles[0];
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            groundSlashScript.active = false;
            projectileScript.active = true;
            projectileScript.projectile = projectileScript.projectiles[1];
        }
    }

}
