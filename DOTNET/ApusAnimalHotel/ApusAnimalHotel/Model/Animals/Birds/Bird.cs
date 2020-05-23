using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-02-06
/// 
/// A animal object that has a "is a" relationship to the class Animal. This class maintains
/// data about a birds species.
/// 
namespace AnimalMotel.Model.Animals.Birds
{
    /// <summary>
    /// The Bird class contains properties and methods that are common to all birds.
    /// </summary>
    /// <seealso cref="AnimalMotel.Model.Animals" />
    [Serializable]
    [XmlInclude(typeof(Ostrich)),
     XmlInclude(typeof(Penguin))]
    public abstract class Bird : Animal, ISerializable
    {
        #region Class properties         
        /// <summary>
        /// Gets or sets a value indicating whether this instance can fly.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can fly; otherwise, <c>false</c>.
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
        /// <summary>
        /// Used in Serialization as a key in the key, value pair.
        /// </summary>
        private enum Fields { Species }
        #endregion

        /// <summary>
        /// Initializes a copy instance of the <see cref="Bird"/> class.
        /// </summary>
        /// <param name="other">The other Bird that is to be copied.</param>
        public Bird(Bird other) : base((Animal)other)
        {
            this.Species = other.Species;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Bird"/> class.
        /// </summary>
        public Bird() : this(String.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Bird"/> class.
        /// </summary>
        /// <param name="canFly">if set to <c>true</c> [can fly] the bird can fly otherwise false.</param>
        public Bird(string species) : this(null, 0, string.Empty, string.Empty, species)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Bird"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="age">The age.</param>
        /// <param name="birdID">The bird identifier.</param>
        /// <param name="canFly">if set to <c>true</c> [can fly] the bird can fly otherwise false.</param>
        public Bird(string name, double age, string birdID, string gender, string species) : base(name, age, birdID, gender)
        {
            Initialize(species);
        }

        /// <summary>
        /// Initializes the birds properties.
        /// </summary>
        /// <param name="canFly">if set to <c>true</c> [can fly] the bird can fly otherwise false.</param>
        private void Initialize(string species)
        {
            Species = species;
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

        }
        /// <summary>
        /// De-serialize the object to restore its content.
        /// </summary>
        /// <param name="info">Serialize information get the object using a key value pair.</param>
        /// <param name="context"></param>
        public Bird(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Species = (string)info.GetValue(Fields.Species.ToString(), typeof(string));
        }

        #endregion
    }
}
