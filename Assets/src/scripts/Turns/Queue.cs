using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queue<T>
{
    private List<T> _queue = new List<T>();

    public void Insert(T individual)
    {
        _queue.Add(individual);
    }

    public void Remove()
    {
        T peek = _queue[0];
        _queue.Remove(peek);
    }

    public void Clear()
    {
        _queue.Clear();
    }

    public T Peek()
    {
        return _queue[0];
    }
}
