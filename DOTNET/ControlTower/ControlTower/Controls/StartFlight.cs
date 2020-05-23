using ControlTower.ControlTowerEventArgs;
using System;
/// <summary>
/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-05-05
/// </summary>
namespace ControlTower.Controls
{
    class StartFlight : FlightButton
    {
        /// <summary>
        /// Event handler for this button.
        /// Make use of the generic event handler EventHandler<T> and 
        /// to make use of the custom EvenArgs.
        /// </summary>
        public event EventHandler<TakeOffEventArgs> FlightDeparture;
        /// <summary>
        /// Invoke event.
        /// </summary>
        private void OnFlightDeparted()
        {
            FlightDeparture?.Invoke(this, new TakeOffEventArgs());
        }

        public override bool ButtonCanExecute(object parameter)
        {
            return Isvalid;
        }
        /// <summary>
        /// Button has been pressed perform actions.
        /// </summary>
        /// <param name="parameter">Object from the XAML</param>
        public override void ButtonExecute(object parameter)
        {
            Isvalid = false;
            ProcessAction(Isvalid);//inform other button of action
            IsEnabled = false;//start button is only allowed to be used once
            OnFlightDeparted();
        }

        public StartFlight(string content) : base()
        {
            ButtonContent = content;
            Isvalid = true;
        }

    }
}
