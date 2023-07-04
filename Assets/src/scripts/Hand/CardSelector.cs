using System.Collections.Generic;
using UnityEngine;

namespace src.scripts.Hand
{
    public class CardSelector : MonoBehaviour, IObservable
    {
        public List<GameObject> selectedCardsPlaye1 = new List<GameObject>();    //Lista de cartas selececionadas
        private Hand _player;   //Referencia do Player base

        private void Start() => _player = GetComponent<Hand>();     //Atribuicao


        /// <summary>
        /// Muda o material externo da carta
        /// </summary>
        /// <param name="selected">Material desejado</param>
        /// <param name="hitInfo">A carta que deve ser mudada</param>
        private void ChangeMaterial(bool selected, RaycastHit hitInfo)
        {
            GameObject card = hitInfo.collider.gameObject;
            GameObject outline = card.transform.GetChild(0).GetChild(0).gameObject;
            outline.SetActive(selected);
        }

        public void OnNotify(RaycastHit hitTag)
        {
            if (hitTag.collider.CompareTag("MyCards") && !selectedCardsPlaye1.Contains(hitTag.collider.gameObject) && selectedCardsPlaye1.Count < 2)
            {
                ChangeMaterial(true, hitTag);
                selectedCardsPlaye1.Add(hitTag.collider.gameObject);
            }else if (hitTag.collider.CompareTag("MyCards") && selectedCardsPlaye1.Contains(hitTag.collider.gameObject))
            {
                ChangeMaterial(false, hitTag);
                selectedCardsPlaye1.Remove(hitTag.collider.gameObject);
            }
        }
    }
}
