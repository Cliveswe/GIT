using InvoiceMaker.Model;
using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
///<summary>
/// Created by Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-02-06
/// </summary>
namespace InvoiceMaker.ViewModel
{
    class GenerateInvoiceDocument
    {
        private InvoiceUI invoiceUI;
        private const int stdFontSize = 14;
        private const int stdFontSizeLarge = 18;
        private const int companyTitleIndex = 0;
        /// <summary>
        /// Get the invoice title and value as a StringBuilder.
        /// </summary>
        /// <param name="maxLength">Int length of the string.</param>
        /// <returns>StringBuilder</returns>
        private StringBuilder GetInvoiceIdDates(string strTitle, string strData)
        {
            StringBuilder str = new StringBuilder();

            str.Append(
                String.Format("{0,-10}\t: {1}", strTitle, strData)
                );
            str.AppendLine();
            return str;
        }
        /// <summary>
        /// Get the company name.
        /// </summary>
        /// <returns>TextBlock</returns>
        public TextBlock GetCompanyName()
        {
            TextBlock blk = GetTextBlock(stdFontSizeLarge);
            string str = invoiceUI.FormattedInvoiceCompanyAddress;
            string[] array = str.Split('\n');
            blk.Text = array[companyTitleIndex];
            blk.FontWeight = FontWeights.Bold;
            return blk;
        }
        /// <summary>
        /// Get the invoice id, date and due date. Dates are in short format.
        /// </summary>
        /// <returns>String</returns>
        private string GetInvoiceAndDates()
        {
            string msg = string.Empty;

            msg += invoiceUI.InvoiceTitle + "\n";
            msg += GetInvoiceIdDates(invoiceUI.InvoiceIdTitle, invoiceUI.InvoiceId);
            msg += GetInvoiceIdDates(invoiceUI.InvoiceDateTitle, invoiceUI.InvoiceDate.ToShortDateString());
            msg += GetInvoiceIdDates(invoiceUI.InvoiceDueDateTitle, invoiceUI.InvoiceDueDate.ToShortDateString());

            return msg;
        }
        /// <summary>
        /// Build a matrix of invoice item details.
        /// </summary>
        /// <param name="item">Object containing the invoice data.</param>
        /// <returns>StringBuilder formated string.</returns>
        private StringBuilder GetDetailedInvoiceItem(InvoiceItemsIO item)
        {
            StringBuilder str = new StringBuilder();

            int quantity = int.Parse(item.QuantityTxt);
            Decimal unitPrice = decimal.Parse(item.InvoiceItemObj.UnitPriceTxt);
            double tax = double.Parse(item.InvoiceItemObj.TaxTxt);
            double totalTax = double.Parse(item.TotalTaxTxt);
            double total = double.Parse(item.TotalTxt);

            str.Append(
                String.Format("{0,-50} {1,8:D} {2,12:f2} {3,10:f2} {4,10:f2} {5,10:f2}",
                item.InvoiceItemObj.DescriptionTxt,
                quantity,
                unitPrice,
                tax,
                totalTax,
                total
                )
                );
            str.AppendLine();
            return str;
        }
        /// <summary>
        /// Build a matrix of invoiced item in detail with a header.
        /// </summary>
        /// <returns>TextBlock</returns>
        public string GetInvoiceItems()
        {
            StringBuilder str = new StringBuilder();
            //header
            str.Append(String.Format("{0,-50} {1,-8} {2,-12} {3,-10} {4,-10} {5,-10}",
                 InvoiceItem.ItemKeyEnum.Description.ToString().ToLower(),
                 InvoiceItem.ItemKeyEnum.Quantity.ToString().ToLower(),
                 InvoiceItem.ItemKeyEnum.UnitPrice.ToString().ToLower(),
                 InvoiceItem.ItemKeyEnum.Tax.ToString().ToLower() + "(%)",
                 "Total tax".ToLower(),
                 "Total".ToLower()));
            str.AppendLine();
            //add each item in the invoice
            foreach (InvoiceItemsIO item in invoiceUI.InvoiceItems)
            {
                str.Append(GetDetailedInvoiceItem(item));
            }

            return str.ToString();
        }

        /// <summary>
        /// Get the company address.
        /// </summary>
        /// <returns>TextBlock</returns>
        public TextBlock GetCompanyAddress()
        {
            TextBlock blk = GetTextBlock(stdFontSize);

            blk.Text = invoiceUI.FormattedInvoiceCompanyAddress;

            return blk;
        }

        /// <summary>
        /// Get the invoice number and both dates.
        /// </summary>
        /// <returns>String</returns>
        public TextBlock GetHeadder()
        {
            TextBlock blk = GetTextBlock(stdFontSize);

            blk.Text = GetInvoiceAndDates();

            return blk;

        }
        /// <summary>
        /// Get the receiver company address from the invoice
        /// </summary>
        /// <returns>string</returns>
        private string GetReciverAddress()
        {
            string str = string.Empty;

            str = invoiceUI.InvoiceReceiverAddress;

            return str;
        }
        /// <summary>
        /// Get the receiver company address.
        /// </summary>
        /// <returns>TextBlock</returns>
        public TextBlock GetReceiverAddress()
        {
            TextBlock blk = GetTextBlock(stdFontSize);

            blk.Text = GetReciverAddress();

            return blk;
        }
        /// <summary>
        /// Get the company contact details from the invoice.
        /// </summary>
        /// <returns>String</returns>
        private string GetCompanyContact()
        {
            string str = string.Empty;

            str = invoiceUI.CompanyContactInformation;

            return str;
        }
        /// <summary>
        /// Get the company contact details.
        /// </summary>
        /// <returns>TextBlock</returns>
        public TextBlock GetSenderContactInfo()
        {

            TextBlock blk = GetTextBlock(stdFontSize);

            blk.Text = GetCompanyContact();

            return blk;
        }

