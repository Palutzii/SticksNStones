using UnityEngine;

public class PlayerGun : MonoBehaviour{
    [SerializeField] Transform firingPoint;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float firingSpeed;

    public static PlayerGun Instance;

    float _lastTimeShot = 0;

    void Awake(){
        Instance = GetComponent<PlayerGun>();
    }

    public void Shoot(){
        if (_lastTimeShot + firingSpeed <= Time.time){
            _lastTimeShot = Time.time;
            Instantiate(projectilePrefab, firingPoint.position, firingPoint.rotation);
        }
        
    }
}
