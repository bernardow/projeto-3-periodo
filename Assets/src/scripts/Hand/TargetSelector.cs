using Photon.Pun;
using src.scripts.Deck;
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
        
        public void OnNotify(RaycastHit hit)
        {
            if (hit.collider.CompareTag("Player") && _player._cardSelector.selectedCardsPlaye1.Count is > 0 and < 2)
            {
                selectedPlayer = hit.transform.parent.gameObject;
                GameObject card = _player._cardSelector.selectedCardsPlaye1[0];
                CardUnit cardUnit = card.GetComponent<CardUnit>();
                FgLibrary.CardsType cardType = cardUnit.cardsType;
                if (cardType == FgLibrary.CardsType.ForceDiscard)
                {
                    PhotonView targetPhotonView = selectedPlayer.GetComponent<PhotonView>();
                    _player.photonViewPlayer.RPC("DiscardTwo", RpcTarget.Others, targetPhotonView.ViewID);
                    _player.trash.MoveToTrash(card, _player.player1Hand, _player._cardSelector.selectedCardsPlaye1, _player.playerManager);
                    _player.turnManager.GetComponent<PhotonView>().RPC("SkipTurn", RpcTarget.All);
                    return;
                }else if (cardType == FgLibrary.CardsType.RainbowDamage)
                {
                    int damage = Mathf.FloorToInt(selectedPlayer.GetComponent<CardPlayer>().life / 4);
                    _player.playerManager.playerCardsNum--;
                    _player.attack.ThrowCard(selectedPlayer, damage);
                    return;
                }
                    
                _player.attack.ThrowCard(selectedPlayer, cardUnit.card.damage * _player.cardPlayer.bonus);
            }
                
        }
    }
}
