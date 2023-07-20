using Photon.Pun;
using src.scripts.Deck;
using UnityEngine;

namespace src.scripts.Hand
{
    public class TargetSelector : MonoBehaviour, IObservable
    {
        private Hand _player;
        public GameObject selectedPlayer;
        
        void Start() => _player = GetComponent<Hand>();

        /// <summary>
        /// Take care of rainbow damage special card action
        /// </summary>
        private void RainbowDamage()
        {
            int damage = Mathf.FloorToInt(selectedPlayer.GetComponent<CardPlayer>().life / 4);
            _player.PlayerManager.playerCardsNum--;
            _player.Attack.ThrowCard(selectedPlayer, damage);   
        }

        /// <summary>
        /// Takes care of the Discard two Special card
        /// </summary>
        /// <param name="card">Card that`s casting it</param>
        private void ForceDiscard(GameObject card)
        {
            PhotonView targetPhotonView = selectedPlayer.GetComponent<PhotonView>();
            _player.photonViewPlayer.RPC("DiscardTwo", RpcTarget.Others, targetPhotonView.ViewID);
            _player.trash.MoveToTrash(card, _player);
            _player.turnManager.GetComponent<PhotonView>().RPC("SkipTurn", RpcTarget.All);   
        }
        
        /// <summary>
        /// Responsible for throwing the card and make the actions corresponding to it`s type
        /// </summary>
        /// <param name="hit">Check if it`s "Player"</param>
        public void OnNotify(RaycastHit hit)
        {
            if (hit.collider.CompareTag("Player") && _player.CardSelector.selectedCardsPlaye1.Count is > 0 and < 2)
            {
                selectedPlayer = hit.transform.parent.gameObject;
                GameObject card = _player.CardSelector.selectedCardsPlaye1[0];
                CardUnit cardUnit = card.GetComponent<CardUnit>();
                Extensions.CardsType cardType = cardUnit.cardsType;
                
                //Force discard case
                if (cardType == Extensions.CardsType.ForceDiscard)
                {
                    ForceDiscard(card);
                    return;
                }

                //Rainbow Damage case
                if (cardType == Extensions.CardsType.RainbowDamage)
                {
                    RainbowDamage();
                    return;
                }
                    
                _player.Attack.ThrowCard(selectedPlayer, cardUnit.card.damage * _player.CardPlayer.bonus);
            }
                
        }
    }
}
