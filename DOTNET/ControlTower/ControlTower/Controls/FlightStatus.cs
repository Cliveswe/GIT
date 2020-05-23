using ControlTower.ControlTowerEventArgs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
/// <summary>
/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-05-05
/// </summary>
namespace ControlTower.Controls
{
    class FlightStatus : INotifyPropertyChanged
    {
        #region Event handler
        /// <summary>
        /// Event handler for this button.
        /// Make use of the generic event handler EventHandler<T> and 
        /// to make use of the custom EvenArgs.
        /// </summary>
        public event EventHandler<ChangeRouteEventArgs> ChangedRoute;
        #endregion

        #region Class properties
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
        /// List of possible flight status
        /// </summary>
        public List<string> Status
        {
            get {
                return _status;
            }
            set {
                _status = value;
                OnPropertyChanged("Status");
            }
        }
        private List<string> _status;
        /// <summary>
        /// The selected status.
        /// </summary>
        public int StatusSelected
        {
            get {
                return _statusSelected;
            }
            set {
                _statusSelected = value;
                if (value > 0)
                {
                    OnPublishRouteChange();
                }
                OnPropertyChanged("StatusSelected");
            }
        }
        private int _statusSelected;
        #endregion

        #region Toggle the combobox
        /// <summary>
        /// Toggle a button stat of can execute.
        /// </summary>
        /// <param name="isValid">True to activate false to deactivate</param>
        public void TriggerToggle(bool isValid)
        {
            IsEnabled = !isValid;
        }
        #endregion

        private void OnPublishRouteChange()
        {
            if (StatusSelected > 0)
            {
                ChangedRoute?.Invoke(this, new ChangeRouteEventArgs(Status[StatusSelected]));
            }
        }
        public FlightStatus()
        {
            Status = new List<string> { "Change route", "10 degrees", "20 degrees", "30 degrees", "40 degrees" };
            StatusSelected = -1;
            IsEnabled = false;
        }

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
