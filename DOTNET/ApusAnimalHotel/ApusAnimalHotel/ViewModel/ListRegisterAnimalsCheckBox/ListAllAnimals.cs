using ApusAnimalHotel.Model;
using ApusAnimalHotel.ViewModel.ButtonControls;
using ApusAnimalHotel.ViewModel.Commands;
using ApusAnimalHotel.ViewModel.GroupListBoxIO;
using System.Collections.Generic;
/// <summary>
/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-03-11
/// </summary>
namespace ApusAnimalHotel.ViewModel.ListRegisterAnimalsCheckBox
{
     class ListAllAnimals: AnimalCheckBox, PublisherList, ObserverAnimalButton
    {
        private readonly string title = "List of registered animals";

        public object AnimalManger { get; private set; }

        private List<string> animalObjectsFromManager;

        private bool isSelected;
        
        /// <summary>
        /// Can the checkbox be executed.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override bool CheckBoxCanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// CheckBox has been pressed, execute method.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void CheckBoxExecute(object parameter)
        {
            isSelected = (bool)parameter;

            if (isSelected)
            {
                CollectAnimalObjectData();
            }
            else
            {
                CollectAnimalObjectNull();
            }

            NotifyAllObservers();
        }

        /// <summary>
        /// There is no collectable data for a animal object and is null.
        /// </summary>
        private void CollectAnimalObjectNull()
        {
            animalObjectsFromManager = new List<string>();
        }

        /// <summary>
        /// Collect the relevant data for all Animal Objects.
        /// </summary>
        private void CollectAnimalObjectData()
        {
            int numberOfAnimals = AnimalManager.GetInstance.NumberOfAnimalResidents;
            animalObjectsFromManager = new List<string>();

            for (int index = 0; index < numberOfAnimals; index++)
            {
                string species = AnimalManager.GetInstance.GetAnimalAt(index).GetType().Name;
                if (!animalObjectsFromManager.Contains(species))
                {
                    animalObjectsFromManager.Add(species);
                }
            }

            animalObjectsFromManager.Sort();
        }
        /// <summary>
        /// Re-set the check box as unselected.
        /// </summary>
        public void ResetCheckbox()
        {
            IsChecked = false;
            CheckBoxExecute(false);

        }
        #region Subscriber        
        /// <summary>
        /// Updates from a pressed button. In this case some update occurred.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public void UpdateFromNewAnimalButton()
        {
            CollectAnimalObjectData();
            NotifyAllObservers();
        }
        #endregion

        #region Publisher
        List<Observer> observers = new List<Observer>();//list of observers
        /// <summary>
        /// Attaches the specified observer.
        /// </summary>
        /// <param name="observer">The observer.</param>
        public void Attach(Observer observer)
        {
            observers.Add(observer);
        }
        /// <summary>
        /// Gets the published state.
        /// </summary>
        /// <returns>Data from the AnimaManager</returns>
        public AnimalSubscription GetState()
        {
            AnimalSubscription animalSubscription = new AnimalSubscription(isSelected, animalObjectsFromManager);
            //List<string> sendList = new List<string>(animalObjectsFromManager);
            return animalSubscription;
        }
        /// <summary>
        /// Notifies all observers.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public void NotifyAllObservers()
        {
            foreach(Observer observer in observers)
            {
                observer.Update();
            }
        }

        #endregion


        public ListAllAnimals()
        {
            isSelected = false;
            CheckBoxContent = title;
            //ex:RelayCommands RelayCommands(method, property)
            //list of commands
            CheckBoxCommand = new RelayCommands(CheckBoxExecute, CheckBoxCanExecute);
            
        }

    }
}
