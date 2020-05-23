/// <summary>
/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date: 2019-04-09
/// </summary>
namespace ApusAnimalHotel.ViewModel.MenuUI.File
{
    /// <summary>
    /// This class encapsulates a status of a variable that can be used 
    /// in the context of indicating that a file has or has not been saved.
    /// </summary>
    class SaveFileState
    {
        #region Class properties
        private bool isSaved;
        #endregion

        /// <summary>
        /// Has a file been saved.
        /// </summary>
        /// <returns>True if saved otherwise false.</returns>
        public bool HasBeenSaved()
        {
            return isSaved;
        }
        /// <summary>
        /// Set the class status to saved.
        /// </summary>
        public void IsSaved()
        {
            isSaved = true;
        }
        /// <summary>
        /// Set the class status to not saved.
        /// </summary>
        public void NotSaved()
        {
            isSaved = false;
        }
        public SaveFileState()
        {
            NotSaved();
        }
    }
}
