using ApusAnimalHotel.Model.ListManagerRegister;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ApusAnimalHotel.ViewModel.FoodDetails
{
    class FoodDetailsInDetail : INotifyPropertyChanged
    {
        

        /// <summary>
        /// Gets or sets the food details as list.
        /// </summary>
        /// <value>
        /// The food details as list.
        /// </value>
        public ObservableCollection<string> FoodDetailsAsList
        {
            get {
                return _foodDetailsAsList;
            }
            set {
                _foodDetailsAsList = value;
                OnPropertyChanged("FoodDetailsAsList");
            }
        }
        private ObservableCollection<string> _foodDetailsAsList;

        /// <summary>
        /// Refreshes the food details list.
        /// </summary>
        public void RefreshList()
        {
            FoodDetailsAsList.Clear();
            SetFoodDetailsList();
        }
        /// <summary>
        /// Sets the food details list.
        /// </summary>
        private void SetFoodDetailsList()
        {
           
            ListManager<Recipe> recipeManager = RecipeManager.GetInstance;
            //show the contents of the manager
            for (int index = 0; index < recipeManager.Count; index++)
            {
                //Recipe recipe = recipeManager.GetAt(index);
                Recipe recipe = recipeManager.GetAt(index);
                FoodDetailsAsList.Add(recipe.ToString());
            }
        }
        /// <summary>
        /// Add some test the data.
        /// </summary>
        private void TestData()
        {
            Recipe xrecipe = new Recipe();
            xrecipe.Name = "Dog Special";
            ListManager<string> ingredients = new ListManager<string>();
            ingredients.Add("2 pig ears");
            ingredients.Add("1 medium-sized bone");
            xrecipe.Ingredients = ingredients;
            //save start up data in manager
            RecipeManager.GetInstance.Add(xrecipe);

            Recipe yrecipe = new Recipe();
            yrecipe.Name = "Penguin";
            ListManager<string> yIngredients = new ListManager<string>();
            yIngredients.Add("2 fish");
            yIngredients.Add("more fish");
            yrecipe.Ingredients = yIngredients;
            RecipeManager.GetInstance.Add(yrecipe);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="FoodDetailsInDetail"/> class.
        /// </summary>
        public FoodDetailsInDetail()
        {
            FoodDetailsAsList = new ObservableCollection<string>();

            //start up data
           // TestData();

           
            SetFoodDetailsList();


        }

        #region INotifyPropertyChanged members
        /// <summary>
        /// This is boiler plate code that was added when one want to notify a change on a class
        /// property. This is where the code and the UI communicate through the event handler.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raise a notification that a property has been changed.
        /// </summary>
        /// <param name="propertyName">A string of the property name</param>
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
