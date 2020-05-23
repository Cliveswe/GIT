using System;
/// <summary>
/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-05-05
/// </summary>
namespace ControlTower.ControlTowerEventArgs
{
    class LandEventArgs : EventArgs
    {
        public readonly string Land;

        public LandEventArgs()
        {
            Land = "Arrived";
        }
    }
}
