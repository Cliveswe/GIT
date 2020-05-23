using ControlTower.Controls;
using ControlTower.Controls.TextInputOutput;
using ControlTower.ControlTowerEventArgs;
using ControlTower.Logger;
using ControlTower.Utils;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
/// <summary>
/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-05-05
/// </summary>
namespace ControlTower.Subscribers
{
    class AirTrafficControlTower : INotifyPropertyChanged
    {
        #region Window Titles
        private enum ContentTiltlesEnum { Control_Tower, Next_flight, Send_next_Areoplane_to_Runway, Enter_Flight_Number }
        /// <summary>
        /// Window title
        /// </summary>
        public string Title
        {
            get { return ContentOfEnumToText<ContentTiltlesEnum>.GetContentText(ContentTiltlesEnum.Control_Tower); }
        }
        /// <summary>
        /// Group box title
        /// </summary>
        public string EnterFlightNumberHeader
        {
            get { return ContentOfEnumToText<ContentTiltlesEnum>.GetContentText(ContentTiltlesEnum.Enter_Flight_Number); }
        }
        /// <summary>
        /// Label title
        /// </summary>
        public string NextFlightLabel
        {
            get { return ContentOfEnumToText<ContentTiltlesEnum>.GetContentText(ContentTiltlesEnum.Next_flight) + ":"; }
        }
        #endregion

        #region Traffic Control Tower Controls
        /// <summary>
        /// Button Send Next Aeroplane To Runway
        /// </summary>
        public SendNextAeroplaneToRunwayButton SendNextAeroPlaneToRunway
        {
            get; set;
        }
        /// <summary>
        /// Textbox content
        /// </summary>
        public TextInputOutput FlightCodeTextbox
        {
            get; set;
        }

        //
        public LogControlTowerActions Logger
        {
            get; set;
        }
        #endregion

        #region Display flight details using ListView
        #region Properties
        private enum ListViewHeadersEnum { Flight_Code, Status, Time }
        /// <summary>
        /// Flight code header
        /// </summary>
        public string FlightCodeTag
        {
            get { return ContentOfEnumToText<ListViewHeadersEnum>.GetContentText(ListViewHeadersEnum.Flight_Code); }
        }
        /// <summary>
        /// Flight status header
        /// </summary>
        public string FlightStatusTag
        {
            get { return ContentOfEnumToText<ListViewHeadersEnum>.GetContentText(ListViewHeadersEnum.Status); }
        }
        /// <summary>
        /// Flight time stamp header
        /// </summary>
        public string FlightTimeStampTag
        {
            get { return ContentOfEnumToText<ListViewHeadersEnum>.GetContentText(ListViewHeadersEnum.Time) + " ..."; }
        }
        /// <summary>
        /// A queue of flights.
        /// </summary>
        public ObservableCollection<AreoplaneFlightStatus> FlightQueue
        {
            get {
                return _flightQueue;
            }
            set {
                _flightQueue = value;
                OnPropertyChanged("FlightQueue");
            }
        }
        private ObservableCollection<AreoplaneFlightStatus> _flightQueue;
        #endregion

        #region Methods
        /// <summary>
        /// Amend flight details or add new flight.
        /// </summary>
        /// <param name="flightStatus">Status of a flight</param>
        private void UpdateFlightQueue(FlightStatusEventArgs flightStatus)
        {
            //using LINQ to search for an item in the collection
            AreoplaneFlightStatus item = FlightQueue.FirstOrDefault(
                i => i.FlightCode == flightStatus.FlightCode);

            //what to do with the item
            if (item != null)
            {
                AmendFlightStatus(item, flightStatus);
                OnPropertyChanged("FlightQueue");
            }
            else
            {
                //add new flight
                AddNewFlight(flightStatus);
            }
        }
        /// <summary>
        /// Amend the status of a flight that is listed in the traffic controller.
        /// </summary>
        /// <param name="item">A flight in the traffic controller list</param>
        /// <param name="flightStatus">New status of the flight</param>
        private void AmendFlightStatus(AreoplaneFlightStatus item, FlightStatusEventArgs flightStatus)
        {
            int i = FlightQueue.IndexOf(item);
            //amend flight details
            item.UpdateFlightStatus(flightStatus.FlightStatus,
                            flightStatus.FlightStatusUpdateTime);
            FlightQueue.RemoveAt(i);
            FlightQueue.Insert(i, item);
        }

        /// <summary>
        /// Add a new flight to the air traffic controller.
        /// </summary>
        /// <param name="flightStatus">Flight status</param>
        private void AddNewFlight(FlightStatusEventArgs flightStatus)
        {
            FlightQueue.Add(new AreoplaneFlightStatus(
                flightStatus.FlightCode,
                flightStatus.FlightStatus,
                flightStatus.FlightStatusUpdateTime));
        }
        #endregion
        #endregion

        #region Initialisation
        /// <summary>
        /// Add controls to the application.
        /// </summary>
        private void AddControls()
        {
            SendNextAeroPlaneToRunway = new SendNextAeroplaneToRunwayButton(
                ContentOfEnumToText<ContentTiltlesEnum>.GetContentText(
                    ContentTiltlesEnum.Send_next_Areoplane_to_Runway));
            SetFlightCodeTextbox();
        }
        /// <summary>
        /// Input to the set a new flight code.
        /// </summary>
        private void SetFlightCodeTextbox()
        {
            FlightCodeTextbox = new TextInputOutput(
               ContentOfEnumToText<ContentTiltlesEnum>.GetContentText(
               ContentTiltlesEnum.Next_flight));
            FlightCodeTextbox.SetErrorMessage("Cannot have an empty flight code.");
        }
        /// <summary>
        /// Connect delegates to call back methods.
        /// </summary>
        private void AddEvents()
        {
            SendNextAeroPlaneToRunway.StatusOfFlight += AircraftFlightStatus;
            SendNextAeroPlaneToRunway.RegisterWithButtonActiveState(new Action<bool>(ResetInputControls));
            SendNextAeroPlaneToRunway.SendFlightToRunway += AircraftFlightStatus;
            //Attach control tower to logger
            SendNextAeroPlaneToRunway.SendFlightToRunway += LogControlTowerActions.LogStatus;
        }

        /// <summary>
        /// Clear the input textbox to reset the controls
        /// </summary>
        /// <param name="reset">True to reset otherwise false</param>
        private void ResetInputControls(bool reset)
        {
            if (reset)
            {
                FlightCodeTextbox.ClearTextIO();
            }
        }
        #endregion

        /// <summary>
        /// Event was fired as a aircraft flight status was changed.
        /// </summary>
        /// <param name="sender">Object that triggered the even</param>
        /// <param name="flightStatus">Event arguments</param>
        private void AircraftFlightStatus(Object sender, FlightStatusEventArgs flightStatus)
        {
            UpdateFlightQueue(flightStatus);
        }
        /// <summary>
        /// Class constructor
        /// </summary>
        public AirTrafficControlTower()
        {
            FlightQueue = new ObservableCollection<AreoplaneFlightStatus>();
            Logger = new LogControlTowerActions();
            AddControls();
            AddEvents();

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
