using UnityEngine;

public class CameraController : MonoBehaviour{
    [SerializeField] Transform target;
    [SerializeField] Vector3 targetOffset;
    [SerializeField] float movementSpeed;

    void Update(){
        MoveCamera();
    }

    void MoveCamera(){
        transform.position = Vector3.Lerp(transform.position, target.position + targetOffset,
            movementSpeed * Time.deltaTime);
    }
}
