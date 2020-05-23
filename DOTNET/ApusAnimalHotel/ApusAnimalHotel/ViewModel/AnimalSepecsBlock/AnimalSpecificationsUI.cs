using AnimalMotel.Model.Animals;
using AnimalMotel.Model.Animals.Birds;
using AnimalMotel.Model.Animals.Insects;
using AnimalMotel.Model.Animals.Mammals;
using ApusAnimalHotel.Model;
using ApusAnimalHotel.ViewModel.AnimalImagesLoader;
using ApusAnimalHotel.ViewModel.AnimalSpecificationsBoxIO;
using ApusAnimalHotel.ViewModel.ButtonControls;
using ApusAnimalHotel.ViewModel.GroupListBoxIO;
using ApusAnimalHotel.ViewModel.ListRegisterAnimalsCheckBox;
using System;
using System.Globalization;
using System.Linq;
/// <summary>
/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-03-05
/// </summary>
namespace ApusAnimalHotel.ViewModel.AnimalSepecsBlock
{
    /// <summary>
    /// This class is the controlling class for animal specifications.
    /// </summary>
    /// <seealso cref="ApusAnimalHotel.ViewModel.GroupListBoxIO.Observer" />
    class AnimalSpecificationsUI : Observer
    {
        #region Class UI properties
        /// <summary>
        /// Gets the animal specifications block header.
        /// </summary>
        /// <value>
        /// The animal specifications block header.
        /// </value>
        public string AnimalSpecificationsBlockHeader
        {
            get;
            private set;
        }

        /// <summary>
        /// Hold an instance of a object that manipulates the name of an 
        /// animal used in the UI.
        /// </summary>
        /// <value>
        /// The name of animal UI.
        /// </value>
        public InputOutputAnimalName NameOfAnimalUI
        {
            get;
            set;
        }

        /// <summary>
        /// Hold an instance of a object that manipulates the age of an 
        /// animal used in the UI.
        /// </summary>
        /// <value>
        /// The age of animal UI.
        /// </value>
        public InputOutputAnimalAge AgeOfAnimalUI
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        /// <value>
        /// The gender.
        /// </value>
        public SelectGender Gender
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the categories.
        /// </summary>
        /// <value>
        /// The categories.
        /// </value>
        public AnimalCategories Categories
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the animal species.
        /// </summary>
        /// <value>
        /// The animal species.
        /// </value>
        public AnimalObjects AnimalSpecies
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the animal specifications.
        /// </summary>
        /// <value>
        /// The specifications.
        /// </value>
        public AnimalSpecifications Specifications
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the add new animal.
        /// </summary>
        /// <value>
        /// The add new animal.
        /// </value>
        public AddNewAnimalButton AddNewAnimal
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the animal image.
        /// </summary>
        /// <value>
        /// The animal image.
        /// </value>
        public AnimalImagesUI AnimalImage
        {
            get {
                return _animalImage;
            }
            set {
                _animalImage = value;
            }
        }
        private AnimalImagesUI _animalImage;

        /// <summary>
        /// Gets or sets the list animals.
        /// </summary>
        /// <value>
        /// The list animals.
        /// </value>
        public ListAllAnimals ListAnimals
        {
            get {
                return _listAnimals;
            }
            set {
                _listAnimals = value;
            }
        }
        private ListAllAnimals _listAnimals;
        #endregion

        #region Subscriber        
        /// <summary>
        /// Updates this instance.
        /// </summary>
        public void Update()
        {
            AnimalSubscription obj = ListAnimals.GetState();
            string addobj = AddNewAnimal.GetStateOfAnimalButton();
            //ResetAnimalSpecificationsUI();
        }
        #endregion

        /// <summary>
        /// Update the UI for a specific animal object.
        /// </summary>
        /// <param name="animalId">The id of the animal</param>
        public void UpdateAnimalSpecifications(string animalId)
        {
            Animal animal;
            int count;

            count = AnimalManager.GetInstance.NumberOfAnimalResidents;

            if ((count > 0) && (!string.IsNullOrEmpty(animalId)))
            {
                animal = GetAnimal(animalId, count);
                UpdateUI(animal);
            }
            else
            {
                //ResetAnimalSpecificationsUI();
            }
        }

        /// <summary>
        /// Updates the UI.
        /// </summary>
        /// <param name="animal">The animal.</param>
        private void UpdateUI(Animal animal)
        {
            NameOfAnimalUI.SetAnimalName(animal.Name);
            AgeOfAnimalUI.SetAnimalAge(animal.Age.ToString(CultureInfo.InvariantCulture));
            Gender.SetGender(animal.Gender);
            Categories.SetCategory(animal.GetSpeciesType());
            //AnimalSpecies.SetAnimalObjectContent(animal.GetSpeciesType());
            if (!ListAnimals.GetState().IsActive)
            {
                AnimalSpecies.SetAnimalObjectAsSelected(animal.GetSpeciesType());
            }
            SetSpecifications(animal);
        }

