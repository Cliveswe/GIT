using System.Windows;
using InvoiceMaker.ButtonControls;
using InvoiceMaker.ViewModel.Util;
///<summary>
/// Created by Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-02-06
/// </summary>
namespace InvoiceMaker.ViewModel.ButtonControls
{
    class AppExit : InvoiceButton
    {

        private readonly string boxMsg = "Are you sure that you want to exit the program?";
        private readonly string messageBoxCaption = "Exit confirmation!";

        public override bool ButtonCanExecute(object parameter)
        {
            return true;
        }

      /// <summary>
      /// Exit the current window.
      /// </summary>
      /// <param name="parameter">WPF window object</param>
        public override void ButtonExecute(object parameter)
        {
           
        Window windowApp = new Window();

            windowApp = (Window)parameter;
            if (AppOkCancelMessageBox.AppOKCancelMessageBox(boxMsg, messageBoxCaption) == 1)
            {
                windowApp.Close();//close current window
                Application.Current.Shutdown();//kill application
            }
        }

        public AppExit()
        {
            ButtonContent = "Exit";
        }

    }
}

