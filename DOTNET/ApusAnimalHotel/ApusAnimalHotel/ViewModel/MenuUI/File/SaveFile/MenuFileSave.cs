using ApusAnimalHotel.Model;

namespace ApusAnimalHotel.ViewModel.MenuUI.File
{
    class MenuFileSave : MenuFileItems
    {
        private readonly string msg = "Register saved to file.";//extension filter
        private readonly string msgCaption = "Save Animal Manager as a binary file.";

        public override bool MenuItemCanExecute(object parameter)
        {
            return true;//!AnimalManager.GetInstance.RegisterHasBeenSaved();
        }
        /// <summary>
        /// Command for saving content to a new binary or text file. 
        /// </summary>
        /// <param name="parameter">Object from WPF</param>
        public override void MenuItemExecute(object parameter)
        {
            MenuFileSaveAs SaveToFile;
            string test = (string)parameter;
            if (AnimalManager.GetInstance.SaveBinaryFile() == false)
            {
                if ((string)parameter == ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.Binary_File))
                {
                    SaveToFile = new MenuFileSaveAs(ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.Save),
                     ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.Binary_File));

                    SaveToFile.MenuItemExecute(ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.Binary_File));
                }
                else if ((string)parameter == ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.Text_File))
                {
                    SaveToFile = new MenuFileSaveAs(ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.Save),
                     ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.Text_File));

                    SaveToFile.MenuItemExecute(ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.Text_File));
                }

            }
            else
            {
                AppOkMessageBox.AppOKMessageBox(msg, msgCaption);
            }
        }

        /// <summary>
        /// Create a tooltip use to give instruction via the UI.
        /// </summary>
        /// <param name="save">The action to be performed.</param>
        /// <param name="extension">The file's extension type.</param>
        private void ToolTip(string save, string extension)
        {
            Tooltip = "Use \"" +
                save + " - " +
                extension +
                "\" to save modified content to a " + extension.ToLower() + ".";
        }

        public MenuFileSave(string save, string extension) : base()
        {
            MenuItemContent = extension;
            ToolTip(save, extension);
        }
    }
}
