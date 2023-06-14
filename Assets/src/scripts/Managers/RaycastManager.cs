using System;
using System.Collections;
using System.Collections.Generic;
using src.scripts.Hand;
using UnityEngine;

public class RaycastManager : MonoBehaviour
{
    private ObservableObject _observableObject;
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private CardSelector cardSelector;
    [SerializeField] private Discard discard;
    [SerializeField] private Puller puller;

        private void Start()
    {
        _observableObject = new ObservableObject();
        _observableObject.AddObserver(puller);
        _observableObject.AddObserver(cardSelector);
        _observableObject.AddObserver(discard);
    }

    private void Update() => ShootRay();

    private void ShootRay()
    {
        Ray ray = _playerCamera!.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity) && Input.GetMouseButtonDown(0))
        {
            _observableObject.NotifyObservers(hitInfo);
        }
    }
}
