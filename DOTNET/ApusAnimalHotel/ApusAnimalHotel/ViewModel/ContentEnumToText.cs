using System;

/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-02-06
/// 
/// Convert Enum values string.
namespace ApusAnimalHotel.ViewModel
{
    class ContentEnumToText
    {
        /// <summary>
        /// Convert an enum to a string and replacing all underscore symbol with a blank space.
        /// </summary>
        /// <param name="contentTextEnum">The ContentTextEnum enum.</param>
        /// <returns>A converted enum value to string.</returns>
        internal static string GetContentText(ContentTextEnum contentTextEnum)
        {
            string contentText = "something went wrong!";
            foreach (ContentTextEnum iContentTextEnum in Enum.GetValues(typeof(ContentTextEnum)))
            {
                if (iContentTextEnum == contentTextEnum)
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
