using InvoiceMaker.Model;
using InvoiceMaker.ViewModel.Util;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
///<summary>
/// Created by Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-02-06
/// </summary>
namespace InvoiceMaker.ViewModel
{
    class InvoiceUI : INotifyPropertyChanged
    {
        public string InvoiceId
        {
            get {
                return _invoiceId;
            }
            set {
                _invoiceId = value;
                OnPropertyChanged("InvoiceId");
            }
        }
        private string _invoiceId;

        #region UI labels
        public string InvoiceTitle
        {
            get {
                return "Invoice".ToUpper();
            }
        }
        public string InvoiceIdTitle
        {
            get {
                return "Invoice Number";
            }
        }

        public string InvoiceDateTitle
        {
            get {
                return "Invoice date";
            }
        }
        public string InvoiceDueDateTitle
        {
            get {
                return "Due date";
            }
        }
        public string TotalTitle
        {
            get {
                return "Total";
            }
        }
        public string DiscountTitle
        {
            get {
                return "Discount/Voucher";
            }
        }
        public string AmountToPayTitle
        {
            get {
                return "Amount to pay";
            }
        }
        #endregion

        #region Dates
        public DateTime InvoiceDate
        {
            get {
                return _invoiceDate;
            }
            set {
                _invoiceDate = value;
                OnPropertyChanged("InvoiceDate");
            }
        }
        public DateTime _invoiceDate;
        public DateTime InvoiceDueDate
        {
            get {
                return _invoiceDueDate;
            }
            set {
                _invoiceDueDate = value;
                OnPropertyChanged("InvoiceDueDate");
            }
        }
        public DateTime _invoiceDueDate;
        #endregion

        #region Address
        /// <summary>
        /// The address of the company that receives an invoice.
        /// </summary>
        public string InvoiceCompanyAddress
        {
            get {
                return _invoiceCompanyAddress;

            }
            set {
                _invoiceCompanyAddress = value;
                FormattedInvoiceCompanyAddress = value;
                OnPropertyChanged("InvoiceCompanyAddress");
            }
        }
        private string _invoiceCompanyAddress;
        /// <summary>
        /// A re-formatted version of the property InvoiceCompanyAddress.
        /// </summary>
        public string FormattedInvoiceCompanyAddress
        {
            get {
                return _formattedInvoiceCompanyAddress;
            }
            set {
                string res = string.Empty;
                res = value;

                _formattedInvoiceCompanyAddress = res.Replace(", ", "\n");
                OnPropertyChanged("FormattedInvoiceCompanyAddress");
            }
        }
        private string _formattedInvoiceCompanyAddress;
        /// <summary>
        /// Company contact information.
        /// </summary>
        public string CompanyContactInformation
        {
            get {
                return _companyContactInformation;
            }
            set {
                _companyContactInformation = value;
                OnPropertyChanged("CompanyContactInformation");
            }
        }
        private string _companyContactInformation;
        /// <summary>
        /// The address of the company that receives the invoice.
        /// </summary>
        public string InvoiceReceiverAddress
        {
            get {
                return _invoiceReceiverAddress;
            }
            set {
                _invoiceReceiverAddress = value;
                OnPropertyChanged("InvoiceReceiverAddress");
            }
        }
        private string _invoiceReceiverAddress;
        #endregion

        #region Invoice Items
        /// <summary>
        /// Collection of items in the invoice
        /// </summary>
        public ObservableCollection<InvoiceItemsIO> InvoiceItems
        {
            get {
                return _invoiceItems;
            }
            set {
                _invoiceItems = value;
                OnPropertyChanged("InvoiceItems");
            }
        }
        private ObservableCollection<InvoiceItemsIO> _invoiceItems;

        #region Tally Total, Tax and Discount
        int precession = 2;//number of decimal places

        /// <summary>
        /// Total tax for all items.
        /// </summary>
        public string TaxTotal
        {
            get {
                return "Tax\n" + _taxTotal;
            }
            set {
                _taxTotal = value;
                OnPropertyChanged("TaxTotal");
            }
        }
        private string _taxTotal;
        /// <summary>
        /// Total for all items including tax.
        /// </summary>
        public string TotalIncludingTax
        {
            get {
                return "Including tax\n" + _totalIncludingTax;
            }
            set {
                _totalIncludingTax = value;
                OnPropertyChanged("TotalIncludingTax");
            }
        }
        private string _totalIncludingTax;
        /// <summary>
        /// Total for all items including tax.
        /// </summary>
        public string AmountToPay
        {
            get {
                return "Amount To Pay\n" + _amountToPay;
            }
            set {
                _amountToPay = value;
                OnPropertyChanged("AmountToPay");
            }
        }
        private string _amountToPay;
        /// <summary>
        /// A discount value.
        /// </summary>
        public string DiscountValueIO
        {
            get {
                return _discountValueIO;
            }
            set {
                _discountValueIO = value.Replace(".", ",");
                CalculateAmountToPay();
                OnPropertyChanged("DiscountValueIO");
            }
        }
        private string _discountValueIO;
        #endregion
        #endregion
        public void AddInvoice(Invoice invoiceSource)
        {
            InvoiceId = invoiceSource.InvoiceNumber;
            InvoiceDate = invoiceSource.InvoiceInceptionDate.GetInvoiceDate();
            InvoiceDueDate = invoiceSource.InvoiceDueDate.GetInvoiceDate();
            InvoiceCompanyAddress = invoiceSource.SenderCompanyAddress.GetAddress();
            CompanyContactInformation = GetCompanyContactInfo(invoiceSource.ContactInfo);
            InvoiceReceiverAddress = GetFormattedAddress(invoiceSource.ReceiverCompanyAddress);
            //add each item to the observable collection of items
            AddEachItemToCollection(invoiceSource);
            //Calculate the invoice totals
            CalculateInvoiceTotals();
        }

