/// <summary>
/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-03-07
/// </summary>
namespace ApusAnimalHotel.ViewModel
{
    /// <summary>
    /// Determines whether the specified string contains "y" of "n" only.
    /// </summary>
    class ValidateTextYNInputOutput
    {
        /// <summary>
        /// Determines whether the specified string has "y" of "n".
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns>
        ///   <c>true</c> if the specified string has "y" or "n"; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasYN(string str)
        {
            bool validate = false;

            if (str.Length == 1)
            {
               if (string.Equals(str, "y") || string.Equals(str, "n"))
                {
                    validate = true;
                }
            }
            return validate;
        }
    }
}
