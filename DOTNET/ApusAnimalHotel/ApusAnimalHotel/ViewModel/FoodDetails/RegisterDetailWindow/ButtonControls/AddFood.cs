using ApusAnimalHotel.ViewModel.ButtonControls;

namespace ApusAnimalHotel.ViewModel.FoodDetails.RegisterDetailWindow.ButtonControls
{
    class AddFood : ChildWindowControl
    {
        FoodDetailsInDetail foodDetailsInDetail;

        FoodRegister register;

        private int selectedIndex;
        /// <summary>
        /// Indicates that the RecipeManager was updated.
        /// </summary>
        public bool RecipeManagerIsUpdated
        {
            get {
                return _reciepManagerIsUpdated;
            }
            set {
                _reciepManagerIsUpdated = value;
            }
        }
        private bool _reciepManagerIsUpdated;

        #region Button controls        
        /// <summary>
        /// Can the button be executed.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        public override bool ButtonCanExecute(object parameter)
        {
            #region If Editing a Recipe is Required (code not active)
            /*bool good = false;
            selectedIndex = -1;

            if (parameter != null)
            {
                good = int.TryParse(parameter.ToString(), out selectedIndex);
            }
            if ((selectedIndex > -1) && good)
            {
                return true;
            }
            return false;*/
            #endregion

            return true;

        }

        /// <summary>
        /// Updates the recipe manager.
        /// </summary>
        /// <param name="register">The register.</param>
        private void UpdateRecipeManager(FoodRegister register)
        {
            //update recipe manager
            if ((register.IsModified) && (selectedIndex > -1))
            {
                RecipeManager.GetInstance.ChangeAt(register.GetRegisterResults, selectedIndex);
            }
            else if(register.IsModified)
            {

                // RecipeManager.GetInstance.Add(register.GetRegisterResults); 
                RecipeManager.GetInstance.AddRecipe(register.GetRegisterResults);
            }
            SetRecipeManagerIsUpdated();
        }

        private void UpdateFoodDetailsInDetail()
        {
            foodDetailsInDetail.RefreshList();
        }
        /// <summary>
        /// Sets the recipe manager to is updated.
        /// </summary>
        private void SetRecipeManagerIsUpdated()
        {
            RecipeManagerIsUpdated = true;
        }

        /// <summary>
        /// Resets the recipe manager is not updated.
        /// </summary>
        private void ResetRecipeManagerIsUpdated()
        {
            RecipeManagerIsUpdated = false;
            selectedIndex = -1;
        }
        /// <summary>
        /// Button is pressed execute method.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void ButtonExecute(object parameter)
        {
            if (selectedIndex > -1)
            {
                register = new FoodRegister(selectedIndex);
            }
            else
            {
                register = new FoodRegister();
            }

            ShowIngredientsWindow(register);
            UpdateRecipeManager(register);
            UpdateFoodDetailsInDetail();
        }
        #endregion


        public AddFood(FoodDetailsInDetail foodDetailsInDetail) : base()
        {
            this.foodDetailsInDetail = foodDetailsInDetail;
            ButtonContent = ContentEnumToText.GetContentText(ContentTextEnum.Add_Food);
            ResetRecipeManagerIsUpdated();
        }
    }
}
