/// <summary>
/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-03-05
/// </summary>
namespace ApusAnimalHotel.ViewModel.LabelTextBoxIO
{
    /// <summary>
    /// This class extends the base class TextInputOutput to add methods for converting
    /// string to double and double to string.
    /// </summary>
    /// <seealso cref="ApusAnimalHotel.ViewModel.LabelTextBoxIO.TextInputOutput" />
    public abstract class TextInputOutputDecoratorDouble : TextInputOutput
    {
        /// <summary>
        /// Text to double.
        /// </summary>
        /// <returns>double</returns>
        public abstract double TextIOToDouble();
        /// <summary>
        /// Double to text.
        /// </summary>
        /// <returns>string</returns>
        public abstract void DoubleToTextIO(double number);

        /// <summary>
        /// Initializes a new instance of the <see cref="TextInputOutputDecoratorDouble"/> class.
        /// </summary>
        public TextInputOutputDecoratorDouble(string label): base(label)
        {

        }
    }
}
