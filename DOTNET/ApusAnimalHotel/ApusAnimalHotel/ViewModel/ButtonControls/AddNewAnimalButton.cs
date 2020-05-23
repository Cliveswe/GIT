using AnimalMotel.Model.Animals;
using AnimalMotel.Model.Animals.Birds;
using AnimalMotel.Model.Animals.Insects;
using AnimalMotel.Model.Animals.Mammals;
using ApusAnimalHotel.Model;
using ApusAnimalHotel.ViewModel.AnimalSepecsBlock;
using System;
using System.Collections.Generic;
using System.Globalization;
/// <summary>
/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-03-09
/// </summary>
namespace ApusAnimalHotel.ViewModel.ButtonControls
{
    class AddNewAnimalButton : AnimalButton, SubjectNewAnimalButton
    {
        private readonly string buttonTitle = "Add new animal";
        //access to the user interface input and output
        private AnimalSpecificationsUI animalSpecificationsUI;

        #region Add New Animal Button Control        
        /// <summary>
        /// Can the button be executed.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>True if the button is active otherwise false</returns>
        public override bool ButtonCanExecute(object parameter)
        {
            bool validate = true;
            //is animal name valid
            if (!animalSpecificationsUI.NameOfAnimalUI.AnimalName.IsValid)
            {
                validate = false;
            }
            //is animal age valid
            if (!animalSpecificationsUI.AgeOfAnimalUI.AnimalAge.IsValid)
            {
                validate = false;
            }
            //is animal category valid
            if (!animalSpecificationsUI.Specifications.SpecificationsForCategory.IsValid)
            {
                validate = false;
            }
            //is animal object valid
            if (!animalSpecificationsUI.Specifications.SpecificationsForAnimalObject.IsValid)
            {
                validate = false;
            }
            //is animal gender valid
            if (animalSpecificationsUI.Gender.IsSelected <= animalSpecificationsUI.Gender.noItemSelected)
            {
                validate = false;
            }
            return validate;
        }

        /// <summary>
        /// Button is pressed execute method.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public override void ButtonExecute(object parameter)
        {
            //animal category selected
            string animalCategory = this.animalSpecificationsUI.Categories.Contents[
                this.animalSpecificationsUI.Categories.IsSelected];
            //animal object selected
            string animalObject = this.animalSpecificationsUI.AnimalSpecies.Contents[
                this.animalSpecificationsUI.AnimalSpecies.IsSelected];

            publishAnimalObject = animalObject;

            FilterForCategory(animalCategory, animalObject);//filter for categories
            NotifyAllObserversOfAnimalButton();//notify observers of an update
            animalSpecificationsUI.ResetAnimalSpecificationsUI();//reset the UI
        }
        #endregion

        #region Publish
        private string publishAnimalObject;
        private List<ObserverAnimalButton> observers = new List<ObserverAnimalButton>();
        /// <summary>
        /// Attaches a subscriber to new animal button.
        /// </summary>
        /// <param name="observer">The observer.</param>
        public void AttachToNewAnimalButton(ObserverAnimalButton observer)
        {
            observers.Add(observer);
        }
        /// <summary>
        /// Publish results.
        /// </summary>
        /// <returns></returns>
        public string GetStateOfAnimalButton()
        {
            return publishAnimalObject;
        }
        /// <summary>
        /// Notifies all subscribers that an update occurred.
        /// </summary>
        public void NotifyAllObserversOfAnimalButton()
        {
            foreach (ObserverAnimalButton observer in observers)
            {
                observer.UpdateFromNewAnimalButton();
            }
        }
        #endregion

        /// <summary>
        /// Given a category now choose an animal object.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="animalObj">The animal object.</param>
        private void FilterForCategory(string category, string animalObj)
        {
            string name = animalSpecificationsUI.NameOfAnimalUI.GetAnimalName();
            double age = animalSpecificationsUI.AgeOfAnimalUI.AnimalAge.TextIOToDouble();
            string gender = animalSpecificationsUI.Gender.GetGender();

            //Try to convert the string to an enum
            AnimalCategoryEnum cat = (AnimalCategoryEnum)Enum.Parse(typeof(AnimalCategoryEnum), category);

            switch (cat)
            {
                case AnimalCategoryEnum.Bird:
                    BirdSelected(animalObj, name, age, gender);
                    break;
                case AnimalCategoryEnum.Insect:
                    InsectSelected(animalObj, name, age, gender);
                    break;
                case AnimalCategoryEnum.Mammal:
                    MammalSelected(animalObj, name, age, gender);
                    break;
            }
        }

