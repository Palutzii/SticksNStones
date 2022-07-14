using UnityEngine;

public class Projectile : MonoBehaviour{
    [SerializeField] float projectileSpeed;
    [SerializeField] float maxProjectileDistance;
    
    Vector3 _firingPoint;
    bool _iShouldMove = false;

    void Start(){
        
    }

    void Update(){
        if (_iShouldMove){
            MoveProjectile();
        }
    }

    public void Move(){
        _iShouldMove = true;
        _firingPoint = transform.position;
    }

    void MoveProjectile(){
        if (Vector3.Distance(_firingPoint,transform.position) > maxProjectileDistance){
            ProjectilePool.Instance.ReturnToPool(this);
            _iShouldMove = false;
        }
        else{
           transform.Translate(Vector3.forward * (projectileSpeed * Time.deltaTime)); 
        }
        
    }
}
