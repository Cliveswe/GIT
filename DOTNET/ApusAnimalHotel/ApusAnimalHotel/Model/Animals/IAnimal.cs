using AnimalMotel.Model.FeedingPlan;
using System;
/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-02-20
/// 
/// This is the Interface class for the animal object. This class is implemented by classes 
/// which in turn add specific data and function relevant to their type.
/// 
namespace AnimalMotel.Model.Animals
{
    interface IAnimal
    {
        #region Class properties        
        /// <summary>
        /// Gets the unique animal identifier.
        /// </summary>
        /// <value>
        /// A unique animal identifier.
        /// </value>
        string AnimalID
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the name of the animal.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the gender of the animal.
        /// </summary>
        /// <value>
        /// The gender.
        /// </value>
        string Gender
        {
            get;
            set;
        }
        #endregion

        EaterEnum GetEaterType();
        FoodSchedule GetFoodSchedule();
        String GetSpeciesType();
    }
}
