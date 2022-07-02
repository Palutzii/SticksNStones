using UnityEngine;

public class Projectile : MonoBehaviour{
    [SerializeField] float projectileSpeed;
    [SerializeField] float maxProjectileDistance;
    
    Vector3 _firingPoint;

    void Start(){
        _firingPoint = transform.position;
    }

    void Update(){
        MoveProjectile();
    }

    void MoveProjectile(){
        if (Vector3.Distance(_firingPoint,transform.position) > maxProjectileDistance){
            Destroy(this.gameObject);
        }
        else{
           transform.Translate(Vector3.forward * (projectileSpeed * Time.deltaTime)); 
        }
        
    }
}
