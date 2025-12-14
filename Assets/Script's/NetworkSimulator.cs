using UnityEngine;
using System.Collections.Generic;

public class NetworkSimulator : MonoBehaviour
{
    public GhostController ghost;
    public float latency = 0.05f;

    private Queue<SyncPacket> buffer = new Queue<SyncPacket>();

    public void Send(SyncPacket pkt)
    {
        buffer.Enqueue(pkt);
    }

    private void Update()
    {
        if (buffer.Count == 0)
            return;

        SyncPacket pkt = buffer.Peek();

        if (Time.time - pkt.timestamp >= latency)
        {
            ghost.ApplySyncPacket(pkt);   
            buffer.Dequeue();
        }
    }
}
