using System.Windows;
///<summary>
/// Created by Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-02-06
/// </summary>
namespace InvoiceMaker.ViewModel.Util
{
    class AppOkCancelMessageBox
    {
        public enum ButtonSelectionEnum { Cancel, OK }
        /// <summary>
        /// A message box that ask if the user wants to terminate the running app. If the use
        /// presses OK return 1, Cancel return 0, otherwise return -1.
        /// </summary>
        /// <returns>An int.</returns>
        public static int AppOKCancelMessageBox(string msg, string caption)
        {
            string messageBoxText = msg;
            string messageBoxCaption = caption;
            MessageBoxButton button = MessageBoxButton.OKCancel;
            MessageBoxImage image = MessageBoxImage.Warning;

            MessageBoxResult messageSelected = MessageBox.Show(messageBoxText, messageBoxCaption, button, image);

            switch (messageSelected)
            {
                case MessageBoxResult.OK:
                    return (int)ButtonSelectionEnum.OK;
                case MessageBoxResult.Cancel:
                    return (int)ButtonSelectionEnum.Cancel;
                default:
                    return -1;
            }
        }
    }
}
