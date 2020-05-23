using AnimalMotel.Model.Animals;
using ApusAnimalHotel.Model;
using ApusAnimalHotel.ViewModel.ButtonControls;
using ApusAnimalHotel.ViewModel.Commands;
using ApusAnimalHotel.ViewModel.GroupListBoxIO;
using ApusAnimalHotel.ViewModel.ListAnimaObjectsInDetail.SortingMethods;
using ApusAnimalHotel.ViewModel.ListRegisterAnimalsCheckBox;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
/// <summary>
/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-03-11
/// </summary>
namespace ApusAnimalHotel.ViewModel.ListAnimaObjectsInDetail
{
    class ListAnimalObjectsInDetailUI : Subject, Observer, INotifyPropertyChanged, ObserverAnimalButton
    {
        private int noItemSelected = -1;

        #region Tag titles           
        /// <summary>
        /// Gets the header.
        /// </summary>
        /// <value>
        /// The header.
        /// </value>
        public string Header
        {
            get {
                return ContentEnumToText.GetContentText(ContentTextEnum.List_of_registered_animals);
            }
        }
        /// <summary>
        /// Gets the identifier tag.
        /// </summary>
        /// <value>
        /// The identifier tag.
        /// </value>
        public string IDTag
        {
            get {
                return ContentEnumToText.GetContentText(ContentTextEnum.ID);
            }
        }
        /// <summary>
        /// Gets the name tag.
        /// </summary>
        /// <value>
        /// The name tag.
        /// </value>
        public string NameTag
        {
            get {
                return ContentEnumToText.GetContentText(ContentTextEnum.Name);
            }
        }
        /// <summary>
        /// Gets the age tag.
        /// </summary>
        /// <value>
        /// The age tag.
        /// </value>
        public string AgeTag
        {
            get {
                return ContentEnumToText.GetContentText(ContentTextEnum.Age);
            }
        }
        /// <summary>
        /// Gets the gender tag.
        /// </summary>
        /// <value>
        /// The gender tag.
        /// </value>
        public string GenderTag
        {
            get {
                return ContentEnumToText.GetContentText(ContentTextEnum.Gender);
            }
        }
        /// <summary>
        /// Gets the special characteristics tag.
        /// </summary>
        /// <value>
        /// The special characteristics tag.
        /// </value>
        public string SpecialCharacteristicsTag
        {
            get {
                return ContentEnumToText.GetContentText(ContentTextEnum.Special_Characteristics);
            }
        }
        #endregion

        #region List of animals by species properties        
        /// <summary>
        /// Gets or sets the registered animals.
        /// </summary>
        /// <value>
        /// The registered animals.
        /// </value>
        public ObservableCollection<ListedAnimalDetails> RegisteredAnimals
        {
            get {
                return _registeredAnimals;
            }
            set {
                _registeredAnimals = value;
                OnPropertyChanged("RegisteredAnimals");
            }
        }
        private ObservableCollection<ListedAnimalDetails> _registeredAnimals;
        /// <summary>
        /// Gets or sets the is selected.
        /// </summary>
        /// <value>
        /// The is selected.
        /// </value>
        public int IsSelected
        {
            get {
                return _isSelected;
            }
            set {
                _isSelected = value;
                if (value > noItemSelected)
                {
                    ItemSelected(value);
                }
                OnPropertyChanged("IsSelected");
                NotifyAllObservers();
            }
        }
        private int _isSelected;
        #endregion

        #region Subscriptions
        private ListAllAnimals listAllAnimals;
        private AnimalObjects animalObjects;
        private AddNewAnimalButton addNewAnimal;
        /// <summary>
        /// Updates this instance.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void Update()
        {
            bool isActive = listAllAnimals.GetState().IsActive;
            string animalObj = animalObjects.GetState();

            if (isActive)
            {
                GetRegisteredAnimals(animalObj);
            }
            if (!isActive)
            {
                id = null;
                GetRegisteredAnimals();
            }
        }

