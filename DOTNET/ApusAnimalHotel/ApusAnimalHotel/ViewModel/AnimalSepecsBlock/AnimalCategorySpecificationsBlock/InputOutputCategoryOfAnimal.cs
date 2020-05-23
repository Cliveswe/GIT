using ApusAnimalHotel.ViewModel.GroupListBoxIO;
using ApusAnimalHotel.ViewModel.LabelTextBoxIO;
using System;
using System.Collections.Generic;
using System.Globalization;
/// <summary>
/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-03-07
/// </summary>
namespace ApusAnimalHotel.ViewModel.AnimalSepecsBlock.AnimalCategorySpecificationsBlock
{
    /// <summary>
    /// This class performs error control for animal categories input output. 
    /// </summary>
    /// <seealso cref="ApusAnimalHotel.ViewModel.LabelTextBoxIO.TextInputOutput" />
    /// <seealso cref="ApusAnimalHotel.ViewModel.GroupListBoxIO.Observer" />
    class InputOutputCategoryOfAnimal : TextInputOutput, Observer
    {
        private readonly string defaultErrorMessage = "First select from category.";

        #region Subscribe to Publisher
        private Subject subject;
        private Subject subjectAnimalObj;
        /// <summary>
        /// Updates this instance with data from the publisher.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void Update()
        {
            //string str = TextIO;//save any input text

            SetCategoryDefault();
            SetCategoryData(subject.GetState());
            
            //TextIO = str;//restore input text
        }
        #endregion

        #region Dictionary of Categories for label        
        /// <summary>
        /// Gets the animal categories dictionary.
        /// </summary>
        /// <value>
        /// The animal categories dictionary.
        /// </value>
        public Dictionary<AnimalCategoryEnum, string> AnimalCategoriesDictionary
        {
            get {
                return _animalCategoriesDictionary;
            }
            private set {
                _animalCategoriesDictionary = value;
            }
        }
        private Dictionary<AnimalCategoryEnum, string> _animalCategoriesDictionary;
        /// <summary>
        /// Initializes a dictionary of animal properties by category.
        /// </summary>
        private void InitializeCategoryDictionary()
        {
            AnimalCategoriesDictionary = new Dictionary<AnimalCategoryEnum, string>()
            {
                {AnimalCategoryEnum.Bird, "Species" },
                {AnimalCategoryEnum.Insect, "Poisonous (y/n)" },
                {AnimalCategoryEnum.Mammal, "Nr. of teeth" }
            };
        }
        #endregion

        #region Dictionary of Error messages         
        /// <summary>
        /// Gets the error category dictionary.
        /// </summary>
        /// <value>
        /// The error category dictionary.
        /// </value>
        public Dictionary<AnimalCategoryEnum, string> ErrorCategoryDictionary
        {
            get {
                return _errorCategoryDictionary;
            }
            private set {
                _errorCategoryDictionary = value;
            }
        }
        private Dictionary<AnimalCategoryEnum, string> _errorCategoryDictionary;
        /// <summary>
        /// Initializes the error message dictionary.
        /// </summary>
        private void InitializeErrorMessageDictionary()
        {
            ErrorCategoryDictionary = new Dictionary<AnimalCategoryEnum, string>()
            {
                {AnimalCategoryEnum.Bird, "What is the species of the " +
                AnimalCategoryEnum.Bird.ToString().ToLower() + "."},
                {AnimalCategoryEnum.Insect, "Is the " +
                AnimalCategoryEnum.Insect.ToString().ToLower() + " poisonous (y/n)?" },
                {AnimalCategoryEnum.Mammal, "How many teeth does the " +
                AnimalCategoryEnum.Mammal.ToString().ToLower() + " have?"}
            };
        }
        #endregion

