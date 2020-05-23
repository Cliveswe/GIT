using InvoiceMaker.Model;
using System;
///<summary>
/// Created by Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-02-06
/// </summary>
namespace InvoiceMaker.ViewModel
{
    class GenerateInvoiceObj
    {
        private enum SourceKeyEnum
        {
            InvoiceNumber, InvoiceDate, DueDate, ReceiveCompanyName, ReciverContactName, ReceiverStreetAddress,
            ReciverZipCode, ReciverCity, ReciverCountry, NumberOfItems, SenderCompanyName, SenderStreetAddress,
            SenderZipCode, SenderCity, SenderCountry, SenderPhoneNumber, SenderURL
        }

        private static Invoice invoice;

        #region Address Sender / Receiver
        /// <summary>
        /// Retrieve the receivers address.
        /// </summary>
        /// <param name="invoice">Invoice to be updated.</param>
        /// <param name="source">Array of strings</param>
        /// <returns></returns>
        private static void AddReceiverAddress(string[] source)
        {

            invoice.ReceiverCompanyAddress.AddAddressData(InvoiceAddress.InvoiceAddressKeyEnum.CompanyName
                , source[(int)SourceKeyEnum.ReceiveCompanyName]);
            invoice.ReceiverCompanyAddress.AddAddressData(InvoiceAddress.InvoiceAddressKeyEnum.ContactPerson,
                source[(int)SourceKeyEnum.ReciverContactName]);
            invoice.ReceiverCompanyAddress.AddAddressData(InvoiceAddress.InvoiceAddressKeyEnum.StreetAddress,
                source[(int)SourceKeyEnum.ReceiverStreetAddress]);
            invoice.ReceiverCompanyAddress.AddAddressData(InvoiceAddress.InvoiceAddressKeyEnum.ZipCode,
                source[(int)SourceKeyEnum.ReciverZipCode]);
            invoice.ReceiverCompanyAddress.AddAddressData(InvoiceAddress.InvoiceAddressKeyEnum.City,
                source[(int)SourceKeyEnum.ReciverCity]);
            invoice.ReceiverCompanyAddress.AddAddressData(InvoiceAddress.InvoiceAddressKeyEnum.Country,
                source[(int)SourceKeyEnum.ReciverCountry]);

        }
        private static void AddSenderAddress(string[] source, int offset)
        {

            invoice.SenderCompanyAddress.AddAddressData(InvoiceAddress.InvoiceAddressKeyEnum.CompanyName,
               source[offset + (int)SourceKeyEnum.SenderCompanyName]);
            invoice.SenderCompanyAddress.AddAddressData(InvoiceAddress.InvoiceAddressKeyEnum.StreetAddress,
            source[offset + (int)SourceKeyEnum.SenderStreetAddress]);
            invoice.SenderCompanyAddress.AddAddressData(InvoiceAddress.InvoiceAddressKeyEnum.ZipCode,
            source[offset + (int)SourceKeyEnum.SenderZipCode]);
            invoice.SenderCompanyAddress.AddAddressData(InvoiceAddress.InvoiceAddressKeyEnum.City,
            source[offset + (int)SourceKeyEnum.SenderCity]);
            invoice.SenderCompanyAddress.AddAddressData(InvoiceAddress.InvoiceAddressKeyEnum.Country,
            source[offset + (int)SourceKeyEnum.SenderCountry]);
        }
        #endregion

        /// <summary>
        /// Retrieve the invoice number.
        /// </summary>
        /// <param name="source">String array.</param>
        private static void AddInvoiceNumber(string[] source)
        {
            invoice.AddInvoiceNumber(source[(int)SourceKeyEnum.InvoiceNumber]);
        }

        #region Add Invoice Date and Due Date
        /// <summary>
        /// Retrieve the invoice date.
        /// </summary>
        /// <param name="source">String array.</param>
        private static void AddInvoiceDate(string[] source)
        {
            invoice.AddInvoiceDate(source[(int)SourceKeyEnum.InvoiceDate]);
        }

