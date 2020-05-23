using ControlTower.ControlTowerEventArgs;
using ControlTower.Utils;
using System;
using System.IO;
/// <summary>
/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-05-05
/// </summary>
namespace ControlTower.Logger
{
    class LogControlTowerActions
    {
        private static string currentDirectory;
        private static string currentPath;
        /// <summary>
        /// Set the current directory.
        /// </summary>
        private static void SetCurrentDirectory()
        {
            //get the current directory
            currentDirectory = Directory.GetCurrentDirectory();
        }

        /// <summary>
        /// Set the current path and file to log control tower events.
        /// </summary>
        private static void SetCurrentPath()
        {
            //create a path and text file name to log data
            currentPath = System.IO.Path.Combine(currentDirectory, "LogOfControlTowerActions.txt");
        }

        /// <summary>
        /// Event was fired as a aircraft flight status was changed.
        /// </summary>
        /// <param name="sender">Object that triggered the even</param>
        /// <param name="flightStatus">Event arguments</param>
        public static void LogStatus(Object sender, FlightStatusEventArgs flightStatus)
        {
            SetCurrentDirectory();
            SetCurrentPath();
            try
            {
                //Open a file and amend data then close the file
                File.AppendAllText(currentPath, sender.ToString() + " :: " +
                        flightStatus.FlightCode + " :: " +
                        flightStatus.FlightStatus + " :: " +
                        flightStatus.FlightStatusUpdateTime + "\n");
            }
            catch (Exception e)
            {
                AppOkMessageBox.AppOKMessageBox("Error with resource LogStatus " + e, "Logger Error");
            }
        }
    }
}