        private void GetRegisteredAnimals()
        {
            int count = AnimalManager.GetInstance.NumberOfAnimalResidents;
            Animal animal;
            
            ClearRegisteredAnimalsCollection();
            if (count > 0)
            {
                for (int index = 0; index < count; index++)
                {
                    animal = AnimalManager.GetInstance.GetAnimalAt(index);
                    AddAnimlToRegisterOfAnimals(animal);
                }
            }
        }
        /// <summary>
        /// Get a registered animal.
        /// </summary>
        /// <param name="species">The species.</param>
        private void GetRegisteredAnimals(string species)
        {
            int count = AnimalManager.GetInstance.NumberOfAnimalResidents;
            Animal animal;
            
            ClearRegisteredAnimalsCollection();

            if (count > 0)
            {
                for (int index = 0; index < count; index++)
                {
                    animal = AnimalManager.GetInstance.GetAnimalAt(index);
                    if (species == animal.GetSpeciesType())//animal.GetType().Name)
                    {
                        AddAnimlToRegisterOfAnimals(animal);
                    }
                }
            }
        }

        /// <summary>
        /// Add an animal object to the collection of animals.
        /// </summary>
        /// <param name="animal">Animal object</param>
        private void AddAnimlToRegisterOfAnimals(Animal animal)
        {
            ListedAnimalDetails newAnimal = new ListedAnimalDetails(animal);

            RegisteredAnimals.Add(new ListedAnimalDetails(animal));
        }

        /// <summary>
        /// Clear the contents of the register collection of animals.
        /// </summary>
        private void ClearRegisteredAnimalsCollection()
        {
            RegisteredAnimals.Clear();
        }

        #region Subscriber New, Delete Animal buttons 

        /// <summary>
        /// Updates from delete or new animal button via a publisher.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void UpdateFromNewAnimalButton()
        {
            if (!string.IsNullOrEmpty(addNewAnimal.GetStateOfAnimalButton()))
            {
                AddNewAnimal(addNewAnimal.GetStateOfAnimalButton());
            }
            else
            {
                //delete occurred, refresh
                Update();
            }
        }
        /// <summary>
        /// A new animal has been added.
        /// </summary>
        private void AddNewAnimal(string species)
        {
            if (listAllAnimals.GetState().IsActive)
            {
                GetRegisteredAnimals(species);
            }
            else
            {
                GetRegisteredAnimals();
            }
        }

        #endregion
        #endregion

        #region Publish
        private List<Observer> observers = new List<Observer>();
        /// <summary>
        /// Attach an subscriber.
        /// </summary>
        /// <param name="observer"></param>
        public void Attach(Observer observer)
        {
            observers.Add(observer);
        }

        /// <summary>
        /// Publish results.
        /// </summary>
        /// <returns>The identification of an animal.</returns>
        public string GetState()
        {
            if (IsSelected > noItemSelected)
            {
                return id;
            }
            return null;
        }

        /// <summary>
        /// Notify all subscribers that an update occurred.
        /// </summary>
        public void NotifyAllObservers()
        {
            foreach (Observer observer in observers)
            {
                observer.Update();
            }
        }

        #endregion

        #region Selection from list                
        private string id;
        /// <summary>
        /// Get the identification of the selected animal and update this class data.
        /// </summary>
        /// <param name="index">Index into a collection of animals</param>
        private void ItemSelected(int index)
        {
            id = GetAnimalById(index);
            //NotifyAllObservers();
        }

        /// <summary>
        /// Gets the animal by identifier.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        private string GetAnimalById(int index)
        {
            return RegisteredAnimals.ElementAt(index).ID;
        }

        #endregion

        #region Relay commands ex:RelayCommands RelayCommands(method, property)

        /// <summary>
        /// Load the commands that perform a sorting function.
        /// </summary>
        private void LoadCommands()
        {
            SortByNameCommand = new RelayCommands(SortByNameExecute, SortByNameCanExecute);
            SortByAgeCommand = new RelayCommands(SortByAgeExecute, SortByAgeCanExecute);
            SortByGenderCommand = new RelayCommands(SortByGenderExecute, SortByGenderCanExecute);
        }

        #region Sorting method
        /// <summary>
        /// Gets or sets a value indicating whether [sort toggle].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [sort toggle]; otherwise, <c>false</c>.
        /// </value>
        private bool SortToggle
        {
            get {
                bool old_sortToggle = _sortToggle;
                _sortToggle = old_sortToggle ? false : true;
                return old_sortToggle;
            }
            set {
                _sortToggle = value;
            }
        }
        private bool _sortToggle;

