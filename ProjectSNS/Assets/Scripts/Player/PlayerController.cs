using UnityEngine;

public class PlayerController : MonoBehaviour{
    
    [SerializeField] float movementSpeed;

    void Update(){
        HandleMovementInput();
        HandlerRotationInput();
        HandleShootInput();
    }

    void HandleMovementInput(){
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0, vertical);
        transform.Translate(movement * (movementSpeed * Time.deltaTime), Space.World);
    }

    void HandlerRotationInput(){
        RaycastHit hit;
        if (Camera.main != null){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit)){
                transform.LookAt(new Vector3(hit.point.x, transform.position.y,hit.point.z));
            }
        }
    }

    void HandleShootInput(){
        if (Input.GetButton("Fire1")){
            //Shoot
            PlayerGun.Instance.Shoot();
        }
    }
}
