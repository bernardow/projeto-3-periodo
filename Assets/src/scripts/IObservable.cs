using UnityEngine;

namespace src.scripts
{
    public interface IObservable
    {
        void OnNotify(RaycastHit hit);
    }
}
