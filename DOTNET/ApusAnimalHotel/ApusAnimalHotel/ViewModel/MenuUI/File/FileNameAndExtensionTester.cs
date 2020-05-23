using System.Linq;

namespace ApusAnimalHotel.ViewModel.MenuUI.File
{
    public static class FileNameAndExtensionTester
    {

        /// <summary>
        /// A test to compare the file names extension with a valid extension.
        /// </summary>
        /// <param name="fileName">Path and file name</param>
        /// <param name="fileExtencion">Test extension</param>
        /// <returns>True if the file name has a valid extension, otherwise false.</returns>
        public static bool TestForFileExtension(string fileName, string fileExtension)
        {
            string exten = System.IO.Path.GetExtension(fileName);//file extension

            if (exten == "." + fileExtension.Split('.').Last())//test if the file extension is valid
            {
                return true;
            }
            return false;

        }

        /// <summary>
        /// UI feedback when validating file name extension.
        /// </summary>
        /// <param name="fileName">Path and file name</param>
        public static void ExtensionNotValid(string fileName)
        {
            string givenExtension = System.IO.Path.GetExtension(fileName);//get the files extension
            givenExtension = "." + givenExtension.Split('.').Last();//create a message to use in the UI

            AppOkMessageBox.AppOKMessageBox(givenExtension + " is not a valid extension!",
                "Extension validation");
        }
    }
}
