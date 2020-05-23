using System.Text.RegularExpressions;
/// <summary>
/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-03-07
/// </summary>
namespace ApusAnimalHotel.ViewModel
{
    /// <summary>
    /// Determines whether the specified string contains only a non negative
    /// integer or zero.
    /// </summary>
    class ValidateTextIntNonNullInputOutput
    {
        /// <summary>
        /// Determines whether [is int not negative] [the specified string].
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns>
        ///   <c>true</c> if [is int not negative] [the specified string]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsIntNotNegative(string str)
        {
            int res;
            bool validInt;

            validInt = int.TryParse(str, out res);
            
            if (validInt && (res > -1))
            {
                return true;
            }
            return false;
        }
    }
}
