/// <summary>
/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-02-25
/// </summary>
namespace ApusAnimalHotel.ViewModel.ListAnimaObjectsInDetail.SortingMethods
{
    /// <summary>
    /// Class sorting algorithm using SortingByMetod.
    /// Compare a variable name with another variable name contained within two objects.
    /// </summary>
    /// <seealso cref="AnimalMotel.ViewModel.SortingMethods.SortingByMethod" />
    class ComparNames : SortingMethodBy
    {
        public ComparNames()
        {
        }
        /// <summary>
        ///  Compares a specified animal name with the other animal name.
        /// </summary>
        /// <param name="animalName">Name of the animal.</param>
        /// <param name="otherAnimalName">Name of the other animal.</param>
        /// <returns></returns>
        public override int Compare(ListedAnimalDetails animalName, ListedAnimalDetails otherAnimalName)
        {
            return animalName.Name.CompareTo(otherAnimalName.Name);
        }
    }
}
