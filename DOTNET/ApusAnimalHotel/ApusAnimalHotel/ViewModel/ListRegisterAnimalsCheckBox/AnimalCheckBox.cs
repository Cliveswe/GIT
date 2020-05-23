using System;
using System.ComponentModel;
using ApusAnimalHotel.ViewModel.Commands;
/// <summary>
/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-03-09
/// </summary>
namespace ApusAnimalHotel.ViewModel.ListRegisterAnimalsCheckBox
{
    abstract class AnimalCheckBox : ICheckBoxAnimal
    {
        /// <summary>
        /// Gets or sets the content of the checkbox.
        /// </summary>
        /// <value>
        /// The content of the button.
        /// </value>
        public string CheckBoxContent
        {
            get {
                return _checkBoxContent;
            }
            set {
                _checkBoxContent = value;
                OnPropertyChanged("CheckBoxContent");
            }
        }
        private string _checkBoxContent;

        /// <summary>
        /// Gets or sets the checkbox IsChecked property.
        /// </summary>
        /// <value>
        /// True if the button is checked otherwise false.
        /// </value> 
        public bool IsChecked
        {
            get {
                return _isChecked;
            }
            set {
                _isChecked = value;
                OnPropertyChanged("IsChecked");
            }
        }
        private bool _isChecked;
        #region Relay commands ex:RelayCommands RelayCommands(method, property)
        /// <summary>
        /// Gets or sets the button command.
        /// </summary>
        /// <value>
        /// The button command.
        /// </value>
        public RelayCommands CheckBoxCommand
        {
            get; set;
        }
        /// <summary>
        /// Adds the relay command.
        /// </summary>
        private void AddRelayCommand()
        {
            CheckBoxCommand = new RelayCommands(CheckBoxExecute, CheckBoxCanExecute);
        }
        /// <summary>
        /// Button is pressed execute method.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public abstract void CheckBoxExecute(Object parameter);

        /// <summary>
        /// Can the button be executed.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        public abstract bool CheckBoxCanExecute(Object parameter);

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
