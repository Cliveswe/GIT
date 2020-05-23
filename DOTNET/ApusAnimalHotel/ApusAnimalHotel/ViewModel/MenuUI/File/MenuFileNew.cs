using ApusAnimalHotel.Model;
using ApusAnimalHotel.ViewModel.FoodDetails;
/// <summary>
/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-05-05
/// </summary>
namespace ApusAnimalHotel.ViewModel.MenuUI.File
{
    class MenuFileNew : MenuFileItems
    {
        private readonly string messageBoxMsg = "One of the registers has not been saved.\n\nPress \"Ok\" to initialize the application.\n\nPress \"Cancel\" to return to current session.";
        private readonly string messageBoxCaption = "Initialize application?";
        private readonly string tooltip = "\"" +
            ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.New) +
            "\" reset the application to its start up state.";
        /// <summary>
        /// Set the File menu item "New" header.
        /// </summary>
        private void SetHeader()
        {
            MenuItemContent = ContentOfEnumToText<MenuContentTextEnum>.GetContentText(MenuContentTextEnum.New);
        }


        #region Main UI
        private MenuFile mainMenu;
        #endregion

        #region Command for Menu item "Exit" 
        /// <summary>
        /// Can the Menu item "New" be executed.
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
        /// <param name="parameter"></param>
        public override void MenuItemExecute(object parameter)
        {
            if (AnimalManager.GetInstance.RegisterHasBeenSaved() &&
                RecipeManager.GetInstance.RegisterHasBeenSaved())
            {
                mainMenu.ResetAppForNew();

            }
            else
            {
                if (AnimalManager.GetInstance.Count > 0)
                {
                    if (AppOkCancelMessageBox.AppOKCancelMessageBox(messageBoxMsg, messageBoxCaption) ==
                        (int)AppOkCancelMessageBox.ButtonSelectionEnum.OK)
                    {
                        mainMenu.ResetAppForNew();
                    }
                }
            }
        }
        #endregion

        public MenuFileNew(MenuFile mainMenu) : base()
        {

            this.mainMenu = mainMenu;

            SetHeader();
            Tooltip = tooltip;
        }
    }
}
