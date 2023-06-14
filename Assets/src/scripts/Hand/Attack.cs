using System;
using Photon.Pun;
using UnityEngine;

namespace src.scripts.Hand
{
    public class Attack : MonoBehaviour
    {
        private Hand _player;

        private void Start() => _player = GetComponent<Hand>();

        private void ThrowCard(GameObject target)
        {
            if (_player._cardSelector.selectedCardsPlaye1.Count is > 0 and < 2)
            {
                _player._cardSelector.selectedCardsPlaye1[0].GetComponent<Rigidbody>().AddForce(target.transform.position - transform.position);
                _player.photonViewPlayer.RPC("DealDamage", RpcTarget.All, target.GetComponent<PhotonView>().ViewID);
                _player.player1Hand.Remove(_player._cardSelector.selectedCardsPlaye1[0]);
                _player._cardSelector.selectedCardsPlaye1.Remove(_player._cardSelector.selectedCardsPlaye1[0]);
            }
        }


        [PunRPC]
        public void DealDamage(int id)
        {
            GameObject targetPlayer = PhotonView.Find(id).gameObject;
            CardPlayer targetCardPlayer = targetPlayer.GetComponent<CardPlayer>();
            targetCardPlayer.life--;
        }
    }
}
