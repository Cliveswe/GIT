using AnimalMotel.Model.Animals;
using AnimalMotel.Model.FeedingPlan;
using ApusAnimalHotel.Model;
using ApusAnimalHotel.ViewModel.GroupListBoxIO;
using ApusAnimalHotel.ViewModel.ListAnimaObjectsInDetail;
using ApusAnimalHotel.ViewModel.ListRegisterAnimalsCheckBox;
using System.ComponentModel;
/// <summary>
/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-03-15
/// </summary>
namespace ApusAnimalHotel.ViewModel.FeedingScheduleCheckBox
{
    /// <summary>
    /// This class displays the feeding schedule of an animal by genus.
    /// </summary>
    /// <seealso cref="ApusAnimalHotel.ViewModel.LabelTextBoxIO.TextInputOutput" />
    class FeedingSchedulelUI : INotifyPropertyChanged, Observer
    {
        #region Class properties
        /// <summary>
        /// Gets the feeding schedule header.
        /// </summary>
        /// <value>
        /// The feeding schedule header.
        /// </value>
        public string FeedingScheduleHeader
        {
            get {
                return ContentEnumToText.GetContentText(ContentTextEnum.Feeding_schedule);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is enabled; otherwise, <c>false</c>.
        /// </value>
        public bool IsEnabled
        {
            get {
                return _isEnabled;
            }
            set {
                _isEnabled = value;
                OnPropertyChanged("IsActive");
            }
        }
        private bool _isEnabled;

        /// <summary>
        /// Gets or sets the diet type header.
        /// </summary>
        /// <value>
        /// The diet type header.
        /// </value>
        public DietTypeHeaderIO DietTypeHeader
        {
            get {
                return _dietTypeHeader;
            }
            set {
                _dietTypeHeader = value;
            }
        }
       private DietTypeHeaderIO _dietTypeHeader;

        /// <summary>
        /// Gets or sets the feeding schedule.
        /// </summary>
        /// <value>
        /// The feeding schedule.
        /// </value>
        public FeedingScheduleDetails FeedingSchedule
        {
            get {
                return _feedingSchedule;
            }
            set {
                _feedingSchedule = value;
            }
        }
        private FeedingScheduleDetails _feedingSchedule;
        #endregion

        #region Subscription
        private ListAnimalObjectsInDetailUI publisher;
        private ListAllAnimals listAllAnimalsPublisher;
        /// <summary>
        /// Updates this instance.
        /// </summary>
        public void Update()
        {
            GetAnimalDetails(publisher.GetState());
        }

        /// <summary>
        /// Gets the animal details.
        /// </summary>
        /// <param name="id">The identifier.</param>
        private void GetAnimalDetails(string id)
        {
            int count;
            Animal animal = null;

            count = AnimalManager.GetInstance.NumberOfAnimalResidents;

            if ((count > 0) && (!string.IsNullOrWhiteSpace(id))) {
                for (int index = 0; count > index; index++)
                {
                    animal = AnimalManager.GetInstance.GetAnimalAt(index);        
                    if (animal.AnimalID == id)
                    {
                        break;
                    }
                }
            }

            UpdateUI(animal);
        }

        /// <summary>
        /// Updates the UI.
        /// </summary>
        /// <param name="animal">The animal.</param>
        private void UpdateUI(Animal animal)
        {
            FoodSchedule fs;

            FeedingSchedule.RestFeedingSchedule();

            if (animal != null)
            {
                fs = animal.GetFoodSchedule();
                FeedingSchedule.SetDietType(animal.GetEaterType());
                FeedingSchedule.SetFeedingDiet(fs);
            }
           /* else
            {
                FeedingSchedule.RestFeedingSchedule();
            }*/
        }
        #endregion
        /// <summary>
        /// Initializes a new instance of the <see cref="FeedingSchedulelUI"/> class.
        /// </summary>
        public FeedingSchedulelUI(ListAnimalObjectsInDetailUI animalObjectsInDetailUI, ListAllAnimals animalObjectsListed)
        {
            publisher = animalObjectsInDetailUI;
            publisher.Attach(this);

            listAllAnimalsPublisher = animalObjectsListed;
            listAllAnimalsPublisher.Attach(this);

            DietTypeHeader = new DietTypeHeaderIO();
            IsEnabled = false;
            FeedingSchedule = new FeedingScheduleDetails();

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
