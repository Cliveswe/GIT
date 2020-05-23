using ApusAnimalHotel.ViewModel.LabelTextBoxIO;
/// <summary>
/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-03-05
/// </summary>
namespace ApusAnimalHotel.ViewModel.AnimalSepecsBlock
{/// <summary>
/// A class that holds an instance of an animals name.
/// </summary>
    public class InputOutputAnimalName
    {/// <summary>
     /// 
     /// </summary>
        public TextInputOutput AnimalName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the name of the animal.
        /// </summary>
        /// <returns></returns>
        public string GetAnimalName()
        {
            return AnimalName.TextIO;
        }

        /// <summary>
        /// Sets the name of the animal.
        /// </summary>
        /// <param name="name">The name.</param>
        public void SetAnimalName(string name)
        {
            AnimalName.TextIO = name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InputOutputAnimalName"/> class.
        /// </summary>
        public InputOutputAnimalName()
        {
            AnimalName = new TextInputOutput(ContentEnumToText.GetContentText(ContentTextEnum.Name));
            AnimalName.SetErrorMessage("Cannot have an empty name");
        }
    }
}