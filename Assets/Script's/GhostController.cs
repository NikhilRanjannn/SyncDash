using UnityEngine;

public class GhostController : MonoBehaviour
{
    public float positionLerpSpeed = 12f;
    public float predictionAmount = 0.10f;

    private Vector3 targetPos;
    private Vector3 targetVel;

    private bool initialized = false;

   
    public void ApplySyncPacket(SyncPacket pkt)
    {
        targetPos = pkt.position;
        targetVel = pkt.velocity;

        if (!initialized)
        {
            transform.position = pkt.position;
            initialized = true;
        }
    }

    private void FixedUpdate()
    {
        if (!initialized)
            return;

        Vector3 predicted = targetPos + targetVel * predictionAmount;

        transform.position = Vector3.Lerp(
            transform.position,
            predicted,
            positionLerpSpeed * Time.fixedDeltaTime
        );
    }
}
