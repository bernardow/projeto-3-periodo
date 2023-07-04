using System.Collections.Generic;
using UnityEngine;

public class ObservableCardsTransform
{
    private List<ICardPosObserver> _observers = new List<ICardPosObserver>();

    public void AddObserver(ICardPosObserver target) => _observers.Add(target);

    public void RemoveObserver(ICardPosObserver target)
    {
        if (_observers.Contains(target))
        {
            _observers.Remove(target);
            return;
        }
        
        Debug.LogError("Observer not found");
    }

    public void NotifyObservers(float x, float y, float z, float xRotation, float yRotation, float zRotation, float wRotation, int  id)
    {
        foreach (ICardPosObserver observer in _observers)
        {
            observer.OnNotify(x,y,z,xRotation, yRotation, zRotation, wRotation, id);
        }
    }
}
