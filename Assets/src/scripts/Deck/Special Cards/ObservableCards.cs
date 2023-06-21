using System.Collections.Generic;
using UnityEngine;

public class ObservableCards
{
    private List<IObserverCard> obserservers = new List<IObserverCard>();

    public void AddObserver(IObserverCard element) => obserservers.Add(element);

    public void RemoveObserver(IObserverCard element)
    {
        if (obserservers.Contains(element))
            obserservers.Remove(element);
        else Debug.LogError("Observer not in the list");
    }

    public void NotifyObservers(CardPlayer player)
    {
        foreach (var observer in obserservers)
        {
            observer.OnNotify(player);
        }
    }
}
