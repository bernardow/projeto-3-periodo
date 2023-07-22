using Photon.Pun;
using src.scripts.Managers;
using UnityEngine;

namespace src.scripts.Hand
{
    public class Attack : MonoBehaviour
    {
        private Hand _player;
        [SerializeField] private float force; //Shot force

        private void Start() => _player = GetComponent<Hand>();

        /// <summary>
        /// Throw Card at a target
        /// </summary>
        /// <param name="target">Target of choice</param>
        /// <param name="damage">Damage of the card</param>
        public void ThrowCard(GameObject target, int damage)
        {
            if (_player.CardSelector.selectedCardsPlaye1.Count is > 0 and < 2)
            {
                Rigidbody cardRigidbody = _player.CardSelector.selectedCardsPlaye1[0].AddComponent<Rigidbody>();
                
                //Throw card, deal damage and reset bonus
                cardRigidbody.AddForce((target.transform.GetChild(2).position - _player.CardSelector.selectedCardsPlaye1[0].transform.position + Vector3.up * 3) * force, ForceMode.Impulse);
                _player.photonViewPlayer.RPC("DealDamage", RpcTarget.All, target.GetComponent<PhotonView>().ViewID, damage);
                _player.CardPlayer.bonus = 1;
                
                //Remove the card from player
                _player.RemoveCardFromPlayer(_player.CardSelector.selectedCardsPlaye1[0]);

                //Play effect
                AudioManager.Instance.Play("AttackEffect");
            }
        }
    }
}
