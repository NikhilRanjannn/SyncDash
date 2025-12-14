using UnityEngine;

public struct SyncPacket
{
    public Vector3 position;
    public Vector3 velocity;
    public float horizontalInput;
    public bool jumpInput;
    public float timestamp;

    public SyncPacket(Vector3 pos, Vector3 vel, float h, bool jump, float time)
    {
        position = pos;
        velocity = vel;
        horizontalInput = h;
        jumpInput = jump;
        timestamp = time;
    }
}