        /// <summary>
        /// Sets the category.
        /// </summary>
        /// <param name="animalSpecies">The animal species.</param>
        private void SetSpecifications(Animal animal)
        {
            string animalSpecies = string.Empty;

            animalSpecies = animal.GetSpeciesType();

            if (Enum.GetNames(typeof(BirdEnum)).Contains(animalSpecies))
            {
                SetBirdSpecifications(animal);
            }
            if (Enum.GetNames(typeof(InsectEnum)).Contains(animalSpecies))
            {
                SetInsectSpecifications(animal);
            }
            if (Enum.GetNames(typeof(MammalEnum)).Contains(animalSpecies))
            {
                SetMammalSpecifications(animal);
            }

        }
        /// <summary>
        /// Sets the bird specifications.
        /// </summary>
        /// <param name="animal">The animal.</param>
        private void SetBirdSpecifications(Animal animal)
        {
            string category = string.Empty;
            double animalObj = 0;

            switch (animal.GetSpeciesType())
            {
                case nameof(BirdEnum.Ostrich):
                    Ostrich o = (Ostrich)animal;
                    category = o.Species;
                    animalObj = o.WingSpan;
                    break;
                case nameof(BirdEnum.Penguin):
                    Penguin p = (Penguin)animal;
                    category = p.Species;
                    animalObj = p.WingSpan;
                    break;

            }

            Specifications.SpecificationsForCategory.SetTextIO(category);
            Specifications.SpecificationsForAnimalObject.SetTextIO(animalObj);
        }

        /// <summary>
        /// Sets the mammal specifications.
        /// </summary>
        /// <param name="animal">The animal.</param>
        private void SetMammalSpecifications(Animal animal)
        {
            int category = 0;
            string animalObj = string.Empty;

            switch (animal.GetSpeciesType())
            {
                case nameof(MammalEnum.Dog):
                    Dog d = (Dog)animal;
                    category = d.NumberOfTeeth;
                    animalObj = d.Breed;
                    break;
                case nameof(MammalEnum.Dolphin):
                    Dolphin p = (Dolphin)animal;
                    category = p.NumberOfTeeth;
                    animalObj = p.Species;
                    break;

            }

            Specifications.SpecificationsForCategory.SetTextIO(category);
            Specifications.SpecificationsForAnimalObject.SetTextIO(animalObj);
        }

        /// <summary>
        /// Sets the insect specifications.
        /// </summary>
        /// <param name="animal">The animal.</param>
        private void SetInsectSpecifications(Animal animal)
        {
            bool category = false;
            bool animalObj = false;

            switch (animal.GetSpeciesType())
            {
                case nameof(InsectEnum.Bee):
                    Bee b = (Bee)animal;
                    category = b.IsPoisonous;
                    animalObj = b.LarvalPhase;
                    break;
                case nameof(InsectEnum.Fly):
                    Fly f = (Fly)animal;
                    category = f.IsPoisonous;
                    animalObj = f.LarvalPhase;
                    break;

            }

            Specifications.SpecificationsForCategory.SetTextIO(category);
            Specifications.SpecificationsForAnimalObject.SetTextIO(animalObj);
        }

        /// <summary>
        /// Get a copy of the animal from the register.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        private Animal GetAnimal(string id, int count)
        {
            Animal animal = null;
            for (int index = 0; index < count; index++)
            {
                animal = AnimalManager.GetInstance.GetAnimalAt(index);
                if (animal.AnimalID == id)//found animal
                {
                    break;
                }
            }
            return animal;
        }

        /// <summary>
        /// Sets the animal specifications header.
        /// </summary>
        private void SetAnimalSpecsHeader()
        {
            AnimalSpecificationsBlockHeader = ContentEnumToText.GetContentText(ContentTextEnum.Animal_specifications);
        }

        /// <summary>
        /// Resets the animal specifications UI.
        /// </summary>
        public void ResetAnimalSpecificationsUI()
        {
            NameOfAnimalUI.AnimalName.ClearTextIO();
            AgeOfAnimalUI.AnimalAge.ClearTextIO();
            Gender.ResetIsSelected();
            Categories.ResetIsSelected();
            AnimalSpecies.ResetAnimalObjects();
            Specifications.ResetSpecifications();
            ListAnimals.ResetCheckbox();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AnimalSpecificationsUI"/> class.
        /// </summary>
        public AnimalSpecificationsUI()
        {
            ListAnimals = new ListAllAnimals();
            ListAnimals.Attach(this);//subscribe to List animals checkbox
            SetAnimalSpecsHeader();
            NameOfAnimalUI = new InputOutputAnimalName();
            AgeOfAnimalUI = new InputOutputAnimalAge();
            Gender = new SelectGender();
            Categories = new AnimalCategories(ListAnimals);
            AnimalSpecies = new AnimalObjects(Categories, ListAnimals);//subscribe to categories
            Categories.SetAnimalSpeciesListener(AnimalSpecies);//listen to AnimalSpecies
            Specifications = new AnimalSpecifications(Categories, AnimalSpecies);//subscribe to categories and animal species
            AddNewAnimal = new AddNewAnimalButton(this);

            AnimalImage = new AnimalImagesUI();
        }
    }
}