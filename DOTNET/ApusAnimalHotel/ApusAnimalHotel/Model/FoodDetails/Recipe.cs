using ApusAnimalHotel.Model.ListManagerRegister;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
/// <summary>
/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date: 2019-04-09
/// </summary>
namespace ApusAnimalHotel.ViewModel.FoodDetails
{
    [Serializable]
    public class Recipe : ISerializable
    {
        #region class properties
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get {
                return name;
            }
            set {
                name = value;
            }
        }
        private string name;
        /// <summary>
        /// Gets or sets the ingredients.
        /// </summary>
        /// <value>
        /// The ingredients.
        /// </value>
        public ListManager<string> Ingredients
        {
            get {
                return m_ingredients;
            }
            set {
                m_ingredients = value;
            }
        }
        private ListManager<string> m_ingredients;
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Recipe"/> class.
        /// </summary>
        public Recipe()
        {
            Name = string.Empty;
            Ingredients = new ListManager<string>();
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {

            string res = string.Empty;


            List<string> ingredients = Ingredients.ToSrtingList();
            foreach (string item in ingredients)
            {
                res = res + " " + item;
            }
            return Name + ": " + res;
        }

        #region Serialization
        /// <summary>
        /// Serialize the object.
        /// </summary>
        /// <param name="info">Serialize information from the object in a key value pair.</param>
        /// <param name="context"></param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", Name);
            info.AddValue("Ingredients", Ingredients);
        }
        /// <summary>
        /// De-serialize the object to restore its content.
        /// </summary>
        /// <param name="info">Serialize information get the object using a key value pair.</param>
        /// <param name="context"></param>
        public Recipe(SerializationInfo info, StreamingContext context)
        {
            Name = (string)info.GetValue("Name", typeof(string));
            Ingredients = (ListManager<string>)info.GetValue("Ingredients", typeof(ListManager<string>));
        }
        #endregion
    }
}
