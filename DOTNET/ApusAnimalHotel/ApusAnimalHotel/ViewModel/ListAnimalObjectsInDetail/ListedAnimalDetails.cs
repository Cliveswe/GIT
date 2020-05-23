using AnimalMotel.Model.Animals;
using AnimalMotel.Model.Animals.Birds;
using AnimalMotel.Model.Animals.Insects;
using AnimalMotel.Model.Animals.Mammals;
using ApusAnimalHotel.ViewModel.AnimalCriteria;
using System.Globalization;
/// <summary>
/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-03-05
/// </summary>
namespace ApusAnimalHotel.ViewModel.ListAnimaObjectsInDetail
{
    class ListedAnimalDetails
    {
        #region class properties
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string ID
        {
            get {
                return _id;
            }
            set {
                _id = value;
            }
        }
        private string _id;
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get {
                return _name;
            }
            set {
                _name = value;
            }
        }
        private string _name;
        /// <summary>
        /// Gets or sets the age.
        /// </summary>
        /// <value>
        /// The age.
        /// </value>
        public string Age
        {
            get {
                return _age;
            }
            set {
                _age = value;
            }
        }
        private string _age;
        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        /// <value>
        /// The gender.
        /// </value>
        public string Gender
        {
            get {
                return _gender;
            }
            set {
                _gender = value;
            }
        }
        private string _gender;
        /// <summary>
        /// Gets or sets the special characteristics.
        /// </summary>
        /// <value>
        /// The special characteristics.
        /// </value>
        public string SpecialCharacteristics
        {
            get {
                return _specialCharacteristics;
            }
            set {
                _specialCharacteristics = value;
            }
        }
        private string _specialCharacteristics;

        #endregion

        private Animal animal;
        /// <summary>
        /// Extracts the data from animal.
        /// </summary>
        private void ExtractDataFromAnimal()
        {
            //string t = string.Empty;
            ExtractAnimalData();

            if ((new CriteriaBird().CriteriaSuccess(animal)) == AnimalCategoryEnum.Bird)
            {
                ExtractBirdData();
            }
            if ((new CriteriaMammal().CriteriaSuccess(animal)) == AnimalCategoryEnum.Mammal)
            {
                ExtractMammalData();
            }
            if ((new CriteriaInsect().CriteriaSuccess(animal)) == AnimalCategoryEnum.Insect)
            {
                ExtractInsectData();
            }
        }
        /// <summary>
        /// Extracts the animal data.
        /// </summary>
        private void ExtractAnimalData()
        {
            this.ID = animal.AnimalID;
            this.Name = animal.Name;
            this.Age = animal.Age.ToString(CultureInfo.InvariantCulture);
            this.Gender = animal.Gender;
        }

        /// <summary>
        /// Extracts the bird data.
        /// </summary>
        private void ExtractBirdData()
        {
            string wingspan = string.Empty;
            string species = string.Empty;

            switch (animal.GetSpeciesType())
            {
                case nameof(BirdEnum.Ostrich):
                    Ostrich o = (Ostrich)animal;
                    wingspan = o.WingSpan.ToString(CultureInfo.InvariantCulture);
                    species = o.Species;
                    break;
                case nameof(BirdEnum.Penguin):
                    Penguin p = (Penguin)animal;
                    wingspan = p.WingSpan.ToString(CultureInfo.InvariantCulture);
                    species = p.Species;
                    break;
            }
            this.SpecialCharacteristics = "Species " + species + " wingspan (m) " + wingspan;
        }

        /// <summary>
        /// Extracts the insect data.
        /// </summary>
        private void ExtractInsectData()
        {
            bool poisonous = true;
            bool larval = true;

            switch (animal.GetSpeciesType())
            {
                case nameof(InsectEnum.Bee):
                    Bee b = (Bee)animal;
                    poisonous = b.IsPoisonous;
                    larval = b.LarvalPhase;
                    break;
                case nameof(InsectEnum.Fly):
                    Fly f = (Fly)animal;
                    poisonous = f.IsPoisonous;
                    larval = f.LarvalPhase;
                    break;
            }
            this.SpecialCharacteristics = "Is poisonous: " + 
                (poisonous ? "yes (y)" : "no (n)")
                + " is in larval state: " + 
                (larval ? "yes (y)" : "no (n)");
        }

        /// <summary>
        /// Extracts the mammal data.
        /// </summary>
        private void ExtractMammalData()
        {
            string teeth = string.Empty;
            string breed = string.Empty;
            string textBreed = string.Empty;

            switch (animal.GetSpeciesType())
            {
                case nameof(MammalEnum.Dog):
                    Dog dog = (Dog)animal;
                    teeth = dog.NumberOfTeeth.ToString(CultureInfo.InvariantCulture);
                    breed = dog.Breed;
                    textBreed = "breed";
                    break;
                case nameof(MammalEnum.Dolphin):
                    Dolphin dolphin = (Dolphin)animal;
                    teeth = dolphin.NumberOfTeeth.ToString(CultureInfo.InvariantCulture);
                    breed = dolphin.Species;
                    textBreed = "species";
                    break;
            }
            this.SpecialCharacteristics = "Nr. of teeth: " + teeth + " " + textBreed + ": " + breed;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListedAnimalDetails"/> class.
        /// </summary>
        /// <param name="animal">The animal.</param>
        public ListedAnimalDetails(Animal animal)
        {
            this.animal = animal;

            ExtractDataFromAnimal();
        }
    }
}
