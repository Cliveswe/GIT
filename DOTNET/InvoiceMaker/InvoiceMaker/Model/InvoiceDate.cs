using System;
using System.Globalization;
///<summary>
/// Created by Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-02-06
/// </summary>
namespace InvoiceMaker.Model
{
    class InvoiceDate
    {
        /// <summary>
        /// Invoice date
        /// </summary>
        public string DateOfInvoice
        {
            get {
                return _dateOfInvoice;
            }
            private set {
                _dateOfInvoice = value;
            }
        }
        private string _dateOfInvoice;

        /// <summary>
        /// Get the invoice date.
        /// </summary>
        /// <returns>The invoice date is returned in the DateTime format.</returns>
        public DateTime GetInvoiceDate()
        {
            return DateTime.ParseExact(DateOfInvoice, "yyyy-MM-dd", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Update the invoice date.
        /// </summary>
        /// <param name="dateTime">DateTime object.</param>
        public void UpdateInvoiceDate(DateTime dateTime)
        {
            DateOfInvoice = dateTime.ToString("yyyy-MM-dd");
        }
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="date">Date in string format.</param>
        public InvoiceDate(string date)
        {
            DateOfInvoice = date;
        }
    }
}
