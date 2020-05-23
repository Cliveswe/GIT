using System;
/// <summary>
/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-05-05
/// </summary>
namespace ControlTower.ControlTowerEventArgs
{
    class ChangeRouteEventArgs : EventArgs
    {
        /// <summary>
        /// Current route of the flight.
        /// </summary>
        public readonly string Route;

        /// <summary>
        /// Current route
        /// </summary>
        /// <param name="route">Route as a string</param>
        public ChangeRouteEventArgs(string route)
        {
            Route = route;
        }
        /// <summary>
        /// Current route is empty.
        /// </summary>
        public ChangeRouteEventArgs()
        {
            Route = string.Empty;
        }
    }
}
