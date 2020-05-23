using ApusAnimalHotel.ViewModel.LabelTextBoxIO;

namespace ApusAnimalHotel.ViewModel.FoodDetails.RegisterDetailWindow
{
    class StaffRegister : RegisterHeader
    {
        private Staff staff;

        /// <summary>
        /// Gets the recipe from the recipe manager if it exists or an empty recipe.
        /// </summary>
        private void GetStaff()
        {
            staff = new Staff();
        }

        /// <summary>
        /// Property that holds the results from the register.
        /// </summary>
        public Staff GetRegisterResults
        {
            get {
                return GetUpdatedStaff();
            }
        }
        /// <summary>
        /// Get the updated staff.
        /// </summary>
        /// <returns></returns>
        private Staff GetUpdatedStaff()
        {
            UpdateStaff();
            return staff;
        }
        /// <summary>
        /// Updates the recipe from the modified register details list.
        /// </summary>
        private void UpdateStaff()
        {
            staff.Qualifications.DeleteAll();
            staff.Name = TitleName.TextIO;
            foreach (string item in RegisterDetailsAsList)
            {
                staff.Qualifications.Add(item);
            }
        }
        /// <summary>
        /// Set the group title input name and set error message.
        /// </summary>
        private void SetGroupTitleInputName()
        {
            GroupTitle = "Qualifications";
            GroupTitleInputName = new TextInputOutput(GroupTitle);
            GroupTitleInputName.SetErrorMessage(GroupTitleInputName.Label + " can not be empty!");
        }

        /// <summary>
        /// Set the window title and title name.
        /// </summary>
        private void SetTitle()
        {
            Title = "Staff Planning";
            TitleName = new TextInputOutput("Name");
            TitleName.SetErrorMessage(TitleName.Label + " can not be empty!");
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Initializer()
        {
            SetTitle();
            SetGroupTitleInputName();
            GetStaff();
        }

        public StaffRegister() : base()
        {
            Initializer();
            TitleIsActive = true;
        }
    }
}
