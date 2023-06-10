using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Queue<T> : IPunObservable
{
    private T playerData;
    
    public List<T> _queue = new List<T>();

    
    
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

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(playerData);
            stream.SendNext(_queue);
        }
        else
        {
            playerData = (T)stream.ReceiveNext();
            _queue = (List<T>)stream.ReceiveNext();
        }
    }
}
