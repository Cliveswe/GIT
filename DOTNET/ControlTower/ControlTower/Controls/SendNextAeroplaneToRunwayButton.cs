using ControlTower.ControlTowerEventArgs;
using ControlTower.Logger;
using ControlTower.Publishers;
using System;
using System.Windows;
/// <summary>
/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-05-05
/// </summary>
namespace ControlTower.Controls
{
    class SendNextAeroplaneToRunwayButton : FlightButton
    {

        #region Start Flight UI
        public FlightWindow FlightWindow
        {
            get; set;
        }
        #endregion

        #region Button controls
        #region Runway event
        /// <summary>
        /// Event handler for this class.
        /// Make use of the generic event handler EventHandler<T> and 
        /// to make use of the custom EvenArgs.
        /// </summary>
        public event EventHandler<FlightStatusEventArgs> SendFlightToRunway;
        /// <summary>
        /// Create an event argument class to contain the flight code, status of the
        /// flight and the current time.
        /// </summary>
        /// <param name="status">Flight status</param>
        private void OnProcessRunwayStatus(string status)
        {
            SendFlightToRunway?.Invoke(this, new FlightStatusEventArgs(status, "Sent to Runway", DateTime.Now));
        }
        #endregion
        /// <summary>
        /// Can the button be executed.
        /// </summary>
        /// <param name="parameter">Object parameter.</param>
        /// <returns>True if the button can be executed, otherwise false.</returns>
        public override bool ButtonCanExecute(Object parameter)
        {
            bool isValid = false;

            if ((parameter != null) && !string.IsNullOrEmpty((string)parameter))
            {
                IsEnabled = true;
                isValid = true;
            }

            return isValid;
        }

        private string FlightCodeToUpper(Object flightCode)
        {
            string msg = (string)flightCode;
            return msg.ToUpper();
        }
        /// <summary>
        /// Start a new flight.
        /// </summary>
        /// <param name="parameter">Object parameter</param>
        public override void ButtonExecute(Object parameter)
        {
            if (parameter != null)
            {
                //AeroplaneToStart((string)parameter);
                AeroplaneToStart(FlightCodeToUpper(parameter));
                ProcessAction(true);
                OnProcessRunwayStatus(FlightCodeToUpper(parameter));
            }
        }
        #endregion

        #region Send Aeroplane To Runway Event 
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
        private void OnProcessFlightStatus(FlightStatusEventArgs status)
        {
            StatusOfFlight?.Invoke(this, status);
            //has the flight landed
            AeroplaneToGate(status, new LandEventArgs());
        }
        /// <summary>
        /// If the flight has arrived then close the flight window.
        /// </summary>
        /// <param name="status">Flight status</param>
        /// <param name="arrival">Event arguments for landing event</param>
        private void AeroplaneToGate(FlightStatusEventArgs status, LandEventArgs arrival)
        {
            //search all open windows
            foreach (Window window in Application.Current.Windows)
            {
                //find the flight to land
                if ((window.Name == status.FlightCode) && (status.FlightStatus == arrival.Land))
                {
                    //close the flights window
                    window.Close();
                }
            }
        }
        #endregion

        /// <summary>
        /// Start the flight window
        /// </summary>
        /// <param name="flightCode">Flight code</param>
        private void AeroplaneToStart(string flightCode)
        {
            //create a new window
            FlightWindow = new FlightWindow();
            FlightWindow.Name = flightCode;
            //add data context to the window
            FlightWindow.DataContext = new Flight(flightCode);
            //display the window
            FlightWindow.Show();
            //attach callback methods
            AttachCallBack();
        }
        /// <summary>
        /// Attach a callback to an event.
        /// </summary>
        private void AttachCallBack()
        {
            //get the data context from the window
            Flight currentFlight = (Flight)FlightWindow.DataContext;
            //attach the callback method
            currentFlight.StatusOfFlight += InFlight;
            //attach a logger callback method
            currentFlight.StatusOfFlight += LogControlTowerActions.LogStatus;
        }
        /// <summary>
        /// Callback method
        /// </summary>
        /// <param name="subject">Event object</param>
        /// <param name="flightStatus">Event arguments</param>
        private void InFlight(Object subject, FlightStatusEventArgs flightStatus)
        {
            //pass on the flight status
            OnProcessFlightStatus(new FlightStatusEventArgs(flightStatus.FlightCode,
                flightStatus.FlightStatus,
                flightStatus.FlightStatusUpdateTime));

        }
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="title">Flight code</param>
        public SendNextAeroplaneToRunwayButton(string title) : base()
        {
            ButtonContent = title;
        }
    }
}
