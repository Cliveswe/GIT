using System.Windows;
///<summary>
/// Created by Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-02-06
/// </summary>
namespace InvoiceMaker.ViewModel.Util
{
    class AppOkMessageBox
    {/// <summary>
     /// A message box that ask if the user wants to terminate the running app. If the use
     /// presses OK return 1, Cancel return 0, otherwise return -1.
     /// </summary>
     /// <returns>An int.</returns>
        public static int AppOKMessageBox(string msg, string caption)
        {
            string messageBoxText = msg;
            string messageBoxCaption = caption;
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage image = MessageBoxImage.Warning;

            MessageBoxResult messageSelected = MessageBox.Show(messageBoxText, messageBoxCaption, button, image);

            switch (messageSelected)
            {
                case MessageBoxResult.OK:
                    return 1;
                default:
                    return -1;
            }
        }
    }
}