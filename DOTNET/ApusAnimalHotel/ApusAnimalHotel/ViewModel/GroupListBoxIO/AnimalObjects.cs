using ApusAnimalHotel.ViewModel.ListRegisterAnimalsCheckBox;
using System;
using System.Collections.Generic;
using System.Linq;
/// <summary>
/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-03-07
/// </summary>
namespace ApusAnimalHotel.ViewModel.GroupListBoxIO
{
     class AnimalObjects : GroupListBoxInteractive, Observer, Subject
    {
        #region Subscribers to Publishers
        private Subject subject;
        private PublisherList listAnimalsPublisher;

        /// <summary>
        /// Update this class with data from the publisher (Subject)
        /// </summary>
        public void Update()
        {
            if (!listAnimalsPublisher.GetState().IsActive)
            {
                SetAnimalObjectContent(subject.GetState());
            }
            else
            {
                UpdateAnimalObjectContent(listAnimalsPublisher.GetState());
            }
        }

        /// <summary>
        /// Updates the content of the animal object.
        /// </summary>
        /// <param name="animalSubscription">The animal subscription.</param>
        /// <exception cref="NotImplementedException"></exception>
        private void UpdateAnimalObjectContent(AnimalSubscription animalSubscription)
        {
           // if (animalSubscription.IsActive)
            //{
                Contents = animalSubscription.AnimalObjects;
            //}
        }
        #endregion

        #region Publisher
        //A list of observers of this class
        private List<Observer> observers = new List<Observer>();
        /// <summary>
        /// Attaches the specified observer.
        /// </summary>
        /// <param name="observer">The observer.</param>
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
            if (IsSelected == noItemSelected)
            {
                str = string.Empty;
            }
            else
            {
                str = Contents[IsSelected];
            }

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

        /// <summary>
        /// Gets or sets what has been selected from Contents.
        /// </summary>
        /// <value>
        /// The is selected.
        /// </value>
        public override int IsSelected
        {
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
        /// Populates the content.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public override void PopulateContent()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sets the content of the animal object.
        /// </summary>
        /// <param name="str">The string.</param>
        public void SetAnimalObjectContent(string str)
        {
            AnimalCategoryEnum res;

            Enum.TryParse(str, out res);//convert string to enum
            switch (res)
            {
                case AnimalCategoryEnum.Bird:
                    Contents = Enum.GetNames(typeof(BirdEnum)).ToList();
                    break;
                case AnimalCategoryEnum.Insect:
                    Contents = Enum.GetNames(typeof(InsectEnum)).ToList();
                    break;
                case AnimalCategoryEnum.Mammal:
                    Contents = Enum.GetNames(typeof(MammalEnum)).ToList();
                    break;
                default:
                    ClearContents();
                    break;
            }
        }

        public void SetAnimalObjectAsSelected(string animalSpecies)
        {
            if (Enum.GetNames(typeof(BirdEnum)).Contains(animalSpecies))
            {
                SetAnimalObjectContent(AnimalCategoryEnum.Bird.ToString());
            }
            if (Enum.GetNames(typeof(InsectEnum)).Contains(animalSpecies))
            {
                SetAnimalObjectContent(AnimalCategoryEnum.Insect.ToString());
            }
            if (Enum.GetNames(typeof(MammalEnum)).Contains(animalSpecies))
            {
                SetAnimalObjectContent(AnimalCategoryEnum.Mammal.ToString());
            }
            SetIsSelected(Contents.IndexOf(animalSpecies));
        }

       
        private void SetIsSelected(int value)
        {
            if (IsSelected != value)
            {
                IsSelected = value;
            }
        }

        /// <summary>
        /// Sets the header.
        /// </summary>
        public override void SetHeader()
        {
            GroupListBoxHeader = ContentEnumToText.GetContentText(ContentTextEnum.Animal_Object);
        }

        /// <summary>
        /// Resets the is selected.
        /// </summary>
        public override void ResetIsSelected()
        {
            IsSelected = noItemSelected;
        }

        /// <summary>
        /// Resets the animal objects.
        /// </summary>
        public void ResetAnimalObjects() {
            ResetIsSelected();
            ClearContents();
            ResetIsEnabled();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AnimalObjects"/> class.
        /// </summary>
        public AnimalObjects(Subject subject, PublisherList listAnimalsPublisher) : base()
        {
            SetHeader();
            //subscribe to publishers
            this.listAnimalsPublisher = listAnimalsPublisher;
            listAnimalsPublisher.Attach(this);
            
            this.subject = subject;
            subject.Attach(this);
        }
    }
}
