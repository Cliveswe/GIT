using System;
using System.Linq;
/// <summary>
/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-03-05
/// </summary>
namespace ApusAnimalHotel.ViewModel.GroupListBoxIO
{
    /// <summary>
    /// This class contains the elements used for gender.
    /// </summary>
    /// <seealso cref="ApusAnimalHotel.ViewModel.GroupListBoxIO.GroupListBoxInteractive" />
    public class SelectGender : GroupListBoxInteractive
    {
        /// <summary>
        /// Gets or sets what has been selected from Contents.
        /// </summary>
        /// <value>
        /// The is selected.
        /// </value>
        public override int IsSelected {
            get {
                return _isSelected;
            }
            set {
                _isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }
        private int _isSelected;

        /// <summary>
        /// Populates the content with the different genders.
        /// </summary>
        public override void PopulateContent()
        {
            Contents = Enum.GetNames(typeof(GenderEnum)).ToList();
        }

        /// <summary>
        /// Sets the header.
        /// </summary>
        public override void SetHeader()
        {
            GroupListBoxHeader = ContentEnumToText.GetContentText(ContentTextEnum.Gender);
        }

        /// <summary>
        /// Get the animal gender.
        /// </summary>
        /// <returns>String if gender is selected otherwise empty string.</returns>
        public string GetGender() {
            if(IsSelected > noItemSelected)
            {
                return Contents[IsSelected];
            }
            return string.Empty;
        }

        /// <summary>
        /// Sets the gender.
        /// </summary>
        /// <param name="gender">The gender.</param>
        public void SetGender(string gender)
        {
            IsSelected = Contents.IndexOf(gender);
        }

        /// <summary>
        /// Resets the gender.
        /// </summary>
        public override void ResetIsSelected()
        {
            IsSelected = noItemSelected;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectGender"/> class.
        /// </summary>
        public SelectGender(): base()
        {
            PopulateContent();
            SetHeader();
        }
    }
}