        private void CalculateInvoiceTotals()
        {
            CalculateTaxTotal();
            CalculateTotalIncludingTax();
            CalculateAmountToPay();
        }
        /// <summary>
        /// Calculate the total tax for all items.
        /// </summary>
        private void CalculateTaxTotal()
        {
            double taxTotal = 0;

            foreach (InvoiceItemsIO item in InvoiceItems)
            {
                taxTotal += double.Parse(item.TotalTaxTxt);
            }
            taxTotal = Math.Round((double)taxTotal, precession);

            TaxTotal = taxTotal.ToString();
        }
        /// <summary>
        /// Calculate the total including tax for all items.
        /// </summary>
        private void CalculateTotalIncludingTax()
        {
            double totalIncludingTax = 0;

            foreach (InvoiceItemsIO item in InvoiceItems)
            {
                totalIncludingTax += double.Parse(item.TotalTxt);
            }

            totalIncludingTax = Math.Round((double)totalIncludingTax, precession);

            TotalIncludingTax = totalIncludingTax.ToString();
        }
        /// <summary>
        /// Calculate the total amount to pay.
        /// </summary>
        private void CalculateAmountToPay()
        {
            double res = 0;

            foreach (InvoiceItemsIO item in InvoiceItems)
            {
                res += double.Parse(item.TotalTxt);
            }

            res = CalculateWithDiscount(res);
            res = Math.Round(res, precession);
            
            AmountToPay = res.ToString();
        }
        /// <summary>
        /// Calculate the total amount to pay including discount.
        /// </summary>
        /// <param name="sum">Total amount to pay as double.</param>
        /// <returns>Double</returns>
        private double CalculateWithDiscount(double sum)
        {
            double res = 0;

            if (!string.IsNullOrEmpty(DiscountValueIO))
            {
               res = double.Parse(_discountValueIO);
                sum -= res;
            }
            return sum;
        }

        /// <summary>
        /// Get the phone and web address for the company and format 
        /// the resulting contact information.
        /// </summary>
        /// <param name="contactInfo">The address of the company.</param>
        /// <returns>A string.</returns>
        private string GetCompanyContactInfo(InvoiceContactInfo contactInfo)
        {
            string res = string.Empty;

            res = "Tel: " + contactInfo.PhoneNumber +
                 "\nURL: " + contactInfo.URL;

            return res;
        }

        /// <summary>
        /// Get an address and format it. After each address field a new line is
        /// amended to the resulting string.
        /// </summary>
        /// <param name="address">An address object</param>
        /// <returns>Address as a formated string.</returns>
        private string GetFormattedAddress(InvoiceAddress address)
        {
            string res = string.Empty;
            int maxLenth = address.Address.Length;
            string currentData = string.Empty;

            for (int index = 0; index < maxLenth; index++)
            {
                currentData = address.Address[index];
                if (!string.IsNullOrEmpty(currentData))
                {
                    res += currentData;
                }
                if (index < (maxLenth - 1))
                {
                    res += "\n";
                }
            }

            return res;
        }
        /// <summary>
        /// Add each item to the observable collection of items.
        /// </summary>
        /// <param name="invoiceSource">ObservableCollection of InvoiceItemsIO</param>
        private void AddEachItemToCollection(Invoice invoiceSource)
        {
            InvoiceItems = new ObservableCollection<InvoiceItemsIO>();

            foreach (InvoiceItem item in invoiceSource.InvoiceItems)
            {
                InvoiceItems.Add(
                    AddEvents(item)
                    );
            }
        }
        /// <summary>
        /// Add the events that update both the user interface and source properties.
        /// </summary>
        /// <param name="item">Invoice item</param>
        /// <returns></returns>
        private InvoiceItemsIO AddEvents(InvoiceItem item)
        {
            InvoiceItemsIO invoiceItemsIO = null;

            invoiceItemsIO = new InvoiceItemsIO(item);
            //add event handlers
            invoiceItemsIO.QuantityChangedEvent += item.ChangeQuantity;
            invoiceItemsIO.QuantityChangedEvent += QuantityChanged;

            return invoiceItemsIO;
        }
        /// <summary>
        /// Event that updates the quantity of an item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args">Arguments that contain the updated data.</param>
        public void QuantityChanged(Object sender, QuantityEventArgs args)
        {
            ObservableCollection<InvoiceItemsIO> tmp = InvoiceItems;
            InvoiceItems = null;
            InvoiceItems = tmp;
            CalculateInvoiceTotals();
        }
        /// <summary>
        /// Class constructor.
        /// </summary>
        public InvoiceUI()
        {
            InvoiceItems = new ObservableCollection<InvoiceItemsIO>();
            InvoiceDate = InvoiceDueDate = DateTime.Now;
            DiscountValueIO = "0,00";
        }

        #region INotifyPropertyChanged members
        /// <summary>
        /// This is boiler plate code that was added when one want to notify a change on a class
        /// property. This is where the code and the UI communicate through the event handler.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raise a notification that a property has been changed.
        /// </summary>
        /// <param name="propertyName">A string of the property name</param>
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
