using AnimalMotel.Model.Animals;
/// <summary>
/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-03-05
/// </summary>
namespace ApusAnimalHotel.ViewModel.AnimalCriteria
{
    /// <summary>
    /// Criteria that returns the name of the genus of a animal object.
    /// </summary>
    interface IGenusCriteria
    {
         AnimalCategoryEnum? CriteriaSuccess(Animal animal);
    }
}
