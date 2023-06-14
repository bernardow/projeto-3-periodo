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
        /// <param name="material">Material desejado</param>
        /// <param name="hitInfo">A carta que deve ser mudada</param>
        private void ChangeMaterial(Material material, RaycastHit hitInfo)
        {
            Renderer outLineRenderer = null;
            Renderer[] renderers = hitInfo.collider.GetComponentsInChildren<Renderer>();
            foreach (var render in renderers)
            {
                if (render.name != hitInfo.collider.name)
                    outLineRenderer = render;
            }
            outLineRenderer!.material = material;
        }

        public void OnNotify(RaycastHit hitTag)
        {
            if (hitTag.collider.CompareTag("MyCards") && !selectedCardsPlaye1.Contains(hitTag.collider.gameObject) && selectedCardsPlaye1.Count < 2)
            {
                ChangeMaterial(_player.selectedMaterial, hitTag);
                selectedCardsPlaye1.Add(hitTag.collider.gameObject);
            }else if (hitTag.collider.CompareTag("MyCards") && selectedCardsPlaye1.Contains(hitTag.collider.gameObject))
            {
                ChangeMaterial(_player.defaultMaterial, hitTag);
                selectedCardsPlaye1.Remove(hitTag.collider.gameObject);
            }
        }
    }
}
