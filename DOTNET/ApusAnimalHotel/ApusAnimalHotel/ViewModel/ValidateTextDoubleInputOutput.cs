using System.Globalization;
/// <summary>
/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-03-07
/// </summary>
namespace ApusAnimalHotel.ViewModel
{
    /// <summary>
    /// Validate if a string can be parsed to double and is greater than zero.
    /// </summary>
    public class ValidateTextDoubleInputOutput
    {
        /// <summary>
        /// Checks the input.
        /// </summary>
        /// <param name="msg">The Message.</param>
        /// <returns>True if msg could be parsed to double, otherwise false.</returns>
        public static bool CheckInput(string msg)
        {
            double resDouble;
            bool valid;

            valid = double.TryParse(msg, NumberStyles.Any, CultureInfo.InvariantCulture, out resDouble);
            if (valid && (resDouble > 0))
            {
                return false;
            }
            return true;
        }
    }
}
