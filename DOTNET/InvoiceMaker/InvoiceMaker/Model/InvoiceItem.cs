using InvoiceMaker.ViewModel;
using System;
using System.Globalization;
///<summary>
/// Created by Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-02-06
/// </summary>
namespace InvoiceMaker.Model
{
     class InvoiceItem
    {
        /// <summary>
        /// Key to index the array of item details.
        /// </summary>
        public enum ItemKeyEnum
        {
            Description, Quantity, UnitPrice, Tax
        }

        private int precession = 2;//number of decimal places precession.
        /// <summary>
        /// Size of the array of item details.
        /// </summary>
        public int NumberOfItemDetails => ListOfItems.Length;

        #region Item Components
        /// <summary>
        /// Description of item
        /// </summary>
        public string DescriptionTxt
        {
            get {
                return _descriptionTxt;
            }
        }
        private string _descriptionTxt;
        /// <summary>
        /// Number of items.
        /// </summary>
        public string QuantityTxt
        {
            get {
                return _quantityTxt;
            }
        }
        private string _quantityTxt;
        /// <summary>
        /// Price per item.
        /// </summary>
        public string UnitPriceTxt
        {
            get {
                return _unitPriceTxt;
            }
        }
        private string _unitPriceTxt;
        /// <summary>
        /// Items tax.
        /// </summary>
        public string TaxTxt
        {
            get {
                return _taxTxt;
            }
        }
        private string _taxTxt;
        #endregion

        /// <summary>
        /// Array of item details.
        /// </summary>
        public string[] ListOfItems
        {
            get {
                return _listOfItems;
            }
            private set {
                _listOfItems = value;
            }
        }
        private string[] _listOfItems;
        /// <summary>
        /// Add the invoice details into properties.
        /// </summary>
        /// <param name="itemKey">ItemKey used as a key to place an item details into a property.</param>
        /// <param name="itemDetail">The item detail as a string.</param>
        private void AddDetailedItem(int itemKey, string itemDetail)
        {
            switch (itemKey)
            {
                case (int)ItemKeyEnum.Description:
                    _descriptionTxt = itemDetail;
                    break;
                case (int)ItemKeyEnum.Quantity:
                    _quantityTxt = itemDetail;
                    break;
                case (int)ItemKeyEnum.Tax:
                    _taxTxt = Convert.ToDecimal(itemDetail, SwedishCulture()).ToString();
                    break;
                case (int)ItemKeyEnum.UnitPrice:
                    _unitPriceTxt = Convert.ToDecimal(itemDetail, SwedishCulture()).ToString();
                    break;
            }
        }
        /// <summary>
        /// Adjust input to Swedish culture.
        /// </summary>
        /// <returns>ClutureInfo</returns>
        public CultureInfo SwedishCulture()
        {
            CultureInfo culture = new CultureInfo("sv-SE");
            culture.NumberFormat.NumberDecimalSeparator = ".";
            return culture;
        }
        /// <summary>
        /// Calculate the quantity times the price of the item.
        /// </summary>
        /// <returns>Double</returns>
        private double QuantityTimePrice()
        {
            double res = 0;
            double quantity = double.Parse(QuantityTxt);
            double unitPrice = double.Parse(UnitPriceTxt);

            if ((quantity > 0) && (unitPrice > 0))
            {
                res = quantity * unitPrice;
            }
            return res;
        }
        /// <summary>
        /// Calculate the items total coast.
        /// </summary>
        /// <returns>Double total coast.</returns>
        public double GetTotal()
        {
            double res = 0;
            if (QuantityTimePrice() > 0)
            {
                res = QuantityTimePrice() * (1 + (double.Parse(TaxTxt) / 100));
            }
            return Math.Round((double)res, precession);
        }
        /// <summary>
        /// Calculate the items total tax.
        /// </summary>
        /// <returns>Double total tax coast.</returns>
        public double GetTotalTax()
        {
            double res = 0;

            if (QuantityTimePrice() > 0)
            {
                res = GetTotal() - QuantityTimePrice();
            }
            return Math.Round((double)res, precession);
        }
        /// <summary>
        /// Add an item detail to the array of item details.
        /// </summary>
        /// <param name="itemKey">ItemKeyEnum used as a key to index the array of item details.</param>
        /// <param name="itemDetail">The item detail as a string.</param>
        public void AddItemInDetail(ItemKeyEnum itemKey, string itemDetail)
        {
            ListOfItems[(int)itemKey] = itemDetail;
            AddDetailedItem((int)itemKey, itemDetail);
        }

        /// <summary>
        /// Event that updates the quantity of an item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args">Arguments that contain the updated data.</param>
        public void ChangeQuantity(Object sender, QuantityEventArgs args)
        {
            _quantityTxt = args.value;
        }
        /// <summary>
        /// Class constructor.
        /// </summary>
        public InvoiceItem()
        {
            ListOfItems = new string[(int)ItemKeyEnum.Tax + 1];
        }
    }
}
