using AnimalMotel.Model.Animals.Birds;
using AnimalMotel.Model.Animals.Insects;
using AnimalMotel.Model.Animals.Mammals;
using AnimalMotel.Model.FeedingPlan;
using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-02-06
/// 
/// This is the super class for the animal object. This class is inherited by sub-classes that
/// in turn add specific data to relevant to their type.
/// 
namespace AnimalMotel.Model.Animals
{
    /// <summary>
    /// Base animal class that is inherited by child classes.
    /// </summary>

    [Serializable]
    [XmlInclude(typeof(Mammal)),
        XmlInclude(typeof(Bird)),
        XmlInclude(typeof(Insect))]
    public abstract class Animal : IAnimal, ISerializable
    {
        #region Class properties        
        /// <summary>
        /// Gets the unique animal identifier.
        /// </summary>
        /// <value>
        /// A unique animal identifier.
        /// </value>
        public string AnimalID
        {
            get {
                return _animalID;
            }
            set {
                _animalID = value;
            }
        }
        private string _animalID;
        /// <summary>
        /// Gets the name of the animal.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get {
                return _name;
            }
            set {
                _name = value;
            }
        }
        private string _name;
        /// <summary>
        /// Gets the age of the animal.
        /// </summary>
        /// <value>
        /// The age.
        /// </value>
        public double Age
        {
            get {
                return _age;
            }
            set {
                _age = value;
            }
        }
        private double _age;
        /// <summary>
        /// Gets the gender of the animal.
        /// </summary>
        /// <value>
        /// The gender.
        /// </value>
        public string Gender
        {
            get {
                return _gender;
            }
            set {
                _gender = value;
            }
        }
        private string _gender;
        /// <summary>
        /// Used in Serialization as a key in the key, value pair.
        /// </summary>
        private enum Fields { Name, Age, AnimalID, Gender }
        #endregion

        #region Interface methods as abstract
        public abstract EaterEnum GetEaterType();
        public abstract FoodSchedule GetFoodSchedule();
        public abstract string GetSpeciesType();
        #endregion

        /// <summary>
        /// Initializes a copy instance of the <see cref="Animal"/> class.
        /// </summary>
        /// <param name="other">The other Animal that is to be copied.</param>
        public Animal(Animal other)
        {
            this.Name = other.Name;
            this.Age = other.Age;
            this.AnimalID = other.AnimalID;
            this.Gender = other.Gender;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Animal"/> class.
        /// </summary>
        /// <param name="name">Animal name.</param>
        /// <param name="age">Animal age.</param>
        public Animal(string name, double age, string amimalID, string gender)
        {
            Name = name;
            Age = age;
            Gender = gender;
            AnimalID = amimalID;//generate a unique id for the animal
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Animal"/> class.
        /// Initializes the instance properties to default values.
        /// </summary>
        public Animal() : this(null, 0, string.Empty, string.Empty)
        {
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return "ID: " + AnimalID + "\nName: " + Name + "\nAge: " + Age + "\nGender " + Gender;
        }

        #region Serialization
        /// <summary>
        /// Serialize the object.
        /// </summary>
        /// <param name="info">Serialize information from the object in a key value pair.</param>
        /// <param name="context"></param>
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(Fields.AnimalID.ToString(), AnimalID);
            info.AddValue(Fields.Name.ToString(), Name);
            info.AddValue(Fields.Age.ToString(), Age);
            info.AddValue(Fields.Gender.ToString(), Gender);

        }
        /// <summary>
        /// De-serialize the object to restore its content.
        /// </summary>
        /// <param name="info">Serialize information get the object using a key value pair.</param>
        /// <param name="context"></param>
        public Animal(SerializationInfo info, StreamingContext context)
        {
            AnimalID = (string)info.GetValue(Fields.AnimalID.ToString(), typeof(string));
            Name = (string)info.GetValue(Fields.Name.ToString(), typeof(string));
            Age = (double)info.GetValue(Fields.Age.ToString(), typeof(double));
            Gender = (string)info.GetValue(Fields.Gender.ToString(), typeof(string));
        }

        #endregion
    }
}