        /// <summary>
        /// Retrieve the invoice due date.
        /// </summary>
        /// <param name="source">String array.</param>
        private static void AddInvoiceDueDate(string[] source)
        {
            invoice.AddInvoiceDueDate(source[(int)SourceKeyEnum.DueDate]);
        }

        /// <summary>
        /// Retrieve the company address.
        /// </summary>
        /// <param name="source">String array.</param>
        private static void AddCompanyAddress(string[] source)
        {
            InvoiceItem item = new InvoiceItem();
            int offset = (int)SourceKeyEnum.NumberOfItems;

            offset = (int.Parse(invoice.NumberOfItems) * item.NumberOfItemDetails);

            invoice.AddSenderCompanyAddress();

            AddSenderAddress(source, offset);
         
        }

        /// <summary>
        /// Add the address for the company that sends an invoice.
        /// </summary>
        /// <param name="source">String array.</param>
        private static void AddReciverCompanyAddress(string[] source)
        {
            invoice.AddReceiverCompanyAddress();
            AddReceiverAddress(source);
        }
        #endregion

        #region Add Items
        /// <summary>
        /// Add an item to the invoice.
        /// </summary>
        /// <param name="source">String array.</param>
        private static void AddInvoicedItems(string[] source)
        {
            invoice.SetNumberOfItems(source[(int)SourceKeyEnum.NumberOfItems]);
            AddItems(source);
        }
        /// <summary>
        /// Add each item to the invoice.
        /// </summary>
        /// <param name="source">String array.</param>
        private static void AddItems(string[] source)
        {
            int indexLength = int.Parse(source[(int)SourceKeyEnum.NumberOfItems]);
            InvoiceItem item;

            for (int offset = 0; offset < indexLength; offset++)
            {
                item = new InvoiceItem();

                invoice.AddItems(AddItemDetails(item,
                    (int)SourceKeyEnum.NumberOfItems + 1,
                    offset,
                    source));
            }
        }
        /// <summary>
        /// Add an item details to the invoice.
        /// </summary>
        /// <param name="item">The item detail to be added.</param>
        /// <param name="start">Starting position within the source.</param>
        /// <param name="offset">The item to be added from the starting position.</param>
        /// <param name="source">String array.</param>
        /// <returns></returns>
        private static InvoiceItem AddItemDetails(InvoiceItem item, int start, int offset, string[] source)
        {
            int index = start + (offset * item.NumberOfItemDetails);

            for (int i = 0; i < item.NumberOfItemDetails; i++)
            {
                Enum.TryParse(i.ToString(), out InvoiceItem.ItemKeyEnum key);
                item.AddItemInDetail(key, source[index]);
                index++;
            }

            return item;
        }
        #endregion

        #region Add Contact Details
        /// <summary>
        /// Retrieve both the phone number and web address.
        /// </summary>
        /// <param name="source">String array.</param>
        private static void AddContactDetails(string[] source)
        {
            int index = (int.Parse(source[(int)SourceKeyEnum.NumberOfItems])
                * new InvoiceItem().NumberOfItemDetails);

            invoice.AddPhoneNumber(source[index +
                (int)SourceKeyEnum.SenderPhoneNumber]);
            invoice.AddURL(source[index +
                (int)SourceKeyEnum.SenderURL]);
        }
        #endregion

        /// <summary>
        /// Generate a populated invoice object.
        /// </summary>
        /// <param name="source">Array of strings of raw invoice data.</param>
        /// <returns>Invoice object.</returns>
        public static Invoice GenerateInvoice(Invoice sourceInvoice, string[] source)
        {
            invoice = sourceInvoice;

            AddInvoiceNumber(source);
            AddInvoiceDate(source);
            AddInvoiceDueDate(source);
            //add company address
            AddReciverCompanyAddress(source);
            //add items
            AddInvoicedItems(source);
            //add sender address
            AddCompanyAddress(source);
            //add sender contact details
            AddContactDetails(source);
            
            return invoice;
        }
    }
}
