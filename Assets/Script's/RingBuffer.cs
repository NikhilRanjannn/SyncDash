using UnityEngine;

public class RingBuffer<T>
{
    private T[] buffer;
    private int head = 0;
    private int tail = 0;
    private int count = 0;
    private int capacity;

    public int Count => count;

    public RingBuffer(int size)
    {
        capacity = Mathf.Max(1, size);
        buffer = new T[capacity];
    }

    public void Push(T item)
    {
        buffer[tail] = item;
        tail = (tail + 1) % capacity;

        if (count == capacity)
        {
            head = (head + 1) % capacity;
        }
        else count++;
    }

    public bool TryPop(out T item)
    {
        if (count == 0)
        {
            item = default;
            return false;
        }

        item = buffer[head];
        head = (head + 1) % capacity;
        count--;
        return true;
    }
}
