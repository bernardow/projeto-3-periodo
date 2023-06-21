using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObserverCard
{
    void OnNotify(CardPlayer player);
}
