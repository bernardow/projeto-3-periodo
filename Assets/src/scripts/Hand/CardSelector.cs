using System.Collections.Generic;
using UnityEngine;

namespace src.scripts.Hand
{
    public class CardSelector : MonoBehaviour
    {
        public List<GameObject> selectedCardsPlaye1 = new List<GameObject>();    //Lista de cartas selececionadas
        private Hand _player;   //Referencia do Player base

        private void Start() => _player = GetComponent<Hand>();     //Atribuicao

        /// <summary>
        /// Atira um raio da camera do jogador. Se atingir uma carta dele nao selecionada, atualiza a lista de cartas selecionadas e muda o material. Caso o cantrário ele retira os dois
        /// </summary>
        /// <param name="canSelect">Verifica se o Jogador pode selecionar alguma ou mais cartas</param>
        public void SelectCard(bool canSelect)
        {
            if (canSelect)
            {
                Ray ray = _player.playerCamera!.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity) && Input.GetMouseButtonDown(0))
                {
                    if (hit.collider.CompareTag("MyCards") && !selectedCardsPlaye1.Contains(hit.collider.gameObject) && selectedCardsPlaye1.Count < 2)
                    {
                        ChangeMaterial(_player.selectedMaterial, hit);
                        selectedCardsPlaye1.Add(hit.collider.gameObject);
                    }else if (hit.collider.CompareTag("MyCards") && selectedCardsPlaye1.Contains(hit.collider.gameObject))
                    {
                        ChangeMaterial(_player.selectedMaterial, hit);
                        selectedCardsPlaye1.Remove(hit.collider.gameObject);
                    }
                }
            }
        }

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
        
    }
}
