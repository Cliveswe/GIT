using ApusAnimalHotel.Model.ListManagerRegister;
using System.Collections.Generic;
/// <summary>
/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date: 2019-04-09
/// </summary>
namespace ApusAnimalHotel.ViewModel.FoodDetails
{
    class Staff
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
        /// Gets or sets the staffs qualifications.
        /// </summary>
        /// <value>
        /// The ingredients.
        /// </value>
        public ListManager<string> Qualifications
        {
            get {
                return m_qualifications;
            }
            set {
                m_qualifications = value;
            }
        }
        private ListManager<string> m_qualifications;
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Recipe"/> class.
        /// </summary>
        public Staff()
        {
            Name = string.Empty;
            Qualifications = new ListManager<string>();
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

            List<string> qualifications = Qualifications.ToSrtingList();
            foreach (string item in qualifications)
            {
                res = res + " " + item;
            }
            return Name + ": " + res;
        }
    }
}