        /// <summary>
        /// A birds was selected. Now specify the species of the bird.
        /// </summary>
        /// <param name="animalObject">Specific animal object.</param>
        /// <param name="name">It's name.</param>
        /// <param name="age">It's age.</param>
        /// <param name="gender">It's gender.</param>
        private void BirdSelected(string animalObject, string name, double age, string gender)
        {
            BirdEnum birdObj = (BirdEnum)Enum.Parse(typeof(BirdEnum), animalObject);
            string category = animalSpecificationsUI.Specifications.GetSpecificationForCategory();
            double animalObj = Convert.ToDouble(animalSpecificationsUI.Specifications.GetSpecificationForAnimalObject(), CultureInfo.InvariantCulture);

            switch (birdObj)
            {
                case BirdEnum.Ostrich:
                    AddNewAnimalToRegister(
                        new Ostrich(name, age, string.Empty, gender, category, animalObj)
                        );
                    break;
                case BirdEnum.Penguin:
                    AddNewAnimalToRegister(
                        new Penguin(name, age, string.Empty, gender, category, animalObj)
                        );
                    break;
            }
        }

        /// <summary>
        /// A insect was selected. Now specify the species of the insect.
        /// </summary>
        /// <param name="animalObject">Specific animal object.</param>
        /// <param name="name">It's name.</param>
        /// <param name="age">It's age.</param>
        /// <param name="gender">It's gender.</param>
        private void InsectSelected(string animalObject, string name, double age, string gender)
        {
            InsectEnum birdObj = (InsectEnum)Enum.Parse(typeof(InsectEnum), animalObject);
            bool category = animalSpecificationsUI.Specifications.GetSpecificationForCategory().Equals("y") ? true : false;
            bool animalObj = animalSpecificationsUI.Specifications.GetSpecificationForAnimalObject().Equals("y") ? true : false;

            switch (birdObj)
            {
                case InsectEnum.Bee:
                    AddNewAnimalToRegister(
                        new Bee(name, age, string.Empty, gender, category, animalObj)
                        );
                    break;
                case InsectEnum.Fly:
                    AddNewAnimalToRegister(
                        new Fly(name, age, string.Empty, gender, category, animalObj)
                        );
                    break;
            }
        }
        /// <summary>
        /// A mammal was selected. Now specify the species of the mammal.
        /// </summary>
        /// <param name="animalObject">Specific animal object.</param>
        /// <param name="name">It's name.</param>
        /// <param name="age">It's age.</param>
        /// <param name="gender">It's gender.</param>
        private void MammalSelected(string animalObject, string name, double age, string gender)
        {
            MammalEnum birdObj = (MammalEnum)Enum.Parse(typeof(MammalEnum), animalObject);
            int category = int.Parse(animalSpecificationsUI.Specifications.GetSpecificationForCategory());
            string animalObj = animalSpecificationsUI.Specifications.GetSpecificationForAnimalObject();

            switch (birdObj)
            {
                case MammalEnum.Dog:
                    AddNewAnimalToRegister(
                        new Dog(name, age, string.Empty, gender, category, animalObj)
                        );
                    break;
                case MammalEnum.Dolphin:
                    AddNewAnimalToRegister(
                        new Dolphin(name, age, string.Empty, gender, category, animalObj)
                        );
                    break;
            }
        }
        /// <summary>
        /// Adds a new animal to the animal register.
        /// </summary>
        /// <param name="animal">The animal.</param>
        private void AddNewAnimalToRegister(Animal animal)
        {
            AnimalManager.GetInstance.AddAnimal(animal);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddNewAnimalButton"/> class.
        /// </summary>
        /// <param name="animalSpecificationsUI">The animal specifications UI.</param>
        public AddNewAnimalButton(AnimalSpecificationsUI animalSpecificationsUI) : base()
        {
            ButtonContent = buttonTitle;
            this.animalSpecificationsUI = animalSpecificationsUI;
            publishAnimalObject = string.Empty;
        }
    }
}
