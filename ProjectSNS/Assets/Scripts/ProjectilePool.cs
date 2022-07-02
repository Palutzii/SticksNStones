using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour{
    [SerializeField] float poolSize;
    [SerializeField] GameObject projectilePrefab;

    List<Projectile> _projectilesInPool;

    public static ProjectilePool Instance;

    void Awake(){
        Instance = GetComponent<ProjectilePool>();
    }

    void Start()
    {
        InitializePool();
    }

    public Projectile Instantiate(Vector3 position, Quaternion rotation){
        Projectile _projectile = _projectilesInPool[0];
        _projectile.transform.position = position;
        _projectile.transform.rotation = rotation;
        _projectilesInPool.Remove(_projectile);

        return _projectile;
    }

    public void ReturnToPool(Projectile _projectile){
        _projectile.transform.position = transform.position;
        _projectilesInPool.Add(_projectile);
    }

    void InitializePool(){
        _projectilesInPool = new List<Projectile>();
        
        for (int i = 0; i < poolSize; i++){
            GameObject _projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
            _projectilesInPool.Add(_projectile.GetComponent<Projectile>());
        }
    }
}
