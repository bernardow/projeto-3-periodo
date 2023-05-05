using System;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

namespace src.scripts.Managers
{
    public class PlayerManager : MonoBehaviour
    {
        public int cardsPulled;
        public bool canPull;
        public int playerCardsNum;

        private void OnEnable() => cardsPulled = 0;

        private void Update()
        {
            CanPull();
            
        }

        private void CanPull()
        {
            if (cardsPulled < 1 && playerCardsNum < 4)
                canPull = true;
            else
                canPull = false;
            
        }

        public bool CanSelect() => !canPull;
        
        
    }
}
