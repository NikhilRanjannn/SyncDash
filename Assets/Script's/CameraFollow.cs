using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;        // PlayerCube
    public Vector3 offset = new Vector3(0, 6f, -10f);
    public float smoothSpeed = 8f;

    private void LateUpdate()
    {
        if (!target) return;

        // Target camera position
        Vector3 desiredPos = target.position + offset;

        // Smooth movement 
        Vector3 smoothPos = Vector3.Lerp(
            transform.position,
            desiredPos,
            smoothSpeed * Time.deltaTime
        );

        transform.position = smoothPos;

        
    }
}
