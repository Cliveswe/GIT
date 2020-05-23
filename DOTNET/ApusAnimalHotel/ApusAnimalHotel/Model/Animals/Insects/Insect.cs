using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-02-06
/// 
/// A animal object that has a "is a" relationship to the class Animal. This class maintains
/// data about insects and if they are poisonous or not.
/// 
namespace AnimalMotel.Model.Animals.Insects

{
    /// <summary>
    /// The Mammal class contains properties that are common to all mammals.
    /// </summary>
    /// <seealso cref="AnimalMotel.Model.Animals" />
    [Serializable]
    [XmlInclude(typeof(Bee)),
     XmlInclude(typeof(Fly))]
    public abstract class Insect : Animal, ISerializable
    {
        #region Class properties        
        /// <summary>
        /// Is the insect poisonous.
        /// </summary>
        /// <value>
        /// The number of teeth.
        /// </value>
        public bool IsPoisonous
        {
            get {
                return _isPoisonous;
            }
            set {
                _isPoisonous = value;
            }
        }
        private bool _isPoisonous;
        /// <summary>
        /// Used in Serialize as a key in the key, value pair.
        /// </summary>
        private enum Fields { IsPoisonous }
        #endregion

        /// <summary>
        /// Initializes a copy instance of the <see cref="Insect"/> class.
        /// </summary>
        /// <param name="other">The other Insect that is to be copied.</param>
        public Insect(Insect other) : base((Animal)other)
        {
            this.IsPoisonous = other.IsPoisonous;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Insect"/> class.
        /// </summary>
        public Insect() : this(false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Insect"/> class.
        /// </summary>
        /// <param name="isPoisonous">Number of teeth.</param>
        public Insect(bool isPoisonous) : this(null, 0, string.Empty, string.Empty, isPoisonous)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Insect"/> class and its base class.
        /// </summary>
        /// <param name="numberOfTeeth">Number of teeth.</param>
        /// <param name="name">Animal name.</param>
        /// <param name="age">Animal age.</param>
        public Insect(string name, double age, string mammalID, string gender, bool isPoisonous) : base(name, age, mammalID, gender)
        {
            Initialize(isPoisonous);
        }

        /// <summary>
        /// Initializes the specified mammal properties.
        /// </summary>
        /// <param name="numberOfTeeth">Mammals number of teeth.</param>
        private void Initialize(bool isPoisonous)
        {
            IsPoisonous = isPoisonous;
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return "\nIs the insect poisonous: " + IsPoisonous + base.ToString();
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
            info.AddValue(Fields.IsPoisonous.ToString(), IsPoisonous);

        }
        /// <summary>
        /// De-serialize the object to restore its content.
        /// </summary>
        /// <param name="info">Serialize information get the object using a key value pair.</param>
        /// <param name="context"></param>
        public Insect(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            IsPoisonous = (bool)info.GetValue(Fields.IsPoisonous.ToString(), typeof(bool));
        }

        #endregion
    }
}
