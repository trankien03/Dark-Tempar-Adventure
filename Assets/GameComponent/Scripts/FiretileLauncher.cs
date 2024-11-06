using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiretileLauncher : MonoBehaviour
{   
    //fire of worm
    public GameObject projecttilePrefab;
    public Transform firePoint;

    public void FireProjectile()
    {
        GameObject projectile = Instantiate(projecttilePrefab, firePoint.position, projecttilePrefab.transform.rotation);
        Vector3 oriScale = projectile.transform.localScale;
        //flip the projectile's facing direction and movement based on the direction the character is facing at time of launch
        projectile.transform.localScale = new Vector3(
            oriScale.x * transform.localScale.x > 0 ? 2 : -2,
            oriScale.y,
            oriScale.z
            );
    }

}
