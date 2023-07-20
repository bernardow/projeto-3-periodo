using UnityEngine;

namespace src.scripts.Deck.Special_Cards
{
    public class DoubleDamage : MonoBehaviour, IObserverCard
    {
        private void CheckBonus(CardPlayer cardPlayer) => cardPlayer.bonus++;
    
        /// <summary>
        /// Add bonus to the CardPlayers of choice
        /// </summary>
        /// <param name="player">CardPlayer of choice</param>
        public void OnNotify(CardPlayer player) => CheckBonus(player);
    
    }
}
