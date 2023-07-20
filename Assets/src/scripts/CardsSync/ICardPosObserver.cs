namespace src.scripts.CardsSync
{
    /// <summary>
    /// Interface of the ObservableCardsTransform observer pattern
    /// </summary>
    public interface ICardPosObserver
    { 
        /// <summary>
        /// Implementation of the sync card position
        /// </summary>
        /// <param name="x">x Pos</param>
        /// <param name="y">y Pos</param>
        /// <param name="z">z Pos</param>
        /// <param name="xRotation">x Rotation</param>
        /// <param name="yRotation">y Rotation</param>
        /// <param name="zRotation">z Rotation</param>
        /// <param name="wRotation">w Rotation</param>
        /// <param name="id">ID of the card</param>
        public void OnNotify(float x, float y, float z, float xRotation, float yRotation, float zRotation, float wRotation, int  id);
    }
}
