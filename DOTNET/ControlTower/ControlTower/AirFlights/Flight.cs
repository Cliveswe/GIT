using ControlTower.Controls;
using ControlTower.ControlTowerEventArgs;
using ControlTower.Utils;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
/// <summary>
/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-05-05
/// </summary>
namespace ControlTower.Publishers
{
    class Flight
    {
        #region Window Titles
        private enum ContentTiltlesEnum { Start, Land, Change_Route, degrees, Flight_Controls, Missing_flight_code }
        public string Title
        {
            get {
                return _title;

            }
            set {
                _title = value;
                OnPropertyChanged("Title");

            }
        }
        private string _title;
        /// <summary>
        /// Flight controls combo box header 
        /// </summary>
        public string FlightControlsHeader
        {
            get { return ContentOfEnumToText<ContentTiltlesEnum>.GetContentText(ContentTiltlesEnum.Flight_Controls); }
        }

        #endregion

        #region Flight Controls
        #region Start
        public StartFlight Start
        {
            get; set;
        }
        #endregion

        #region Land
        public LandFlight Land
        {
            get; set;
        }
        #endregion

        #region Change Route
        public FlightStatus Route
        {
            get; set;
        }
        #endregion
        #endregion

        #region Flight event
        /// <summary>
        /// Event handler for this class.
        /// Make use of the generic event handler EventHandler<T> and 
        /// to make use of the custom EvenArgs.
        /// </summary>
        public event EventHandler<FlightStatusEventArgs> StatusOfFlight;
        /// <summary>
        /// Create an event argument class to contain the flight code, status of the
        /// flight and the current time.
        /// </summary>
        /// <param name="status">Flight status</param>
        private void OnProcessFlightStatus(string status)
        {
            StatusOfFlight?.Invoke(this, new FlightStatusEventArgs(Title, status, DateTime.Now));
        }
        #endregion

        #region Flight Image
        /// <summary>
        /// Gets or sets an image file to been shown.
        /// </summary>
        /// <value>
        /// The show image file.
        /// </value>
        public ImageSource ShowImageFile
        {
            get {
                return _showImageFile;
            }
            set {
                _showImageFile = value;
                OnPropertyChanged("ShowImageFile");
            }
        }
        private ImageSource _showImageFile;
        #endregion

        /// <summary>
        /// Initialise the class.
        /// </summary>
        private void Initialise()
        {
            Start = new StartFlight(ContentOfEnumToText<ContentTiltlesEnum>.GetContentText(ContentTiltlesEnum.Start));
            Land = new LandFlight(ContentOfEnumToText<ContentTiltlesEnum>.GetContentText(ContentTiltlesEnum.Land));
            Route = new FlightStatus();

            SetTogelActionStartAndLand();

            Start.FlightDeparture += Depature;
            Land.FlightArrived += Arrival;
            Route.ChangedRoute += RouteChanged;
        }

        #region Event handlers from flight controls
        /// <summary>
        /// Flight has arrived at destination.
        /// </summary>
        /// <param name="sender">Object Land</param>
        /// <param name="eventArgs">Land event arguments</param>
        private void Arrival(Object sender, LandEventArgs eventArgs)
        {
            OnProcessFlightStatus(eventArgs.Land);
        }
        /// <summary>
        /// Flight has changed route.
        /// </summary>
        /// <param name="sender">Object Route</param>
        /// <param name="eventArgs">Route event arguments</param>
        private void RouteChanged(Object sender, ChangeRouteEventArgs eventArgs)
        {
            OnProcessFlightStatus(eventArgs.Route);
        }
        /// <summary>
        /// Flight is ready to start.
        /// </summary>
        /// <param name="sender">Object Start</param>
        /// <param name="eventArgs">Start event arguments</param>
        private void Depature(Object sender, TakeOffEventArgs eventArgs)
        {
            OnProcessFlightStatus(eventArgs.TakeOff);
        }
        #endregion

        /// <summary>
        /// Connect the start and land button so that they can toggle on and off.
        /// </summary>
        private void SetTogelActionStartAndLand()
        {
            // Land.RegisterWithButtonActiveState(new Action<bool>(Start.TriggerToggle));
            Start.RegisterWithButtonActiveState(new Action<bool>(Land.TriggerToggle));
            Start.RegisterWithButtonActiveState(new Action<bool>(Route.TriggerToggle));
        }

        /// <summary>
        /// Carrier is a aeroplane or flight and we set the logo for the 
        /// carrier.
        /// </summary>
        private void SetCarrierLogo()
        {
            AirlineLogo airlineLogo = new AirlineLogo();
            ImageSource sourceOfImage;

            if (airlineLogo.GetImage(Title, out sourceOfImage))
            {
                ShowImageFile = sourceOfImage;
            }
            else
            {
                ShowImageFile = airlineLogo.GetStandardImage();
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="title">Flight code</param>
        public Flight(string title)
        {
            Title = title;
            Initialise();
            SetCarrierLogo();
        }
        /// <summary>
        /// Constructor
        /// </summary>
        public Flight() : this(ContentOfEnumToText<ContentTiltlesEnum>.GetContentText(ContentTiltlesEnum.Missing_flight_code))
        {
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
