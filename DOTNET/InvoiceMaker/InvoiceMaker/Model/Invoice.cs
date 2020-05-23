using System.Collections.Generic;
///<summary>
/// Created by Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-02-06
/// </summary>
namespace InvoiceMaker.Model
{
     class Invoice
    {
        /// <summary>
        /// Invoice number
        /// </summary>
        public string InvoiceNumber
        {
            get {
                return _invoiceNumber;
            }
            private set {
                _invoiceNumber = value;
            }
        }
        private string _invoiceNumber;

        #region Dates
        /// <summary>
        /// Date the invoice was created. incepted.
        /// </summary>
        public InvoiceDate InvoiceInceptionDate
        {
            get {
                return _invoiceInceptionDate;
            }
            private set {
                _invoiceInceptionDate = value;
            }
        }
        private InvoiceDate _invoiceInceptionDate;
        /// <summary>
        /// Due date for the invoice.
        /// </summary>
        public InvoiceDate InvoiceDueDate
        {
            get {
                return _invoiceDueDate;
            }
            private set {
                _invoiceDueDate = value;
            }
        }
        private InvoiceDate _invoiceDueDate;
        #endregion

        #region Contact Details
        #region Address
        /// <summary>
        /// Address of the company.
        /// </summary>
        public InvoiceAddress ReceiverCompanyAddress
        {
            get {
                return _receiverCompanyAddress;
            }
            private set {
                _receiverCompanyAddress = value;
            }
        }
        private InvoiceAddress _receiverCompanyAddress;
        /// <summary>
        /// Address of the company that sent the invoice.
        /// </summary>
        public InvoiceAddress SenderCompanyAddress
        {
            get {
                return _senderCompanyAddress;
            }
            private set {
                _senderCompanyAddress = value;
            }
        }
        private InvoiceAddress _senderCompanyAddress;
        #endregion

        #region Phone Number URL
        /// <summary>
        /// Telephone number and URL web address.
        /// </summary>
        public InvoiceContactInfo ContactInfo
        {
            get {
                return _contactInfo;
            }
            private set {
                _contactInfo = value;
            }
        }
        private InvoiceContactInfo _contactInfo;
        #endregion

        #endregion

        #region Items
        public string NumberOfItems {
            get {
                return _numberOfItems;
            }
            private set {
                _numberOfItems = value;
            }
        }
        private string _numberOfItems;
        /// <summary>
        /// List of items.
        /// </summary>
        public List<InvoiceItem> InvoiceItems
        {
            get {
                return _invoiceItems;
            }
            private set {
                _invoiceItems = value;
            }
        }
        public List<InvoiceItem> _invoiceItems;
        #endregion

        /// <summary>
        /// Add the invoice number.
        /// </summary>
        /// <param name="i">Invoice number as a string</param>
        public void AddInvoiceNumber(string i)
        {
            InvoiceNumber = i;
        }
        /// <summary>
        /// Add the invoice date.
        /// </summary>
        /// <param name="date">Invoice date as a string.</param>
        public void AddInvoiceDate(string date)
        {
            InvoiceInceptionDate = new InvoiceDate(date);
        }
        /// <summary>
        /// Add the invoice due date.
        /// </summary>
        /// <param name="date">Invoice due date as a string.</param>
        public void AddInvoiceDueDate(string date)
        {
            InvoiceDueDate = new InvoiceDate(date);
        }

        /// <summary>
        /// Add the address of a receiver.
        /// </summary>
        public void AddReceiverCompanyAddress()
        {
            ReceiverCompanyAddress = new InvoiceAddress();
        }
        /// <summary>
        /// Add the address of a sender.
        /// </summary>
        public void AddSenderCompanyAddress()
        {
            SenderCompanyAddress = new InvoiceAddress();
        }

        /// <summary>
        /// Set the number of items in this invoice.
        /// </summary>
        /// <param name="itemNumber"></param>
        public void SetNumberOfItems(string itemNumber)
        {
            NumberOfItems = itemNumber;

            InvoiceItems = new List<InvoiceItem>();
        }
        /// <summary>
        /// Add a contact info object.
        /// </summary>
        private void CreateContactInfo()
        {
            if (ContactInfo == null)
            {
                ContactInfo = new InvoiceContactInfo();
            }

        }
        /// <summary>
        /// Add a telephone number.
        /// </summary>
        /// <param name="msg">Telephone number as a string.</param>
        public void AddPhoneNumber(string msg)
        {
            CreateContactInfo();
            ContactInfo.AddPhoneNumber(msg);
        }
        /// <summary>
        /// Add a web address.
        /// </summary>
        /// <param name="msg">URL as a string.</param>
        public void AddURL(string msg)
        {
            CreateContactInfo();
            ContactInfo.AddURL(msg);
        }

        /// <summary>
        /// Add the invoice items details to the invoice.
        /// </summary>
        /// <param name="item">InvoiceItem item</param>
        public void AddItems(InvoiceItem item)
        {
            InvoiceItems.Add(item);
        }
        /// <summary>
        /// Default company address
        /// </summary>
        private void AddCompanyAddress()
        {
            //contact address
            SenderCompanyAddress = new InvoiceAddress();
            SenderCompanyAddress.AddAddressData(InvoiceAddress.InvoiceAddressKeyEnum.CompanyName ,"Apu Beverages Inc");
            SenderCompanyAddress.AddAddressData(InvoiceAddress.InvoiceAddressKeyEnum.StreetAddress, "Verages st 12");
            SenderCompanyAddress.AddAddressData(InvoiceAddress.InvoiceAddressKeyEnum.ZipCode, "12345");
            SenderCompanyAddress.AddAddressData(InvoiceAddress.InvoiceAddressKeyEnum.City, "Somecity");
            SenderCompanyAddress.AddAddressData(InvoiceAddress.InvoiceAddressKeyEnum.Country, "Some Country");
            SenderCompanyAddress.AddAddressData(InvoiceAddress.InvoiceAddressKeyEnum.ContactPerson, string.Empty);
            //contact info
            ContactInfo = new InvoiceContactInfo();
            ContactInfo.AddPhoneNumber("000 123456");
            ContactInfo.AddURL("Beverages.com");
           
        }
        public Invoice()
        {
            AddCompanyAddress();
        }
    }
}
