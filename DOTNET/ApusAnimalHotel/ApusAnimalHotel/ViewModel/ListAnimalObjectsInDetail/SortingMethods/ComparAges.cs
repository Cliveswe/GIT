using System.Globalization;
/// <summary>
/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-02-25
/// </summary>
namespace ApusAnimalHotel.ViewModel.ListAnimaObjectsInDetail.SortingMethods
{
    /// <summary>
    /// Class sorting algorithm using SortingByMetod.
    /// Compare a variable age with another variable age contained within two objects.
    /// </summary>
    /// <seealso cref="AnimalMotel.ViewModel.SortingMethods.SortingByMethod" />
    internal class ComparAges : SortingMethodBy { 

        public ComparAges()
        {
        }
        /// <summary>
        /// Compares a specified animal age with the other animal age.
        /// </summary>
        /// <param name="animalAge">The animal age.</param>
        /// <param name="otherAnimalAge">The other animal age.</param>
        /// <returns></returns>
        public override int Compare(ListedAnimalDetails animalAge, ListedAnimalDetails otherAnimalAge)
        {
            return double.Parse(animalAge.Age, CultureInfo.InvariantCulture).
                   CompareTo(double.Parse(otherAnimalAge.Age, CultureInfo.InvariantCulture));
        }
    }
}
