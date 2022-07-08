using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour{

    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float TimeBetweenShoots;

    public static PlayerShoot Instance;

    float _timeStamp = 0f;

    void Awake(){
        Instance = GetComponent<PlayerShoot>();
    }

    void FixedUpdate()
    {
        
    }

    public void ShootInput(){
        if ((Time.time >= _timeStamp) && Input.GetButtonDown("Fire1")){
            Fire();
            _timeStamp = Time.time + TimeBetweenShoots;
        }
    }

    void Fire(){
        var projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        //Add velocity to the bullet
        projectile.GetComponent<Rigidbody>().velocity = projectile.transform.forward * 50;
        Destroy(projectile,2f);
    }
}
