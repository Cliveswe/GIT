using ApusAnimalHotel.ViewModel.AnimalSepecsBlock.AnimalCategorySpecificationsBlock;
using ApusAnimalHotel.ViewModel.AnimalSepecsBlock.AnimalObjectSpecificationsBlock;
using ApusAnimalHotel.ViewModel.GroupListBoxIO;
using ApusAnimalHotel.ViewModel.LabelTextBoxIO;
/// <summary>
/// clive.leddy@gmail.com
/// Date 2019-03-07
/// </summary>
namespace ApusAnimalHotel.ViewModel.AnimalSpecificationsBoxIO
{
    /// <summary>
    /// This class sets the header for the group box. In addition it holds instances of
    /// error control classes as properties.
    /// </summary>
    /// <seealso cref="ApusAnimalHotel.ViewModel.GroupListBoxIO.Observer" />
    class AnimalSpecifications : Observer
    {
        #region Class properties
        /// <summary>
        /// Gets or sets the specifications header.
        /// </summary>
        /// <value>
        /// The specifications header.
        /// </value>
        public TextInputOutput SpecificationsHeader
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the specifications for category members.
        /// </summary>
        /// <value>
        /// The specifications for category.
        /// </value>
        public InputOutputCategoryOfAnimal SpecificationsForCategory
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the specifications for animal object members.
        /// </summary>
        /// <value>
        /// The specifications for animal object.
        /// </value>
        public InputOutputAnimalObject SpecificationsForAnimalObject
        {
            get;
            set;
        }
        #endregion

        #region Subscribe to Publisher
        private Subject subject;//the publisher
        private AnimalObjects animalSubject;//the publisher
        /// <summary>
        /// Update this class with data from the publisher (Subject)
        /// </summary>
        public void Update()
        {
            SetHeader(subject.GetState());
        }

        #endregion

        /// <summary>
        /// Sets the header.
        /// </summary>
        /// <param name="str">The string.</param>
        private void SetHeader(string str)
        {
            if (!string.IsNullOrWhiteSpace(str)) {
                SpecificationsHeader.TextIO = str + " " + 
                    ContentEnumToText.GetContentText(ContentTextEnum.Specifications).ToLower();
            }
            else {
                SpecificationsHeader.TextIO = ContentEnumToText.GetContentText(ContentTextEnum.Specifications);
            }
            
            if(str == "-1")
            {
                SpecificationsHeader.TextIO = ContentEnumToText.GetContentText(ContentTextEnum.Specifications);
            }
        }

        /// <summary>
        /// Gets the specification for category.
        /// </summary>
        /// <returns></returns>
        public string GetSpecificationForCategory()
        {
            return SpecificationsForCategory.TextIO;
        }
        /// <summary>
        /// Gets the specification for animal object.
        /// </summary>
        /// <returns></returns>
        public string GetSpecificationForAnimalObject()
        {
            return SpecificationsForAnimalObject.TextIO;
        }
        /// <summary>
        /// Resets the user interface specifications.
        /// </summary>
        public void ResetSpecifications()
        {
            SetHeader(string.Empty);
            SpecificationsForCategory.ResetInputOutputCategoryOfAnimal();
            SpecificationsForAnimalObject.ResetInputOutputAnimalObject();
        }


       

        /// <summary>
        /// Initializes a new instance of the <see cref="AnimalSpecifications"/> class.
        /// </summary>
        /// <param name="animalCategori">The animal categories.</param>
        /// <param name="animalObjects">The animal objects.</param>
        public AnimalSpecifications(AnimalCategories animalCategories, AnimalObjects animalObjects)
        {
            subject = animalCategories;//publisher
            subject.Attach(this);//subscribe to publish
            animalSubject = animalObjects;
            animalSubject.Attach(this);
            SpecificationsHeader = new TextInputOutput(string.Empty);
            SetHeader(string.Empty);
            SpecificationsForCategory = new InputOutputCategoryOfAnimal(animalObjects, animalCategories);
            SpecificationsForAnimalObject = new InputOutputAnimalObject(animalObjects, animalCategories);
        }
    }
}