        #region Validate input output
        private string validationKey;//a key used to validate data        
        /// <summary>
        /// Validates the input. Can be overridden.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <returns>true if valid otherwise false</returns>
        public override bool ValidateInput(string msg)
        {
            bool checkInput = true;

            if (!string.IsNullOrWhiteSpace(validationKey))
            {
                if ((AnimalCategoryEnum)Enum.Parse(typeof(AnimalCategoryEnum), validationKey) == AnimalCategoryEnum.Bird)
                {
                    checkInput = ValidBird(msg);
                }
                if ((AnimalCategoryEnum)Enum.Parse(typeof(AnimalCategoryEnum), validationKey) == AnimalCategoryEnum.Insect)
                {
                    checkInput = ValidInsect(msg);
                }
                if ((AnimalCategoryEnum)Enum.Parse(typeof(AnimalCategoryEnum), validationKey) == AnimalCategoryEnum.Mammal)
                {
                    checkInput = ValidMammal(msg);
                }
            }
            else
            {
                SetErrorMessage(defaultErrorMessage);
            }

            IsValid = checkInput;
            return checkInput;
        }
        /// <summary>
        /// Validation of category bird.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <returns></returns>
        private bool ValidBird(string msg)
        {
            bool validate = true;

            if (!string.IsNullOrWhiteSpace(msg) &&
                ValidateTextInputOutput.IsAlphabet(msg))
            {
                validate = false;
            }
            return validate;
        }

        /// <summary>
        /// Validation of category insect.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <returns></returns>
        private bool ValidInsect(string msg)
        {
            bool validate = true;
            if (!string.IsNullOrWhiteSpace(msg) &&
                ValidateTextYNInputOutput.HasYN(msg))
            {
                validate = false;
            }
            return validate;
        }
        /// <summary>
        /// Validation of category mammal.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <returns></returns>
        private bool ValidMammal(string msg)
        {
            bool validate = true;

            if (!string.IsNullOrEmpty(msg) &&
                ValidateTextIntNonNullInputOutput.IsIntNotNegative(msg))
            {
                validate = false;
            }
            return validate;
        }
        #endregion

        private void SetCategoryDefault()
        {
            SetErrorMessage(defaultErrorMessage);
            SetLabelTextIO(string.Empty);
        }
        /// <summary>
        /// Sets a label name by type of category.
        /// </summary>
        /// <param name="str">The string.</param>
        private void SetCategoryData(string str)
        {
            validationKey = str;

            foreach (KeyValuePair<AnimalCategoryEnum, string> itemPare in AnimalCategoriesDictionary)
            {
                if (Enum.Parse(typeof(AnimalCategoryEnum), str).Equals(itemPare.Key))
                {
                    UpdateErrorMessage(itemPare.Key);//this statement must be first
                    SetLabelTextIO(itemPare.Value);
                    break;
                }
            }
        }

        /// <summary>
        /// Updates the error message.
        /// </summary>
        /// <param name="key">The key.</param>
        private void UpdateErrorMessage(AnimalCategoryEnum key)
        {
            string errorMsg;
            if (ErrorCategoryDictionary.TryGetValue(key, out errorMsg))
            {
                SetErrorMessage(errorMsg);
            }
        }

        /// <summary>
        /// Sets the label.
        /// </summary>
        /// <param name="labelTitle">The label title.</param>
        private void SetLabelTextIO(string labelTitle)
        {
            Label = labelTitle;
            ClearTextIO();
        }

        /// <summary>
        /// Sets the text.
        /// </summary>
        /// <param name="str">The string.</param>
        public void SetTextIO(string str)
        {
            TextIO = str;
        }

        /// <summary>
        /// Sets the text.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        public void SetTextIO(bool value)
        {
            SetTextIO(value ? "y" : "n");
        }

        /// <summary>
        /// Sets the text.
        /// </summary>
        /// <param name="number">The number.</param>
        public void SetTextIO(int number)
        {
            string str = number.ToString(CultureInfo.InvariantCulture);
            SetTextIO(str);
        }

        /// <summary>
        /// Initializes the dictionary.
        /// </summary>
        private void InitializeDictionary()
        {
            InitializeCategoryDictionary();
            InitializeErrorMessageDictionary();
        }

        /// <summary>
        /// Resets the input output category of animal.
        /// </summary>
        public void ResetInputOutputCategoryOfAnimal()
        {
            SetErrorMessage(defaultErrorMessage);
            SetLabelTextIO(string.Empty);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InputOutputCategoryOfAnimal"/> class.
        /// </summary>
        /// <param name="animalCategories">The animal categories.</param>
        public InputOutputCategoryOfAnimal(AnimalObjects animalObjects, AnimalCategories animalCategories) : base(string.Empty)
        {
            //subscribe to publisher
            subject = animalCategories;
            subject.Attach(this);
            subjectAnimalObj = animalObjects;
            animalObjects.Attach(this);
            //validation key has not been set
            validationKey = string.Empty;

            //initialize a dictionary of animal category properties
            InitializeDictionary();
        }
    }
}
