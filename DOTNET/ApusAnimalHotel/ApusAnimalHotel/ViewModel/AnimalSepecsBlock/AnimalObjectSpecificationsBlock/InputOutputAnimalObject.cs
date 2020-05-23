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
namespace ApusAnimalHotel.ViewModel.AnimalSepecsBlock.AnimalObjectSpecificationsBlock
{
    /// <summary>
    /// This class performs error control for animal objects input output. 
    /// </summary>
    /// <seealso cref="ApusAnimalHotel.ViewModel.LabelTextBoxIO.TextInputOutput" />
    /// <seealso cref="ApusAnimalHotel.ViewModel.GroupListBoxIO.Observer" />
    class InputOutputAnimalObject : TextInputOutput, Observer
    {
        private readonly string defaultErrorMessage = "Select from animal object.";

        #region Subscribe to Publisher
        private Subject subjectAnimalObjects;//animal objects
        /// <summary>
        /// Updates this instance with data from the publisher.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void Update()
        {
            SetAnimalCategoryData();//subjectAnimalCategory.GetState());
            SetAnimalObjectData(subjectAnimalObjects.GetState());
            
        }

        private Subject subjectAnimalCategory;

        #endregion

        #region Dictionary of Categories for label        
        /// <summary>
        /// Gets the animal object dictionary. The key is a string. While the value 
        /// of the dictionary is a list of animal objects.
        /// </summary>
        /// <value>
        /// The animal object dictionary.
        /// </value>
        public Dictionary<string, List<string>> AnimalObjectDictionary
        {
            get {
                return _animalObjectDictionary;
            }
           private set {
                _animalObjectDictionary = value;
            }
        }
        private Dictionary<string, List<string>> _animalObjectDictionary;

        /// <summary>
        /// Initializes a dictionary of animal properties by category.
        /// </summary>
        private void InitializeCategoryDictionary()
        {
            AnimalObjectDictionary = new Dictionary<string, List<string>>()
            {
                {"Wingspan (m)", new List<string>(Enum.GetNames(typeof(BirdEnum))) },
                {"Is in larval phase (y/n)", new List<string>(Enum.GetNames(typeof(InsectEnum)))},
                {"Breed", new List<string>(Enum.GetNames(typeof(MammalEnum)))}
            };
        }
        #endregion

        #region Dictionary of Error messages         
        /// <summary>
        /// Gets the error category dictionary. The key of the dictionary is a list
        /// of identifiers as keys. While the value is a string.
        /// </summary>
        /// <value>
        /// The error category dictionary.
        /// </value>
        /// </summary>
        public Dictionary<List<string>, string> ErrorCategoryDictionary
        {
            get {
                return _errorCategoryDictionary;
            }
            private set {
                _errorCategoryDictionary = value;
            }
        }
        private Dictionary<List<string>, string> _errorCategoryDictionary;
        /// <summary>
        /// Initializes the error messages dictionary.
        /// </summary>
        private void InitializeErrorMessagesDictionary()
        {
            ErrorCategoryDictionary = new Dictionary<List<string>, string>()
            {
                {new List<string>(Enum.GetNames(typeof(BirdEnum))), " wingspan is recorded in meters."},
                {new List<string>(Enum.GetNames(typeof(InsectEnum))), " is in larval phase, \"y\" for yes or \"n\" for no.?"},
                {new List<string>(Enum.GetNames(typeof(MammalEnum))), " is of what breed."}
            };
        }

        #endregion

        #region Validation input output
        private string validationKey;//a key used to validate data
        /// <summary>
        /// Validates the input.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <returns>True if not valid otherwise false.</returns>
        public override bool ValidateInput(string msg)
        {
            bool checkValue = true;

            if (!string.IsNullOrWhiteSpace(validationKey))
            {
                if (Enum.IsDefined(typeof(BirdEnum), validationKey))
                {
                    checkValue = ValidateForBird(msg);
                }
                if (Enum.IsDefined(typeof(InsectEnum), validationKey))
                {
                    checkValue = ValidateForInsect(msg);
                }
                if (Enum.IsDefined(typeof(MammalEnum), validationKey))
                {
                    checkValue = ValidateForMammal(msg);
                }
            }
            else
            {
                SetDefaultErrorMessage();
            }
            IsValid = checkValue;
            return checkValue;
        }
        /// <summary>
        /// Validates for mammal.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <returns></returns>
        private bool ValidateForMammal(string msg)
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
        /// Validates for insect.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <returns></returns>
        private bool ValidateForInsect(string msg)
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
        /// Validates for bird.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <returns></returns>
        private bool ValidateForBird(string msg)
        {
            bool validate = true;
            if (!string.IsNullOrWhiteSpace(msg) &&
                !ValidateTextDoubleInputOutput.CheckInput(msg))
            {
                validate = false;
            }
            return validate;
        }
        #endregion

        /// <summary>
        /// When a animal category is selected clear this classes data.
        /// </summary>
        /// <param name="str">The string.</param>
        private void SetAnimalCategoryData()
        {
            SetDefaultErrorMessage();
            Label = string.Empty;
            ClearTextIO();
        }

        /// <summary>
        /// Sets the default error message.
        /// </summary>
        private void SetDefaultErrorMessage()
        {
            SetErrorMessage(defaultErrorMessage);
        }

        /// <summary>
        /// Sets the animal object data.
        /// </summary>
        /// <param name="str">The string.</param>
        private void SetAnimalObjectData(string str)
        {
            validationKey = str;
            foreach (KeyValuePair<string, List<string>> itemPare in AnimalObjectDictionary)
            {
                if (itemPare.Value.Contains(str)){
                    UpdateErrorMessage(str);//this statement must be first
                    SetLabelTextIO(itemPare.Key);
                }
            }
        }

        /// <summary>
        /// Updates the error message.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        private void UpdateErrorMessage(string msg)
        {
            foreach (KeyValuePair<List<string>, string> itemPare in ErrorCategoryDictionary)
            {
                if (itemPare.Key.Contains(msg))
                {
                    SetErrorMessage(msg + itemPare.Value);
                }
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
        /// <param name="nr">The nr as double</param>
        public void SetTextIO(double nr)
        {
            string str = nr.ToString(CultureInfo.InvariantCulture);
            SetTextIO(str);
        }

        /// <summary>
        /// Sets the text io.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        public void SetTextIO(bool value)
        {
            SetTextIO(value ? "y" : "n");
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
        /// Resets the input output animal object.
        /// </summary>
        public void ResetInputOutputAnimalObject()
        {
            SetDefaultErrorMessage();
            SetLabelTextIO(string.Empty);
        }

        /// <summary>
        /// Initializes the dictionary.
        /// </summary>
        private void InitializeDictionary()
        {
            InitializeCategoryDictionary();
            InitializeErrorMessagesDictionary();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InputOutputAnimalObject"/> class.
        /// </summary>
        /// <param name="animalObjects">The animal objects.</param>
        public InputOutputAnimalObject(AnimalObjects animalObjects, AnimalCategories animalCategories) :base(string.Empty)
        {
            //subscribe to publishers
            subjectAnimalObjects = animalObjects;
            subjectAnimalObjects.Attach(this);
            subjectAnimalCategory = animalCategories;
            subjectAnimalCategory.Attach(this);

            InitializeDictionary();
            SetLabelTextIO(string.Empty);
        }

    }
}
