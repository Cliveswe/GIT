using System;
/// <summary>
/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-05-05
/// </summary>
namespace ControlTower.ControlTowerEventArgs
{
    class FlightStatusEventArgs : EventArgs
    {
        public readonly string FlightCode;
        public readonly string FlightStatus;
        public readonly DateTime FlightStatusUpdateTime;

        public FlightStatusEventArgs(string flightCode, string flightStatus, DateTime dateTime)
        {
            FlightStatus = flightStatus;
            FlightCode = flightCode;
            FlightStatusUpdateTime = dateTime;
        }
    }
}
