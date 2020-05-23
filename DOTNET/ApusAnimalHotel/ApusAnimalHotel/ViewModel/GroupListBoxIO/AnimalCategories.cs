using AnimalMotel.Model.Animals;
using ApusAnimalHotel.ViewModel.ListRegisterAnimalsCheckBox;
using System;
using System.Collections.Generic;
using System.Linq;
/// <summary>
/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-03-05
/// </summary>
namespace ApusAnimalHotel.ViewModel.GroupListBoxIO
{
    class AnimalCategories : GroupListBoxInteractive, Subject, Observer
    {
        #region Publisher
        //A list of observers of this class
        private List<Observer> observers = new List<Observer>();

        public void Attach(Observer observer)
        {
            observers.Add(observer);
        }

        /// <summary>
        /// Gets the state.
        /// </summary>
        /// <returns></returns>
        public string GetState()
        {
            string str = string.Empty;
            str = Enum.ToObject(typeof(AnimalCategoryEnum), IsSelected).ToString();
            return str;
        }

        /// <summary>
        /// Notifies all observers.
        /// </summary>
        public void NotifyAllObservers()
        {
            if (IsEnabled && (IsSelected > noItemSelected))
            {
                foreach (Observer observer in observers)
                {
                    observer.Update();
                }
            }
        }
        #endregion

        #region PublisherList Observer
        private PublisherList listAnimalsPublisher;
        private AnimalObjects animalObjects;
        /// <summary>
        /// Updates this instance.
        /// </summary>
        public void Update()
        {
            AnimalSubscription animalSubscription;

            animalSubscription = listAnimalsPublisher.GetState();
            UpdateAnimalCategories(animalSubscription);

            if (animalSubscription.IsActive && !string.IsNullOrEmpty(animalObjects.GetState())) { 
            //string str = animalObjects.GetState();//caught animal object selection 
                //string index = string.Empty;
                SelectCategoryViaListner(animalObjects.GetState());
                /*index = GetIndexForCategory(str);
                _isSelected = Contents.IndexOf(index);
                OnPropertyChanged("IsSelected");*/
            }
        }
        
        /// <summary>
        /// Category is selected from a listener. When list animal is checked, the category
        /// is selected from animal objects.
        /// </summary>
        /// <param name="state">Animal objected selected.</param>
        private void SelectCategoryViaListner(string state)
        {
            string index = string.Empty;

            index = GetIndexForCategory(state);
            /*  _isSelected = Contents.IndexOf(index);
              OnPropertyChanged("IsSelected");*/
            IsSelected = Contents.IndexOf(index);
            
        }
        
        /// <summary>
        /// Updates the animal categories with data from the publisher.
        /// </summary>
        /// <param name="animalSubscription">The animal subscription.</param>
        private void UpdateAnimalCategories(AnimalSubscription animalSubscription)
        {
            if (animalSubscription.IsActive) {
                IsEnabled = false;
                IsSelected = noItemSelected;
            }
            else
            {
                IsEnabled = true;
            }
        }
        #endregion
        /// <summary>
        /// Gets or sets what has been selected from Contents.
        /// </summary>
        /// <value>
        /// The is selected.
        /// </value>
        public override int IsSelected {
            get {
                return _isSelected;
            }
            set {
                _isSelected = value;
                NotifyAllObservers();
                OnPropertyChanged("IsSelected");
            }
        }
        private int _isSelected;
       
        /// <summary>
        /// Sets the header.
        /// </summary>
        public override void SetHeader()
        {
            GroupListBoxHeader = ContentEnumToText.GetContentText(ContentTextEnum.Category);
        }

        /// <summary>
        /// Resets the is selected.
        /// </summary>
        public override void ResetIsSelected()
        {
            IsSelected = noItemSelected;
        }
        /// <summary>
        /// Get the category name of an animal by species.
        /// </summary>
        /// <param name="animalSpecies">Animal species</param>
        /// <returns>Category name of a animal</returns>
        private string GetIndexForCategory(string animalSpecies)
        {
            string index = string.Empty;

            if (Enum.GetNames(typeof(BirdEnum)).Contains(animalSpecies))
            {
                index = AnimalCategoryEnum.Bird.ToString();
            }
            if (Enum.GetNames(typeof(InsectEnum)).Contains(animalSpecies))
            {
                index = AnimalCategoryEnum.Insect.ToString();
            }
            if (Enum.GetNames(typeof(MammalEnum)).Contains(animalSpecies))
            {
                index = AnimalCategoryEnum.Mammal.ToString();
            }
            return index;
        }

        /// <summary>
        /// Sets the category.
        /// </summary>
        /// <param name="animalSpecies">The animal species.</param>
        public void SetCategory(string animalSpecies)
        {
            string index = string.Empty;

            index = GetIndexForCategory(animalSpecies);
           
            if (!string.IsNullOrWhiteSpace(index))
            {
                UpdateIsSelected(index);
            }
        }

        
        /// <summary>
        /// Updates is selected.
        /// </summary>
        /// <param name="item">The item.</param>
        private void UpdateIsSelected(string item)
        {
            //ToggleIsEnable();
            IsSelected = Contents.IndexOf(item);
            //ToggleIsEnable();
        }

        /// <summary>
        /// Toggle the is enable.
        /// </summary>
        private void ToggleIsEnable()
        {
            IsEnabled = !IsEnabled;
        }

        /// <summary>
        /// Populates the content.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public override void PopulateContent()
        {
            Contents = Enum.GetNames(typeof(AnimalCategoryEnum)).ToList();
        }

        /// <summary>
        /// Sets the animal species listener.
        /// </summary>
        /// <param name="animalObjects">The animal objects.</param>
        public void SetAnimalSpeciesListener(AnimalObjects animalObjects)
        {
            this.animalObjects = animalObjects;
            animalObjects.Attach(this);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AnimalCategories"/> class.
        /// </summary>
        /// <param name="listAnimalsPublisher">The list animals publisher.</param>
        public AnimalCategories(PublisherList listAnimalsPublisher) : base()
        {
            SetHeader();
            PopulateContent();
            //subscriptions
            this.listAnimalsPublisher = listAnimalsPublisher;
            listAnimalsPublisher.Attach(this);
        }

        
    }
}
