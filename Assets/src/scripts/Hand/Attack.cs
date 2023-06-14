using System;
using Photon.Pun;
using UnityEngine;

namespace src.scripts.Hand
{
    public class Attack : MonoBehaviour
    {
        private Hand _player;
        [SerializeField] private float force;

        private void Start() => _player = GetComponent<Hand>();

        public void ThrowCard(GameObject target)
        {
            if (_player._cardSelector.selectedCardsPlaye1.Count is > 0 and < 2)
            {
                _player._cardSelector.selectedCardsPlaye1[0].AddComponent<Rigidbody>();
                _player._cardSelector.selectedCardsPlaye1[0].GetComponent<Rigidbody>().AddForce((target.transform.position - transform.position) * force, ForceMode.Impulse);
                _player.photonViewPlayer.RPC("DealDamage", RpcTarget.All, target.GetComponent<PhotonView>().ViewID);
                _player.player1Hand.Remove(_player._cardSelector.selectedCardsPlaye1[0]);
                _player._cardSelector.selectedCardsPlaye1.Remove(_player._cardSelector.selectedCardsPlaye1[0]);
            }
        }
    }
}
