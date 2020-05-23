using AnimalMotel.Model.FeedingPlan;
using System;
using System.Runtime.Serialization;

/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-02-06
/// 
/// A animal object that has a "is a" relationship to the class Mammal. This class maintains
/// data about a mammals breed.
/// 
namespace AnimalMotel.Model.Animals.Mammals
{
    /// <summary>
    /// The Dog class contains properties and methods that are common to all dogs (aka K9's).
    /// Note there are many breeds of dog.
    /// </summary>
    /// <seealso cref="AnimalMotel.Model.Mammal" />
    [Serializable]
    public class Dog : Mammal, ISerializable
    {

        #region Class properties        
        /// <summary>
        /// Gets or sets the breed.
        /// </summary>
        /// <value>
        /// The breed.
        /// </value>
        public string Breed
        {
            get {
                return _breed;
            }
            set {
                _breed = value;
            }
        }
        private string _breed;

        private EaterEnum diet;

        public FoodSchedule dietRequirements;

        /// <summary>
        /// Used in Serialization as a key in the key, value pair.
        /// </summary>
        private enum Fields { Breed, FoodSchedule }
        #endregion

        /// <summary>
        /// Initializes a copy instance of the <see cref="Dog"/> class.
        /// </summary>
        /// <param name="other">The other Dog that is to be copied.</param>
        public Dog(Dog other) : base((Mammal)other)
        {
            this.Breed = other.Breed;
            this.dietRequirements = other.dietRequirements;
            this.diet = other.diet;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Dog"/> class.
        /// </summary>
        /// <param name="k9Name">Name of the k9.</param>
        /// <param name="k9Age">The k9 age.</param>
        /// <param name="k9ID">The k9 identifier.</param>
        /// <param name="k9Breed">The k9 breed.</param>
        /// <param name="k9NumberOfTeeth">The k9 number of teeth.</param>
        public Dog(string k9Name, double k9Age, string k9ID, string gender, int k9NumberOfTeeth, string k9Breed) :
            base(k9Name, k9Age, k9ID, gender, k9NumberOfTeeth)
        {
            Breed = k9Breed;
            SetDiet();
        }
        /// <summary>
        /// Set the diet of the Dolphin.
        /// </summary>
        private void SetDiet()
        {
            diet = EaterEnum.Carnivore;
            dietRequirements = new FoodSchedule();
            PopulateFoodSchedule();
        }
        private void PopulateFoodSchedule()
        {
            dietRequirements.AddFoodScheduleItem("Dog diet.");
            dietRequirements.AddFoodScheduleItem("Adult dogs should be fed at least twice a day");
            dietRequirements.AddFoodScheduleItem("High quality balanced premium commercial dog food that is appropriate for their life stage.");
            dietRequirements.AddFoodScheduleItem("The amount of food required will depend on your dog's size, breed, age and level of exercise");
            dietRequirements.AddFoodScheduleItem("But take care not to overfeed or underfeed.");
            dietRequirements.AddFoodScheduleItem("Natural foods include fresh human - grade raw meat(e.g.raw lamb).");
            dietRequirements.AddFoodScheduleItem("Raw meaty bones and finely cut vegetables.");
            dietRequirements.AddFoodScheduleItem("Fresh drinking water must be available at all times");
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return "\nBreed: " + Breed + base.ToString();
        }
        /// <summary>
        /// Gets the type of the eater.
        /// </summary>
        /// <returns></returns>
        public override EaterEnum GetEaterType()
        {
            return diet;
        }
        /// <summary>
        /// Gets the food schedule.
        /// </summary>
        /// <returns></returns>
        public override FoodSchedule GetFoodSchedule()
        {
            return new FoodSchedule(dietRequirements);
        }
        /// <summary>
        /// Gets the type of the species.
        /// </summary>
        /// <returns></returns>
        public override string GetSpeciesType()
        {

            return this.GetType().Name;
        }

        #region Serialize
        /// <summary>
        /// Serialize the object.
        /// </summary>
        /// <param name="info">Serialize information from the object in a key value pair.</param>
        /// <param name="context"></param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(Fields.Breed.ToString(), Breed);
            info.AddValue(Fields.FoodSchedule.ToString(), dietRequirements);
        }
        /// <summary>
        /// De-serialize the object to restore its content.
        /// </summary>
        /// <param name="info">Serialize information get the object using a key value pair.</param>
        /// <param name="context"></param>
        public Dog(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Breed = (string)info.GetValue(Fields.Breed.ToString(), typeof(string));
            dietRequirements = (FoodSchedule)info.GetValue(Fields.FoodSchedule.ToString(), typeof(FoodSchedule));
        }

        [OnDeserialized]
        void OnDeserialized(StreamingContext context)
        {
            //SetDiet();
        }
        public Dog()
        {

        }
        #endregion
    }
}
