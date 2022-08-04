using Interface;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    [SerializeField] GameObject arrowDecal;
    [SerializeField] int damageAmount;

    readonly float speed = 100f;
    readonly float timeToDestroy = 3f;

    public Vector3 target{ get; set; }
    public bool hit{ get; set; }

    void Update(){
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (!hit && Vector3.Distance(transform.position, target) < .01f) Destroy(gameObject);
    }

    void OnEnable(){
        Destroy(gameObject, timeToDestroy);
    }

    void OnCollisionEnter(Collision collision){
        var damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable == null) return;
        damageable.Damage(damageAmount);

        var contact = collision.GetContact(0);
        Instantiate(arrowDecal, contact.point + contact.normal * .001f, Quaternion.LookRotation(contact.normal));
        Destroy(gameObject);
    }
}