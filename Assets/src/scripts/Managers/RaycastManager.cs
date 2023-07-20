using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using src.scripts;
using src.scripts.Hand;
using UnityEngine;

public class RaycastManager : MonoBehaviour
{
    private ObservableObject _observableObject;
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private CardSelector cardSelector;
    [SerializeField] private Discard discard;
    [SerializeField] private Puller puller;
    [SerializeField] private TargetSelector targetSelector;

    [SerializeField] private LayerMask layers;

    [SerializeField] private bool isMobile;

        private void Start()
    {
        _observableObject = new ObservableObject();
        _observableObject.AddObserver(puller);
        _observableObject.AddObserver(cardSelector);
        _observableObject.AddObserver(discard);
        _observableObject.AddObserver(targetSelector);
    }

    private void Update() => ShootRay();

    private void ShootRay()
    {
        Ray ray = _playerCamera!.ScreenPointToRay(Input.mousePosition);
        Ray phoneRay = new Ray();
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            phoneRay = _playerCamera!.ScreenPointToRay(touch.position);
        }

        if (Physics.Raycast(isMobile? phoneRay : ray, out RaycastHit hitInfo, Mathf.Infinity, layers) && Input.GetMouseButtonDown(0))
        {
            _observableObject.NotifyObservers(hitInfo);
        }
    }
}
