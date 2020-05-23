using ApusAnimalHotel.ViewModel.MenuUI.File;

namespace ApusAnimalHotel.ViewModel.MenuUI
{
    class Menu
    {
        /// <summary>
        /// The File menu item.
        /// </summary>
        public MenuFile MenuFile
        {
            get;
            set;
        }

        /// <summary>
        /// Reference to the main window.
        /// </summary>
        protected UIMainWindowIO Main
        {
            get {
                return _main;
            }
            private set {
                _main = value;
            }
        }
        private UIMainWindowIO _main;

        /// <summary>
        /// Reset the application.
        /// </summary>
        public void ResetApp()
        {
            Main.ResetApp();
        }
        /// <summary>
        /// Reset the application to startup state.
        /// </summary>
        public void ResetAppForNew()
        {
            Main.ResetApp();
            Main.ResetFoodDetails();
        }
        /// <summary>
        /// Refresh the application.
        /// </summary>
        public void RefreshApp()
        {
            Main.RefreshApp();
        }
        public Menu(UIMainWindowIO main)
        {
            Main = main;
            MenuFile = new MenuFile(this);//File menu items
        }
    }
}
