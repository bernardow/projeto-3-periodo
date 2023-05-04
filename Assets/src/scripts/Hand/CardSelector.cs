using System.Collections.Generic;
using UnityEngine;

namespace src.scripts.Hand
{
    public class CardSelector : Hand
    {
        private List<GameObject> selectedCards = new List<GameObject>();

        public void SelectCard(Material selectedMaterial, Material defaultMaterial)
        {
            Ray ray = Camera.main!.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity) && Input.GetMouseButtonDown(0))
            {
                if (hit.collider.CompareTag("MyCards") && !selectedCards.Contains(hit.collider.gameObject) && selectedCards.Count < 2)
                {
                    ChangeMaterial(selectedMaterial, hit);
                    selectedCards.Add(hit.collider.gameObject);
                }else if (hit.collider.CompareTag("MyCards") && selectedCards.Contains(hit.collider.gameObject))
                {
                    ChangeMaterial(defaultMaterial, hit);
                    selectedCards.Remove(hit.collider.gameObject);
                }
            }
        }

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
