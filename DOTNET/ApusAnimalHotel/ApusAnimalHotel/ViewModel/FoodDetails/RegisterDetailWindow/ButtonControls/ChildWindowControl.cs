using ApusAnimalHotel.ViewModel.ButtonControls;
using ApusAnimalHotel.ViewModel.FoodDetails.FoodDetailsWindow;

namespace ApusAnimalHotel.ViewModel.FoodDetails.RegisterDetailWindow.ButtonControls
{
    class ChildWindowControl: AnimalButton
    {
        /// <summary>
        /// Window instance.
        /// </summary>
        public RegisterInput IngredientsWindow
        {
            get;
            private set;
        }

        #region Child window
        /// <summary>
        /// Create a instance of both ingredients view model and the ingredients UI. Pass the view model to 
        /// the UI then show the UI.
        /// </summary>
        public void ShowIngredientsWindow(RegisterHeader register)
        {
            //ViewModel for the ingredients and pass it the list of ingredients and name of the recipe
            //ingredientsWindowAsChild = new IngredientsViewViewModel(ListOfIngredients, NameOfRecipe);
            //FoodRegister foodRegister = new FoodRegister();

            //create a new instance of the UI ingredients
            //RegisterInput IngredientsWindow = new RegisterInput();
            IngredientsWindow = new RegisterInput();

            //add a DataContext to the UI window
            //IngredientsWindow.DataContext = foodRegister;
            IngredientsWindow.DataContext = register;

            //Show the ingredients UI
            IngredientsWindow.ShowDialog();
            
        }
        #endregion

        #region Button commands override
        public override void ButtonExecute(object parameter)
        {
            
        }

        public override bool ButtonCanExecute(object parameter)
        {
            throw new System.NotImplementedException();
        }
        #endregion

        public ChildWindowControl():base()
        {

        }
    }
}
