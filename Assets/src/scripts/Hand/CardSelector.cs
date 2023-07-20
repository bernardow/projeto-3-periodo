using System.Collections.Generic;
using UnityEngine;

namespace src.scripts.Hand
{
    public class CardSelector : MonoBehaviour, IObservable
    {
        public List<GameObject> selectedCardsPlaye1 = new List<GameObject>();    //Listt of the selected cards
        private Hand _player;   //Player reference

        private void Start() => _player = GetComponent<Hand>();     //Assignments


        /// <summary>
        /// Set the outline
        /// </summary>
        /// <param name="selected">It`s currentle selected</param>
        /// <param name="hitInfo">Card that it`s selected</param>
        private void ChangeMaterial(bool selected, RaycastHit hitInfo)
        {
            GameObject card = hitInfo.collider.gameObject;
            GameObject outline = card.transform.GetChild(0).GetChild(0).gameObject;
            outline.SetActive(selected);
        }

        /// <summary>
        /// Verifies if the card selected it`s already selected and does the action of adding and removing form the list
        /// </summary>
        /// <param name="hitTag">Card that`s being slected</param>
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
