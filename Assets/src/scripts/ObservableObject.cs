using System.Collections.Generic;
using UnityEngine;

public class ObservableObject
{
    private List<IObservable> _observables = new List<IObservable>();

    public void AddObserver(IObservable obj)
    {
        _observables.Add(obj);
    }

    public void RemoveObserver(IObservable obj)
    {
        _observables.Remove(obj);
    }

    public void NotifyObservers(RaycastHit hitInfo)
    {
        foreach (var observer in _observables)
        {
            observer.OnNotify(hitInfo);
        }
    }
}
