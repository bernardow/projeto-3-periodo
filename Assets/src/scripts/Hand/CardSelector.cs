using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace src.scripts.Hand
{
    public class CardSelector : Hand
    {
        [NotNull] public static List<GameObject> selectedCardsPlaye1 = new List<GameObject>();
        

        public void SelectCard(Material selectedMaterial, Material defaultMaterial, bool canSelect, Camera playerCamera)
        {
            if (canSelect)
            {
                Ray ray = playerCamera!.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity) && Input.GetMouseButtonDown(0))
                {
                    if (hit.collider.CompareTag("MyCards") && !selectedCardsPlaye1.Contains(hit.collider.gameObject) && selectedCardsPlaye1.Count < 2)
                    {
                        ChangeMaterial(selectedMaterial, hit);
                        selectedCardsPlaye1.Add(hit.collider.gameObject);
                    }else if (hit.collider.CompareTag("MyCards") && selectedCardsPlaye1.Contains(hit.collider.gameObject))
                    {
                        ChangeMaterial(defaultMaterial, hit);
                        selectedCardsPlaye1.Remove(hit.collider.gameObject);
                    }
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
