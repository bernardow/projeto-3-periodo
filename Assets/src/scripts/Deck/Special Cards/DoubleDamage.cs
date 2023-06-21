using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleDamage : MonoBehaviour, IObserverCard
{
    private void CheckBonus(CardPlayer cardPlayer)
    {
        cardPlayer.bonus++;
    }

    public void OnNotify(CardPlayer player)
    {
        CheckBonus(player);
    }
}
