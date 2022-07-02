using UnityEngine;

public class PlayerGun : MonoBehaviour{
    [SerializeField] Transform firingPoint;
    [SerializeField] float firingSpeed;

    public static PlayerGun Instance;

    float _lastTimeShot = 0;

    void Awake(){
        Instance = GetComponent<PlayerGun>();
    }

    public void Shoot(){
        if (_lastTimeShot + firingSpeed <= Time.time){

            Projectile _projectile = ProjectilePool.Instance.Instantiate(firingPoint.position, firingPoint.rotation);
            _projectile.Move();
            _lastTimeShot = Time.time;
        }
        
    }
}
