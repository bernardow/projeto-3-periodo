using src.scripts.Hand;
using UnityEngine;

namespace src.scripts.Managers
{
    public class RaycastManager : MonoBehaviour
    {
        //New instance of Observable Objects to implementation of an observer
        private ObservableObject _observableObject;
        
        [Header("External References")][Space(20)]
        [SerializeField] private Camera playerCamera;
        [SerializeField] private CardSelector cardSelector;
        [SerializeField] private Discard discard;
        [SerializeField] private Puller puller;
        [SerializeField] private TargetSelector targetSelector;

        [Header("Layer Mask")][Space(20)]
        [SerializeField] private LayerMask layers;

        [Header("Mobile zone")][Space(20)]
        [SerializeField] private bool isMobile;

        //Set observer
        private void Start()
        {
            _observableObject = new ObservableObject();
            _observableObject.AddObserver(puller);
            _observableObject.AddObserver(cardSelector);
            _observableObject.AddObserver(discard);
            _observableObject.AddObserver(targetSelector);
        }

        //Shoots ray every frame
        private void Update() => ShootRay();

        /// <summary>
        /// Notifies observer when the ray hits something after player clicked
        /// </summary>
        private void ShootRay()
        {
            Ray ray = playerCamera!.ScreenPointToRay(Input.mousePosition);
            Ray phoneRay = new Ray();
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                phoneRay = playerCamera!.ScreenPointToRay(touch.position);
            }

            if (Physics.Raycast(isMobile? phoneRay : ray, out RaycastHit hitInfo, Mathf.Infinity, layers) && Input.GetMouseButtonDown(0))
            {
                _observableObject.NotifyObservers(hitInfo);
            }
        }
    }
}