        /// <summary>
        /// Resets the sort toggle.
        /// </summary>
        private void ResetSortToggle()
        {
            SortToggle = false;
        }

        /// <summary>
        /// Sort the ObservableCollection according to a strategy.
        /// </summary>
        /// <param name="sortAnimal"></param>
        /// <returns></returns>
        private List<ListedAnimalDetails> SortList(SortingMethodBy sortAnimal)
        {
            //copy ObservableCollection to List
            List<ListedAnimalDetails> copyRegisteredAnimals = RegisteredAnimals.ToList();

            //use list to sort the data
            copyRegisteredAnimals.Sort(sortAnimal);//sort by animal age

            //toggle from sort to reverse sort
            if (!SortToggle)
            {
                copyRegisteredAnimals.Reverse();
            }

            return copyRegisteredAnimals;
        }
        /// <summary>
        /// Update the list of animals selected by animal object.
        /// </summary>
        /// <param name="sortAnimal"></param>
        private void UpdateListOfAnimalObjects(SortingMethodBy sortAnimal)
        {
            //update the now sorted ObservableCollection update UI 
            RegisteredAnimals = new ObservableCollection<ListedAnimalDetails>(SortList(sortAnimal));
        }

        #region Sort by Name command
        /// <summary>
        /// Gets or sets the sort by name command.
        /// </summary>
        /// <value>
        /// The sort by name command.
        /// </value>
        public RelayCommands SortByNameCommand
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private bool SortByNameCanExecute(Object parameter)
        {
            return true;
        }
        /// <summary>
        /// Sort by name of animal.
        /// </summary>
        /// <param name="parameter"></param>
        private void SortByNameExecute(Object parameter)
        {
            SortingMethodBy sortAnimal = new ComparNames();
            UpdateListOfAnimalObjects(sortAnimal);
        }
        #endregion

        #region Sort by Age command         
        /// <summary>
        /// Gets or sets the sort by species command.
        /// </summary>
        /// <value>
        /// The sort by species command.
        /// </value>
        public RelayCommands SortByAgeCommand
        {
            get;
            set;
        }
        /// <summary>
        /// Can sort by gender of animal.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private bool SortByAgeCanExecute(Object parameter)
        {
            return true;
        }
        /// <summary>
        /// Sort by age of animal.
        /// </summary>
        /// <param name="parameter"></param>
        private void SortByAgeExecute(Object parameter)
        {
            SortingMethodBy sortAnimal = new ComparAges();
            UpdateListOfAnimalObjects(sortAnimal);
        }
        #endregion

        #region Sort by Gender command
        /// <summary>
        /// Gets or sets the sort by species command.
        /// </summary>
        /// <value>
        /// The sort by species command.
        /// </value>
        public RelayCommands SortByGenderCommand
        {
            get;
            set;
        }
        /// <summary>
        /// Can sort by gender of animal.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private bool SortByGenderCanExecute(Object parameter)
        {
            return true;
        }
        /// <summary>
        /// Sort by gender of animal.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        private void SortByGenderExecute(Object parameter)
        {
            SortingMethodBy sortAnimal = new ComparGender();
            //update the now sorted ObservableCollection update UI 
            UpdateListOfAnimalObjects(sortAnimal);
        }


        #endregion

        #endregion

        #endregion

        private void SetListners()
        {
            listAllAnimals.Attach(this);
            animalObjects.Attach(this);
            addNewAnimal.AttachToNewAnimalButton(this);
        }
        public void ResetListOfAnimalInDetail()
        {
            //GetRegisteredAnimals();
            ClearRegisteredAnimalsCollection();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListAnimalObjectsInDetailUI"/> class.
        /// </summary>
        /// <param name="listAllAnimals">The list all animals.</param>
        /// <param name="animalObjects">The animal objects.</param>
        public ListAnimalObjectsInDetailUI(ListAllAnimals listAllAnimals, AnimalObjects animalObjects,
            AddNewAnimalButton addNewAnimal)
        {
            RegisteredAnimals = new ObservableCollection<ListedAnimalDetails>();
            //subscriptions
            this.listAllAnimals = listAllAnimals;

            this.animalObjects = animalObjects;

            //animal buttons
            this.addNewAnimal = addNewAnimal;


            SetListners();
            IsSelected = noItemSelected;

            ResetSortToggle();

            LoadCommands();
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
