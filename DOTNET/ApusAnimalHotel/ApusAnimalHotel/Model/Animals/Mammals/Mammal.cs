using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-02-06
/// 
/// A animal object that has a "is a" relationship to the class Animal. This class maintains
/// data about a mammals number of teeth.
/// 
namespace AnimalMotel.Model.Animals.Mammals
{
    /// <summary>
    /// The Mammal class contains properties that are common to all mammals.
    /// </summary>
    /// <seealso cref="AnimalMotel.Model.Animals" />
    [Serializable]
    [XmlInclude(typeof(Dog)),
     XmlInclude(typeof(Dolphin))]
    public abstract class Mammal : Animal, ISerializable
    {
        #region Class properties        
        /// <summary>
        /// Gets or sets the number of teeth.
        /// </summary>
        /// <value>
        /// The number of teeth.
        /// </value>
        public int NumberOfTeeth
        {
            get {
                return _numberOfTeeth;
            }
            set {
                _numberOfTeeth = value;
            }
        }
        private int _numberOfTeeth;
        /// <summary>
        /// Used in Serialize as a key in the key, value pair.
        /// </summary>
        private enum Fields { NumberOfTeeth }
        #endregion

        /// <summary>
        /// Initializes a copy instance of the <see cref="Mammal"/> class.
        /// </summary>
        /// <param name="other">The other Mammal that is to be copied.</param>
        public Mammal(Mammal other) : base((Animal)other)
        {
            this.NumberOfTeeth = other.NumberOfTeeth;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Mammal"/> class.
        /// </summary>
        public Mammal() : this(0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Mammal"/> class.
        /// </summary>
        /// <param name="numberOfTeeth">Number of teeth.</param>
        public Mammal(int numberOfTeeth) : this(null, 0, string.Empty, string.Empty, numberOfTeeth)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Mammal"/> class and its base class.
        /// </summary>
        /// <param name="numberOfTeeth">Number of teeth.</param>
        /// <param name="name">Animal name.</param>
        /// <param name="age">Animal age.</param>
        public Mammal(string name, double age, string mammalID, string gender, int numberOfTeeth) : base(name, age, mammalID, gender)
        {
            Initialize(numberOfTeeth);
        }

        /// <summary>
        /// Initializes the specified mammal properties.
        /// </summary>
        /// <param name="numberOfTeeth">Mammals number of teeth.</param>
        private void Initialize(int numberOfTeeth)
        {
            NumberOfTeeth = numberOfTeeth;
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return "\nNumber of teeth: " + NumberOfTeeth + base.ToString();
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
            info.AddValue(Fields.NumberOfTeeth.ToString(), NumberOfTeeth);

        }
        /// <summary>
        /// De-serialize the object to restore its content.
        /// </summary>
        /// <param name="info">Serialize information get the object using a key value pair.</param>
        /// <param name="context"></param>
        public Mammal(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            NumberOfTeeth = (int)info.GetValue(Fields.NumberOfTeeth.ToString(), typeof(int));
        }

        #endregion
    }
}
