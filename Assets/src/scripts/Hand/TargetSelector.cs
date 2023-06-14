using UnityEngine;

namespace src.scripts.Hand
{
    public class TargetSelector : MonoBehaviour, IObservable
    {
        private Hand _player;
        public GameObject selectedPlayer;
        
        // Start is called before the first frame update
        void Start()
        {
            _player = GetComponent<Hand>();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void OnNotify(RaycastHit hit)
        {
            if (hit.collider.CompareTag("Player") && _player._cardSelector.selectedCardsPlaye1.Count is > 0 and < 2)
            {
                selectedPlayer = hit.transform.parent.gameObject;
                _player.attack.ThrowCard(selectedPlayer);
            }
                
        }
    }
}
