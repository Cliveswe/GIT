using System;

namespace ApusAnimalHotel.ViewModel.FoodDetails.RegisterDetailWindow.ButtonControls
{
    class AddStaff : ChildWindowControl
    {
        private StaffRegister register;

        private void UpdateStaffManager(StaffRegister register)
        {
            StaffManager.GetInstance.Add(register.GetRegisterResults);
        }

        #region Button Controls
        /// <summary>
        /// Can the button be executed. For now the button is always active.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        public override bool ButtonCanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Button is pressed execute method.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <exception cref="NotImplementedException"></exception>
        public override void ButtonExecute(object parameter)
        {
            register = new StaffRegister();
            
            ShowIngredientsWindow(register);
            UpdateStaffManager(register);
        }
        
        #endregion

        public AddStaff() : base()
        {
            ButtonContent = ContentEnumToText.GetContentText(ContentTextEnum.Add_Staff);    
        }
    }
}
