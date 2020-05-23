/// <summary>
/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-03-05
/// </summary>
namespace ApusAnimalHotel.ViewModel.AnimalSepecsBlock
{
    /// <summary>
    /// A class that holds an instance of an animals age.
    /// </summary>
    class InputOutputAnimalAge
    {
        public DoubleInputOutput AnimalAge
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the age of the animal.
        /// </summary>
        /// <returns></returns>
        public string GetAnimalAge()
        {
            return AnimalAge.TextIO;
        }

        /// <summary>
        /// Sets the animal age.
        /// </summary>
        /// <param name="age">The age.</param>
        public void SetAnimalAge(string age)
        {
            AnimalAge.TextIO = age;
        }
        public InputOutputAnimalAge()
        {
            AnimalAge = new DoubleInputOutput(ContentEnumToText.GetContentText(ContentTextEnum.Age));
            AnimalAge.SetErrorMessage("Age must be a positive number.");
        }
    }
}
