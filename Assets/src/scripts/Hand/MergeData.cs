using UnityEngine;
using static src.scripts.Extensions;

namespace src.scripts.Hand
{
    /// <summary>
    /// Recipes for merge colors
    /// </summary>
    [CreateAssetMenu(fileName = "New Merge Data", menuName = "MergeData")]
    public class MergeData : ScriptableObject
    {
        public CardsType color;
        public CardsType mergeColor1;
        public CardsType mergeColor2;
    }
}
