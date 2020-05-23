using ApusAnimalHotel.Model;
using System.Windows;
/// <summary>
/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-05-05
/// </summary>
namespace ApusAnimalHotel.ViewModel.MenuUI.File
{
    class MenuFileExit : MenuFileItems
    {
        private readonly string boxMsg = "Are you sure that you want to exit the program?";
        private readonly string boxErrorMsg = "\t *-* Warning! *-*\n\n" +
                    "The register has been modified and has yet to be saved.\n";

        private readonly string messageBoxCaption = "Exit confirmation!";
        private readonly string tooltip = "\"" +
           ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.Exit) +
           "\" the application. Make sure that you saved your modifications before exiting!";
        /// <summary>
        /// Set the File menu item "Exit" header.
        /// </summary>
        private void SetHeader()
        {
            MenuItemContent = ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.Exit);
        }

        #region Command for Menu item "Exit" 
        /// <summary>
        /// Can the Menu item "Exit" be executed.
        /// </summary>
        /// <param name="parameter">Default return value of true.</param>
        /// <returns></returns>
        public override bool MenuItemCanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Menu item to be executed.
        /// </summary>
        /// <param name="parameter">Window application as an object</param>
        public override void MenuItemExecute(object parameter)
        {
            string messageBoxMsg = string.Empty;

            Window windowApp = new Window();

            windowApp = (Window)parameter;

            //check if the register has been saved
            if (!AnimalManager.GetInstance.RegisterHasBeenSaved())
            {
                messageBoxMsg = boxErrorMsg + boxMsg;
            }
            else
            {
                messageBoxMsg = boxMsg;
            }
            if (AppOkCancelMessageBox.AppOKCancelMessageBox(messageBoxMsg, messageBoxCaption) == 1)
            {
                windowApp.Close();//close current window
                Application.Current.Shutdown();//kill application
            }
        }
        #endregion

        public MenuFileExit() : base()
        {
            SetHeader();
            Tooltip = tooltip;
        }
    }
}
