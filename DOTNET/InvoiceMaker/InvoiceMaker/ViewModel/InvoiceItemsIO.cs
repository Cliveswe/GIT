
using InvoiceMaker.Model;
using System;
using System.ComponentModel;
///<summary>
/// Created by Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-02-06
/// </summary>
namespace InvoiceMaker.ViewModel
{
    class InvoiceItemsIO
    {
        #region Events
        /// <summary>
        /// Event handlers for prosperities.
        /// Make use of the generic event handler EventHandler<T> and 
        /// to make use of the custom EvenArgs.
        /// </summary>
        public event EventHandler<QuantityEventArgs> QuantityChangedEvent;
        #endregion

        #region Properties
        /// <summary>
        /// Item details.
        /// </summary>
        public InvoiceItem InvoiceItemObj
        {
            get {
                return _invoiceItemObj;
            }
            set {
                _invoiceItemObj = value;
            }
        }
        private InvoiceItem _invoiceItemObj;

        /// <summary>
        /// Item quantity.
        /// </summary>
        public string QuantityTxt
        {
            get {
                return _quantityTxt;
            }
            set {
                _quantityTxt = value;
                Update(value);
                
            }
        }
        private string _quantityTxt;

        /// <summary>
        /// Total tax for an item.
        /// </summary>
        public string TotalTaxTxt
        {
            get {
                return InvoiceItemObj.GetTotalTax().ToString();
            }
        }

        /// <summary>
        /// Total sum for a item.
        /// </summary>
        public string TotalTxt
        {
            get {
                return InvoiceItemObj.GetTotal().ToString();
                
            }
        }
        #endregion

        /// <summary>
        /// The quantity has been changed, trigger events to update the application.
        /// </summary>
        /// <param name="value">String containing a new quantity of an item.</param>
        private void Update(string value)
        {
            QuantityChangedEvent?.Invoke(this, new QuantityEventArgs(value));
        }

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="invoiceItem">Invoiced item details.</param>
        public InvoiceItemsIO(InvoiceItem invoiceItem)
        {
            InvoiceItemObj = invoiceItem;
            QuantityTxt = invoiceItem.QuantityTxt;
        }
    }
}
