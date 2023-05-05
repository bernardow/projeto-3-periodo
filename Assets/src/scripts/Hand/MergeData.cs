using UnityEngine;
using static src.scripts.FgLibrary;

namespace src.scripts.Hand
{
    [CreateAssetMenu(fileName = "New Merge Data", menuName = "MergeData")]
    public class MergeData : ScriptableObject
    {
        public CardsType color;
        public CardsType mergeColor1;
        public CardsType mergeColor2;
    }
}
