using ApusAnimalHotel.Model;
using ApusAnimalHotel.ViewModel.MenuUI.File.SaveFile;
using Microsoft.Win32;
using System;
using System.Linq;

namespace ApusAnimalHotel.ViewModel.MenuUI.File
{
    class MenuFileSaveAs : MenuFileItems, ISaveFiles
    {
        private readonly string binaryExtension = "(*.bin) | *.bin";//save as binary files
        private readonly string textExtension = "(*.txt) | *.txt";//save as text files
        private string extensionFilter;//extension filter
        private string saveFileDialogTitle;//dialog box title

        public override bool MenuItemCanExecute(object parameter)
        {
            string test = (string)parameter;
            return true;// !AnimalManager.GetInstance.RegisterHasBeenSaved();
        }
        /// <summary>
        /// Command for saving content to a new binary file. 
        /// </summary>
        /// <param name="parameter">Object from calling source.</param>
        public override void MenuItemExecute(object parameter)
        {
            SaveFileDialog saveFileDialog;

            //binary save
            if ((string)parameter == ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.Binary_File))
            {
                extensionFilter = ExtensionFilter(ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.Binary_File),
                    binaryExtension);
                saveFileDialogTitle = ExtensionSaveFileTitle(ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.Animal_Register),
                    ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.Binary_File).ToLower());

            }//text open
            else if ((string)parameter == ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.Text_File))
            {
                extensionFilter = ExtensionFilter(ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.Text_File),
                    textExtension);
                saveFileDialogTitle = ExtensionSaveFileTitle(ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.Animal_Register),
                    ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.Menu_register).ToLower());
            }

            saveFileDialog = CreateSaveFileDialogBox(extensionFilter, saveFileDialogTitle);

            ShowDialogBox(saveFileDialog, parameter);
        }

        /// <summary>
        /// Launch the dialog box and perform validation on returning input.
        /// </summary>
        /// <param name="saveFileDialog">Save file dialog box.</param>
        /// <param name="parameter">parameters from WPF as object</param>
        private void ShowDialogBox(SaveFileDialog saveFileDialog, object parameter)
        {
            if (saveFileDialog.ShowDialog() == true)//show the UI save as dialog box
            {
                if (CheckFileName(saveFileDialog.FileName, (string)parameter))//validate the extension
                {
                    Save(saveFileDialog.FileName, (string)parameter);//save a valid file name
                }
                else
                {
                    ExtensionNotValid(saveFileDialog.FileName);//extension is not valid
                }
            }

        }
        /// <summary>
        /// UI feedback when validating file name extension.
        /// </summary>
        /// <param name="fileName">Path and file name</param>
        private void ExtensionNotValid(string fileName)
        {
            string givenExtension = System.IO.Path.GetExtension(fileName);//get the files extension

            givenExtension = "." + givenExtension.Split('.').Last();//create a message to use in the UI

            AppOkMessageBox.AppOKMessageBox(givenExtension + " is not a valid extension!",
                "Extension validation");
        }
        /// <summary>
        /// Check the file name against valid extension permitted in this app.
        /// </summary>
        /// <param name="fileName">Path and file name</param>
        /// <param name="extension">Valid extension</param>
        /// <returns>>True if the file name has a valid extension, otherwise false.</returns>
        private bool CheckFileName(string fileName, string extension)
        {
            if (extension == ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.Binary_File))
            {
                return TestForFileExtencion(fileName, binaryExtension);
            }
            else if (extension == ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.Text_File))
            {
                return TestForFileExtencion(fileName, textExtension);
            }
            return false;
        }
        /// <summary>
        /// A test to compare the file names extension with a valid extension.
        /// </summary>
        /// <param name="fileName">Path and file name</param>
        /// <param name="fileExtencion">Test extension</param>
        /// <returns>True if the file name has a valid extension, otherwise false.</returns>
        public bool TestForFileExtencion(string fileName, string fileExtencion)
        {
            string exten = System.IO.Path.GetExtension(fileName);//file extension

            if (exten == "." + fileExtencion.Split('.').Last())//test if the file extension is valid
            {
                return true;
            }
            return false;

        }
        /// <summary>
        /// Instantiate a new save file dialog box.
        /// </summary>
        /// <param name="extensionFilter">File extensions</param>
        /// <param name="saveFileDialogTitle">Dialog box title</param>
        /// <returns>Save as dialog box</returns>
        private SaveFileDialog CreateSaveFileDialogBox(string extensionFilter, string saveFileDialogTitle)
        {
            //new dialog box
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            //set the extension filter
            saveFileDialog.Filter = extensionFilter;
            saveFileDialog.AddExtension = true;
            //set the dialog box title
            saveFileDialog.Title = saveFileDialogTitle;
            //start path decided by environment variable
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

            return saveFileDialog;
        }
        /// <summary>
        /// Instruct the register to save content to a specific path and file.
        /// </summary>
        /// <param name="filePath">Path and file name as string.</param>
        public void Save(string filePath, string extension)
        {
            try
            {
                if (extension == ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.Binary_File))
                {
                    AnimalManager.GetInstance.SaveFileAsBinary(@filePath);
                }
                else if ((extension == ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.Text_File)) ||
                   (extension == ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.XML)))
                {
                    AnimalManager.GetInstance.SaveFileAsText(@filePath);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Could not save to " + extension.ToLower() + ".", ex);
            }
        }

        #region Dialog box Title and Extension as text
        /// <summary>
        /// Create an extension filter containing a name and extension types.
        /// </summary>
        /// <param name="name">Ex. type of file to be saved as text.</param>
        /// <param name="extention">A list of extensions as a string.</param>
        /// <returns></returns>
        public string ExtensionFilter(string name, string extention)
        {
            if ((name != null) && (extention != null))
            {
                return name + " " + extention;
            }
            return "Incorrect extension filter!";
        }
        /// <summary>
        /// Create an title base on the file extension.
        /// </summary>
        /// <param name="title">The name of the register to be saved.</param>
        /// <param name="extention">The file extension.</param>
        /// <returns></returns>
        public string ExtensionSaveFileTitle(string title, string extention)
        {
            if ((title != null) && (extention != null))
            {
                return "Save the content of " + title + " as a " + extention + ".";
            }
            return "Incorrect extension filter for title!";
        }
        #endregion

        /// <summary>
        /// Create a tooltip use to give instruction via the UI.
        /// </summary>
        /// <param name="save">The action to be performed.</param>
        /// <param name="extension">The file's extension type.</param>
        public void ToolTip(string save, string extension)
        {
            Tooltip = "Use \"" +
                save + " - " +
                extension +
                "\" to save modified content to a " + extension.ToLower() + ".";
        }

        public MenuFileSaveAs(string save, string extension) : base()
        {
            MenuItemContent = extension;
            ToolTip(save, extension);
        }
    }
}
