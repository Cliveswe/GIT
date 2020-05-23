using ApusAnimalHotel.ViewModel.FoodDetails;
using ApusAnimalHotel.ViewModel.MenuUI.File.SaveFile;
using Microsoft.Win32;
using System;
using System.Linq;

namespace ApusAnimalHotel.ViewModel.MenuUI.File.XML
{
    class XMLExportTo : MenuFileItems, ISaveFiles
    {
        private readonly string XMLExtension = "(*.xml) | *.xml";//save as text files
        private string extensionFilter;//extension filter
        private string ioFileDialogTitle;//dialog box title

        #region Main UI
        private MenuFile mainMenu;
        #endregion

        public override bool MenuItemCanExecute(object parameter)
        {
            return true;
        }

        public override void MenuItemExecute(object parameter)
        {
            SaveFileDialog saveFileDialog = null;
            OpenFileDialog openFileDialog = null;

            extensionFilter = ExtensionFilter(ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.XML),
                    XMLExtension);

            if ((string)parameter == ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.Export_to_XML_File))
            {
                ioFileDialogTitle = ExtensionSaveFileTitle(ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.Recipe_Register),
                    ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.Ingredients).ToLower());

                if (RecipeManager.GetInstance.Count > 0)
                {
                    saveFileDialog = CreateSaveFileDialogBox(extensionFilter, ioFileDialogTitle);
                    ShowDialogBox(saveFileDialog, parameter);
                }
                else
                {
                    AppOkMessageBox.AppOKMessageBox("The register contains no data to export.", "Recipe Manager");
                }
            }
            else if ((string)parameter == ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.Import_from_XML_File))
            {
                ioFileDialogTitle = ExtensionLoadFileTitle(ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.Recipe_Register),
                    ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.Ingredients).ToLower());

                openFileDialog = CreateLoadFileDialogBox(extensionFilter, ioFileDialogTitle);
                ShowDialogBox(openFileDialog, parameter);
                mainMenu.RefreshApp();
            }

        }
        /// <summary>
        /// Launch the dialog box and perform validation on returning input.
        /// </summary>
        /// <param name="openFileDialog">Load file dialog box.</param>
        /// <param name="parameter">parameters from WPF as object</param>
        private void ShowDialogBox(OpenFileDialog openFileDialog, object parameter)
        {
            if (openFileDialog.ShowDialog() == true)//show the UI save as dialog box
            {

                if (CheckFileName(openFileDialog.FileName, (string)parameter))//validate the extension
                {
                    Load(openFileDialog.FileName, (string)parameter);//save a valid file name
                }
                else
                {
                    ExtensionNotValid(openFileDialog.FileName);//extension is not valid
                }
            }

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
            if ((extension == ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.Export_to_XML_File)) ||
                (extension == ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.Import_from_XML_File)))
            {
                return TestForFileExtencion(fileName, XMLExtension);
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
        public void Save(string filePath, string extension)
        {
            RecipeManager.GetInstance.XMLExportTo(@filePath);
        }

        public void Load(string filePath, string extention)
        {
            RecipeManager.GetInstance.XMLImportFrom(@filePath);
        }
        /// <summary>
        /// Instantiate a new save file dialog box.
        /// </summary>
        /// <param name="extensionFilter">File extensions</param>
        /// <param name="openFileDialogTitle">Dialog box title</param>
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
        /// Instantiate a new load file dialog box.
        /// </summary>
        /// <param name="extensionFilter">File extensions</param>
        /// <param name="saveFileDialogTitle">Dialog box title</param>
        /// <returns>Save as dialog box</returns>
        private OpenFileDialog CreateLoadFileDialogBox(string extensionFilter, string openFileDialogTitle)
        {
            //new dialog box
            OpenFileDialog openFileDialog = new OpenFileDialog();

            //set the extension filter
            openFileDialog.Filter = extensionFilter;
            openFileDialog.AddExtension = true;
            //set the dialog box title
            openFileDialog.Title = openFileDialogTitle;
            //start path decided by environment variable
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

            return openFileDialog;
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
        /// <returns>A valid title as string or a text containing an error message.</returns>
        public string ExtensionSaveFileTitle(string title, string extention)
        {
            if ((title != null) && (extention != null))
            {
                return "Save the content of " + title + " as a " + extention + ".";
            }
            return "Incorrect extension filter for title!";
        }
        /// <summary>
        /// Create an title base on the file extension.
        /// </summary>
        /// <param name="title">The name of the register to be loaded.</param>
        /// <param name="extention">The file extension.</param>
        /// <returns>A valid title as string or a text containing an error message.</returns>
        public string ExtensionLoadFileTitle(string title, string extention)
        {
            if ((title != null) && (extention != null))
            {
                return "Load the content of " + title + " as a " + extention + ".";
            }
            return "Incorrect extension filter for title!";
        }
        #endregion

        /// <summary>
        /// Create a tooltip use to give instruction via the UI.
        /// </summary>
        /// <param name="end">The action to be performed.</param>
        /// <param name="extension">The file's extension type.</param>
        public void ToolTip(string end, string extension)
        {
            Tooltip = "Use \"" +
                    end + " - " +
                    extension;
            if (extension == ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.Export_to_XML_File))
            {
                Tooltip = Tooltip +
                    "\" to save modified content to a " + end.ToLower() + " file.";
            }
            else
            {
                Tooltip = Tooltip +
                    "\" to load content from a " + end.ToLower() + " file.";
            }
        }

        public XMLExportTo(string end, string extension, MenuFile mainMenu) : base()
        {
            this.mainMenu = mainMenu;
            MenuItemContent = extension;
            ToolTip(end, extension);
        }
    }
}
