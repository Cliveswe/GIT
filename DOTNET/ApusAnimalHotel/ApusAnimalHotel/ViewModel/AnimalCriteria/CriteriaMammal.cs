using AnimalMotel.Model.Animals;
using System;
/// <summary>
/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-03-13
/// </summary>
namespace ApusAnimalHotel.ViewModel.AnimalCriteria
{
    /// <summary>
    /// Mammal criteria.
    /// </summary>
    /// <seealso cref="ApusAnimalHotel.Model.AnimalCriteria.IGenusCriteria" />
    class CriteriaMammal : IGenusCriteria
    {
        /// <summary>
        /// Criteria for success: Check if the animal object is a mammal object.
        /// </summary>
        /// <param name="animal">The animal.</param>
        /// <returns>null if not a mammal</returns>
        public AnimalCategoryEnum? CriteriaSuccess(Animal animal)
        {
            foreach (string item in Enum.GetNames(typeof(MammalEnum)))
            {
                if (animal.GetSpeciesType() == item)
                {
                    return AnimalCategoryEnum.Mammal;
                }
            }
            return null;
        }

    }
}
