using ApusAnimalHotel.ViewModel.ButtonControls;
using ApusAnimalHotel.ViewModel.FoodDetails.FoodDetailsWindow;
using ApusAnimalHotel.ViewModel.FoodDetails.RegisterDetailWindow;
using ApusAnimalHotel.ViewModel.FoodDetails.RegisterDetailWindow.ButtonControls;

namespace ApusAnimalHotel.ViewModel.FoodDetails
{
    class FoodDetailsUI
    {
        public string FoodDetailsHeader
        {
            get {
                return ContentEnumToText.GetContentText(ContentTextEnum.Food_details);
            }
        }

        public FoodDetailsInDetail FoodDetails
        {
            get;
            set;
        }

        #region Add Food and Staff Buttons        
        /// <summary>
        /// Gets or sets the button logic for adding food in detail.
        /// </summary>
        /// <value>
        /// The add food button.
        /// </value>
        public AddFood AddFoodButton
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the button logic for adding staff details.
        /// </summary>
        /// <value>
        /// The add staff button.
        /// </value>
        public AddStaff AddStaffButton
        {
            get;
            set;
        }
        #endregion

        public FoodDetailsUI(): base()
        {
            FoodDetails = new FoodDetailsInDetail();
            AddFoodButton = new AddFood(FoodDetails);
            AddStaffButton = new AddStaff();
            //Temporary show child window
           /* RegisterHeader register = new FoodRegister();
            ShowIngredientsWindow(register);*/
        }
    }
}
