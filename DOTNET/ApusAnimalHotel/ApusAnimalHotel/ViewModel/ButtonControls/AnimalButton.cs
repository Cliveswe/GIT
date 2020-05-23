using System;
using System.ComponentModel;
using ApusAnimalHotel.ViewModel.Commands;
/// <summary>
/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-02-25
/// </summary>
namespace ApusAnimalHotel.ViewModel.ButtonControls
{
    abstract class AnimalButton : IButtonAnimal
    {
        /// <summary>
        /// Gets or sets the content of the button.
        /// </summary>
        /// <value>
        /// The content of the button.
        /// </value>
        public string ButtonContent
        {
            get {
                return _buttonContent;
            }
            set {
                _buttonContent = value;
                OnPropertyChanged("ButtonContent");
            }
        }
        private string _buttonContent;

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
        /// Clears the content of the button.
        /// </summary>
        public void ClearButtonContent()
        {
            ButtonContent = string.Empty;
        }

        /// <summary>
        /// Determines whether [is enabled default].
        /// </summary>
        public void IsEnabledDefault()
        {
            IsEnabled = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AnimalButton"/> class.
        /// </summary>
        public AnimalButton()
        {
            ClearButtonContent();
            IsEnabledDefault();
            AddRelayCommand();
        }

        #region Button Error Control
        public string Error => throw new NotImplementedException();

        public string this[string columnName] => throw new NotImplementedException();
        #endregion

        #region Relay commands ex:RelayCommands RelayCommands(method, property)
        /// <summary>
        /// Gets or sets the button command.
        /// </summary>
        /// <value>
        /// The button command.
        /// </value>
        public RelayCommands ButtonCommand
        {
            get; set;
        }
        /// <summary>
        /// Adds the relay command.
        /// </summary>
        private void AddRelayCommand()
        {
            ButtonCommand = new RelayCommands(ButtonExecute, ButtonCanExecute);
        }
        /// <summary>
        /// Button is pressed execute method.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public abstract void ButtonExecute(Object parameter);

        /// <summary>
        /// Can the button be executed.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        public abstract bool ButtonCanExecute(Object parameter);

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
