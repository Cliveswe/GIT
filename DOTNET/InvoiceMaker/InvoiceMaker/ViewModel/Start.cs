using InvoiceMaker.Model;
using InvoiceMaker.ViewModel.ButtonControls;
using InvoiceMaker.ViewModel.Util;
using System;
///<summary>
/// Created by Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-02-06
/// </summary>
namespace InvoiceMaker.ViewModel
{
    class Start 
    {
        #region class properties
        public string AppTitle {
             get {
                return _appTitle;
            }
        }
        private string _appTitle;

        private string[] invoiceRawData;//invoice data

        private Invoice invoice;
        #endregion

        #region UI property
        public InvoiceUI InvoiceUI_IO
        {
            get {
                return _invoiceUI_IO;
            }
            private set {
                _invoiceUI_IO = value;
            }
        }
        private InvoiceUI _invoiceUI_IO;
        #endregion

        #region Objects for Invoice button controls
        public LoadInvoiceButton LoadInvoice
        {
            get;set;
        }
        /// <summary>
        /// Load an company logo.
        /// </summary>
        public LoadImageButton LoadImage
        {
            get;set;
        }
        /// <summary>
        /// Pint the invoice
        /// </summary>
        public PrintInvoiceButton PrintInvoice
        {
            get;set;
        }
        
        public AppExit ExitApp
        {
            get;set;
        }
        #endregion
        private void Initialize()
        {
            _appTitle = "Invoice Reader";

            //Hook into events
            //find an invoice file to load into the application
            LoadInvoice = new LoadInvoiceButton();
            //listen for a file name to an invoice
            LoadInvoice.InvoiceFileName += LoadInvoice_InvoiceFileName;

            InvoiceUI_IO = new InvoiceUI();
            //Load an image to the invoice
            LoadImage = new LoadImageButton();
            //Print invoice
            PrintInvoice = new PrintInvoiceButton();
            //Exit the application
            ExitApp = new AppExit();

        }
        /// <summary>
        /// Process the located invoice file.
        /// </summary>
        /// <param name="sender">Object that sent the event</param>
        /// <param name="x">Information regarding the event.</param>
        private void LoadInvoice_InvoiceFileName(object sender, LoadInvoiceFileNameEventArgs eArgs)
        {
            invoice = new Invoice();
            try
            {
                invoiceRawData = InvoiceParser.ParseInvoice(eArgs.invoiceFileName);
                invoice = GenerateInvoiceObj.GenerateInvoice(invoice, invoiceRawData);
                InvoiceUI_IO.AddInvoice(invoice);
            }
            catch (Exception ex) {
                throw new Exception("Error during event: load invoice\n" + ex);
            }
        }
        /// <summary>
        /// Class constructor.
        /// </summary>
        public Start()
        {
            Initialize();
        }
    }
}
