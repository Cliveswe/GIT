using AnimalMotel.Model.FeedingPlan;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-02-06
/// 
/// A animal object that has a "is a" relationship to the class Bird. This class maintains
/// data about a birds wingspan.
/// 
namespace AnimalMotel.Model.Animals.Birds
{
    /// <summary>
    /// The Ostrich class contains properties and methods that are common to all Ostrich.
    /// </summary>
    /// <seealso cref="AnimalMotel.Model.Bird" />
    [Serializable]
    public class Ostrich : Bird, ISerializable
    {
        #region Class properties        
        /// <summary>
        /// Gets or sets the wing span.
        /// </summary>
        /// <value>
        /// The wing span.
        /// </value>
        public double WingSpan
        {
            get {
                return _wingSpan;
            }
            set {
                _wingSpan = value;
            }
        }
        private double _wingSpan;

        private EaterEnum diet;

        public FoodSchedule dietRequirements;
        /// <summary>
        /// Used in Serialization as a key in the key, value pair.
        /// </summary>
        private enum Fields { WingSpan, FoodSchedule }
        #endregion

        /// <summary>
        /// Initializes a copy instance of the <see cref="Ostrich"/> class.
        /// </summary>
        /// <param name="other">The other Ostrich that is to be copied.</param>
        public Ostrich(Ostrich other) : base((Bird)other)
        {
            this.WingSpan = other.WingSpan;
            this.dietRequirements = other.dietRequirements;
            this.diet = other.diet;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Ostrich"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="age">The age.</param>
        /// <param name="birdID">The bird identifier.</param>
        /// <param name="canFly">if set to <c>true</c> [can fly].</param>
        /// <param name="wingSpan">The wing span.</param>
        public Ostrich(string name, double age, string birdID, string gender, string species, double wingSpan) :
            base(name, age, birdID, gender, species)
        {
            WingSpan = wingSpan;
            SetDiet();

        }
        /// <summary>
        /// Set the diet of the Dolphin.
        /// </summary>
        private void SetDiet()
        {
            diet = EaterEnum.Herbivore;
            dietRequirements = new FoodSchedule();
            PopulateFoodSchedule();
        }

        private void PopulateFoodSchedule()
        {
            dietRequirements.AddFoodScheduleItem("Ostrich diet.");
            dietRequirements.AddFoodScheduleItem("Ostrich birds are browsers, they do not graze.");
            dietRequirements.AddFoodScheduleItem("They mainly feed on seeds, shrubs, grass, fruit and flowers.");
            dietRequirements.AddFoodScheduleItem("Occasionally they also eat insects such as locusts");
            dietRequirements.AddFoodScheduleItem("Lacking teeth, they swallow pebbles that act as gastrocolic to grind food in the gizzard.");
        }

        public override string ToString()
        {
            return "\nWingspan: " + WingSpan + base.ToString();
        }
        /// <summary>
        /// The animals consumption classification.
        /// </summary>
        /// <returns>EaterEnum</returns>
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
            return new FoodSchedule(this.dietRequirements);
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
            info.AddValue(Fields.WingSpan.ToString(), WingSpan);
            info.AddValue(Fields.FoodSchedule.ToString(), dietRequirements);
        }
        /// <summary>
        /// De-serialize the object to restore its content.
        /// </summary>
        /// <param name="info">Serialize information get the object using a key value pair.</param>
        /// <param name="context"></param>
        public Ostrich(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            WingSpan = (double)info.GetValue(Fields.WingSpan.ToString(), typeof(double));
            dietRequirements = (FoodSchedule)info.GetValue(Fields.FoodSchedule.ToString(), typeof(FoodSchedule));
        }

        [OnDeserialized]
        void OnDeserialized(StreamingContext context)
        {
            // SetDiet();
        }
        public Ostrich()
        {

        }
        #endregion
    }
}
