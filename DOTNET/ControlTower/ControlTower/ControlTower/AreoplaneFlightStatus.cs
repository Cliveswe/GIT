using System;
/// <summary>
/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-05-05
/// </summary>
namespace ControlTower.Subscribers
{
    class AreoplaneFlightStatus
    {
        /// <summary>
        /// Designated flight code. 
        /// </summary>
        public string FlightCode
        {
            get {
                return _flightCode;
            }
            private set {
                _flightCode = value;
            }
        }
        private string _flightCode;
        /// <summary>
        /// Flight current status.
        /// </summary>
        public string FlightStatus
        {
            get {
                return _flightStatus;
            }
            private set {
                _flightStatus = value;
            }
        }
        private string _flightStatus;
        /// <summary>
        /// The time when the flight status was updated.
        /// </summary>
        public string FlightUpdateTime
        {
            get {
                return _flightUpdateTime;
            }
            private set {
                _flightUpdateTime = value;
            }
        }
        private string _flightUpdateTime;

        /// <summary>
        /// Convert date and time to month in text, day as numbers
        /// time in 24h.
        /// </summary>
        /// <param name="dateTime">Time stamp of the flight status</param>
        /// <returns></returns>
        private string DateTimeToString(DateTime dateTime)
        {
            return dateTime.ToString("MMMM dd - HH:mm:ss");
        }
        /// <summary>
        /// Update the flight status.
        /// </summary>
        /// <param name="status">Flight status</param>
        /// <param name="dateTime">Time stamp of the flight status</param>
        public void UpdateFlightStatus(string status, DateTime dateTime)
        {
            FlightStatus = status;
            FlightUpdateTime = DateTimeToString(dateTime);
        }
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="flightCode">Designated flight code for a flight.</param>
        /// <param name="flightStatus">Flights status.</param>
        /// <param name="dateTime">Time stamp of the flight status.</param>
        public AreoplaneFlightStatus(string flightCode, string flightStatus, DateTime dateTime)
        {
            FlightCode = flightCode;
            FlightStatus = flightStatus;
            FlightUpdateTime = DateTimeToString(dateTime);
        }
    }
}
