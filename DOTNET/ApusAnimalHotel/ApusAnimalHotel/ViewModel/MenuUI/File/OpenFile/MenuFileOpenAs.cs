using ApusAnimalHotel.Model;
using Microsoft.Win32;
using System;

namespace ApusAnimalHotel.ViewModel.MenuUI.File.OpenFile
{
    class MenuFileOpenAs : MenuFileItems, IOpenFile
    {
        private readonly string binaryExtension = "(*.bin) | *.bin";//save as binary files
        private readonly string textExtension = "(*.txt) | *.txt";//save as text files
        private string extensionFilter;//extension filter
        private string saveFileDialogTitle;//dialog box title

        #region Main UI
        private MenuFile mainMenu;
        #endregion

        public override bool MenuItemCanExecute(object parameter)
        {
            return true;
        }

        public override void MenuItemExecute(object parameter)
        {
            OpenFileDialog openFileDialog;
            //binary open 
            if ((string)parameter == ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.Binary_File))
            {
                extensionFilter = ExtensionFilter(ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.Binary_File),
                    binaryExtension);
                saveFileDialogTitle = ExtensionOpenFileTitle(ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.Animal_Register),
                    ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.Binary_File).ToLower());

            }//text open
            else if ((string)parameter == ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.Text_File))
            {
                extensionFilter = ExtensionFilter(ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.Text_File),
                    textExtension);
                saveFileDialogTitle = ExtensionOpenFileTitle(ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.Animal_Register),
                    ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.Text_File).ToLower());
            }

            openFileDialog = CreateOpenileDialogBox(extensionFilter, saveFileDialogTitle);

            ShowDialogBox(openFileDialog, parameter);
        }

        /// <summary>
        /// Launch the dialog box and perform validation on returning input.
        /// </summary>
        /// <param name="saveFileDialog">Save file dialog box.</param>
        /// <param name="parameter">parameters from WPF as object</param>
        private void ShowDialogBox(OpenFileDialog openFileDialog, object parameter)
        {
            try
            {
                if (openFileDialog.ShowDialog() == true)//show the UI save as dialog box
                {
                    if (CheckFileName(openFileDialog.FileName, (string)parameter))//validate the extension
                    {
                        Open(openFileDialog.FileName, (string)parameter);//save a valid file name
                    }
                    else
                    {
                        FileNameAndExtensionTester.ExtensionNotValid(openFileDialog.FileName);//extension is not valid
                    }
                }
            }
            catch (Exception e)
            {
                AppOkMessageBox.AppOKMessageBox("Something went wrong!\n Ignoring file selection.\n" + e.Message.ToString(), "Reading from file.");
            }
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
                return FileNameAndExtensionTester.TestForFileExtension(fileName, binaryExtension);
            }
            else if (extension == ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.Text_File))
            {
                return FileNameAndExtensionTester.TestForFileExtension(fileName, textExtension);
            }
            return false;
        }

        /// <summary>
        /// Instantiate a new save file dialog box.
        /// </summary>
        /// <param name="extensionFilter">File extensions</param>
        /// <param name="saveFileDialogTitle">Dialog box title</param>
        /// <returns>Save as dialog box</returns>
        private OpenFileDialog CreateOpenileDialogBox(string extensionFilter, string saveFileDialogTitle)
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
        /// <summary>
        /// Create a tooltip use to give instruction via the UI.
        /// </summary>
        /// <param name="open">The action to be performed.</param>
        /// <param name="extension">The file's extension type.</param>
        public void ToolTip(string open, string extension)
        {
            Tooltip = "Use \"" +
                open + " - " +
                extension +
                "\" to open a file and load it's content to the register.";
        }
        /// <summary>
        /// Open a file and load it to the register.
        /// </summary>
        /// <param name="filePath">Path and file name.</param>
        /// <param name="extension">File extension.</param>
        public void Open(string filePath, string extension)
        {
            try
            {
                if (IsRegisteredSaved())
                {
                    if (extension == ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.Binary_File))
                    {
                        mainMenu.ResetApp();
                        AnimalManager.GetInstance.LoadBinaryFile(@filePath);
                    }
                    else if ((extension == ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.Text_File)) ||
                       (extension == ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.XML)))
                    {
                        mainMenu.ResetApp();
                        AnimalManager.GetInstance.LoadTextFile(@filePath);
                    }

                    mainMenu.RefreshApp();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Content of the binary file is either corrupt or has an incorrect format.\n" + ex.Message.ToString(), ex);
            }
        }
        /// <summary>
        /// Check if the register has been saved. If not display an Ok message box.
        /// </summary>
        /// <returns>True if the register has been saved otherwise false.</returns>
        private bool IsRegisteredSaved()
        {
            if (!AnimalManager.GetInstance.RegisterHasBeenSaved())
            {
                AppOkMessageBox.AppOKMessageBox("Registered needs to be saved. Choose menu \"" +
                     ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.Save) + "\"\\\"" +
                      ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.Save_as) + " or " +
                       ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.New) +
                       " before continuing.", "Unsaved register.");
                return false;
            }
            return true;
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
        public string ExtensionOpenFileTitle(string title, string extention)
        {
            if ((title != null) && (extention != null))
            {
                return "Open the content of " + title + " as a " + extention + ".";
            }
            return "Incorrect extension filter for title!";
        }
        #endregion

        public MenuFileOpenAs(string open, string extension, MenuFile mainMenu)
        {
            this.mainMenu = mainMenu;
            MenuItemContent = extension;
            ToolTip(open, extension);
        }
    }
}
