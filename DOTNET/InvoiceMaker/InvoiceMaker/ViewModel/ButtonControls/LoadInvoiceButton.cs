using InvoiceMaker.ButtonControls;
using InvoiceMaker.ViewModel.Util;
using Microsoft.Win32;
using System;
///<summary>
/// Created by Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-02-06
/// </summary>
namespace InvoiceMaker.ViewModel.ButtonControls
{
    class LoadInvoiceButton : InvoiceButton
    {
        #region Event handler
        /// <summary>
        /// Event handler for this button.
        /// Make use of the generic event handler EventHandler<T> and 
        /// to make use of the custom EvenArgs.
        /// </summary>
        public event EventHandler<LoadInvoiceFileNameEventArgs> InvoiceFileName;
        #endregion

        private string fileExt = "txt files (*.txt)|*.txt";
        private string boxTitle = "Open file";

        public override bool ButtonCanExecute(object parameter)
        {
            return true;
        }
        /// <summary>
        /// Load invoice button has been pressed.
        /// </summary>
        /// <param name="parameter">Object containing parameters from the UI.</param>
        public override void ButtonExecute(object parameter)
        {
            ShowDialogBox(OpenFileDialogBox.OpenileDialogBox(fileExt, boxTitle));
        }

        /// <summary>
        /// Launch the dialog box and perform validation on returning input.
        /// </summary>
        /// <param name="saveFileDialog">Save file dialog box.</param>
        /// <param name="parameter">parameters from WPF as object</param>
        private void ShowDialogBox(OpenFileDialog openFileDialog)
        {
            string filename;
            try
            {
                if (openFileDialog.ShowDialog() == true)//show the UI save as dialog box
                {
                    filename = openFileDialog.FileName;
                    //trigger the event to inform the observers
                    InvoiceFileName?.Invoke(this, new LoadInvoiceFileNameEventArgs(filename));
                }
            }
            catch (Exception e)
            {
                AppOkMessageBox.AppOKMessageBox("Something went wrong!\nIgnoring file selection.\n" + e.Message.ToString(), "Reading from file.");
            }
        }

        public LoadInvoiceButton() : base()
        {
            ButtonContent = "Load Invoice";
        }
    }
}
