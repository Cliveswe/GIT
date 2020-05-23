using AnimalMotel.Model.Animals.Birds;
using AnimalMotel.Model.Animals.Insects;
using AnimalMotel.Model.Animals.Mammals;
using ApusAnimalHotel.Model;
using ApusAnimalHotel.ViewModel.AnimalSepecsBlock;
using ApusAnimalHotel.ViewModel.ButtonControls;
using ApusAnimalHotel.ViewModel.FeedingScheduleCheckBox;
using ApusAnimalHotel.ViewModel.FoodDetails;
using ApusAnimalHotel.ViewModel.GroupListBoxIO;
using ApusAnimalHotel.ViewModel.ListAnimaObjectsInDetail;
using ApusAnimalHotel.ViewModel.MenuUI;
/// <summary>
/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-03-05
/// </summary>
namespace ApusAnimalHotel.ViewModel
{
    /// <summary>
    /// This is the main class that is initialized and used with the XAML.
    /// </summary>
    /// <seealso cref="ApusAnimalHotel.ViewModel.GroupListBoxIO.Observer" />
    class UIMainWindowIO : Observer, ObserverAnimalButton
    {
        #region Window title        
        /// <summary>
        /// Gets the window title.
        /// </summary>
        /// <value>
        /// The window title.
        /// </value>
        public string WindowTitle
        {
            get {
                return ContentEnumToText.GetContentText(ContentTextEnum.Apus_Animal_Motel);
            }
        }
        #endregion

        #region Objects for animal specifications, food list for an animal, listed of animals by species.
        /// <summary>
        /// Gets or sets the animal specifications. This property holds an
        /// instance of a class that controls and manipulates the input and
        /// output for animal specifications used in the user interface.
        /// </summary>
        /// <value>
        /// The animal specifications.
        /// </value>
        public AnimalSpecificationsUI AnimalSpecifications
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the list animal objects in detail. This property holds an
        /// instance of a class that displays a selected list of animal from the 
        /// register.
        /// </summary>
        /// <value>
        /// The list animal objects in detail.
        /// </value>
        public ListAnimalObjectsInDetailUI ListAnimalObjectsInDetail
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the feeding schedule.
        /// </summary>
        /// <value>
        /// The feeding schedule.
        /// </value>
        public FeedingSchedulelUI FeedingScheduel
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the delete animal.
        /// </summary>
        /// <value>
        /// The delete animal.
        /// </value>
        public DeleteAnimalButton DeleteAnimal
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the food details.
        /// </summary>
        /// <value>
        /// The food details.
        /// </value>
        public FoodDetailsUI FoodDetails
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or set the UI Menu itmes
        /// </summary>
        public Menu Menu
        {
            get;
            set;
        }
        #endregion

        #region Subscriber
        public void Update()
        {
           string updataData;
            updataData = ListAnimalObjectsInDetail.GetState();
            AnimalSpecifications.UpdateAnimalSpecifications(updataData);

        }
        /// <summary>
        /// Updates from animal buttons.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public void UpdateFromNewAnimalButton()
        {
           //AnimalSpecifications.ResetAnimalSpecificationsUI();
            //AnimalSpecifications.AnimalSpecies

        }

        #endregion

        public void ResetApp()
        {
            AnimalSpecifications.ResetAnimalSpecificationsUI();
            ListAnimalObjectsInDetail.ResetListOfAnimalInDetail();
            AnimalManager.GetInstance.ResetManager();
            AnimalSpecifications.AnimalImage.ImageButton.ResetImage();
        }
     
        public void ResetFoodDetails()
        {
            RecipeManager.GetInstance.ResetManager();
            FoodDetails.FoodDetails.RefreshList();
        }
        public void RefreshApp()
        {
            ListAnimalObjectsInDetail.Update();
            FoodDetails.FoodDetails.RefreshList();
        }
        /// <summary>
        /// Populates the animal list with trivial test data.
        /// </summary>
        private void PopulateAnimalList()
        {
            AnimalManager.GetInstance.AddAnimal(new Dog("TestDog1", 4.2, string.Empty, "Female", 42, "Irish Setter"));
            AnimalManager.GetInstance.AddAnimal(new Dog("TestDog2", 6.0, string.Empty, "Male", 42, "Irish Wolfhound"));
            AnimalManager.GetInstance.AddAnimal(new Dog("AtestDog", 2.5, string.Empty, "Male", 42, "Irish Wolfhound"));
            AnimalManager.GetInstance.AddAnimal(new Dog("BtestDog", 2.5, string.Empty, "Male", 42, "Irish Wolfhound"));
            AnimalManager.GetInstance.AddAnimal(new Dog("ZtestDog", 2.5, string.Empty, "Male", 42, "Irish Wolfhound"));
            AnimalManager.GetInstance.AddAnimal(new Dolphin("TestDolphin1", 12, string.Empty, "Unknown", 90, "Bottlenose"));
            AnimalManager.GetInstance.AddAnimal(new Dolphin("TestDolphin2", 38.2, string.Empty, "Unknown", 62, "Atlantic Humpbacked"));
            AnimalManager.GetInstance.AddAnimal(new Ostrich("TestOstrich1", 22.5, string.Empty, "Male", "Blue neck", 2));
            AnimalManager.GetInstance.AddAnimal(new Ostrich("TestOstrich2", 32.5, string.Empty, "Female", "Red neck", 2));
            AnimalManager.GetInstance.AddAnimal(new Penguin("TestPenguin1", 2.5, string.Empty, "Unknown", "Emperor", 2));
            AnimalManager.GetInstance.AddAnimal(new Penguin("TestPenguin2", 10.5, string.Empty, "Unknown", "Adelie", 0.7));
            AnimalManager.GetInstance.AddAnimal(new Bee("beee", 1, string.Empty, "Male", true, false));
            // AnimalManager.GetInstance.AddAnimal(new Fly("Blue bottle", 1, string.Empty, "Female", false, true));
        }

        private void AttactSubscribers()
        {
            DeleteAnimal.AttachToDeleteAnimalButton(this);
            ListAnimalObjectsInDetail.Attach(this);
        }
        /// <summary>
        /// Initialize class.
        /// </summary>
        private void Initialize()
        {
            AnimalSpecifications = new AnimalSpecificationsUI();

            ListAnimalObjectsInDetail = new ListAnimalObjectsInDetailUI(AnimalSpecifications.ListAnimals,
                AnimalSpecifications.AnimalSpecies,
                AnimalSpecifications.AddNewAnimal);

            DeleteAnimal = new DeleteAnimalButton(ListAnimalObjectsInDetail);
            DeleteAnimal.AttachToDeleteAnimalButton(ListAnimalObjectsInDetail);
            DeleteAnimal.AttachToDeleteAnimalButton(AnimalSpecifications.ListAnimals);
            FeedingScheduel = new FeedingSchedulelUI(ListAnimalObjectsInDetail, AnimalSpecifications.ListAnimals);

            FoodDetails = new FoodDetailsUI();

            Menu = new Menu(this);

        }
        public UIMainWindowIO()
        {
            Initialize();
            
            AttactSubscribers();

            //PopulateAnimalList();

            ResetApp();
        }


    }
}
