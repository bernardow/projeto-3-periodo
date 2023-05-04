using System;
using UnityEngine;

namespace src.scripts.Managers
{
    public class PlayerManager : MonoBehaviour
    {
        public int cardsPulled;
        public bool canPull;
        public int playerCardsNum;

        private void Update()
        {
            CanPull();
        }

        private void CanPull()
        {
            if (cardsPulled < 1 && playerCardsNum < 4)
                canPull = true;
            else canPull = false;
        }

        private void CanSelect()
        {
            
        }
    }
}
