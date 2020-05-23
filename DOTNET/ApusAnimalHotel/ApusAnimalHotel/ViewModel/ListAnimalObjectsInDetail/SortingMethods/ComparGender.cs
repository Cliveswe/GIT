/// <summary>
/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-02-25
/// </summary>
namespace ApusAnimalHotel.ViewModel.ListAnimaObjectsInDetail.SortingMethods
{
    /// <summary>
    /// Class sorting algorithm using SortingByMetod.
    /// Compare a variable Gender with another variable Gender contained within two objects.
    /// </summary>
    /// <seealso cref="AnimalMotel.ViewModel.SortingMethods.SortingByMethod" />
    class ComparGender : SortingMethodBy
    {
        public ComparGender()
        {
        }
        /// <summary>
        /// Compares the specified animal gender.
        /// </summary>
        /// <param name="animalGender">The animal gender.</param>
        /// <param name="otherAnimalGender">The other animal gender.</param>
        /// <returns></returns>
        public override int Compare(ListedAnimalDetails animalGender, ListedAnimalDetails otherAnimalGender)
        {
            return animalGender.Gender.CompareTo(otherAnimalGender.Gender);
        }
    }
}
