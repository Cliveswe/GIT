using System;
/// <summary>
/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-05-05
/// </summary>
namespace ControlTower.ControlTowerEventArgs
{
    class TakeOffEventArgs : EventArgs
    {
        public readonly string TakeOff;

        public TakeOffEventArgs()
        {
            TakeOff = "Departed";
        }
    }
}
