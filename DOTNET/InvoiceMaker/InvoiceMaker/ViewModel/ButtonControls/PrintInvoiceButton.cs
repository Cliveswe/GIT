using InvoiceMaker.ButtonControls;
using InvoiceMaker.ViewModel.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
///<summary>
/// Created by Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-02-06
/// </summary>
namespace InvoiceMaker.ViewModel.ButtonControls
{
    class PrintInvoiceButton : InvoiceButton
    {
        private GenerateInvoiceDocument document;
        /// <summary>
        /// The button can execute only if a correct invoice has been loaded
        /// </summary>
        /// <param name="parameter">InvoiceUI object.</param>
        /// <returns>True if valid false otherwise.</returns>
        public override bool ButtonCanExecute(object parameter)
        {
            InvoiceUI invoiceUI = parameter as InvoiceUI;

            if ((parameter == null) || (invoiceUI.InvoiceId == null))
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Print a formated invoice document.
        /// </summary>
        /// <param name="parameter">InvoiceUI object.</param>
        public override void ButtonExecute(object parameter)
        {
            InvoiceUI invoiceUI = parameter as InvoiceUI;
         

            document.AddInvoiceData(invoiceUI);

            PrintInvoiceDialogBox.PrintInvoiceDialog(document);
          
        }

        public PrintInvoiceButton()
        {
            ButtonContent = "Print invoice";
            document = new GenerateInvoiceDocument();
        }
    }
}
