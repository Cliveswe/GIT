using System;

/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-05-06
/// 
/// Convert Enum values string.
namespace ApusAnimalHotel.ViewModel.MenuUI
{
    class ContentOfEnumToText<T>
    {
        /// <summary>
        /// Convert an enum to a string and replacing all underscore symbol with a blank space.
        /// </summary>
        /// <param name="contentTextEnum">The ContentTextEnum enum.</param>
        /// <returns>A converted enum value to string.</returns>
        internal static string GetContentText(T contentTextEnum)
        {
            string contentText = "something went wrong!";
            foreach (T iContentTextEnum in Enum.GetValues(typeof(T)))
            {
                if (iContentTextEnum.Equals(contentTextEnum))
                {
                    contentText = iContentTextEnum.ToString();//convert enum to string
                    contentText = contentText.Replace("_", " ");//replace underscore with a blank space
                    break;
                }
            }
            return contentText;
        }
    }
}
