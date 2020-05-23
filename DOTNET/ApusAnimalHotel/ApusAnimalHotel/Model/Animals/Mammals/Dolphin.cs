using AnimalMotel.Model.FeedingPlan;
using System;
using System.Runtime.Serialization;

/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-02-06
/// 
/// A animal object that has a "is a" relationship to the class mammal. This class maintains
/// data about a mammals species.
/// 
namespace AnimalMotel.Model.Animals.Mammals
{
    /// <summary>
    /// The Dolphin class contains properties and methods that are common to all dolphins.
    /// Note there are 8 species of dolphin.
    /// </summary>
    /// <seealso cref="AnimalMotel.Model.Mammal" />
    [Serializable]
    public class Dolphin : Mammal, ISerializable
    {
        #region Class properties        
        /// <summary>
        /// Gets the species.
        /// </summary>
        /// <value>
        /// The species.
        /// </value>
        public string Species
        {
            get {
                return _species;
            }
            set {
                _species = value;
            }
        }
        private string _species;

        private EaterEnum diet;

        public FoodSchedule dietRequirements;
        /// <summary>
        /// Used in Serialization as a key in the key, value pair.
        /// </summary>
        private enum Fields { Species, FoodSchedule }
        #endregion

        /// <summary>
        /// Initializes a copy instance of the <see cref="Dolphin"/> class.
        /// </summary>
        /// <param name="other">The other Dolphin that is to be copied.</param>
        public Dolphin(Dolphin other) : base((Mammal)other)
        {
            this.Species = other.Species;
            this.dietRequirements = other.dietRequirements;
            this.diet = other.diet;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Dolphin"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="age">The age.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="species">The species.</param>
        /// <param name="numberOfTeeth">The number of teeth.</param>
        public Dolphin(string name, double age, string id, string gender, int numberOfTeeth, string species) :
           base(name, age, id, gender, numberOfTeeth)
        {
            Species = species;
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
            dietRequirements.AddFoodScheduleItem("Dolphin diet.");
            dietRequirements.AddFoodScheduleItem("While some dolphins eat fishes like herring, cod or mackerel.");
            dietRequirements.AddFoodScheduleItem("Some others eat squids or other cephalopods.");
            dietRequirements.AddFoodScheduleItem("Large dolphins like orcas, eat marine mammals.");
            dietRequirements.AddFoodScheduleItem("For example seals or sea lions and sometimes even turtles.");
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return "\nSpecies: " + Species + base.ToString();
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

        #region Serialization
        /// <summary>
        /// Serialize the object.
        /// </summary>
        /// <param name="info">Serialize information from the object in a key value pair.</param>
        /// <param name="context"></param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(Fields.Species.ToString(), Species);
            info.AddValue(Fields.FoodSchedule.ToString(), dietRequirements);
        }
        /// <summary>
        /// De-serialize the object to restore its content.
        /// </summary>
        /// <param name="info">Serialize information get the object using a key value pair.</param>
        /// <param name="context"></param>
        public Dolphin(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Species = (string)info.GetValue(Fields.Species.ToString(), typeof(string));
            dietRequirements = (FoodSchedule)info.GetValue(Fields.FoodSchedule.ToString(), typeof(FoodSchedule));
        }

        [OnDeserialized]
        void OnDeserialized(StreamingContext context)
        {
            //SetDiet();
        }
        public Dolphin()
        {

        }
        #endregion
    }
}