        /// <summary>
        /// Create a grid of a defined size.
        /// </summary>
        /// <param name="rows">Number of rows.</param>
        /// <param name="cols">Number of Columns.</param>
        /// <returns>Grid</returns>
        public Grid CreateAGrid(int rows, int cols, int height, int width)
        {
            // Create the Grid
            Grid myGrid = new Grid();
            myGrid.Width = width;
            myGrid.Height = height;
            myGrid.HorizontalAlignment = HorizontalAlignment.Left;
            myGrid.VerticalAlignment = VerticalAlignment.Top;
            myGrid.ShowGridLines = false;

            // Define the Columns
            for (int i = 0; i < cols; i++)
            {
                myGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            // Define the Rows
            for (int i = 0; i < rows; i++)
            {
                myGrid.RowDefinitions.Add(new RowDefinition());
            }
            
            return myGrid;
        }

        private TextBlock SetTextBlockForGrid(string str, int row, int col)
        {
            TextBlock blk = GetTextBlock(stdFontSize);
            blk.Text = str;
            Grid.SetRow(blk, row);
            Grid.SetColumn(blk, col);
            return blk;
        }

        public Grid GetInvoiceTotalAmountAndTax()
        {
            Grid grid = CreateAGrid(3, 3, 165, 350);//a 3 by 3 grid
            //add title
            TextBlock title = SetTextBlockForGrid(invoiceUI.TotalTitle, 0, 0);
            title.FontWeight = FontWeights.Bold;
            grid.Children.Add(title);
            //add tax
            TextBlock tax = SetTextBlockForGrid(invoiceUI.TaxTotal, 0, 1);
            tax.FontWeight = FontWeights.Bold;
            grid.Children.Add(tax);

            //add total including tax
            TextBlock includingTax = SetTextBlockForGrid(invoiceUI.TotalIncludingTax, 0, 2);
            includingTax.FontWeight = FontWeights.Bold;
            grid.Children.Add(includingTax);

            //add discount voucher
            TextBlock discountTitle = SetTextBlockForGrid(invoiceUI.DiscountTitle, 1, 1);
            discountTitle.FontWeight = FontWeights.Bold;
            grid.Children.Add(discountTitle);
            TextBlock discount = SetTextBlockForGrid(invoiceUI.DiscountValueIO, 1, 2);
            discount.FontWeight = FontWeights.Bold;
            grid.Children.Add(discount);

            //amount to pay
            TextBlock amountToPayTitle = SetTextBlockForGrid(invoiceUI.AmountToPayTitle, 2, 1);
            amountToPayTitle.FontWeight = FontWeights.Bold;
            grid.Children.Add(amountToPayTitle);
            TextBlock amountToPay = SetTextBlockForGrid(invoiceUI.AmountToPay, 2, 2);
            amountToPay.FontWeight = FontWeights.Bold;
            grid.Children.Add(amountToPay);

            return grid;
        }

        private TextBlock GetTextBlock(double fontSize)
        {
            //set a default value
            if (fontSize < 10)
            {
                fontSize = stdFontSize;
            }
            TextBlock blk = new TextBlock();
            blk.FontFamily = new FontFamily("Times Courier");
            // a FontFamily object that we pass the string name of the font
            // like "Arial".
            blk.FontSize = fontSize; //simply an integer.
            blk.FontStretch = FontStretches.Expanded;
            // Font Stretch is an Enumerated Type, so we need to 
            // initialize it from the Enumerated Type FontStretches
            // Enumerated Types are those that have predefined values
            blk.FontStyle = FontStyles.Italic; // Another Enumerated Type blk.FontWeight = FontWeights.Bold; //Another Enumerated Type
            blk.TextWrapping = TextWrapping.Wrap; // Another Enumerated Type
            blk.Margin = new Thickness(5, 5, 5, 5);
            // A thickness object defines how large the margin or padding should // be. One can set uniform thickness by passing a single Float
            // value, or optionally set each side of the box manually like 
            // above. The parameters are new Thickness(left, top, right, bottom)
            // This is very similar to the CSS margin property (EX. margin: 1em // 2em 1em 2em)
            blk.Padding = new Thickness(1,1,1,1);//setting all padding to 2 on all sides.

            return blk;
        }

        /// <summary>
        /// Get the invoice number.
        /// </summary>
        /// <returns></returns>
        public string GetInvoiceId()
        {
            return invoiceUI.InvoiceId;
        }
        /// <summary>
        /// Add a parsed invoice.
        /// </summary>
        /// <param name="invoiceUI">A parsed and calculated invoice from object InvoiceUI</param>
        public void AddInvoiceData(InvoiceUI invoiceUI)
        {
            this.invoiceUI = invoiceUI;
        }

        public GenerateInvoiceDocument()
        {
            invoiceUI = new InvoiceUI();

        }
    }
}
