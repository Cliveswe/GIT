using System.ComponentModel;
using ApusAnimalHotel.ViewModel.Commands;
/// <summary>
/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-05-05
/// </summary>
namespace ApusAnimalHotel.ViewModel.MenuUI.File
{
    public abstract class MenuFileItems
    {

        /// <summary>
        /// Gets or sets the content of the Menu item.
        /// </summary>
        /// <value>
        /// The content of the Menu item.
        /// </value>
        public string MenuItemContent
        {
            get {
                return _menuItemContent;
            }
            set {
                _menuItemContent = value;
                OnPropertyChanged("MenuItemContent");
            }
        }
        private string _menuItemContent;
        /// <summary>
        /// Tooltip information on the menu item.
        /// </summary>
        public string Tooltip
        {
            get {
                return _tooltip;
            }
            set {
                _tooltip = value;
                OnPropertyChanged("Tooltip");
            }
        }
        private string _tooltip;
        /// <summary>
        /// Gets or sets a value indicating whether this instance is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is enabled; otherwise, <c>false</c>.
        /// </value>
        public bool IsEnabled
        {
            get {
                return _isEnabled;
            }
            set {
                _isEnabled = value;
                OnPropertyChanged("IsEnabled");
            }
        }
        private bool _isEnabled;

        /// <summary>
        /// Clears the content of the Menu item.
        /// </summary>
        public void ClearMenuItmeContent()
        {
            MenuItemContent = string.Empty;
        }

        /// <summary>
        /// Determines whether [is enabled default].
        /// </summary>
        public void IsEnabledDefault()
        {
            IsEnabled = true;
        }

        /// <summary>
        /// Abstract method, can the Menu Item be executed?
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public abstract bool MenuItemCanExecute(object parameter);
        /// <summary>
        /// Abstract method that executes the logic for a menu item.
        /// </summary>
        /// <param name="parameter"></param>
        public abstract void MenuItemExecute(object parameter);

        public MenuFileItems()
        {
            ClearMenuItmeContent();
            IsEnabledDefault();
            AddRelayCommand();
            Tooltip = string.Empty;
        }

        #region Menu Item Command
        /// <summary>
        /// Gets or sets the Menu item command.
        /// </summary>
        /// <value>
        /// The Menu item command.
        /// </value>
        public RelayCommands MenuItemCommand
        {
            get;
            set;
        }
        /// <summary>
        /// Adds the relay command.
        /// </summary>
        private void AddRelayCommand()
        {
            MenuItemCommand = new RelayCommands(MenuItemExecute, MenuItemCanExecute);
        }

        #region Menu Item Error Control
        public string Error => throw new System.NotImplementedException();
        public string this[string columnName] => throw new System.NotImplementedException();
        #endregion

        #endregion

        #region INotifyPropertyChanged members
        /// <summary>
        /// This is boiler plate code that was added when one want to notify a change on a class
        /// property. This is where the code and the UI communicate through the event handler.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raise a notification that a property has been changed.
        /// </summary>
        /// <param name="propertyName">A string of the property name</param>
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
