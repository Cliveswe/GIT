using ApusAnimalHotel.ViewModel.LabelTextBoxIO;
using System.Globalization;
/// <summary>
/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-03-05
/// </summary>
namespace ApusAnimalHotel.ViewModel.AnimalSepecsBlock
{
    /// <summary>
    /// This class extends the functions of the class TextInputOutput. This overrides
    /// the validation of TextInputOutput with logic valid to this classes use.
    /// </summary>
    public class DoubleInputOutput : TextInputOutputDecoratorDouble
    {
        /// <summary>
        /// Double to text.
        /// </summary>
        /// <param name="number"></param>
        public override void DoubleToTextIO(double number)
        {
            TextIO = number.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Text to double.
        /// </summary>
        /// <returns>
        /// double
        /// </returns>
        public override double TextIOToDouble()
        {
            return double.Parse(TextIO, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Validates the input. Can be overridden.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <returns></returns>
        public override bool ValidateInput(string msg)
        {
            bool checkInput = true;

            if (!string.IsNullOrWhiteSpace(msg))
            {
                checkInput = ValidateTextDoubleInputOutput.CheckInput(msg);
            }

            IsValid = checkInput;
            return checkInput;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DoubleInputOutput"/> class.
        /// </summary>
        public DoubleInputOutput(string label) : base(label)
        {
        }
    }
}