using ApusAnimalHotel.ViewModel.LabelTextBoxIO;

namespace ApusAnimalHotel.ViewModel.FoodDetails.RegisterDetailWindow
{
    class FoodRegister : RegisterHeader
    {
        private int Indexer;

        private Recipe recipe;

        #region Populate Properties        
        /// <summary>
        /// Gets the recipe from the recipe manager if it exists or an empty recipe.
        /// </summary>
        private void GetRecipe()
        {
            if (Indexer > NoItemSelected)
            {
                recipe = RecipeManager.GetInstance.GetAt(Indexer);
            }
            else
            {
                recipe = new Recipe();
            }
        }
        /// <summary>
        /// Property that holds the results from the register.
        /// </summary>
        public Recipe GetRegisterResults
        {
            get {
                return GetUpdatedRecipe();
            }
        }

        /// <summary>
        /// Updates the recipe from the modified register details list.
        /// </summary>
        private void UpdateRecipe()
        {
            if (IsModified)
            {
                recipe.Ingredients.DeleteAll();
                recipe.Name = TitleName.TextIO;
                foreach (string item in RegisterDetailsAsList)
                {
                    recipe.Ingredients.Add(item);
                }
            }
        }
        /// <summary>
        /// Get the updated recipe.
        /// </summary>
        /// <returns></returns>
        private Recipe GetUpdatedRecipe()
        {
            UpdateRecipe();
            return recipe;
        }

        /// <summary>
        /// Populates the properties.
        /// </summary>
        private void PopulateProperties()
        {
            TitleName.TextIO = recipe.Name;

            PopulateRegisterList();

        }
        /// <summary>
        /// Populates the register list.
        /// </summary>
        private void PopulateRegisterList()
        {

            for (int index = 0; index < recipe.Ingredients.Count; index++)
            {
                RegisterDetailsAsList.Add(recipe.Ingredients.GetAt(index));
            }

        }
        #endregion

        /// <summary>
        /// Set the group title input name and set error message.
        /// </summary>
        private void SetGroupTitleInputName()
        {
            GroupTitle = "Add Ingredients";
            GroupTitleInputName = new TextInputOutput("Ingredients");
            GroupTitleInputName.SetErrorMessage(GroupTitleInputName.Label + " can not be empty!");

        }

        /// <summary>
        /// Set the window title and title name.
        /// </summary>
        private void SetTitle()
        {
            Title = "Register Food";
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
            GetRecipe();
            PopulateProperties();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="FoodRegister"/> class.
        /// </summary>
        /// <param name="index">The index.</param>
        public FoodRegister(int index) : base()
        {
            TitleIsActive = false;
            Indexer = index;
            Initializer();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="FoodRegister"/> class.
        /// Note: This constructor is use in assignment 3 only. Do not use this
        /// constructor if further development requires that we edit a recipe
        /// that exists in the RecipeManager.
        /// </summary>
        public FoodRegister()
        {
            TitleIsActive = true;
            Indexer = -1;
            Initializer();
        }
    }
}
