using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePointController : MonoBehaviour
{
    [SerializeField]
    GameObject laserPrefab;

    [SerializeField]
    float fireRate = 2.0f;

    float nextFireTime;

    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            FireLaser();
        }
    }

    void FireLaser()
    {
        if (laserPrefab != null)
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, transform.rotation);
            nextFireTime = Time.time + 1.0f / fireRate;
        }
    }
}