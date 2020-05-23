using Microsoft.Win32;
using System;
///<summary>
/// Created by Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-02-06
/// </summary>
namespace InvoiceMaker.ViewModel.Util
{
    class OpenFileDialogBox
    {

        /// <summary>
        /// Instantiate a new open file dialog box.
        /// </summary>
        /// <param name="extensionFilter">File extensions</param>
        /// <param name="saveFileDialogTitle">Dialog box title</param>
        /// <returns>File name of a selected file.</returns>
        public static OpenFileDialog OpenileDialogBox(string extensionFilter, string saveFileDialogTitle)
        {
            //new dialog box
            OpenFileDialog openFileDialog = new OpenFileDialog();

            //set the extension filter
            openFileDialog.Filter = extensionFilter;
            openFileDialog.AddExtension = true;
            //set the dialog box title
            openFileDialog.Title = saveFileDialogTitle;
            //start path decided by environment variable
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

            return openFileDialog;
        }
    }
}
