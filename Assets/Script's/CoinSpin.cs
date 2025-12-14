using UnityEngine;

public class CoinSpin : MonoBehaviour
{
    public float rotateSpeed = 90f;
    public float bobSpeed = 2f;
    public float bobAmount = 0.25f;

    private float startY;

    void Start()
    {
        startY = transform.position.y;
    }

    void Update()
    {
        // Rotate coin
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime, Space.World);

        
        float newY = startY + Mathf.Sin(Time.time * bobSpeed) * bobAmount;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
