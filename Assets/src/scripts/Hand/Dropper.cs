using System.Collections.Generic;
using static src.scripts.FgLibrary;
using UnityEngine;

namespace src.scripts.Hand
{
    public class Dropper : Hand
    {
        public void DropCard(List<GameObject> playerHand, List<GameObject> selectedCards, List<string> mesaCards, List<Transform> mesaPlaces, Material defaultMat)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag("Mesa 1") && Input.GetMouseButtonDown(0) && selectedCards.Count is > 0 and < 2 && !mesaCards.Contains(selectedCards[0].name))
                {
                    GameObject card = selectedCards[0];
                    PlaceCard(card, mesaPlaces, defaultMat);
                    playerHand.Remove(card);
                    selectedCards.Remove(card);
                    mesaCards.Add(card.name);
                }
            }
        }

        private void PlaceCard(GameObject card, List<Transform> places, Material defaultMaterial)
        {
            card.transform.position = places[0].position + Vector3.up * 0.1f;
            card.transform.rotation = Quaternion.identity;
            card.transform.rotation = Quaternion.Euler(90, 0, 0);

            Renderer render = GetChildComponent<Renderer>(card);
            render.material = defaultMaterial;
            places.Remove(places[0]);
        }
    }
}
