using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    public FGLibrary.CardsType cardType;
    public Mesh cardMesh;
}
